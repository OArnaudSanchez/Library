using Library.Application.Interfaces.Persistence;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _dbContext;

        public AuthorRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Authors.ToListAsync(cancellationToken);
        }

        public async Task<Author> GetAuthorAsync(Guid authorId, CancellationToken cancellationToken)
        {
            return await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == authorId);
        }

        public async Task AddAuthorAsync(Author author, CancellationToken cancellationToken)
        {
            await _dbContext.Authors.AddAsync(author, cancellationToken);
        }

        public void UpdateAuthorSync(Author author)
        {
            _dbContext.Entry(author).State = EntityState.Modified;
        }

        public void DeleteAuthorSync(Author author)
        {
            _dbContext.Remove(author);
        }
    }
}