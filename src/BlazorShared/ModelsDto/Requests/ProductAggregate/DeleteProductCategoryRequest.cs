using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.ProductCategory
{
    public class DeleteProductCategoryRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}