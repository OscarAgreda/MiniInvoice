using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateCustomerAccountException : ArgumentException
    {
        public DuplicateCustomerAccountException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
