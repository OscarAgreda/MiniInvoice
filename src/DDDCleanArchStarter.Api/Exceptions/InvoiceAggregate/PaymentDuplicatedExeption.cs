using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicatePaymentException : ArgumentException
    {
        public DuplicatePaymentException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
