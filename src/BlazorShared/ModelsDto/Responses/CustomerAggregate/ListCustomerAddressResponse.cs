using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class ListCustomerAddressResponse : BaseResponse
    {
        public ListCustomerAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListCustomerAddressResponse()
        {
        }

        public int Count { get; set; }
        public List<CustomerAddressDto> CustomerAddresses { get; set; } = new();
    }
}