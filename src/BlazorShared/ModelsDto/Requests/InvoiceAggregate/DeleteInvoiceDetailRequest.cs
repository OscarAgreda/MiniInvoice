using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class DeleteInvoiceDetailRequest : BaseRequest
    {
        public Guid InvoiceDetailId { get; set; }
    }
}