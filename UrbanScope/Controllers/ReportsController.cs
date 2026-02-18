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
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            if (report == null) return BadRequest();

            report.Id = Guid.NewGuid();
            report.CreatedAt = DateTime.UtcNow;

            _context.Report.Add(report);
            await _context.SaveChangesAsync();

            return Ok(report);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Report.ToListAsync();
            return Ok(items);
        }
    }
}
