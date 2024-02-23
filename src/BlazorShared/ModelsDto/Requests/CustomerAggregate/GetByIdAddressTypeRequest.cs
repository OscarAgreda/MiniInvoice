using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.AddressType
{
    public class GetByIdAddressTypeRequest : BaseRequest
    {
        public Guid AddressTypeId { get; set; }
    }
}