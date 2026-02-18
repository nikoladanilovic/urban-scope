using Data;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UrbanScope.Helpers;
using UrbanScopeServiceCommon;

namespace UrbanScope.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _events;

        public EventsController(IEventService events)
        {
            _events = events;
        }

        // Public list (or make it [Authorize] if you want)
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EventDto>>> GetAll(CancellationToken ct)
        {
            var result = await _events.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EventDto>> GetById(Guid id, CancellationToken ct)
        {
            var ev = await _events.GetByIdAsync(id, ct);
            if (ev is null) return NotFound();

            return Ok(ev);
        }

        // My events (requires auth)
        [Authorize]
        [HttpGet("mine")]
        public async Task<ActionResult<IReadOnlyList<EventDto>>> GetMine(CancellationToken ct)
        {
            var userId = User.GetUserId();
            var result = await _events.GetMineAsync(userId, ct);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EventDto>> Create([FromBody] EventCreateDto dto, CancellationToken ct)
        {
            var userId = User.GetUserId();
            var created = await _events.CreateAsync(userId, dto, ct);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EventUpdateDto dto, CancellationToken ct)
        {
            var userId = User.GetUserId();
            var ok = await _events.UpdateAsync(id, userId, dto, ct);

            if (!ok) return NotFound(); // or Forbid() if you want to distinguish ownership
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var userId = User.GetUserId();
            var ok = await _events.DeleteAsync(id, userId, ct);

            if (!ok) return NotFound(); // or Forbid()
            return NoContent();
        }
    }
}
