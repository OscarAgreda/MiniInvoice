using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class InvoiceNotFoundException : Exception
  {
    public InvoiceNotFoundException(string message) : base(message)
    {
    }
    public InvoiceNotFoundException(int invoiceId) : base($"No invoice with id {invoiceId} found.")
    {
    }
  }
}
