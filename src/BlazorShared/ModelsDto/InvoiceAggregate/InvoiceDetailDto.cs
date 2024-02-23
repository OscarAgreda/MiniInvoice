using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class InvoiceDetailDto : AuditBase
    {
        public InvoiceDetailDto()
        { }

        public InvoiceDetailDto(Guid invoiceDetailId, Guid invoiceId, Guid productId, System.Guid tenantId, decimal quantity, string productName, decimal? unitPrice, decimal? lineSale, decimal? lineTax, decimal? lineDiscount)
        {
            InvoiceDetailId = Guard.Against.NullOrEmpty(invoiceDetailId, nameof(invoiceDetailId));
            AuditEntityId = invoiceDetailId.ToString();
            AuditId = Guid.NewGuid().ToString();
            AuditEntityType = GetType().Name;
            InvoiceId = Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));
            ProductId = Guard.Against.NullOrEmpty(productId, nameof(productId));
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
            Quantity = Guard.Against.Negative(quantity, nameof(quantity));
            ProductName = Guard.Against.NullOrWhiteSpace(productName, nameof(productName));
            UnitPrice = unitPrice;
            LineSale = lineSale;
            LineTax = lineTax;
            LineDiscount = lineDiscount;
        }

        public InvoiceDto Invoice { get; set; }
        public Guid InvoiceDetailId { get; set; }

        [Required(ErrorMessage = "Invoice is required")]
        public Guid InvoiceId { get; set; }

        public decimal? LineDiscount { get; set; }

        public decimal? LineSale { get; set; }

        public decimal? LineTax { get; set; }

        public ProductDto Product { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(255)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Tenant Id is required")]
        public Guid TenantId { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}