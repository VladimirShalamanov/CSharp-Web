namespace TaskBoardApp.Data
{
    public class DataConstants
    {
        public class Task
        {
            public const int TaskTitleMaxL = 70;
            public const int TaskTitleMinL = 5;

            public const int TaskDescriptionMaxL = 1000;
            public const int TaskDescriptionMinL = 10;
        }

        public class Board
        {
            public const int BoardNameMaxL = 30;
            public const int BoardNameMinL = 3; 
        }
    }
}
