using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class InvoiceDetailGuardExtensions
    {
        public static void DuplicateInvoiceDetail(this IGuardClause guardClause, IEnumerable<InvoiceDetail> existingInvoiceDetails, InvoiceDetail newInvoiceDetail, string parameterName)
        {
            if (existingInvoiceDetails.Any(a => a.InvoiceDetailId == newInvoiceDetail.InvoiceDetailId))
            {
                throw new DuplicateInvoiceDetailException("Cannot add duplicate invoiceDetail.", parameterName);
            }
        }
    }
}
