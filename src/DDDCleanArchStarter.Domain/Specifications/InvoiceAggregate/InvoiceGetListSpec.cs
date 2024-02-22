using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class InvoiceGetListSpec : Specification<Invoice>
    {
        public InvoiceGetListSpec()
        {
            Query.OrderBy(invoice => invoice.InvoiceId)
      .AsNoTracking().EnableCache($"InvoiceGetListSpec");
  }
  }
}
