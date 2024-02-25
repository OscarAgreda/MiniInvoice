using Ardalis.Specification;
namespace DDDInvoicingCleanL.SharedKernel.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();

        void DetachEntity(T entity); // Add this line
    }

    public interface IEntityFactory<T>
    {
        T Create(params object[] args);
    }
}