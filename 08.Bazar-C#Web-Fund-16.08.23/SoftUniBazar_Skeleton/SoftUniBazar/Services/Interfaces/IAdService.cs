namespace SoftUniBazar.Services.Interfaces
{
    using Models.Ad;
    using Models.Category;

    public interface IAdService
    {
        Task<IEnumerable<AdViewModel>> GetAllAdsAsync();
        Task<IEnumerable<AdViewModel>> GetAdsFromUserCartAsync(string userId);


        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task CreateAdAsync(AdFormModel model, string userId);


        Task<AdFormModel?> GetAdForEditAsync(int id);
        Task EditAdAsync(AdFormModel model, int id);


        Task AddToCartAsync(int adId, string userId);
        Task RemoveFromCartAsync(int adId, string userId);


        Task<bool> AdExistsAsync(int id);
        Task<bool> IsOwnerAsync(int adId, string userId);
        Task<bool> AdAlreadyAddedToCartAsync(int adId, string userId);
    }
}