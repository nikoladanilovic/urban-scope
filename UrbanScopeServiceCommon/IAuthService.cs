using System;
using System.Collections.Generic;
using System.Text;
using DTOs;

namespace UrbanScopeServiceCommon
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
        Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default);
        Task<MeDto> GetMeAsync(Guid userId, CancellationToken ct = default);
    }
}
