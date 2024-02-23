using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Address
{
    public class CreateAddressResponse : BaseResponse
    {
        public CreateAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public CreateAddressResponse()
        {
        }

        public AddressDto Address { get; set; } = new();
    }
}