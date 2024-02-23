using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class UpdateInvoiceDetailResponse : BaseResponse
    {
        public UpdateInvoiceDetailResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public UpdateInvoiceDetailResponse()
        {
        }

        public InvoiceDetailDto InvoiceDetail { get; set; } = new();
    }
}