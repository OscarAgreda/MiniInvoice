using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class ListAccountAdjustmentResponse : BaseResponse
    {
        public ListAccountAdjustmentResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListAccountAdjustmentResponse()
        {
        }

        public List<AccountAdjustmentDto> AccountAdjustments { get; set; } = new();
        public int Count { get; set; }
    }
}