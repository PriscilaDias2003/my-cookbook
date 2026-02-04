using MyCookBook.Domain.Repositories;

namespace MyCookBook.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyCookBookDbContext _dbContext;
        public UnitOfWork(MyCookBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
