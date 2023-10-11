namespace Library.Services.Interfaces
{
    using Library.Models;

    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);


        Task<BookViewModel?> GetBookByIdAsync(int id);
        Task AddBookToCollectionAsync(string userId, BookViewModel book);
        Task RemoveBookFromCollectionAsinc(string userId, BookViewModel book);

        Task<AddBookViewModel> GetNewAddBookAsync();
        Task AddBookAsync(AddBookViewModel model);


        Task<AddBookViewModel?> GetBookByIdForEditAsync(int id);
        Task EditBookAsync(int id, AddBookViewModel model);
    }
}
