using Ardalis.Specification;
namespace DDDInvoicingCleanL.SharedKernel.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
    }

    public interface IEntityFactory<T>
    {
        T Create(params object[] args);
    }
}