using AyaBeauty.API.Data;
using AyaBeauty.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeContentController : ControllerBase
    {
        private readonly AyaBeautyDbContext _context;
        public HomeContentController(AyaBeautyDbContext context)
        {
            _context = context;
        }

       [HttpGet]
public async Task<ActionResult<HomeContent>> Get()
{
    var content = await _context.HomeContent
        .Where(h => h.IsActive)
        .FirstOrDefaultAsync();

    if (content == null)
    {
        return NotFound(new { message = "No Home Content Is Found" });
    }
    return Ok(content);
}

        [HttpPost]
        public async Task<ActionResult<HomeContent>> Create([FromBody] HomeContent content)
        {
            content.UpdatedAt = DateTime.UtcNow;
            _context.HomeContent.Add(content);
            await _context.SaveChangesAsync();
            return Ok(content);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HomeContent update)
        {
            var existing = await _context.HomeContent.FindAsync(id);
            if(existing == null)
            return NotFound();
            existing.Title = update.Title;
            existing.Subtitle = update.Subtitle;
            existing.ImageFileName = update.ImageFileName;
            existing.ButtonPrimary = update.ButtonPrimary;
            existing.ButtonSecondary = update.ButtonSecondary;
            existing.IsActive = update.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

    }
}