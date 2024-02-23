using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.PhoneNumber
{
    public class CreatePhoneNumberResponse : BaseResponse
    {
        public CreatePhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public CreatePhoneNumberResponse()
        {
        }

        public PhoneNumberDto PhoneNumber { get; set; } = new();
    }
}