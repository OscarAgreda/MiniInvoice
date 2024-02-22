using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.ProductCategory
{
    public class GetByIdProductCategoryRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}
