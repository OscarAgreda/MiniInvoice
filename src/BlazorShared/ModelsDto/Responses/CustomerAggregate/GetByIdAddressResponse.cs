using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Address
{
    public class GetByIdAddressResponse : BaseResponse
    {
        public GetByIdAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdAddressResponse()
        {
        }
        public AddressDto Address { get; set; } = new();
    }
}
