using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class CreateCustomerPhoneNumberResponse : BaseResponse
    {
        public CreateCustomerPhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateCustomerPhoneNumberResponse()
        {
        }
        public CustomerPhoneNumberDto CustomerPhoneNumber { get; set; } = new();
    }
}
