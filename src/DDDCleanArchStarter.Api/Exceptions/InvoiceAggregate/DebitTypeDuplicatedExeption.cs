using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateDebitTypeException : ArgumentException
    {
        public DuplicateDebitTypeException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
