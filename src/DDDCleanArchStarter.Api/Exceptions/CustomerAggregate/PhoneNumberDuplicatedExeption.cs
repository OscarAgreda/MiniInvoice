using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicatePhoneNumberException : ArgumentException
    {
        public DuplicatePhoneNumberException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
