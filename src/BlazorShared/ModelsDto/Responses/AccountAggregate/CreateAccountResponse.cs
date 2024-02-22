using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Account
{
    public class CreateAccountResponse : BaseResponse
    {
        public CreateAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateAccountResponse()
        {
        }
        public AccountDto Account { get; set; } = new();
    }
}
