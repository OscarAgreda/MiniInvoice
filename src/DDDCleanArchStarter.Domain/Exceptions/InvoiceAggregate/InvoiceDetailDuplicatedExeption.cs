using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateInvoiceDetailException : ArgumentException
    {
        public DuplicateInvoiceDetailException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
