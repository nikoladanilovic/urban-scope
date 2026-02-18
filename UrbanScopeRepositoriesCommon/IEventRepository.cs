using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanScopeRepositoriesCommon
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Event>> GetAllAsync(CancellationToken ct = default);

        // Optional but useful for “my events”
        Task<IReadOnlyList<Event>> GetByUserIdAsync(Guid userId, CancellationToken ct = default);

        Task AddAsync(Event entity, CancellationToken ct = default);
        void Update(Event entity);
        void Remove(Event entity);

        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task<Event?> GetTrackedByIdAsync(Guid id, CancellationToken ct = default);
    }
}
