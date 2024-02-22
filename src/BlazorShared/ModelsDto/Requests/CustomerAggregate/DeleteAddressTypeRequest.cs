using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.AddressType
{
    public class DeleteAddressTypeRequest : BaseRequest
    {
        public Guid AddressTypeId { get; set; }
    }
}
