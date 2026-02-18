using Auth;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DTOs;
using UrbanScopeRepositoriesCommon;
using UrbanScopeServiceCommon;

namespace UrbanScopeService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IJwtTokenService _jwt;

        public AuthService(IUserRepository users, IJwtTokenService jwt)
        {
            _users = users;
            _jwt = jwt;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {
            var email = dto.Email.Trim();
            var emailNorm = email.ToLowerInvariant();

            if (await _users.EmailExistsAsync(emailNorm, ct))
                throw new InvalidOperationException("Email is already in use.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                EmailNormalized = emailNorm,
                Name = "",              // you can add Name to RegisterDto later
                Role = "User",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _users.AddAsync(user, ct);
            await _users.SaveChangesAsync(ct);

            var (token, expiresAtUtc) = _jwt.CreateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAtUtc = expiresAtUtc,
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default)
        {
            var emailNorm = dto.Email.Trim().ToLowerInvariant();

            var user = await _users.GetByEmailAsync(emailNorm, ct);
            if (user is null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            var ok = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!ok)
                throw new UnauthorizedAccessException("Invalid credentials.");

            var (token, expiresAtUtc) = _jwt.CreateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAtUtc = expiresAtUtc,
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<MeDto> GetMeAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _users.GetByIdAsync(userId, ct);
            if (user is null)
                throw new KeyNotFoundException("User not found.");

            return new MeDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
