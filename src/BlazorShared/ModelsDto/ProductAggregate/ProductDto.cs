using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class ProductDto : AuditBase
    {
        public Guid ProductId { get;  set; }
        [Required(ErrorMessage="Product Name is required")]
        [MaxLength(255)]
        public string ProductName { get;  set; }
        [MaxLength(255)]
        public string? ProductDescription { get;  set; }
        [Required(ErrorMessage="Product Unit Price is required")]
        public decimal ProductUnitPrice { get;  set; }
        [Required(ErrorMessage="Product Is Active is required")]
        public bool ProductIsActive { get;  set; }
        [Required(ErrorMessage="Product Minimum Character is required")]
        public int ProductMinimumCharacters { get;  set; }
        [Required(ErrorMessage="Product Minimum Call Minute is required")]
        public int ProductMinimumCallMinutes { get;  set; }
        [Required(ErrorMessage="Product Charge Rate Per Character is required")]
        public decimal ProductChargeRatePerCharacter { get;  set; }
        [Required(ErrorMessage="Product Charge Rate Call Per Second is required")]
        public decimal ProductChargeRateCallPerSecond { get;  set; }
        public bool? IsDeleted { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        public List<InvoiceDetailDto> InvoiceDetails { get; set; } = new();
        public List<ProductCategoryDto> ProductCategories { get; set; } = new();
        public ProductDto() {}
        public ProductDto(Guid productId, string productName, string? productDescription, decimal productUnitPrice, bool productIsActive, int productMinimumCharacters, int productMinimumCallMinutes, decimal productChargeRatePerCharacter, decimal productChargeRateCallPerSecond, bool? isDeleted, System.Guid tenantId)
        {
                ProductId = Guard.Against.NullOrEmpty(productId, nameof(productId));
                AuditEntityId = productId.ToString();
                AuditId = Guid.NewGuid().ToString();
                AuditEntityType = GetType().Name;
                ProductName = Guard.Against.NullOrWhiteSpace(productName, nameof(productName));
                ProductDescription = productDescription;
                ProductUnitPrice = Guard.Against.Negative(productUnitPrice, nameof(productUnitPrice));
                ProductIsActive = Guard.Against.Null(productIsActive, nameof(productIsActive));
                ProductMinimumCharacters = Guard.Against.NegativeOrZero(productMinimumCharacters, nameof(productMinimumCharacters));
                ProductMinimumCallMinutes = Guard.Against.NegativeOrZero(productMinimumCallMinutes, nameof(productMinimumCallMinutes));
                ProductChargeRatePerCharacter = Guard.Against.Negative(productChargeRatePerCharacter, nameof(productChargeRatePerCharacter));
                ProductChargeRateCallPerSecond = Guard.Against.Negative(productChargeRateCallPerSecond, nameof(productChargeRateCallPerSecond));
                IsDeleted = isDeleted;
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }
    }
}
