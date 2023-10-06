namespace TaskBoardApp.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.BoardsWithTasksCount = new List<HomeBoardModel>();
        }

        public int AllTasksCount { get; set; }

        public List<HomeBoardModel> BoardsWithTasksCount { get; set; }

        public int UserTasksCount { get; set; }
    }
}
