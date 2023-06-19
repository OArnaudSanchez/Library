using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Books.ToListAsync(cancellationToken);
        }

        public async Task<Book> GetBookAsync(Guid bookId, CancellationToken cancellationToken)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == bookId, cancellationToken);
        }

        public async Task AddBookAsync(Book book, CancellationToken cancellationToken)
        {
            await _dbContext.Books.AddAsync(book, cancellationToken);
        }

        public void UpdateBookSync(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
        }

        public void DeleteBookSync(Book book)
        {
            _dbContext.Books.Remove(book);
        }
    }
}