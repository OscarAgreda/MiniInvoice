using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.CustomerAddress
{
    public class DeleteCustomerAddressRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}