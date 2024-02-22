using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.Address
{
    public class GetByIdAddressRequest : BaseRequest
    {
        public Guid AddressId { get; set; }
    }
}
