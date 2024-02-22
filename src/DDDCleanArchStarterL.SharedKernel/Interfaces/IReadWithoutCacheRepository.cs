using Ardalis.Specification;
namespace DDDInvoicingCleanL.SharedKernel.Interfaces
{
    public interface IReadWithoutCacheRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}