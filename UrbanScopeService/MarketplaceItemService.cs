using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanScopeRepositoriesCommon;
using UrbanScopeServiceCommon;
using Exceptions;

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

        public async Task<MarketplaceItemDto?> UpdateAsync(Guid userId, Guid itemId, UpdateMarketplaceItemDto dto, CancellationToken ct)
        {
            // You need a way to fetch the entity
            var entity = await _repository.GetByIdAsync(itemId, ct); // assuming you have this already
            if (entity == null) return null;

            if (entity.UserId != userId)
                throw new ForbiddenException("You are not allowed to edit this marketplace item.");

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Price = dto.Price;

            _repository.Update(entity);
            await _repository.SaveChangesAsync(ct);

            return new MarketplaceItemDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                UserId = entity.UserId,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid itemId, CancellationToken ct)
        {
            var entity = await _repository.GetByIdAsync(itemId, ct); // assuming you have this already
            if (entity == null) return false;

            if (entity.UserId != userId)
                throw new ForbiddenException("You are not allowed to delete this marketplace item.");

            _repository.Delete(entity);
            await _repository.SaveChangesAsync(ct);

            return true;
        }
    }
}
