namespace TaskBoardApp.Models.Board
{
    using Task;

    public class BoardViewModel
    {
        public BoardViewModel()
        {
            this.Tasks = new HashSet<TaskViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
         
        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}
