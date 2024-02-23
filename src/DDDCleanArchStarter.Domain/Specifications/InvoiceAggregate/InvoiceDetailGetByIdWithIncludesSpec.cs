using System;

using System.Linq;

using Ardalis.GuardClauses;

using Ardalis.Specification;

using DDDInvoicingClean.Domain.Entities;

namespace DDDInvoicingClean.Domain.Specifications

{

   public class InvoiceDetailByIdWithIncludesSpec : Specification<InvoiceDetail>, ISingleResultSpecification<InvoiceDetail>

   {

       public InvoiceDetailByIdWithIncludesSpec(Guid invoiceDetailId)

       {

           _ = Guard.Against.NullOrEmpty(invoiceDetailId, nameof(invoiceDetailId));

           _ = Query.Where(invoiceDetail => invoiceDetail.InvoiceDetailId == invoiceDetailId)
               .Include(x => x.Invoice)
               .Include(x => x.Product).AsNoTracking().AsSplitQuery()

               .EnableCache($"InvoiceDetailByIdWithIncludesSpec-{invoiceDetailId.ToString()}");

       }

   }

}