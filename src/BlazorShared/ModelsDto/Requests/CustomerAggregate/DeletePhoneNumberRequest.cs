using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.PhoneNumber
{
    public class DeletePhoneNumberRequest : BaseRequest
    {
        public Guid PhoneNumberId { get; set; }
    }
}
