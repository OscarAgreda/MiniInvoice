using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class GetByIdCustomerAddressResponse : BaseResponse
    {
        public GetByIdCustomerAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdCustomerAddressResponse()
        {
        }

        public CustomerAddressDto CustomerAddress { get; set; } = new();
    }
}