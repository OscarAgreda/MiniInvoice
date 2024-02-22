using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateStateException : ArgumentException
    {
        public DuplicateStateException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
