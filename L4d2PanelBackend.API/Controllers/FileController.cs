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
        public IActionResult GetFileList([FromQuery] string? base_path, [FromRoute] string? relative_path)
        {
            string target_path = BaseFileService.RelativePathToAbsulotePath(base_path, HttpUtility.UrlDecode(relative_path));
            return Ok(
                new ViewModels.FileList()
                {
                    files = FileListService.GetFilesList(target_path),
                    directories = FileListService.GetDirectoriesList(target_path)
                }
            );
        }

        [HttpPost("{*relative_path}")]
        public IActionResult MkDir([FromQuery] string? base_path, [FromRoute] string? relative_path)
        {
            string target_path = BaseFileService.RelativePathToAbsulotePath(base_path, HttpUtility.UrlDecode(relative_path));
            Directory.CreateDirectory(target_path);
            return NoContent();
        }

        [HttpDelete("{*relative_path}")]
        public IActionResult Delete([FromQuery] string? base_path, [FromRoute] string? relative_path)
        {
            string target_path = BaseFileService.RelativePathToAbsulotePath(base_path, HttpUtility.UrlDecode(relative_path));
            // May rise conflict? But who care? Fix it later, I hope.
            var attr = System.IO.File.GetAttributes(target_path);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                Directory.Delete(target_path, true);
            }
            else
            {
                System.IO.File.Delete(target_path);
            }
            return NoContent();
        }
    }
}
