using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicatePaymentMethodException : ArgumentException
    {
        public DuplicatePaymentMethodException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
