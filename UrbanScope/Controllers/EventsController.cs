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
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event evt)
        {
            if (evt == null) return BadRequest();

            evt.Id = Guid.NewGuid();

            _context.Event.Add(evt);
            await _context.SaveChangesAsync();

            return Ok(evt);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Event.ToListAsync();
            return Ok(items);
        }
    }
}
