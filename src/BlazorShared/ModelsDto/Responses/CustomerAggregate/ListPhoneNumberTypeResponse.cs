using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.PhoneNumberType
{
    public class ListPhoneNumberTypeResponse : BaseResponse
    {
        public ListPhoneNumberTypeResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListPhoneNumberTypeResponse()
        {
        }

        public int Count { get; set; }
        public List<PhoneNumberTypeDto> PhoneNumberTypes { get; set; } = new();
    }
}