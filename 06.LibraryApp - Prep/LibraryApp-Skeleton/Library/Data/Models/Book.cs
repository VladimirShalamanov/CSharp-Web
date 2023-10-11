namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Book;

    public class Book
    {
        public Book()
        {
            this.UsersBooks = new HashSet<IdentityUserBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitle_MaxL)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(BookAuthor_MaxL)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(BookDescription_MaxL)]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<IdentityUserBook> UsersBooks { get; set; } 
    }
}