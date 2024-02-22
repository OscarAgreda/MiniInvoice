using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetCustomerPhoneNumberWithPhoneNumberKeySpec : Specification<CustomerPhoneNumber>
    {
        public GetCustomerPhoneNumberWithPhoneNumberKeySpec(Guid phoneNumberId)
        {
            Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
            Query.Where(cpn => cpn.PhoneNumberId == phoneNumberId).AsNoTracking().EnableCache($"GetCustomerPhoneNumberWithPhoneNumberKeySpec-{phoneNumberId}");
        }
    }
}
