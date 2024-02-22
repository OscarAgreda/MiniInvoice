using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Account
{
    public class GetByIdAccountResponse : BaseResponse
    {
        public GetByIdAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdAccountResponse()
        {
        }
        public AccountDto Account { get; set; } = new();
    }
}
