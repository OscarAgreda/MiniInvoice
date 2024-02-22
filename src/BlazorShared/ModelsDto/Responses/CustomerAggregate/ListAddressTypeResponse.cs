using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.AddressType
{
    public class ListAddressTypeResponse : BaseResponse
    {
        public ListAddressTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public ListAddressTypeResponse()
        {
        }
        public List<AddressTypeDto> AddressTypes { get; set; } = new();
        public int Count { get; set; }
    }
}
