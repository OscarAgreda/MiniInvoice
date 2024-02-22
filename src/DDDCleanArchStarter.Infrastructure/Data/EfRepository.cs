using Ardalis.Specification.EntityFrameworkCore;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDCleanArchStarter.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}