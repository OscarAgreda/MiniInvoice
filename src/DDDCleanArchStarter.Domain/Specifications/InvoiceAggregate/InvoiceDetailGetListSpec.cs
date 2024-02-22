using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class InvoiceDetailGetListSpec : Specification<InvoiceDetail>
    {
        public InvoiceDetailGetListSpec()
        {
            Query.OrderBy(invoiceDetail => invoiceDetail.InvoiceDetailId)
      .AsNoTracking().EnableCache($"InvoiceDetailGetListSpec");
  }
  }
}
