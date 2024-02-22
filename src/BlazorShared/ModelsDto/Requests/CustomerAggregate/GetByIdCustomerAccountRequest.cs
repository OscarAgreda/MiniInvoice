using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.CustomerAccount
{
    public class GetByIdCustomerAccountRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}
