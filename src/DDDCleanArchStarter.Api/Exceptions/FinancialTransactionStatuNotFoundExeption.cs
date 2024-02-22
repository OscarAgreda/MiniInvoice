using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class FinancialTransactionStatusNotFoundException : Exception
  {
    public FinancialTransactionStatusNotFoundException(string message) : base(message)
    {
    }
    public FinancialTransactionStatusNotFoundException(int rowId) : base($"No financialTransactionStatus with id {rowId} found.")
    {
    }
  }
}
