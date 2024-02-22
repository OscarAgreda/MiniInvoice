using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class InvoiceDto : AuditBase
    {
        public Guid InvoiceId { get;  set; }
        [Required(ErrorMessage="Invoice Number is required")]
        public int InvoiceNumber { get;  set; }
        [Required(ErrorMessage="Account Name is required")]
        [MaxLength(255)]
        public string AccountName { get;  set; }
        [Required(ErrorMessage="Customer Name is required")]
        [MaxLength(255)]
        public string CustomerName { get;  set; }
        [Required(ErrorMessage="Payment State is required")]
        public int PaymentState { get;  set; }
        [MaxLength(255)]
        public string? InternalComments { get;  set; }
        public DateTime? InvoicedDate { get;  set; }
        [Required(ErrorMessage="Invoicing Note is required")]
        [MaxLength(255)]
        public string InvoicingNote { get;  set; }
        public decimal? TotalSale { get;  set; }
        public decimal? TotalSaleTax { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        public AccountDto Account { get;  set; }
        [Required(ErrorMessage="Account is required")]
        public Guid AccountId { get;  set; }
        public List<InvoiceDetailDto> InvoiceDetails { get; set; } = new();
        public InvoiceDto() {}
        public InvoiceDto(Guid invoiceId, Guid accountId, int invoiceNumber, string accountName, string customerName, int paymentState, string? internalComments, DateTime? invoicedDate, string invoicingNote, decimal? totalSale, decimal? totalSaleTax, System.Guid tenantId)
        {
                InvoiceId = Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));
                AuditEntityId = invoiceId.ToString();
                AuditId = Guid.NewGuid().ToString();
                AuditEntityType = GetType().Name;
                AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
                InvoiceNumber = Guard.Against.NegativeOrZero(invoiceNumber, nameof(invoiceNumber));
                AccountName = Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));
                CustomerName = Guard.Against.NullOrWhiteSpace(customerName, nameof(customerName));
                PaymentState = Guard.Against.NegativeOrZero(paymentState, nameof(paymentState));
                InternalComments = internalComments;
                InvoicedDate = invoicedDate;
                InvoicingNote = Guard.Against.NullOrWhiteSpace(invoicingNote, nameof(invoicingNote));
                TotalSale = totalSale;
                TotalSaleTax = totalSaleTax;
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }
    }
}
