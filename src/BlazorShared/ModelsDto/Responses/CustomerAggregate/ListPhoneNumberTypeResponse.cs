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
        public List<PhoneNumberTypeDto> PhoneNumberTypes { get; set; } = new();
        public int Count { get; set; }
    }
}
