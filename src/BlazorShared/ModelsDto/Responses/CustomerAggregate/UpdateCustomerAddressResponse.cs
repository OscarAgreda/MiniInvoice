using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class UpdateCustomerAddressResponse : BaseResponse
    {
        public UpdateCustomerAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateCustomerAddressResponse()
        {
        }

        public CustomerAddressDto CustomerAddress { get; set; } = new();
    }
}