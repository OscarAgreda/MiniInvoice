using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.CustomerAddress
{
    public class DeleteCustomerAddressResponse : BaseResponse
    {
        public DeleteCustomerAddressResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteCustomerAddressResponse()
        {
        }
    }
}
