using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateCustomerPhoneNumberException : ArgumentException
    {
        public DuplicateCustomerPhoneNumberException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
