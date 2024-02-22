using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateAddressException : ArgumentException
    {
        public DuplicateAddressException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
