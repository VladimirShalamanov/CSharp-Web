namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TaskBoardApp.Data;
    using TaskBoardApp.Models;

    //using System.Diagnostics;
    //using TaskBoardApp.Models;

    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext dbContext;

        public HomeController(TaskBoardAppDbContext context)
        {
            dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var taskBoards = dbContext
                .Boards
                .Select(b => b.Name)
                .Distinct();

            var taskCounts = new List<HomeBoardModel>();
            foreach (var bName in taskBoards) 
            {
                var tasksInBoard = dbContext.Tasks.Where(t => t.Board.Name == bName).Count();
                taskCounts.Add(new HomeBoardModel()
                {
                    BoardName = bName,
                    TasksCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTasksCount = dbContext.Tasks.Where(t => t.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel()
            {
                AllTasksCount = dbContext.Tasks.Count(),
                BoardsWithTasksCount = taskCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }
    }
}