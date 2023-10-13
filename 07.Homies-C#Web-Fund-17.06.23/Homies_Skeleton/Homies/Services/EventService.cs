namespace Homies.Services
{
    using Microsoft.EntityFrameworkCore;

    using Homies.Data;
    using Homies.Models;
    using Homies.Contracts;
    using Homies.Data.Models;

    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;

        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<EventViewModel>> GetAllEventsAsync()
        {
            return await dbContext
                .Events
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .ToArrayAsync();
        }
        public async Task<IEnumerable<EventViewModel>> GetJoinedEventsAsync(string userId)
        {
            return await dbContext
                .EventParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new EventViewModel()
                {
                    Id = ep.Event.Id,
                    Name = ep.Event.Name,
                    Start = ep.Event.Start.ToString("yyyy-MM-dd H:mm"),
                    Type = ep.Event.Type.Name,
                    Organiser = ep.Event.Organiser.UserName
                })
                .ToArrayAsync();
        }


        public async Task<IEnumerable<TypesViewModel>> GetAllTypesAsync()
        {
            return await this.dbContext
                .Types
                .Select(t => new TypesViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToArrayAsync();
        }
        public async Task AddEventAsync(EventFormViewModel model, string userId)
        {
            var newEvent = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                Start = model.Start,
                End = model.End,
                TypeId = model.TypeId,
                OrganiserId = userId,
                CreatedOn = DateTime.UtcNow
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }


        public async Task<EventFormViewModel?> GetEventForEditAsync(int id)
        {
            return await this.dbContext
                .Events
                .Where(e => e.Id == id)
                .Select(e => new EventFormViewModel()
                {
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start,
                    End = e.End,
                    TypeId = e.TypeId
                })
                .FirstOrDefaultAsync();
        }
        public async Task EditEventAsync(EventFormViewModel model, int id)
        {
            var currentEvent = await this.dbContext
                .Events
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (currentEvent != null)
            {
                currentEvent.Name = model.Name;
                currentEvent.Description = model.Description;
                currentEvent.Start = model.Start;
                currentEvent.End = model.End;
                currentEvent.TypeId = model.TypeId;

                await this.dbContext.SaveChangesAsync();
            }
        }


        public async Task<EventDetailsViewModel> GetEventDetailsAsync(int id)
        {
            return await this.dbContext
                .Events
                .Where(e => e.Id == id)
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString("yyyy-MM-dd H:mm"),
                    End = e.End.ToString("yyyy-MM-dd H:mm"),
                    Organiser = e.Organiser.UserName,
                    CreatedOn = e.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    Type = e.Type.Name
                })
                .FirstOrDefaultAsync();
        }


        public async Task JoinEventAsync(int eventId, string userId)
        {
            EventParticipant newEventParticipant = new EventParticipant()
            {
                EventId = eventId,
                HelperId = userId
            };

            await this.dbContext.AddAsync(newEventParticipant);
            await this.dbContext.SaveChangesAsync();
        }
        public async Task LeaveEventAsync(int eventId, string userId)
        {
            EventParticipant eventParticipantToRemove = await this.dbContext
                .EventParticipants
                .FirstAsync(e => e.EventId == eventId && e.HelperId == userId);

            this.dbContext.Remove(eventParticipantToRemove);
            await this.dbContext.SaveChangesAsync();
        }


        public async Task<bool> IsOrganizerAsync(int eventId, string userId)
        {
            Event? currentEvent = await this.dbContext
                .Events
                .FirstOrDefaultAsync(e => e.Id == eventId && e.OrganiserId == userId);

            return currentEvent != null;
        }
        public async Task<bool> EventAlreadyJoinedAsync(int eventId, string userId)
        {
            return await this.dbContext
                .EventParticipants
                .AnyAsync(e => e.EventId == eventId && e.HelperId == userId);
        }
        public async Task<bool> EventExistsAsync(int id)
        {
            return await this.dbContext
                .Events
                .AnyAsync(e => e.Id == id);
        }
    }
}