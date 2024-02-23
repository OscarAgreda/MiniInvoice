using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Address
{
    public class ListAddressResponse : BaseResponse
    {
        public ListAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListAddressResponse()
        {
        }

        public List<AddressDto> Addresses { get; set; } = new();
        public int Count { get; set; }
    }
}