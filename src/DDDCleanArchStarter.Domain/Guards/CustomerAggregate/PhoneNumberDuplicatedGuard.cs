using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class PhoneNumberGuardExtensions
    {
        public static void DuplicatePhoneNumber(this IGuardClause guardClause, IEnumerable<PhoneNumber> existingPhoneNumbers, PhoneNumber newPhoneNumber, string parameterName)
        {
            if (existingPhoneNumbers.Any(a => a.PhoneNumberId == newPhoneNumber.PhoneNumberId))
            {
                throw new DuplicatePhoneNumberException("Cannot add duplicate phoneNumber.", parameterName);
            }
        }
    }
}
