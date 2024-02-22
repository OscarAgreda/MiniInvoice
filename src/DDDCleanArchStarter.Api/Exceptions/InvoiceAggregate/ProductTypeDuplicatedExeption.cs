using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateProductTypeException : ArgumentException
    {
        public DuplicateProductTypeException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
