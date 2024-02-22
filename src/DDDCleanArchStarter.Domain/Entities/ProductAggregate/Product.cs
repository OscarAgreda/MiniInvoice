using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class Product : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<InvoiceDetail> _invoiceDetails = new();

        private readonly List<ProductCategory> _productCategories = new();

        public Product(Guid productId, string productName, string? productDescription, decimal productUnitPrice, bool productIsActive, int productMinimumCharacters, int productMinimumCallMinutes, decimal productChargeRatePerCharacter, decimal productChargeRateCallPerSecond, bool? isDeleted, System.Guid tenantId)
        {
            ProductId = Guard.Against.NullOrEmpty(productId, nameof(productId));
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

        private Product()
        { }

        public IEnumerable<InvoiceDetail> InvoiceDetails => _invoiceDetails.AsReadOnly();

        public bool? IsDeleted { get; private set; }

        public IEnumerable<ProductCategory> ProductCategories => _productCategories.AsReadOnly();

        public decimal ProductChargeRateCallPerSecond { get; private set; }

        public decimal ProductChargeRatePerCharacter { get; private set; }

        public string? ProductDescription { get; private set; }

        [Key]
        public Guid ProductId { get; private set; }

        public bool ProductIsActive { get; private set; }
        public int ProductMinimumCallMinutes { get; private set; }
        public int ProductMinimumCharacters { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductUnitPrice { get; private set; }
        public Guid TenantId { get; private set; }

        public void SetProductDescription(string productDescription)
        {
            ProductDescription = productDescription;
        }

        public void SetProductName(string productName)
        {
            ProductName = Guard.Against.NullOrEmpty(productName, nameof(productName));
        }
    }
}