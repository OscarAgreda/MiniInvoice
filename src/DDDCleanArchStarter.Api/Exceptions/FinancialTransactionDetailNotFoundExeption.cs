using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class FinancialTransactionDetailNotFoundException : Exception
  {
    public FinancialTransactionDetailNotFoundException(string message) : base(message)
    {
    }
    public FinancialTransactionDetailNotFoundException(int financialTransactionDetailId) : base($"No financialTransactionDetail with id {financialTransactionDetailId} found.")
    {
    }
  }
}
