namespace Forum.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Forum.Common.Validations.DataConstants.Post;

    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxL)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ContentMaxL)]
        public string Content { get; set; } = null!;
    }
}
