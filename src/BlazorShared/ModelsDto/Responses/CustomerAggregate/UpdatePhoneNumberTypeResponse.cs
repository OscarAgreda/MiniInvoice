using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class UpdatePhoneNumberTypeResponse : BaseResponse
    {
        public UpdatePhoneNumberTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdatePhoneNumberTypeResponse()
        {
        }
        public PhoneNumberTypeDto PhoneNumberType { get; set; } = new();
    }
}
