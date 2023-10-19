namespace SoftUniBazar.Models.Ad
{
    using System.ComponentModel.DataAnnotations;
    using Category;
    using Data.Models;

    using static Common.DataValidationConstants.Ad;

    public class AdFormModel
    {
        public AdFormModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }

        [Required]
        [StringLength(Name_MaxLength, MinimumLength = Name_MinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(Description_MaxLength, MinimumLength = Description_MinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        [Range(1, 5)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}