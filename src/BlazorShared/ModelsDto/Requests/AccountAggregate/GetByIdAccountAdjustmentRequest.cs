using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class GetByIdAccountAdjustmentRequest : BaseRequest
    {
        public Guid AccountAdjustmentId { get; set; }
    }
}