using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Invoice
{
    public class CreateInvoiceResponse : BaseResponse
    {
        public CreateInvoiceResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public CreateInvoiceResponse()
        {
        }

        public InvoiceDto Invoice { get; set; } = new();
    }
}