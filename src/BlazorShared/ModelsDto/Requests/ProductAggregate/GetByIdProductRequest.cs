using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Product
{
    public class GetByIdProductRequest : BaseRequest
    {
        public Guid ProductId { get; set; }
    }
}