using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class AddressGuardExtensions
    {
        public static void DuplicateAddress(this IGuardClause guardClause, IEnumerable<Address> existingAddresses, Address newAddress, string parameterName)
        {
            if (existingAddresses.Any(a => a.AddressId == newAddress.AddressId))
            {
                throw new DuplicateAddressException("Cannot add duplicate address.", parameterName);
            }
        }
    }
}
