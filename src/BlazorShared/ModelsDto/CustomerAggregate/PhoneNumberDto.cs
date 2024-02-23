using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class PhoneNumberDto : AuditBase
    {
        public PhoneNumberDto()
        { }

        public PhoneNumberDto(Guid phoneNumberId, string phoneNumberString)
        {
            PhoneNumberId = Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
            AuditEntityId = phoneNumberId.ToString();
            AuditId = Guid.NewGuid().ToString();
            AuditEntityType = GetType().Name;
            PhoneNumberString = Guard.Against.NullOrWhiteSpace(phoneNumberString, nameof(phoneNumberString));
        }

        public List<CustomerPhoneNumberDto> CustomerPhoneNumbers { get; set; } = new();
        public Guid PhoneNumberId { get; set; }

        [Required(ErrorMessage = "Phone Number String is required")]
        [MaxLength(255)]
        public string PhoneNumberString { get; set; }
    }
}