using DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanScopeServiceCommon
{
    public interface IMarketplaceItemService
    {
        Task<List<MarketplaceItemDto>> GetAllAsync();
        Task<MarketplaceItemDto?> GetByIdAsync(Guid id);
        Task<MarketplaceItemDto> CreateAsync(CreateMarketplaceItemDto dto, Guid sellerId, CancellationToken ct);
        Task<MarketplaceItemDto?> UpdateAsync(Guid userId, Guid itemId, UpdateMarketplaceItemDto dto, CancellationToken ct);
        Task<bool> DeleteAsync(Guid userId, Guid itemId, CancellationToken ct);

    }
}
