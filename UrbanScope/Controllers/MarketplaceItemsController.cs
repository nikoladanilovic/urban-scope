using System;
using System.Threading.Tasks;
using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UrbanScope.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketplaceItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarketplaceItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] MarketplaceItem item)
        {
            if (item == null) return BadRequest();

            item.Id = Guid.NewGuid();
            item.CreatedAt = DateTime.UtcNow;

            _context.MarketplaceItems.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.MarketplaceItems.ToListAsync();
            return Ok(items);
        }
    }
}
