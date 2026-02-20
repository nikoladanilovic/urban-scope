using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanScopeRepositoriesCommon;
using Microsoft.EntityFrameworkCore;

namespace UrbanScopeRepositories
{
    public class MarketplaceItemRepository : IMarketplaceItemRepository
    {
        private readonly AppDbContext _db;

        public MarketplaceItemRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<MarketplaceItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _db.MarketplaceItems
                .AsNoTracking()
                .Include(x => x.User) // remove if you don't want user info here
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyList<MarketplaceItem>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken ct = default)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);

            return await _db.MarketplaceItems
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<MarketplaceItem>> GetByUserIdAsync(
            Guid userId,
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken ct = default)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);

            return await _db.MarketplaceItems
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        public Task AddAsync(MarketplaceItem item, CancellationToken ct = default)
        {
            return _db.MarketplaceItems.AddAsync(item, ct).AsTask();
        }

        public void Update(MarketplaceItem item)
        {
            _db.MarketplaceItems.Update(item);
        }

        public void Delete(MarketplaceItem item)
        {
            _db.MarketplaceItems.Remove(item);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            return _db.MarketplaceItems.AnyAsync(x => x.Id == id, ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
