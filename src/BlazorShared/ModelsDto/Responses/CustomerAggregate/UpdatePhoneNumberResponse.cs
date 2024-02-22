using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.PhoneNumber
{
    public class UpdatePhoneNumberResponse : BaseResponse
    {
        public UpdatePhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdatePhoneNumberResponse()
        {
        }
        public PhoneNumberDto PhoneNumber { get; set; } = new();
    }
}
