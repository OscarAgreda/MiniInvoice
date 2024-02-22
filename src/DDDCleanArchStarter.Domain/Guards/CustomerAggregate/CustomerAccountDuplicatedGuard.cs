using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class CustomerAccountGuardExtensions
    {
        public static void DuplicateCustomerAccount(this IGuardClause guardClause, IEnumerable<CustomerAccount> existingCustomerAccounts, CustomerAccount newCustomerAccount, string parameterName)
        {
            if (existingCustomerAccounts.Any(a => a.RowId == newCustomerAccount.RowId))
            {
                throw new DuplicateCustomerAccountException("Cannot add duplicate customerAccount.", parameterName);
            }
        }
    }
}
