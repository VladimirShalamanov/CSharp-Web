namespace Library.Data
{
    public class DataConstants
    {
        public class Book
        {
            public const int BookTitle_MinL = 10;
            public const int BookTitle_MaxL = 50;

            public const int BookAuthor_MinL = 5;
            public const int BookAuthor_MaxL = 50;

            public const int BookDescription_MinL = 5;
            public const int BookDescription_MaxL = 5000;

            public const double BookRating_MinL = 0.00;
            public const double BookRating_MaxL = 10.00;
        }

        public class Category
        {
            public const int CategoryName_MinL = 5;
            public const int CategoryName_MaxL = 50;
        }
    }
}
