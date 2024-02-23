using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.AddressType
{
    public class CreateAddressTypeResponse : BaseResponse
    {
        public CreateAddressTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public CreateAddressTypeResponse()
        {
        }

        public AddressTypeDto AddressType { get; set; } = new();
    }
}