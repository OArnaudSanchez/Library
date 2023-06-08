using Library.Domain.Entities;

namespace Library.Application.Interfaces.Persistence
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync(CancellationToken cancellationToken);

        Task<Author> GetAuthorAsync(Guid authorId, CancellationToken cancellationToken);

        Task AddAuthorAsync(Author author, CancellationToken cancellationToken);

        void UpdateAuthorSync(Author author);

        void DeleteAuthorSync(Author author);
    }
}