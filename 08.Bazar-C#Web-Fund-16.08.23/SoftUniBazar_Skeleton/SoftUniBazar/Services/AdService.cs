namespace SoftUniBazar.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Models.Ad;
    using Interfaces;
    using Models.Category;
    using Microsoft.Extensions.Logging;

    public class AdService : IAdService
    {
        private readonly BazarDbContext dbContext;

        public AdService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AdViewModel>> GetAllAdsAsync()
        {
            return await this.dbContext
                .Ads
                .Select(a => new AdViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                    CreatedOn = a.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    Category = a.Category.Name,
                    Description = a.Description,
                    Price = a.Price.ToString("f2"),
                    Owner = a.Owner.UserName
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<AdViewModel>> GetAdsFromUserCartAsync(string userId)
        {
            return await this.dbContext
                .AdBuyers
                .Where(a => a.BuyerId == userId)
                .Select(a => new AdViewModel()
                {
                    Id = a.Ad.Id,
                    Name = a.Ad.Name,
                    ImageUrl = a.Ad.ImageUrl,
                    CreatedOn = a.Ad.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    Category = a.Ad.Category.Name,
                    Description = a.Ad.Description,
                    Price = a.Ad.Price.ToString("f2"),
                    Owner = a.Ad.Owner.UserName
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            return await this.dbContext
                .Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();
        }

        public async Task CreateAdAsync(AdFormModel model, string userId)
        {
            Ad newAd = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                OwnerId = userId,
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.UtcNow,
                CategoryId = model.CategoryId
            };

            await dbContext.Ads.AddAsync(newAd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<AdFormModel?> GetAdForEditAsync(int id)
        {
            return await this.dbContext
                .Ads
                .Where(e => e.Id == id)
                .Select(e => new AdFormModel()
                {
                    Name = e.Name,
                    Description = e.Description,
                    ImageUrl = e.ImageUrl,
                    Price = e.Price,
                    CategoryId = e.CategoryId
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditAdAsync(AdFormModel model, int id)
        {
            Ad? ad = await this.dbContext
                .Ads
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (ad != null)
            {
                ad.Name = model.Name;
                ad.Description = model.Description;
                ad.ImageUrl = model.ImageUrl;
                ad.Price = model.Price;
                ad.CategoryId = model.CategoryId;

                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task AddToCartAsync(int adId, string userId)
        {
            AdBuyer newAdBuyer = new AdBuyer()
            {
                AdId = adId,
                BuyerId = userId
            };

            await this.dbContext.AdBuyers.AddAsync(newAdBuyer);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int adId, string userId)
        {
            AdBuyer adBuyerToRemove = await this.dbContext
                .AdBuyers
                .FirstAsync(e => e.AdId == adId && e.BuyerId == userId);

            this.dbContext.Remove(adBuyerToRemove);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsOwnerAsync(int adId, string userId)
        {
            Ad? ad = await this.dbContext
                .Ads
                .FirstOrDefaultAsync(a => a.Id == adId && a.OwnerId == userId);

            return ad != null;
        }

        public async Task<bool> AdExistsAsync(int id)
        {
            return await this.dbContext
                .Ads
                .AnyAsync(e => e.Id == id);
        }

        public async Task<bool> AdAlreadyAddedToCartAsync(int adId, string userId)
        {
            return await this.dbContext
                .AdBuyers
                .AnyAsync(e => e.AdId == adId && e.BuyerId == userId);
        }
    }
}
