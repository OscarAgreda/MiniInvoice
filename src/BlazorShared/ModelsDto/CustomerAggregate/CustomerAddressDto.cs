using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class CustomerAddressDto : AuditBase
    {
        public int RowId { get;  set; }
        public AddressDto Address { get;  set; }
        [Required(ErrorMessage="Address is required")]
        public Guid AddressId { get;  set; }
        public AddressTypeDto AddressType { get;  set; }
        [Required(ErrorMessage="Address Type is required")]
        public Guid AddressTypeId { get;  set; }
        public CustomerDto Customer { get;  set; }
        [Required(ErrorMessage="Customer is required")]
        public Guid CustomerId { get;  set; }
        public CustomerAddressDto() {}
        public CustomerAddressDto(Guid addressId, Guid addressTypeId, Guid customerId)
        {
                AddressId = Guard.Against.NullOrEmpty(addressId, nameof(addressId));
                AddressTypeId = Guard.Against.NullOrEmpty(addressTypeId, nameof(addressTypeId));
                CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
        }
    }
}
