using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateInvoiceException : ArgumentException
    {
        public DuplicateInvoiceException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
