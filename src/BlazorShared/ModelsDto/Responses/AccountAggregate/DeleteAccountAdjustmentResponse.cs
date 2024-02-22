using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class DeleteAccountAdjustmentResponse : BaseResponse
    {
        public DeleteAccountAdjustmentResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteAccountAdjustmentResponse()
        {
        }
    }
}
