namespace Watchlist.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Movie> Movies { get; set; }
    }
}