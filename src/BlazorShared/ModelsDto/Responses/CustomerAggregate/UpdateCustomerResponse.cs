using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Customer
{
    public class UpdateCustomerResponse : BaseResponse
    {
        public UpdateCustomerResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateCustomerResponse()
        {
        }

        public CustomerDto Customer { get; set; } = new();
    }
}