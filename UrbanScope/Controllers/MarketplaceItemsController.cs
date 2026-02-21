using Data;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using UrbanScope.Helpers;
using UrbanScopeServiceCommon;

namespace UrbanScope.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketplaceItemsController : ControllerBase
    {
        private readonly IMarketplaceItemService _service;

        public MarketplaceItemsController(IMarketplaceItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<MarketplaceItemDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MarketplaceItemDto>> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MarketplaceItemDto>> Create([FromBody] CreateMarketplaceItemDto dto, CancellationToken ct)
        {
            var userId = User.GetUserId();
            var created = await _service.CreateAsync(dto, userId, ct);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}
