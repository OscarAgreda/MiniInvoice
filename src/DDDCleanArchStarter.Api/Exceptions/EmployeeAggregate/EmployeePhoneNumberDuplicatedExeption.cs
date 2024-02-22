using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateEmployeePhoneNumberException : ArgumentException
    {
        public DuplicateEmployeePhoneNumberException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
