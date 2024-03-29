using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class ListCustomerPhoneNumberResponse : BaseResponse
    {
        public ListCustomerPhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListCustomerPhoneNumberResponse()
        {
        }

        public int Count { get; set; }
        public List<CustomerPhoneNumberDto> CustomerPhoneNumbers { get; set; } = new();
    }
}