using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateFinancialTransactionDetailException : ArgumentException
    {
        public DuplicateFinancialTransactionDetailException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
