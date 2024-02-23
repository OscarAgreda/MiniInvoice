using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class CreateCustomerPhoneNumberRequest : BaseRequest
    {
        public Guid CustomerId { get; set; }
        public bool PhoneHasBeenVerified { get; set; }
        public Guid PhoneNumberId { get; set; }
        public Guid PhoneNumberTypeId { get; set; }
    }
}