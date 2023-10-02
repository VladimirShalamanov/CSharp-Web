namespace Watchlist.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Movie
    {
        public Movie()
        {
            this.UsersMovies = new HashSet<UserMovie>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Director { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(typeof(decimal), "0.00", "10.00",
            ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        [ForeignKey(nameof(GenreId))]
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public ICollection<UserMovie> UsersMovies { get; set; }
    }
}