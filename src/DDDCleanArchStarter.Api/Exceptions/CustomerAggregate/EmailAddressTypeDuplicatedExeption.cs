using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateEmailAddressTypeException : ArgumentException
    {
        public DuplicateEmailAddressTypeException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
