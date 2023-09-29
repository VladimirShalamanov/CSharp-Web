namespace Forum.Services.Interfaces
{
    using Forum.ViewModels.Post;

    public  interface IPostService
    {
        Task<IEnumerable<PostViewModel>> ListAllAsync();

        Task AddPostAsync(PostAddFormModel postModel);
        
        Task<PostAddFormModel> GetForEditOrDeleteByIdAsync(string id);

        Task EditByIdAsync(string id, PostAddFormModel postEditedModel);

        Task DeleteByIdAsync(string id);
    }
}
