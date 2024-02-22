using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class PhoneNumber : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<CustomerPhoneNumber> _customerPhoneNumbers = new();

        public PhoneNumber(Guid phoneNumberId, string phoneNumberString)
        {
            PhoneNumberId = Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
            PhoneNumberString = Guard.Against.NullOrWhiteSpace(phoneNumberString, nameof(phoneNumberString));
        }

        private PhoneNumber()
        { }

        public IEnumerable<CustomerPhoneNumber> CustomerPhoneNumbers => _customerPhoneNumbers.AsReadOnly();

        [Key]
        public Guid PhoneNumberId { get; private set; }

        public string PhoneNumberString { get; private set; }

        public void SetPhoneNumberString(string phoneNumberString)
        {
            PhoneNumberString = Guard.Against.NullOrEmpty(phoneNumberString, nameof(phoneNumberString));
        }
    }
}