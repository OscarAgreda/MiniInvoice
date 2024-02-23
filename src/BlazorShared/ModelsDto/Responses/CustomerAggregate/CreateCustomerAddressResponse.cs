using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class CreateCustomerAddressResponse : BaseResponse
    {
        public CreateCustomerAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public CreateCustomerAddressResponse()
        {
        }

        public CustomerAddressDto CustomerAddress { get; set; } = new();
    }
}