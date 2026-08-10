using AyaBeauty.API.Data;
using AyaBeauty.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonialsController : ControllerBase
    {
        private readonly AyaBeautyDbContext _context;

        public TestimonialsController(AyaBeautyDbContext context)
        {
            _context = context;
        }

        // GET api/testimonials
        // Returns all active testimonials ordered by DisplayOrder
        [HttpGet]
        public async Task<ActionResult<List<Testimonial>>> Get()
        {
            var testimonials = await _context.Testimonials
                .Where(t => t.IsActive)
                .OrderBy(t => t.DisplayOrder)
                .ToListAsync();

            return Ok(testimonials);
        }

        // POST api/testimonials
        [HttpPost]
        public async Task<ActionResult<Testimonial>> Create([FromBody] Testimonial testimonial)
        {
            testimonial.UpdatedAt = DateTime.UtcNow;
            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();
            return Ok(testimonial);
        }

        // PUT api/testimonials/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Testimonial updated)
        {
            var existing = await _context.Testimonials.FindAsync(id);
            if (existing == null) return NotFound();

            existing.ClientName            = updated.ClientName;
            existing.Quote                 = updated.Quote;
            existing.ProfileImageFileName  = updated.ProfileImageFileName;
            existing.Stars                 = updated.Stars;
            existing.DisplayOrder          = updated.DisplayOrder;
            existing.IsActive              = updated.IsActive;
            existing.UpdatedAt             = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE api/testimonials/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Testimonials.FindAsync(id);
            if (existing == null) return NotFound();

            _context.Testimonials.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}