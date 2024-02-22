using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Customer
{
    public class CreateCustomerResponse : BaseResponse
    {
        public CreateCustomerResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateCustomerResponse()
        {
        }
        public CustomerDto Customer { get; set; } = new();
    }
}
