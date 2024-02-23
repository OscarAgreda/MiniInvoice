using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class UpdateAccountAdjustmentResponse : BaseResponse
    {
        public UpdateAccountAdjustmentResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateAccountAdjustmentResponse()
        {
        }

        public AccountAdjustmentDto AccountAdjustment { get; set; } = new();
    }
}