using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class CreatePhoneNumberTypeRequest : BaseRequest
    {
        public string? Description { get; set; }
        public string PhoneNumberTypeName { get; set; }
        public Guid TenantId { get; set; }
    }
}