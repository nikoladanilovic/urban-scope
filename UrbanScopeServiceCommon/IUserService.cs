using DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanScopeServiceCommon
{
    public interface IUserService
    {
        Task<MeDto> GetMeAsync(Guid userId, CancellationToken ct = default);
    }
}
