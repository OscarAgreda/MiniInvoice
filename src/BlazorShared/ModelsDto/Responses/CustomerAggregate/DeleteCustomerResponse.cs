using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Customer
{
    public class DeleteCustomerResponse : BaseResponse
    {
        public DeleteCustomerResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteCustomerResponse()
        {
        }
    }
}
