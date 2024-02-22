using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.CustomerAccount
{
    public class DeleteCustomerAccountResponse : BaseResponse
    {
        public DeleteCustomerAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteCustomerAccountResponse()
        {
        }
    }
}
