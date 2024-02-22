using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateCountryException : ArgumentException
    {
        public DuplicateCountryException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
