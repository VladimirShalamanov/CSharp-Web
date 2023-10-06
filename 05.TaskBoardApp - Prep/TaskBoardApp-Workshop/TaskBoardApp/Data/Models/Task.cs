namespace TaskBoardApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants.Task;

    public class Task
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskTitleMaxL)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(TaskDescriptionMaxL)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }

        public Board Board { get; set; } = null!;

        [Required]
        //[ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;

        public IdentityUser Owner { get; set; } = null!;
    }
}
