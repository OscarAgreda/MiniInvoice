using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAccount
{
    public class CreateCustomerAccountResponse : BaseResponse
    {
        public CreateCustomerAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public CreateCustomerAccountResponse()
        {
        }

        public CustomerAccountDto CustomerAccount { get; set; } = new();
    }
}