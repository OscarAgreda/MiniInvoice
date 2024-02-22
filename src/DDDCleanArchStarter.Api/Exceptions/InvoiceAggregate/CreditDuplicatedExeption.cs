using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateCreditException : ArgumentException
    {
        public DuplicateCreditException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
