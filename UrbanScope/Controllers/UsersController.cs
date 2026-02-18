using Data;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrbanScopeServiceCommon;

namespace UrbanScope.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        public UsersController(IUserService users)
        {
            _users = users;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<MeDto>> Me(CancellationToken ct)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized("Invalid token: missing user id claim.");

            var me = await _users.GetMeAsync(userId, ct);
            return Ok(me);
        }
    }
}
