namespace TaskBoardApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Board;

    public class Board
    {
        public Board()
        {
            this.Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(BoardNameMaxL)]
        public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set;}
    }
}
