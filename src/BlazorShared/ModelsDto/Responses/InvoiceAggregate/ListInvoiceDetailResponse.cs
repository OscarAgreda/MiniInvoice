using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class ListInvoiceDetailResponse : BaseResponse
    {
        public ListInvoiceDetailResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListInvoiceDetailResponse()
        {
        }

        public int Count { get; set; }
        public List<InvoiceDetailDto> InvoiceDetails { get; set; } = new();
    }
}