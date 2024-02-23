using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.Invoice
{
    public class UpdateInvoiceResponse : BaseResponse
    {
        public UpdateInvoiceResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateInvoiceResponse()
        {
        }

        public InvoiceDto Invoice { get; set; } = new();
    }
}