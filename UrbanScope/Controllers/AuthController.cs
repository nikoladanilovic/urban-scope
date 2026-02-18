using DTOs;
using Microsoft.AspNetCore.Mvc;
using UrbanScopeServiceCommon;

namespace UrbanScope.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto, CancellationToken ct)
        {
            var result = await _auth.RegisterAsync(dto, ct);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto, CancellationToken ct)
        {
            var result = await _auth.LoginAsync(dto, ct);
            return Ok(result);
        }
    }
}
