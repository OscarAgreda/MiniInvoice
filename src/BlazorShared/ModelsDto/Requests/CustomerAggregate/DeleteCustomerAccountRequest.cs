using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.CustomerAccount
{
    public class DeleteCustomerAccountRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}
