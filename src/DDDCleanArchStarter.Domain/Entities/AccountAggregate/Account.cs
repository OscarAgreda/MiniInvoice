using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class Account : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<AccountAdjustment> _accountAdjustments = new();

        private readonly List<CustomerAccount> _customerAccounts = new();

        private readonly List<Invoice> _invoices = new();

        public Account(Guid accountId, string accountNumber, string accountName, string accountDescription, bool? isDeleted, System.Guid tenantId, System.Guid accountTypeId)
        {
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            AccountNumber = Guard.Against.NullOrWhiteSpace(accountNumber, nameof(accountNumber));
            AccountName = Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));
            AccountDescription = Guard.Against.NullOrWhiteSpace(accountDescription, nameof(accountDescription));
            IsDeleted = isDeleted;
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
            AccountTypeId = Guard.Against.NullOrEmpty(accountTypeId, nameof(accountTypeId));
        }

        private Account()
        { }

        public IEnumerable<AccountAdjustment> AccountAdjustments => _accountAdjustments.AsReadOnly();

        public string AccountDescription { get; private set; }

        [Key]
        public Guid AccountId { get; private set; }

        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }
        public Guid AccountTypeId { get; private set; }
        public IEnumerable<CustomerAccount> CustomerAccounts => _customerAccounts.AsReadOnly();
        public IEnumerable<Invoice> Invoices => _invoices.AsReadOnly();
        public bool? IsDeleted { get; private set; }
        public Guid TenantId { get; private set; }

        public void SetAccountDescription(string accountDescription)
        {
            AccountDescription = Guard.Against.NullOrEmpty(accountDescription, nameof(accountDescription));
        }

        public void SetAccountName(string accountName)
        {
            AccountName = Guard.Against.NullOrEmpty(accountName, nameof(accountName));
        }

        public void SetAccountNumber(string accountNumber)
        {
            AccountNumber = Guard.Against.NullOrEmpty(accountNumber, nameof(accountNumber));
        }
    }
}