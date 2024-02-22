using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.PhoneNumber
{
    public class GetByIdPhoneNumberResponse : BaseResponse
    {
        public GetByIdPhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdPhoneNumberResponse()
        {
        }
        public PhoneNumberDto PhoneNumber { get; set; } = new();
    }
}
