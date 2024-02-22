using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class GetByRelsIdsCustomerPhoneNumberRequest : BaseRequest
    {
        public Guid CustomerId { get; set; }
        public Guid PhoneNumberId { get; set; }
        public bool PhoneHasBeenVerified { get; set; }
    }
}
