namespace Forum.ViewModels.Post
{
    using System.ComponentModel.DataAnnotations;

    public class PostViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
