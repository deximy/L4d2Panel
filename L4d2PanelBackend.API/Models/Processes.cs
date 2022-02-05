using SqlSugar;

namespace L4d2PanelBackend.Models
{
    [SugarTable("Processes")]
    public class Processes: BaseModel
    {
        public int? exit_code { get; set; } = null;

        public DateTime? exit_time { get; set; } = null;

        public bool has_exited { get; set; } = false;

        public DateTime start_time { get; set; }
    }
}
