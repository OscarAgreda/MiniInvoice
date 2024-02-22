using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class CreateAccountAdjustmentResponse : BaseResponse
    {
        public CreateAccountAdjustmentResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateAccountAdjustmentResponse()
        {
        }
        public AccountAdjustmentDto AccountAdjustment { get; set; } = new();
    }
}
