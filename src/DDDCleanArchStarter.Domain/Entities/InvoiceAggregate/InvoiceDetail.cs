using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class InvoiceDetail : BaseEntityEv<Guid>, IAggregateRoot
    {
        public InvoiceDetail(Guid invoiceDetailId, Guid invoiceId, Guid productId, System.Guid tenantId, decimal quantity, string productName, decimal? unitPrice, decimal? lineSale, decimal? lineTax, decimal? lineDiscount)
        {
            InvoiceDetailId = Guard.Against.NullOrEmpty(invoiceDetailId, nameof(invoiceDetailId));
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

        private InvoiceDetail()
        { }

        public virtual Invoice Invoice { get; private set; }

        [Key]
        public Guid InvoiceDetailId { get; private set; }

        public Guid InvoiceId { get; private set; }
        public decimal? LineDiscount { get; private set; }
        public decimal? LineSale { get; private set; }
        public decimal? LineTax { get; private set; }
        public virtual Product Product { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Quantity { get; private set; }
        public Guid TenantId { get; private set; }
        public decimal? UnitPrice { get; private set; }

        public void SetInvoiceId(Guid invoiceId)
        {
            InvoiceId = Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));
        }

        public void SetProductId(Guid productId)
        {
            ProductId = Guard.Against.NullOrEmpty(productId, nameof(productId));
        }

        public void SetProductName(string productName)
        {
            ProductName = Guard.Against.NullOrEmpty(productName, nameof(productName));
        }
    }
}