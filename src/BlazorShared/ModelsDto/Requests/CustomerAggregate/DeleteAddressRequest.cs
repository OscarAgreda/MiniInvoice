using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Address
{
    public class DeleteAddressRequest : BaseRequest
    {
        public Guid AddressId { get; set; }
    }
}