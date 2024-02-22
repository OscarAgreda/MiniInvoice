using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Account
{
    public class ListAccountResponse : BaseResponse
    {
        public ListAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public ListAccountResponse()
        {
        }
        public List<AccountDto> Accounts { get; set; } = new();
        public int Count { get; set; }
    }
}
