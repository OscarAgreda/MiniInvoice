using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Account
{
    public class UpdateAccountResponse : BaseResponse
    {
        public UpdateAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateAccountResponse()
        {
        }

        public AccountDto Account { get; set; } = new();
    }
}