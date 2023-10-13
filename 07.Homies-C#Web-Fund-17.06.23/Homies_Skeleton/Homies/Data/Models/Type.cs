namespace Homies.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Type;

    public class Type
    {
        public Type()
        {
            this.Events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeName_MaxL)]
        public string Name { get; set; } = null!;

        public ICollection<Event> Events { get; set; }
    }
}
