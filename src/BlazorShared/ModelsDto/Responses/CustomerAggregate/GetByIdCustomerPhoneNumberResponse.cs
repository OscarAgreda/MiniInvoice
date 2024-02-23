using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class GetByIdCustomerPhoneNumberResponse : BaseResponse
    {
        public GetByIdCustomerPhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdCustomerPhoneNumberResponse()
        {
        }

        public CustomerPhoneNumberDto CustomerPhoneNumber { get; set; } = new();
    }
}