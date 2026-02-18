using DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanScopeServiceCommon
{
    public interface IEventService
    {
        Task<EventDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<EventDto>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<EventDto>> GetMineAsync(Guid userId, CancellationToken ct = default);

        Task<EventDto> CreateAsync(Guid userId, EventCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, Guid userId, EventUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken ct = default);
    }
}
