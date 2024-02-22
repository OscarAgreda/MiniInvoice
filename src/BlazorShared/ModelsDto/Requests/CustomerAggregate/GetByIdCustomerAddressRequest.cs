using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.CustomerAddress
{
    public class GetByIdCustomerAddressRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}
