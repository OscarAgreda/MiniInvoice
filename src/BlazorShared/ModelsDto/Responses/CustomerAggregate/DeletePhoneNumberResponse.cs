using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.PhoneNumber
{
    public class DeletePhoneNumberResponse : BaseResponse
    {
        public DeletePhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeletePhoneNumberResponse()
        {
        }
    }
}
