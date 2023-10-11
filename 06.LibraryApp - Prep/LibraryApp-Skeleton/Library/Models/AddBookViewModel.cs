namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Book;

    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }

        [Required]
        [StringLength(BookTitle_MaxL, MinimumLength = BookTitle_MinL)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(BookAuthor_MaxL, MinimumLength = BookAuthor_MinL)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(BookDescription_MaxL, MinimumLength = BookDescription_MinL)]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Url { get; set; } = null!;

        [Required]
        [Range(BookRating_MinL, BookRating_MaxL)]
        public decimal Rating { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
