using AyaBeauty.API.Data;
using AyaBeauty.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactInfoController : ControllerBase
    {
        private readonly AyaBeautyDbContext _context;

        public ContactInfoController(AyaBeautyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ContactInfo>> Get()
        {
            var content = await _context.ContactInfo
                .Where(c => c.IsActive)
                .FirstOrDefaultAsync();

            if (content == null)
                return NotFound(new { message = "No contact info found." });

            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<ContactInfo>> Create([FromBody] ContactInfo content)
        {
            content.UpdatedAt = DateTime.UtcNow;
            _context.ContactInfo.Add(content);
            await _context.SaveChangesAsync();
            return Ok(content);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactInfo updated)
        {
            var existing = await _context.ContactInfo.FindAsync(id);
            if (existing == null) return NotFound();

            existing.SectionTitle       = updated.SectionTitle;
            existing.SectionDescription = updated.SectionDescription;
            existing.AddressLine1       = updated.AddressLine1;
            existing.AddressLine2       = updated.AddressLine2;
            existing.Phone1             = updated.Phone1;
            existing.Phone2             = updated.Phone2;
            existing.Email              = updated.Email;
            existing.HoursWeekdays      = updated.HoursWeekdays;
            existing.HoursWeekdaysTime  = updated.HoursWeekdaysTime;
            existing.HoursSunday        = updated.HoursSunday;
            existing.InstagramUrl       = updated.InstagramUrl;
            existing.FacebookUrl        = updated.FacebookUrl;
            existing.WhatsappNumber     = updated.WhatsappNumber;
            existing.MapLatitude        = updated.MapLatitude;
            existing.MapLongitude       = updated.MapLongitude;
            existing.IsActive           = updated.IsActive;
            existing.UpdatedAt          = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }
      
    }
}