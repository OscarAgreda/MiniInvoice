using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.ProductCategory
{
    public class GetByRelsIdsProductCategoryRequest : BaseRequest
    {
        public Guid TenantId { get; set; }
        public Guid ProductId { get; set; }
    }
}
