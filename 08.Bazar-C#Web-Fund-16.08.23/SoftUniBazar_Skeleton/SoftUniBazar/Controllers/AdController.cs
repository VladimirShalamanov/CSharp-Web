namespace SoftUniBazar.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Models.Ad;
    using Models.Category;
    using Services.Interfaces;

    public class AdController : BaseController
    {
        private readonly IAdService adService;

        public AdController(IAdService adService)
        {
            this.adService = adService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<AdViewModel> model = await this.adService.GetAllAdsAsync();

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            IEnumerable<AdViewModel> model = await this.adService.GetAdsFromUserCartAsync(GetUserId());

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            IEnumerable<CategoryViewModel> categories = await this.adService.GetAllCategoriesAsync();

            AdFormModel model = new AdFormModel()
            {
                Categories = categories
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await this.adService.GetAllCategoriesAsync();

                return this.View(model);
            }


            await this.adService.CreateAdAsync(model, GetUserId());

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool isOwnerOfAd = await this.adService.IsOwnerAsync(id, GetUserId());

            if (isOwnerOfAd)
            {
                AdFormModel? model = await this.adService.GetAdForEditAsync(id);

                if (model == null)
                {
                    return RedirectToAction(nameof(All));
                }

                model.Categories = await this.adService.GetAllCategoriesAsync();

                return this.View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await this.adService.GetAllCategoriesAsync();

                return this.View(model);
            }

            await this.adService.EditAdAsync(model, id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            bool isExisted = await this.adService.AdExistsAsync(id);
            bool isOwner = await this.adService.IsOwnerAsync(id, GetUserId());
            bool isAdded = await this.adService.AdAlreadyAddedToCartAsync(id, GetUserId());

            if (isExisted && !isOwner && !isAdded)
            {
                await this.adService.AddToCartAsync(id, GetUserId());

                return RedirectToAction(nameof(Cart));
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            bool isAdded = await this.adService.AdAlreadyAddedToCartAsync(id, GetUserId());

            if (isAdded)
            {
                await this.adService.RemoveFromCartAsync(id, GetUserId());

                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(Cart));
        }
    }
}