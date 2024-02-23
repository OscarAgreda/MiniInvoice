using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAccount
{
    public class ListCustomerAccountResponse : BaseResponse
    {
        public ListCustomerAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListCustomerAccountResponse()
        {
        }

        public int Count { get; set; }
        public List<CustomerAccountDto> CustomerAccounts { get; set; } = new();
    }
}