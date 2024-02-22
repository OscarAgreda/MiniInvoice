using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class AccountDto : AuditBase
    {
        public Guid AccountId { get;  set; }
        [Required(ErrorMessage="Account Number is required")]
        [MaxLength(255)]
        public string AccountNumber { get;  set; }
        [Required(ErrorMessage="Account Name is required")]
        [MaxLength(255)]
        public string AccountName { get;  set; }
        [Required(ErrorMessage="Account Description is required")]
        [MaxLength(255)]
        public string AccountDescription { get;  set; }
        public bool? IsDeleted { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        [Required(ErrorMessage="Account Type Id is required")]
        public Guid AccountTypeId { get;  set; }
        public List<AccountAdjustmentDto> AccountAdjustments { get; set; } = new();
        public List<CustomerAccountDto> CustomerAccounts { get; set; } = new();
        public List<InvoiceDto> Invoices { get; set; } = new();
        public AccountDto() {}
        public AccountDto(Guid accountId, string accountNumber, string accountName, string accountDescription, bool? isDeleted, System.Guid tenantId, System.Guid accountTypeId)
        {
                AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
                AuditEntityId = accountId.ToString();
                AuditId = Guid.NewGuid().ToString();
                AuditEntityType = GetType().Name;
                AccountNumber = Guard.Against.NullOrWhiteSpace(accountNumber, nameof(accountNumber));
                AccountName = Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));
                AccountDescription = Guard.Against.NullOrWhiteSpace(accountDescription, nameof(accountDescription));
                IsDeleted = isDeleted;
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
                AccountTypeId = Guard.Against.NullOrEmpty(accountTypeId, nameof(accountTypeId));
        }
    }
}
