namespace SoftUniBazar.Common
{
    public class DataValidationConstants
    {
        public static class Ad
        {
            public const int Name_MinLength = 5;
            public const int Name_MaxLength = 25;

            public const int Description_MinLength = 15;
            public const int Description_MaxLength = 250;
        }

        public static class Category
        {
            public const int Name_MinLength = 3;
            public const int Name_MaxLength = 15;
        }
    }
}