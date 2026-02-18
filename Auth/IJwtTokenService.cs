using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Auth
{
    public interface IJwtTokenService
    {
        (string token, DateTime expiresAtUtc) CreateToken(User user);
    }
}
