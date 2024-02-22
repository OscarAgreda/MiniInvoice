using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class ProductCategoryDto : AuditBase
    {
        public int RowId { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        public ProductDto Product { get;  set; }
        [Required(ErrorMessage="Product is required")]
        public Guid ProductId { get;  set; }
        public ProductCategoryDto() {}
        public ProductCategoryDto(Guid productId, System.Guid tenantId)
        {
                ProductId = Guard.Against.NullOrEmpty(productId, nameof(productId));
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }
    }
}
