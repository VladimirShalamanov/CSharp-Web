namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryName_MaxL)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; }
    }
}