namespace Homies.Contracts
{
    using Homies.Models;

    public interface IEventService
    {
        Task<IEnumerable<EventViewModel>> GetAllEventsAsync();
        Task<IEnumerable<EventViewModel>> GetJoinedEventsAsync(string userId);


        Task<IEnumerable<TypesViewModel>> GetAllTypesAsync();
        Task AddEventAsync(EventFormViewModel model, string userId);


        Task<EventFormViewModel?> GetEventForEditAsync(int id);
        Task EditEventAsync(EventFormViewModel model, int id);


        Task<EventDetailsViewModel> GetEventDetailsAsync(int id);

        Task JoinEventAsync(int eventId, string userId);
        Task LeaveEventAsync(int eventId, string userId);


        Task<bool> IsOrganizerAsync(int eventId, string userId);
        Task<bool> EventAlreadyJoinedAsync(int eventId, string userId);
        Task<bool> EventExistsAsync(int id);
    }
}
