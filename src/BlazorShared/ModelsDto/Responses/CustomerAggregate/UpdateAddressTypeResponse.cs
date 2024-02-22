using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.AddressType
{
    public class UpdateAddressTypeResponse : BaseResponse
    {
        public UpdateAddressTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdateAddressTypeResponse()
        {
        }
        public AddressTypeDto AddressType { get; set; } = new();
    }
}
