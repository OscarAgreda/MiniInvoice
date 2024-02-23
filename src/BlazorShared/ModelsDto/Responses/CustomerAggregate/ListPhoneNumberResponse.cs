using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.PhoneNumber
{
    public class ListPhoneNumberResponse : BaseResponse
    {
        public ListPhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListPhoneNumberResponse()
        {
        }

        public int Count { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
    }
}