using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAccount
{
    public class UpdateCustomerAccountResponse : BaseResponse
    {
        public UpdateCustomerAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateCustomerAccountResponse()
        {
        }

        public CustomerAccountDto CustomerAccount { get; set; } = new();
    }
}