using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanScopeRepositoriesCommon;
using Microsoft.EntityFrameworkCore;

namespace UrbanScopeRepositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _db;

        public EventRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Event?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Events
                .AsNoTracking()
                .Include(e => e.User) // remove if you don’t want User loaded
                .FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<IReadOnlyList<Event>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Events
                .AsNoTracking()
                .OrderByDescending(e => e.EventDate)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Event>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _db.Events
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync(ct);
        }

        public async Task AddAsync(Event entity, CancellationToken ct = default)
        {
            await _db.Events.AddAsync(entity, ct);
        }

        public void Update(Event entity)
        {
            _db.Events.Update(entity);
        }

        public void Remove(Event entity)
        {
            _db.Events.Remove(entity);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Events.AnyAsync(e => e.Id == id, ct);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return _db.SaveChangesAsync(ct);
        }
        public Task<Event?> GetTrackedByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _db.Events.FirstOrDefaultAsync(e => e.Id == id, ct);
        }

    }
}
