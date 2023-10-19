namespace SoftUniBazar.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataValidationConstants.Category;

    public class Category
    {
        public Category()
        {
            this.Ads = new HashSet<Ad>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Name_MaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Ad> Ads { get; set; }
    }
}