using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class CreateCustomerAddressRequest : BaseRequest
    {
        public Guid AddressId { get; set; }
        public Guid AddressTypeId { get; set; }
        public Guid CustomerId { get; set; }
    }
}