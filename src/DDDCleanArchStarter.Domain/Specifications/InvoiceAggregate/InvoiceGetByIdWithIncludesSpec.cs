using System;

using System.Linq;

using Ardalis.GuardClauses;

using Ardalis.Specification;

using DDDInvoicingClean.Domain.Entities;

namespace DDDInvoicingClean.Domain.Specifications

{

   public class InvoiceByIdWithIncludesSpec : Specification<Invoice>, ISingleResultSpecification<Invoice>

   {

       public InvoiceByIdWithIncludesSpec(Guid invoiceId)

       {

           _ = Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));

           _ = Query.Where(invoice => invoice.InvoiceId == invoiceId)
               .Include(x => x.Account)
               .Include(x => x.InvoiceDetails).ThenInclude(x => x.Product).AsNoTracking().AsSplitQuery()

               .EnableCache($"InvoiceByIdWithIncludesSpec-{invoiceId.ToString()}");

       }

   }

}