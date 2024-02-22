using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class CustomerPhoneNumberGuardExtensions
    {
        public static void DuplicateCustomerPhoneNumber(this IGuardClause guardClause, IEnumerable<CustomerPhoneNumber> existingCustomerPhoneNumbers, CustomerPhoneNumber newCustomerPhoneNumber, string parameterName)
        {
            if (existingCustomerPhoneNumbers.Any(a => a.RowId == newCustomerPhoneNumber.RowId))
            {
                throw new DuplicateCustomerPhoneNumberException("Cannot add duplicate customerPhoneNumber.", parameterName);
            }
        }
    }
}
