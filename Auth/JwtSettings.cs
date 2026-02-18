using System;
using System.Collections.Generic;
using System.Text;

namespace Auth
{
    public sealed class JwtSettings
    {
        public string Key { get; set; } = string.Empty;      // long secret
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; } = 60;
    }
}
