using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanScopeRepositoriesCommon;
using UrbanScopeServiceCommon;

namespace UrbanScopeService
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _events;

        public EventService(IEventRepository events)
        {
            _events = events;
        }

        public async Task<EventDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var ev = await _events.GetByIdAsync(id, ct);
            if (ev is null) return null;

            return MapToDto(ev);
        }

        public async Task<IReadOnlyList<EventDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _events.GetAllAsync(ct);
            return list.Select(MapToDto).ToList();
        }

        public async Task<IReadOnlyList<EventDto>> GetMineAsync(Guid userId, CancellationToken ct = default)
        {
            var list = await _events.GetByUserIdAsync(userId, ct);
            return list.Select(MapToDto).ToList();
        }

        public async Task<EventDto> CreateAsync(Guid userId, EventCreateDto dto, CancellationToken ct = default)
        {
            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                EventDate = dto.EventDate,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                UserId = userId
            };

            await _events.AddAsync(ev, ct);
            await _events.SaveChangesAsync(ct);

            // Re-fetch if you want UserName included (because User nav may be null otherwise)
            var created = await _events.GetByIdAsync(ev.Id, ct);
            return created is null ? MapToDto(ev) : MapToDto(created);
        }

        public async Task<bool> UpdateAsync(Guid id, Guid userId, EventUpdateDto dto, CancellationToken ct = default)
        {
            // For update/delete we want tracked entity -> don’t use AsNoTracking in repository for this path.
            // Easiest: add a tracked GetByIdForUpdateAsync OR just query DbContext here.
            // Since we’re keeping strict layering, add a tracked method to repository:
            // GetTrackedByIdAsync

            var ev = await _events.GetTrackedByIdAsync(id, ct);
            if (ev is null) return false;

            if (ev.UserId != userId) return false; // ownership rule

            ev.Title = dto.Title;
            ev.Description = dto.Description;
            ev.EventDate = dto.EventDate;
            ev.Latitude = dto.Latitude;
            ev.Longitude = dto.Longitude;

            await _events.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken ct = default)
        {
            var ev = await _events.GetTrackedByIdAsync(id, ct);
            if (ev is null) return false;

            if (ev.UserId != userId) return false;

            _events.Remove(ev);
            await _events.SaveChangesAsync(ct);
            return true;
        }

        private static EventDto MapToDto(Event ev)
        {
            return new EventDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventDate = ev.EventDate,
                Latitude = ev.Latitude,
                Longitude = ev.Longitude,
                UserId = ev.UserId,
                UserName = ev.User?.Name ?? "" // depends on Include(User)
            };
        }
    }
}
