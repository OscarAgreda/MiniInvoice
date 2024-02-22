using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Customer
{
    public class GetByIdCustomerResponse : BaseResponse
    {
        public GetByIdCustomerResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdCustomerResponse()
        {
        }
        public CustomerDto Customer { get; set; } = new();
    }
}
