namespace TaskBoardApp.Models.Task
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Task;

    public class TaskFormModel
    {
        public TaskFormModel()
        {
            this.Boards = new HashSet<TaskBoardModel>();
        }

        [Required]
        [StringLength(TaskTitleMaxL, MinimumLength = TaskTitleMinL,
            ErrorMessage = "Title should be at least {2} characters long!")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(TaskDescriptionMaxL, MinimumLength = TaskDescriptionMinL,
            ErrorMessage = "Description should be at least {2} characters long!")]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; }
    }
}
