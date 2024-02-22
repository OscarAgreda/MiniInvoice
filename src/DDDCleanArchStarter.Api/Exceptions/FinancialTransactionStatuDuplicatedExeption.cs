using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateFinancialTransactionStatusException : ArgumentException
    {
        public DuplicateFinancialTransactionStatusException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
