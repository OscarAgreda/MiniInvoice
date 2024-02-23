using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class CreateAccountAdjustmentRequest : BaseRequest
    {
        public Guid AccountId { get; set; }
        public string AdjustmentReason { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal TotalChargeCredited { get; set; }
        public decimal TotalTaxCredited { get; set; }
    }
}