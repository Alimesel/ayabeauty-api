using AyaBeauty.API.Data;
using AyaBeauty.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly AyaBeautyDbContext _context;

        public ServicesController(AyaBeautyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Service>>> GetAll()
        {
            var services = await _context.Services
                .Where(s => s.IsActive)
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();

            return Ok(services);
        }

        [HttpPost]
        public async Task<ActionResult<Service>> Create([FromBody] Service service)
        {
            service.UpdatedAt = DateTime.UtcNow;
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return Ok(service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Service updated)
        {
            var existing = await _context.Services.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Title         = updated.Title;
            existing.Description   = updated.Description;
            existing.ImageFileName = updated.ImageFileName;
            existing.OldPrice      = updated.OldPrice;
            existing.NewPrice      = updated.NewPrice;
            existing.DisplayOrder  = updated.DisplayOrder;
            existing.IsActive      = updated.IsActive;
            existing.UpdatedAt     = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Services.FindAsync(id);
            if (existing == null) return NotFound();

            _context.Services.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}