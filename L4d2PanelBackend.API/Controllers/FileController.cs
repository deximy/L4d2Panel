using Microsoft.AspNetCore.Mvc;
using L4d2PanelBackend.Services;
using System.Web;

namespace L4d2PanelBackend.Controllers
{
    [Route("files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet("{*relative_path}")]
        public ViewModels.FileList GetFileList([FromQuery]string? base_path, [FromRoute]string? relative_path)
        {
            string target_path = BaseFileService.RelativePathToAbsulotePath(base_path, HttpUtility.UrlDecode(relative_path));
            return new ViewModels.FileList() {
                files = FileListService.GetFilesList(target_path),
                directories = FileListService.GetDirectoriesList(target_path)
            };
        }
    }
}
