using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class GetByIdPhoneNumberTypeRequest : BaseRequest
    {
        public Guid PhoneNumberTypeId { get; set; }
    }
}