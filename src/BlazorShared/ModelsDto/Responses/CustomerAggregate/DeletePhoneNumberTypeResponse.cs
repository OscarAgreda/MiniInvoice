using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class DeletePhoneNumberTypeResponse : BaseResponse
    {
        public DeletePhoneNumberTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeletePhoneNumberTypeResponse()
        {
        }
    }
}
