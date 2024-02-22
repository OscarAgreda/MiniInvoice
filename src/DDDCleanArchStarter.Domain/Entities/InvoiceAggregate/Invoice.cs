using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class Invoice : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<InvoiceDetail> _invoiceDetails = new();

        public Invoice(Guid invoiceId, Guid accountId, int invoiceNumber, string accountName, string customerName, int paymentState, string? internalComments, DateTime? invoicedDate, string invoicingNote, decimal? totalSale, decimal? totalSaleTax, System.Guid tenantId)
        {
            InvoiceId = Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));
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

        private Invoice()
        { }

        public virtual Account Account { get; private set; }

        public Guid AccountId { get; private set; }

        public string AccountName { get; private set; }

        public string CustomerName { get; private set; }

        public string? InternalComments { get; private set; }

        public DateTime? InvoicedDate { get; private set; }

        public IEnumerable<InvoiceDetail> InvoiceDetails => _invoiceDetails.AsReadOnly();

        [Key]
        public Guid InvoiceId { get; private set; }

        public int InvoiceNumber { get; private set; }
        public string InvoicingNote { get; private set; }
        public int PaymentState { get; private set; }
        public Guid TenantId { get; private set; }
        public decimal? TotalSale { get; private set; }
        public decimal? TotalSaleTax { get; private set; }

        public void SetAccountId(Guid accountId)
        {
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
        }

        public void SetAccountName(string accountName)
        {
            AccountName = Guard.Against.NullOrEmpty(accountName, nameof(accountName));
        }

        public void SetCustomerName(string customerName)
        {
            CustomerName = Guard.Against.NullOrEmpty(customerName, nameof(customerName));
        }

        public void SetInternalComments(string internalComments)
        {
            InternalComments = internalComments;
        }

        public void SetInvoicingNote(string invoicingNote)
        {
            InvoicingNote = Guard.Against.NullOrEmpty(invoicingNote, nameof(invoicingNote));
        }
    }
}