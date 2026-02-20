using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace UrbanScopeRepositoriesCommon
{
    public interface IMarketplaceItemRepository
    {
        Task<MarketplaceItem?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<IReadOnlyList<MarketplaceItem>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken ct = default);

        Task<IReadOnlyList<MarketplaceItem>> GetByUserIdAsync(
            Guid userId,
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken ct = default);

        Task AddAsync(MarketplaceItem item, CancellationToken ct = default);

        void Update(MarketplaceItem item);

        void Delete(MarketplaceItem item);

        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
