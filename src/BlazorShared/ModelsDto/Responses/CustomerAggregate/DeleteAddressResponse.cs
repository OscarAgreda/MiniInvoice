using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Address
{
    public class DeleteAddressResponse : BaseResponse
    {
        public DeleteAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteAddressResponse()
        {
        }
    }
}
