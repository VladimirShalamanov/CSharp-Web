namespace Homies.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Event;

    public class EventFormViewModel
    {
        public EventFormViewModel()
        {
            this.Types = new HashSet<TypesViewModel>();
        }

        [Required]
        [StringLength(EventName_MaxL, MinimumLength = EventName_MinL)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(EventDescription_MaxL, MinimumLength = EventDescription_MinL)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }
        public IEnumerable<TypesViewModel> Types { get; set; }
    }
}