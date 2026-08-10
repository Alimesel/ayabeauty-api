using AyaBeauty.API.Data;
using AyaBeauty.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutContentController : ControllerBase
    {
        private readonly AyaBeautyDbContext _context;

        public AboutContentController(AyaBeautyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<AboutContent>> Get()
        {
            var content = await _context.AboutContent
                .Where(a => a.IsActive)
                .FirstOrDefaultAsync();

            if (content == null)
                return NotFound(new { message = "No about content found." });

            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<AboutContent>> Create([FromBody] AboutContent content)
        {
            content.UpdatedAt = DateTime.UtcNow;
            _context.AboutContent.Add(content);
            await _context.SaveChangesAsync();
            return Ok(content);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AboutContent updated)
        {
            var existing = await _context.AboutContent.FindAsync(id);
            if (existing == null) return NotFound();

            existing.SectionTitle    = updated.SectionTitle;
            existing.PhilosophyTitle = updated.PhilosophyTitle;
            existing.Paragraph1      = updated.Paragraph1;
            existing.Paragraph2      = updated.Paragraph2;
            existing.Paragraph3      = updated.Paragraph3;
            existing.ImageFileName   = updated.ImageFileName;
            existing.IsActive        = updated.IsActive;
            existing.UpdatedAt       = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }
    }
}