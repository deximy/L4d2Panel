using L4d2PanelBackend.API.Models;
using L4d2PanelBackend.API.Repository;
using L4d2PanelBackend.API.ViewModels;
using System.Diagnostics;

namespace L4d2PanelBackend.API.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessesRepository processes_repository_;

        public ProcessService(IProcessesRepository processes_repository)
        {
            processes_repository_ = processes_repository;
        }

        private Process? process_ = null;

        public bool is_running
        {
            get
            {
                return process_ != null && process_.HasExited == false;
            }
        }

        //public Models.Processes? process_info
        //{
        //    get
        //    {
        //        var result = new Models.Processes();
        //        if (process_ != null)
        //        {
        //            result.start_time = process_.StartTime;
        //            result.has_exited = process_.HasExited;
        //            if (process_.HasExited)
        //            {
        //                result.exit_code = process_.ExitCode;
        //                result.exit_time = process_.ExitTime;
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //        return result;
        //    }
        //}

        public async Task<Guid> RunServer(Action<string> callback)
        {
            // is there any row which suggests process is running?
            var logs = await processes_repository_.Query(x => x.has_exited == false);
            if (logs.Any())
            {
                if (process_ == null)
                {
                    // process is null, which means this is the first time to run the server
                    // those row didn't change has_exited may caused by the abnormal stop of the program
                    foreach (var log in logs)
                    {
                        await processes_repository_.Update(GetClosedProcessLog(log));
                    }
                }
                else
                {
                    if (process_.HasExited == false)
                    {
                        throw new InvalidOperationException("An existing process is running.");
                    }
                    else
                    {
                        // process is exited, but didn't synchronize to storage
                        // synchronize it manually
                        foreach (var log in logs)
                        {
                            if (log.start_time != process_.StartTime)
                            {
                                await processes_repository_.Update(GetClosedProcessLog(log));
                            }
                            else
                            {
                                await processes_repository_.Update(GetClosedProcessLog(log, process_));
                            }
                        }
                    }
                }
            }


            if (process_ != null)
            {
                // release last used process resource
                process_.Dispose();
            }

            try
            {
                Environment.SetEnvironmentVariable("LD_LIBRARY_PATH", "/l4d2:/l4d2/bin:");

                process_ = new Process();
                process_.StartInfo.FileName = "unbuffer";
                process_.StartInfo.Arguments = "-p /l4d2/srcds_linux -game left4dead2 -ip 0.0.0.0 -port 27015 +map c2m1_highway";
                process_.StartInfo.UseShellExecute = false;

                var callback_output = new DataReceivedEventHandler(
                    (_, e) => {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            callback.Invoke(e.Data);
                        }
                    }
                );

                process_.StartInfo.RedirectStandardOutput = true;
                process_.OutputDataReceived += callback_output;

                process_.StartInfo.RedirectStandardInput = true;

                process_.EnableRaisingEvents = true;
                process_.Exited += new EventHandler(
                    async (_, _) => {
                        await UpdateCurrentLog();
                    }
                );

                process_.Start();
                process_.BeginOutputReadLine();
            }
            catch (Exception)
            {
                throw;
            }

            var new_process_log = new Processes() {
                start_time = process_.StartTime,
            };
            var guid = await processes_repository_.Add(new_process_log);

            return guid;
        }

        public void ExecCommand(string command)
        {
            if (!is_running)
            {
                throw new InvalidOperationException("No available running process.");
            }

            if (command == null)
            {
                return;
            }

            process_!.StandardInput.WriteLineAsync(command);
        }

        public Task<bool> StopServer()
        {
            if (!is_running)
            {
                throw new InvalidOperationException("No available running process.");
            }

            process_!.Kill();

            return Task.FromResult(true);
        }

        public async Task<List<Guid>> GetProcessStateId(int count)
        {
            var logs = await processes_repository_.Query(_ => true, x => x.start_time, count, false);
            return logs.Select(x => x.id).ToList();
        }

        public async Task<ServerProcessState> GetProcessState(Guid id)
        {
            var log = await processes_repository_.Query(id);
            return new ServerProcessState() {
                start_time = log.start_time,
                exit_time = log.exit_time
            };
        }

        public async Task<List<ServerProcessState>> GetProcessState(List<Guid> id_list)
        {
            var logs = await processes_repository_.Query(id_list);
            return logs
                .Select(
                    x => new ServerProcessState() {
                        start_time = x.start_time,
                        exit_time = x.exit_time,
                    }
                )
                .ToList();
        }

        private async Task UpdateCurrentLog()
        {
            var log = await processes_repository_.Query(x => x.start_time == process_!.StartTime && x.has_exited == false);
            if (log.Count != 1)
            {
                throw new InvalidOperationException($"Unexpected result: the count of log is {log.Count}, expected 1");
            }
            await processes_repository_.Update(GetClosedProcessLog(log.First(), process_));
        }

        private Processes GetClosedProcessLog(Processes log, Process? process = null)
        {
            if (process == null)
            {
                log.exit_code ??= 0;
                log.exit_time ??= DateTime.Now;
            }
            else
            {
                log.exit_code = process.ExitCode;
                log.exit_time = process.ExitTime;
            }
            log.has_exited = true;
            return log;
        }
    }
}
