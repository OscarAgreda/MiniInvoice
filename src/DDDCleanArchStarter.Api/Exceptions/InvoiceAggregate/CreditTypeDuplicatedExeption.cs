using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateCreditTypeException : ArgumentException
    {
        public DuplicateCreditTypeException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
