using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Address
{
    public class UpdateAddressResponse : BaseResponse
    {
        public UpdateAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdateAddressResponse()
        {
        }
        public AddressDto Address { get; set; } = new();
    }
}
