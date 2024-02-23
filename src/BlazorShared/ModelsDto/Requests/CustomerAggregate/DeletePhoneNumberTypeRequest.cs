using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class DeletePhoneNumberTypeRequest : BaseRequest
    {
        public Guid PhoneNumberTypeId { get; set; }
    }
}