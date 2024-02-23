using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class CustomerAccountDto : AuditBase
    {
        public CustomerAccountDto()
        { }

        public CustomerAccountDto(Guid accountId, Guid customerId)
        {
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
        }

        public AccountDto Account { get; set; }

        [Required(ErrorMessage = "Account is required")]
        public Guid AccountId { get; set; }

        public CustomerDto Customer { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public Guid CustomerId { get; set; }

        public int RowId { get; set; }
    }
}