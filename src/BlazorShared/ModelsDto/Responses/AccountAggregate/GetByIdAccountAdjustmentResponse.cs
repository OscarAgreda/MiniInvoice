using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class GetByIdAccountAdjustmentResponse : BaseResponse
    {
        public GetByIdAccountAdjustmentResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdAccountAdjustmentResponse()
        {
        }

        public AccountAdjustmentDto AccountAdjustment { get; set; } = new();
    }
}