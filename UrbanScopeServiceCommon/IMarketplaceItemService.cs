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

    }
}
