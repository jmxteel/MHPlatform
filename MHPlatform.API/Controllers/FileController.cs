using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MHPlatform.API.Controllers
{

    [Route("api/Client")]
    [Authorize(Policy = "CanAccessProducts")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("file-upload")]
        public async Task<IActionResult> UploadMultipleFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files uploaded.");

            try
            {
                string uploadPath = Directory.Exists(@"D:\Uploads") ? @"D:\Uploads" : @"C:\Uploads";
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                List<string> savedFiles = new List<string>();

                foreach (var file in files)
                {
                    // Get the file extension
                    string fileExtension = Path.GetExtension(file.FileName);

                    // Generate a unique filename (e.g., using GUID)
                    string newFileName = $"{Guid.NewGuid()}{fileExtension}";

                    // Full file path
                    string filePath = Path.Combine(uploadPath, newFileName);

                    // Save file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedFiles.Add(filePath);
                }

                return Ok(new { Message = "Files uploaded successfully", FilePaths = savedFiles });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
