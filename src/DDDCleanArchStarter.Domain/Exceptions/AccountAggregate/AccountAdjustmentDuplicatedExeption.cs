using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateAccountAdjustmentException : ArgumentException
    {
        public DuplicateAccountAdjustmentException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
