using AyaBeauty.API.Data;
using AyaBeauty.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly AyaBeautyDbContext _context;

        public GalleryController(AyaBeautyDbContext context)
        {
            _context = context;
        }

        // GET api/gallery
        // Returns all active images ordered by DisplayOrder
        [HttpGet]
        public async Task<ActionResult<List<GalleryImage>>> GetAll()
        {
            var images = await _context.GalleryImages
                .Where(g => g.IsActive)
                .OrderBy(g => g.DisplayOrder)
                .ToListAsync();

            return Ok(images);
        }

        // GET api/gallery/preview
        // Returns first 7 images for the preview mosaic
        [HttpGet("preview")]
        public async Task<ActionResult<List<GalleryImage>>> GetPreview()
        {
            var images = await _context.GalleryImages
                .Where(g => g.IsActive)
                .OrderBy(g => g.DisplayOrder)
                .Take(5)
                .ToListAsync();

            return Ok(images);
        }

        // POST api/gallery
        [HttpPost]
        public async Task<ActionResult<GalleryImage>> Create([FromBody] GalleryImage image)
        {
            image.UpdatedAt = DateTime.UtcNow;
            _context.GalleryImages.Add(image);
            await _context.SaveChangesAsync();
            return Ok(image);
        }

        // PUT api/gallery/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GalleryImage updated)
        {
            var existing = await _context.GalleryImages.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Src           = updated.Src;
            existing.Alt           = updated.Alt;
            existing.Title         = updated.Title;
            existing.Category      = updated.Category;
            existing.CategoryLabel = updated.CategoryLabel;
            existing.Tall          = updated.Tall;
            existing.DisplayOrder  = updated.DisplayOrder;
            existing.IsActive      = updated.IsActive;
            existing.UpdatedAt     = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE api/gallery/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.GalleryImages.FindAsync(id);
            if (existing == null) return NotFound();

            _context.GalleryImages.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}