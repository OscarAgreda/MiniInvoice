using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.InvoiceDetail
{
    public class UpdateInvoiceDetailRequest : BaseRequest
    {
      public Guid InvoiceDetailId { get; set; }
      public Guid InvoiceId { get; set; }
      public Guid ProductId { get; set; }
      public Guid TenantId { get; set; }
      public decimal Quantity { get; set; }
      public string ProductName { get; set; }
      public decimal? UnitPrice { get; set; }
      public decimal? LineSale { get; set; }
      public decimal? LineTax { get; set; }
      public decimal? LineDiscount { get; set; }
        public static UpdateInvoiceDetailRequest FromDto(InvoiceDetailDto invoiceDetailDto)
        {
            return new UpdateInvoiceDetailRequest
            {
                InvoiceDetailId = invoiceDetailDto.InvoiceDetailId,
                InvoiceId = invoiceDetailDto.InvoiceId,
                ProductId = invoiceDetailDto.ProductId,
                TenantId = invoiceDetailDto.TenantId,
                Quantity = invoiceDetailDto.Quantity,
                ProductName = invoiceDetailDto.ProductName,
                UnitPrice = invoiceDetailDto.UnitPrice,
                LineSale = invoiceDetailDto.LineSale,
                LineTax = invoiceDetailDto.LineTax,
                LineDiscount = invoiceDetailDto.LineDiscount
            };
        }
    }
}
