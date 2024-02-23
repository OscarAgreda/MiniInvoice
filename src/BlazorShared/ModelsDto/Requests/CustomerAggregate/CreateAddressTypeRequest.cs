using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.AddressType
{
    public class CreateAddressTypeRequest : BaseRequest
    {
        public string AddressTypeName { get; set; }
        public string? Description { get; set; }
        public Guid TenantId { get; set; }
    }
}