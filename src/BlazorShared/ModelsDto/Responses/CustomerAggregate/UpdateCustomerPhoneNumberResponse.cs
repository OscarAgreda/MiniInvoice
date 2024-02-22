using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class UpdateCustomerPhoneNumberResponse : BaseResponse
    {
        public UpdateCustomerPhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdateCustomerPhoneNumberResponse()
        {
        }
        public CustomerPhoneNumberDto CustomerPhoneNumber { get; set; } = new();
    }
}
