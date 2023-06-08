using Library.Domain.Entities;

namespace Library.Application.Interfaces.Persistence
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync(CancellationToken cancellationToken);

        Task<Book> GetBookAsync(Guid bookId, CancellationToken cancellationToken);

        Task AddBookAsync(Book book, CancellationToken cancellationToken);

        void UpdateBookSync(Book book);

        void DeleteBookSync(Book book);
    }
}