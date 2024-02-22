using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class AddressTypeGuardExtensions
    {
        public static void DuplicateAddressType(this IGuardClause guardClause, IEnumerable<AddressType> existingAddressTypes, AddressType newAddressType, string parameterName)
        {
            if (existingAddressTypes.Any(a => a.AddressTypeId == newAddressType.AddressTypeId))
            {
                throw new DuplicateAddressTypeException("Cannot add duplicate addressType.", parameterName);
            }
        }
    }
}
