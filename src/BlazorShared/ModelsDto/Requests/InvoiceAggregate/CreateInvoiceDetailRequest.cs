using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class CreateInvoiceDetailRequest : BaseRequest
    {
        public Guid InvoiceId { get; set; }
        public decimal? LineDiscount { get; set; }
        public decimal? LineSale { get; set; }
        public decimal? LineTax { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public Guid TenantId { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}