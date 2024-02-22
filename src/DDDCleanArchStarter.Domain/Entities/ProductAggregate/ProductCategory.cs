using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class ProductCategory : BaseEntityEv<int>, IAggregateRoot
    {
        public ProductCategory(Guid productId, System.Guid tenantId)
        {
            ProductId = Guard.Against.NullOrEmpty(productId, nameof(productId));
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        private ProductCategory()
        { }

        public virtual Product Product { get; private set; }

        public Guid ProductId { get; private set; }

        [Key]
        public int RowId { get; private set; }

        public Guid TenantId { get; private set; }

        public void SetProductId(Guid productId)
        {
            ProductId = Guard.Against.NullOrEmpty(productId, nameof(productId));
        }
    }
}