using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.PhoneNumber
{
    public class GetByIdPhoneNumberRequest : BaseRequest
    {
        public Guid PhoneNumberId { get; set; }
    }
}
