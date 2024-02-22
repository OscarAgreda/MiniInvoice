using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class InvoiceDetailNotFoundException : Exception
  {
    public InvoiceDetailNotFoundException(string message) : base(message)
    {
    }
    public InvoiceDetailNotFoundException(int invoiceDetailId) : base($"No invoiceDetail with id {invoiceDetailId} found.")
    {
    }
  }
}
