using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;
using L4d2PanelBackend.API.Repository;
using L4d2PanelBackend.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
    c => {
        c.SwaggerDoc("v1", new() { Title = "L4d2PanelBackend", Version = "v1" });
    }
);
builder.Services.AddSignalR();
builder.Services.AddCors(
    options => {
        options.AddPolicy(
            name: "AllowAll",
            builder => {
                builder.SetIsOriginAllowed((host) => true);
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials();
                builder.WithExposedHeaders("Location", "Upload-Offset", "Upload-Length");
            }
        );
    }
);


builder.Services.AddSingleton<IProcessService, ProcessService>();
builder.Services.AddSingleton<IProcessesRepository, ProcessesMemoryRepository>();


builder.Services.Configure<KestrelServerOptions>(
    options => {
        options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set, the default value will be 30 MB
    }
);
builder.Services.AddSingleton(CreateTusConfiguration);
builder.Services.AddHostedService<ExpiredFilesCleanupService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "L4d2PanelBackend v1"));
}

app.UseCors("AllowAll");

var websocket_options = new WebSocketOptions();
websocket_options.AllowedOrigins.Add("*");
app.UseWebSockets(websocket_options);

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.UseTus(httpContext => httpContext.RequestServices.GetRequiredService<DefaultTusConfiguration>());
app.MapGet("/upload/{fileId}", DownloadFileEndpoint.HandleRoute);

app.UseEndpoints(
    endpoints => {
        endpoints.MapHub<L4d2PanelBackend.API.Hubs.MessageHub>("/hub");
    }
);


app.Run();



static DefaultTusConfiguration CreateTusConfiguration(IServiceProvider serviceProvider)
{
    var env = (IWebHostEnvironment)serviceProvider.GetRequiredService(typeof(IWebHostEnvironment));

    //文件上传路径
    var tus_root_path = "/root";

    return new DefaultTusConfiguration
    {
        UrlPath = "/upload",
        //文件存储路径
        Store = new TusDiskStore(tus_root_path),
        Events = new Events
        {
            OnFileCompleteAsync = async event_context => {
                ITusFile file = await event_context.GetFileAsync();
                Dictionary<string, Metadata> metadata = await file.GetMetadataAsync(event_context.CancellationToken);
                Console.WriteLine(Path.Combine(tus_root_path, file.Id));
                Console.WriteLine(
                    Path.Combine(
                        BaseFileService.RelativePathToAbsulotePath("/l4d2/left4dead2", metadata["location"].GetString(Encoding.UTF8)),
                        metadata["name"].GetString(Encoding.UTF8)
                    )
                );
                File.Copy(
                    Path.Combine(tus_root_path, file.Id),
                    Path.Combine(
                        BaseFileService.RelativePathToAbsulotePath("/l4d2/left4dead2", metadata["location"].GetString(Encoding.UTF8)),
                        metadata["name"].GetString(Encoding.UTF8)
                    ),
                    true
                );
                await ((ITusTerminationStore)event_context.Store).DeleteFileAsync(file.Id, event_context.CancellationToken);
            }
        }
    };
}