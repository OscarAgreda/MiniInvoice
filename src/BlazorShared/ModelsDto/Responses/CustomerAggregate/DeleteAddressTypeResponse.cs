using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.AddressType
{
    public class DeleteAddressTypeResponse : BaseResponse
    {
        public DeleteAddressTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteAddressTypeResponse()
        {
        }
    }
}
