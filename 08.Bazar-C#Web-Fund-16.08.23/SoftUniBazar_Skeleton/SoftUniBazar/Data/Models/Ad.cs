namespace SoftUniBazar.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.DataValidationConstants.Ad;

    public class Ad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Name_MaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(Description_MaxLength)]
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;

        [Required]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}