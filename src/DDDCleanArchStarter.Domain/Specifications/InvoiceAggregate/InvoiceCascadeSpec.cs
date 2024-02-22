using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetInvoiceDetailWithInvoiceKeySpec : Specification<InvoiceDetail>
    {
        public GetInvoiceDetailWithInvoiceKeySpec(Guid invoiceId)
        {
            Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));
            Query.Where(id => id.InvoiceId == invoiceId).AsNoTracking().EnableCache($"GetInvoiceDetailWithInvoiceKeySpec-{invoiceId}");
        }
    }
}
