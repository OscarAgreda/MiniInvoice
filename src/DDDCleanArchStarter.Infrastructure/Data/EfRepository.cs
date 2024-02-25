using Ardalis.Specification.EntityFrameworkCore;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
namespace DDDCleanArchStarter.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {



        private IDbContextTransaction  _transaction;
        private readonly DbContext _dbContext;
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            try
            {
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
            }
        }
        public void RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
            }
        }

        public void DetachEntity(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}