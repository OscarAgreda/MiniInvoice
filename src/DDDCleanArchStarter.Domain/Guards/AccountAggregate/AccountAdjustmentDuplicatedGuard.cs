using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class AccountAdjustmentGuardExtensions
    {
        public static void DuplicateAccountAdjustment(this IGuardClause guardClause, IEnumerable<AccountAdjustment> existingAccountAdjustments, AccountAdjustment newAccountAdjustment, string parameterName)
        {
            if (existingAccountAdjustments.Any(a => a.AccountAdjustmentId == newAccountAdjustment.AccountAdjustmentId))
            {
                throw new DuplicateAccountAdjustmentException("Cannot add duplicate accountAdjustment.", parameterName);
            }
        }
    }
}
