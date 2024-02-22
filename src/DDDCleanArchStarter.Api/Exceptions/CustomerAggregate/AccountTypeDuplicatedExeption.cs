using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateAccountTypeException : ArgumentException
    {
        public DuplicateAccountTypeException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
