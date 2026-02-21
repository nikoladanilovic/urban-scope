using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanScopeRepositoriesCommon;
using UrbanScopeServiceCommon;

namespace UrbanScopeService
{
    public class MarketplaceItemService : IMarketplaceItemService
    {
        private readonly IMarketplaceItemRepository _repository;

        public MarketplaceItemService(IMarketplaceItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MarketplaceItemDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();

            return items.Select(item => new MarketplaceItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                UserId = item.UserId,
                CreatedAt = item.CreatedAt
            }).ToList();
        }

        public async Task<MarketplaceItemDto?> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            if (item == null)
                return null;

            return new MarketplaceItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                UserId = item.UserId,
                CreatedAt = item.CreatedAt
            };
        }

        public async Task<MarketplaceItemDto> CreateAsync(CreateMarketplaceItemDto dto, Guid userId, CancellationToken ct)
        {
            var item = new MarketplaceItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(item);

            return new MarketplaceItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                UserId = item.UserId,
                CreatedAt = item.CreatedAt
            };
        }
    }
}
