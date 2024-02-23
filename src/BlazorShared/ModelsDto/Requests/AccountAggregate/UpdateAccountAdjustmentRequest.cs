using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.AccountAdjustment
{
    public class UpdateAccountAdjustmentRequest : BaseRequest
    {
        public Guid AccountAdjustmentId { get; set; }
        public Guid AccountId { get; set; }
        public string AdjustmentReason { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal TotalChargeCredited { get; set; }
        public decimal TotalTaxCredited { get; set; }

        public static UpdateAccountAdjustmentRequest FromDto(AccountAdjustmentDto accountAdjustmentDto)
        {
            return new UpdateAccountAdjustmentRequest
            {
                AccountAdjustmentId = accountAdjustmentDto.AccountAdjustmentId,
                AccountId = accountAdjustmentDto.AccountId,
                AdjustmentReason = accountAdjustmentDto.AdjustmentReason,
                TotalChargeCredited = accountAdjustmentDto.TotalChargeCredited,
                TotalTaxCredited = accountAdjustmentDto.TotalTaxCredited,
                IsDeleted = accountAdjustmentDto.IsDeleted
            };
        }
    }
}