using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.Invoice
{
    public class DeleteInvoiceRequest : BaseRequest
    {
        public Guid InvoiceId { get; set; }
    }
}
