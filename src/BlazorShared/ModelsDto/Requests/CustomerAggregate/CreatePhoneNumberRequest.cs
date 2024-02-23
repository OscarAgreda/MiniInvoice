using BlazorShared.Models;

namespace BlazorMauiShared.Models.PhoneNumber
{
    public class CreatePhoneNumberRequest : BaseRequest
    {
        public string PhoneNumberString { get; set; }
    }
}