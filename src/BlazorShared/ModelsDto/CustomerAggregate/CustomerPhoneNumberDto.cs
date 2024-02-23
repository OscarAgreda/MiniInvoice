using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class CustomerPhoneNumberDto : AuditBase
    {
        public CustomerPhoneNumberDto()
        { }

        public CustomerPhoneNumberDto(Guid customerId, Guid phoneNumberId, Guid phoneNumberTypeId, bool phoneHasBeenVerified)
        {
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            PhoneNumberId = Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
            PhoneNumberTypeId = Guard.Against.NullOrEmpty(phoneNumberTypeId, nameof(phoneNumberTypeId));
            PhoneHasBeenVerified = Guard.Against.Null(phoneHasBeenVerified, nameof(phoneHasBeenVerified));
        }

        public CustomerDto Customer { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Phone Has Been Verified is required")]
        public bool PhoneHasBeenVerified { get; set; }

        public PhoneNumberDto PhoneNumber { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public Guid PhoneNumberId { get; set; }

        public PhoneNumberTypeDto PhoneNumberType { get; set; }

        [Required(ErrorMessage = "Phone Number Type is required")]
        public Guid PhoneNumberTypeId { get; set; }

        public int RowId { get; set; }
    }
}