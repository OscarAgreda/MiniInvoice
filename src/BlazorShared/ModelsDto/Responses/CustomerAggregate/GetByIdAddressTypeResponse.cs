using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.AddressType
{
    public class GetByIdAddressTypeResponse : BaseResponse
    {
        public GetByIdAddressTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdAddressTypeResponse()
        {
        }
        public AddressTypeDto AddressType { get; set; } = new();
    }
}
