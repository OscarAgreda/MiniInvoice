using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class DeleteAccountAdjustmentRequest : BaseRequest
    {
        public Guid AccountAdjustmentId { get; set; }
    }
}
