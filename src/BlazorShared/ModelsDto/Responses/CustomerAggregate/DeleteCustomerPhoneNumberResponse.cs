using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class DeleteCustomerPhoneNumberResponse : BaseResponse
    {
        public DeleteCustomerPhoneNumberResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteCustomerPhoneNumberResponse()
        {
        }
    }
}
