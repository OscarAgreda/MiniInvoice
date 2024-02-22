using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class CreatePhoneNumberTypeResponse : BaseResponse
    {
        public CreatePhoneNumberTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreatePhoneNumberTypeResponse()
        {
        }
        public PhoneNumberTypeDto PhoneNumberType { get; set; } = new();
    }
}
