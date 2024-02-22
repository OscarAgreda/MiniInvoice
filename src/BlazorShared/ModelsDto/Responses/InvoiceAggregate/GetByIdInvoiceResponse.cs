using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Invoice
{
    public class GetByIdInvoiceResponse : BaseResponse
    {
        public GetByIdInvoiceResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public GetByIdInvoiceResponse()
        {
        }
        public InvoiceDto Invoice { get; set; } = new();
    }
}
