using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateFinancialTransactionException : ArgumentException
    {
        public DuplicateFinancialTransactionException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
