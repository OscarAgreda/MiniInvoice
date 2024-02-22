using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateCityException : ArgumentException
    {
        public DuplicateCityException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
