using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.ProductCategory
{
    public class CreateProductCategoryRequest : BaseRequest
    {
        public Guid ProductId { get; set; }
        public Guid TenantId { get; set; }
    }
}