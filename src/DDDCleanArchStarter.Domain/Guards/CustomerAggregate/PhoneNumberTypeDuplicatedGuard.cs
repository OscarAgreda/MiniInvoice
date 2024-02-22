using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class PhoneNumberTypeGuardExtensions
    {
        public static void DuplicatePhoneNumberType(this IGuardClause guardClause, IEnumerable<PhoneNumberType> existingPhoneNumberTypes, PhoneNumberType newPhoneNumberType, string parameterName)
        {
            if (existingPhoneNumberTypes.Any(a => a.PhoneNumberTypeId == newPhoneNumberType.PhoneNumberTypeId))
            {
                throw new DuplicatePhoneNumberTypeException("Cannot add duplicate phoneNumberType.", parameterName);
            }
        }
    }
}
