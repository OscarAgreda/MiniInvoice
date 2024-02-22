using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateVendorException : ArgumentException
    {
        public DuplicateVendorException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
