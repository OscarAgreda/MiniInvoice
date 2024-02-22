using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicatePaymentTypeException : ArgumentException
    {
        public DuplicatePaymentTypeException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
