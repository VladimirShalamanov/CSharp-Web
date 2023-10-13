namespace Homies.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Homies.Contracts;
    using Homies.Models;

    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await this.eventService.GetAllEventsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var model = await this.eventService.GetJoinedEventsAsync(GetUserId());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var types = await this.eventService.GetAllTypesAsync();

            EventFormViewModel model = new EventFormViewModel()
            {
                Types = types
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Types = await this.eventService.GetAllTypesAsync();

                return View(model);
            }


            await this.eventService.AddEventAsync(model, GetUserId());

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool isOrganizerOfEvent = await this.eventService.IsOrganizerAsync(id, GetUserId());

            if (isOrganizerOfEvent)
            {
                var model = await this.eventService.GetEventForEditAsync(id);

                if (model == null)
                {
                    return RedirectToAction(nameof(All));
                }

                model.Types = await this.eventService.GetAllTypesAsync();
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Types = await this.eventService.GetAllTypesAsync();

                return View(model);
            }


            await this.eventService.EditEventAsync(model, id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            EventDetailsViewModel model = await this.eventService.GetEventDetailsAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            bool isJoined = await this.eventService.EventAlreadyJoinedAsync(id, GetUserId());
            bool isExisted = await this.eventService.EventExistsAsync(id);
            bool isOrganiser = await this.eventService.IsOrganizerAsync(id, GetUserId());

            if (!isJoined && isExisted && !isOrganiser)
            {
                await this.eventService.JoinEventAsync(id, GetUserId());

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            bool isJoined = await this.eventService.EventAlreadyJoinedAsync(id, GetUserId());

            if (isJoined)
            {
                await this.eventService.LeaveEventAsync(id, GetUserId());

                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(Joined));
        }
    }
}