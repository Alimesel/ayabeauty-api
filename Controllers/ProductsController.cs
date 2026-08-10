using AyaBeauty.API.Data;
using AyaBeauty.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AyaBeautyDbContext _context;

        public ProductsController(AyaBeautyDbContext context)
        {
            _context = context;
        }

        // GET api/products
        // Returns all active products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var products = await _context.Products
                .Where(p => p.IsActive)
                .OrderBy(p => p.DisplayOrder)
                .ToListAsync();

            return Ok(products);
        }

        // GET api/products/preview
        // Returns first 3 products for the preview section
        [HttpGet("preview")]
        public async Task<ActionResult<List<Product>>> GetPreview()
        {
            var products = await _context.Products
                .Where(p => p.IsActive)
                .OrderBy(p => p.DisplayOrder)
                .Take(3)
                .ToListAsync();

            return Ok(products);
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // PUT api/products/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updated)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name          = updated.Name;
            existing.Category      = updated.Category;
            existing.CategoryLabel = updated.CategoryLabel;
            existing.Price         = updated.Price;
            existing.OldPrice      = updated.OldPrice;
            existing.Image         = updated.Image;
            existing.Badge         = updated.Badge;
            existing.Popularity    = updated.Popularity;
            existing.DisplayOrder  = updated.DisplayOrder;
            existing.IsActive      = updated.IsActive;
            existing.UpdatedAt     = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE api/products/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return NotFound();

            _context.Products.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}