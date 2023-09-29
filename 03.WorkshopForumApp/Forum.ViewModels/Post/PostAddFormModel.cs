namespace Forum.ViewModels.Post
{
    using System.ComponentModel.DataAnnotations;

    using static Forum.Common.Validations.DataConstants.Post;

    public class PostAddFormModel
    {
        [Required]
        [StringLength(TitleMaxL, MinimumLength = TitleMinL)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxL, MinimumLength = ContentMinL)]
        public string Content { get; set; } = null!;
    }
}
