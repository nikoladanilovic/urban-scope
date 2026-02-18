using DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanScopeServiceCommon;
using UrbanScopeRepositoriesCommon;

namespace UrbanScopeService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        public UserService(IUserRepository users)
        {
            _users = users;
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
