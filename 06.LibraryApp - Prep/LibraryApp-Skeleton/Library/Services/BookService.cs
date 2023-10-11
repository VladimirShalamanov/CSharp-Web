namespace Library.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using Library.Data;
    using Library.Models;
    using Library.Data.Models;
    using Library.Services.Interfaces;

    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            return await dbContext.Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Description = b.Description,
                    CategoryId = b.CategoryId,
                })
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
            return await dbContext
                .Books
                .Select(b => new AllBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId)
        {
            return await dbContext
                .IdentityUserBooks
                .Where(ub => ub.CollectorId == userId)
                .Select(b => new AllBookViewModel()
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    ImageUrl = b.Book.ImageUrl,
                    Description = b.Book.Description,
                    Category = b.Book.Category.Name
                })
                .ToListAsync();
        }


        public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
        {
            bool alreadyAdded = await dbContext.IdentityUserBooks
                .AnyAsync(ub => ub.CollectorId == userId &&
                                ub.BookId == book.Id);

            if (!alreadyAdded)
            {
                var userBook = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = book.Id
                };

                await dbContext.IdentityUserBooks.AddAsync(userBook);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task RemoveBookFromCollectionAsinc(string userId, BookViewModel book)
        {
            var userBookFound = await dbContext.IdentityUserBooks
                .FirstOrDefaultAsync(ub => ub.CollectorId == userId &&
                                           ub.BookId == book.Id);

            if (userBookFound != null)
            {
                dbContext.IdentityUserBooks.Remove(userBookFound);
                await dbContext.SaveChangesAsync();
            }
        }


        public async Task<AddBookViewModel> GetNewAddBookAsync()
        {
            var categories = await dbContext.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            var model = new AddBookViewModel()
            {
                Categories = categories
            };

            return model;
        }
        public async Task AddBookAsync(AddBookViewModel model)
        {
            var book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.Url,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }


        public async Task<AddBookViewModel?> GetBookByIdForEditAsync(int id)
        {
            var categories = await dbContext.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return await dbContext.Books
                 .Where(b => b.Id == id)
                 .Select(b => new AddBookViewModel()
                 {
                     Title = b.Title,
                     Author = b.Author,
                     Url = b.ImageUrl,
                     Rating = b.Rating,
                     Description = b.Description,
                     CategoryId = b.CategoryId,
                     Categories = categories
                 })
                 .FirstOrDefaultAsync();
        }
        public async Task EditBookAsync(int id, AddBookViewModel model)
        {
            var book = await dbContext.Books.FindAsync(id);

            if (book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.Description = model.Description;
                book.ImageUrl = model.Url;
                book.Rating = model.Rating;
                book.CategoryId = model.CategoryId;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
