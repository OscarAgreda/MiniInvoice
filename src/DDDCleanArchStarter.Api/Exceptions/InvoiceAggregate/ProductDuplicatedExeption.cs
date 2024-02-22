using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateProductException : ArgumentException
    {
        public DuplicateProductException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
