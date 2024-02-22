using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
    public class DuplicateProductCategoryException : ArgumentException
    {
        public DuplicateProductCategoryException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
