using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.Product
{
    public class DeleteProductRequest : BaseRequest
    {
        public Guid ProductId { get; set; }
    }
}
