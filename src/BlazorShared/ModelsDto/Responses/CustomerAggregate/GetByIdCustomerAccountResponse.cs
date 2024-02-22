using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.CustomerAccount
{
    public class GetByIdCustomerAccountResponse : BaseResponse
    {
        public GetByIdCustomerAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdCustomerAccountResponse()
        {
        }
        public CustomerAccountDto CustomerAccount { get; set; } = new();
    }
}
