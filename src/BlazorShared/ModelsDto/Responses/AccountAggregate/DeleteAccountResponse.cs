using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Account
{
    public class DeleteAccountResponse : BaseResponse
    {
        public DeleteAccountResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteAccountResponse()
        {
        }
    }
}
