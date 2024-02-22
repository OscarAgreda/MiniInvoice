using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class GetByIdPhoneNumberTypeResponse : BaseResponse
    {
        public GetByIdPhoneNumberTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdPhoneNumberTypeResponse()
        {
        }
        public PhoneNumberTypeDto PhoneNumberType { get; set; } = new();
    }
}
