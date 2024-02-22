using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateDebitException : ArgumentException
    {
        public DuplicateDebitException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
