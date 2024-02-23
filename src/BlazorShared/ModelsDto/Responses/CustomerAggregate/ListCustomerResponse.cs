using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Customer
{
    public class ListCustomerResponse : BaseResponse
    {
        public ListCustomerResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListCustomerResponse()
        {
        }

        public int Count { get; set; }
        public List<CustomerDto> Customers { get; set; } = new();
    }
}