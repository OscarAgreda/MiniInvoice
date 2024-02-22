using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateFinancialTransactionTypeException : ArgumentException
    {
        public DuplicateFinancialTransactionTypeException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
