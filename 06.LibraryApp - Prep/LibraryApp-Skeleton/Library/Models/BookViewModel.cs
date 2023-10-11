namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Library.Data.DataConstants.Book;

    public class BookViewModel
    {
        public int Id { get; set; }

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
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(BookRating_MinL, BookRating_MaxL)]
        public decimal Rating { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
