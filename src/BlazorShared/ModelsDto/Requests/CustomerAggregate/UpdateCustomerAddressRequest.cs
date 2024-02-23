using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class UpdateCustomerAddressRequest : BaseRequest
    {
        public Guid AddressId { get; set; }
        public Guid AddressTypeId { get; set; }
        public Guid CustomerId { get; set; }
        public int RowId { get; set; }

        public static UpdateCustomerAddressRequest FromDto(CustomerAddressDto customerAddressDto)
        {
            return new UpdateCustomerAddressRequest
            {
                RowId = customerAddressDto.RowId,
                AddressId = customerAddressDto.AddressId,
                AddressTypeId = customerAddressDto.AddressTypeId,
                CustomerId = customerAddressDto.CustomerId
            };
        }
    }
}