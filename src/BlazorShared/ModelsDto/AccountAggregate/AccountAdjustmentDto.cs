using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class AccountAdjustmentDto : AuditBase
    {
        public AccountAdjustmentDto()
        { }

        public AccountAdjustmentDto(Guid accountAdjustmentId, Guid accountId, string adjustmentReason, decimal totalChargeCredited, decimal totalTaxCredited, bool? isDeleted)
        {
            AccountAdjustmentId = Guard.Against.NullOrEmpty(accountAdjustmentId, nameof(accountAdjustmentId));
            AuditEntityId = accountAdjustmentId.ToString();
            AuditId = Guid.NewGuid().ToString();
            AuditEntityType = GetType().Name;
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            AdjustmentReason = Guard.Against.NullOrWhiteSpace(adjustmentReason, nameof(adjustmentReason));
            TotalChargeCredited = Guard.Against.Negative(totalChargeCredited, nameof(totalChargeCredited));
            TotalTaxCredited = Guard.Against.Negative(totalTaxCredited, nameof(totalTaxCredited));
            IsDeleted = isDeleted;
        }

        public AccountDto Account { get; set; }
        public Guid AccountAdjustmentId { get; set; }

        [Required(ErrorMessage = "Account is required")]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Adjustment Reason is required")]
        [MaxLength(255)]
        public string AdjustmentReason { get; set; }

        public bool? IsDeleted { get; set; }

        [Required(ErrorMessage = "Total Charge Credited is required")]
        public decimal TotalChargeCredited { get; set; }

        [Required(ErrorMessage = "Total Tax Credited is required")]
        public decimal TotalTaxCredited { get; set; }
    }
}