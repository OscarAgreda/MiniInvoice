using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class GetByRelsIdsCustomerAddressRequest : BaseRequest
    {
        public Guid AddressId { get; set; }
        public Guid CustomerId { get; set; }
    }
}