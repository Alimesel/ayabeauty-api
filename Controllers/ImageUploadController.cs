using Microsoft.AspNetCore.Mvc;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ImageUploadController> _logger;

        public ImageUploadController(IWebHostEnvironment env, ILogger<ImageUploadController> logger)
        {
            _env = env;
            _logger = logger;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = 15_728_640)]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new { error = "No file received." });

                var ct = file.ContentType?.ToLower() ?? "";
                if (!ct.StartsWith("image/"))
                    return BadRequest(new { error = $"Type '{ct}' is not an image." });

                // Use wwwroot/uploads (wwwroot already exists in your project)
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsDir);

                var ext = Path.GetExtension(file.FileName)?.ToLower();
                if (string.IsNullOrEmpty(ext) || ext == ".")
                    ext = ".jpg";

                var fileName = $"{Guid.NewGuid():N}{ext}";
                var fullPath = Path.Combine(uploadsDir, fileName);

                await using var stream = System.IO.File.Create(fullPath);
                await file.CopyToAsync(stream);

                _logger.LogInformation("Uploaded: {FileName}", fileName);

                return Ok(new { url = $"/uploads/{fileName}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Upload failed");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url) || !url.StartsWith("/uploads/"))
                return BadRequest(new { error = "Invalid path." });

            var fullPath = Path.Combine(_env.WebRootPath, "uploads", Path.GetFileName(url));
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            return Ok(new { deleted = true });
        }
    }
}