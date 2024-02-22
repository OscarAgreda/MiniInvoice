using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class AccountAdjustment : BaseEntityEv<Guid>, IAggregateRoot
    {
        public AccountAdjustment(Guid accountAdjustmentId, Guid accountId, string adjustmentReason, decimal totalChargeCredited, decimal totalTaxCredited, bool? isDeleted)
        {
            AccountAdjustmentId = Guard.Against.NullOrEmpty(accountAdjustmentId, nameof(accountAdjustmentId));
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            AdjustmentReason = Guard.Against.NullOrWhiteSpace(adjustmentReason, nameof(adjustmentReason));
            TotalChargeCredited = Guard.Against.Negative(totalChargeCredited, nameof(totalChargeCredited));
            TotalTaxCredited = Guard.Against.Negative(totalTaxCredited, nameof(totalTaxCredited));
            IsDeleted = isDeleted;
        }

        private AccountAdjustment()
        { }

        public virtual Account Account { get; private set; }

        [Key]
        public Guid AccountAdjustmentId { get; private set; }

        public Guid AccountId { get; private set; }
        public string AdjustmentReason { get; private set; }
        public bool? IsDeleted { get; private set; }
        public decimal TotalChargeCredited { get; private set; }
        public decimal TotalTaxCredited { get; private set; }

        public void SetAccountId(Guid accountId)
        {
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
        }

        public void SetAdjustmentReason(string adjustmentReason)
        {
            AdjustmentReason = Guard.Against.NullOrEmpty(adjustmentReason, nameof(adjustmentReason));
        }
    }
}