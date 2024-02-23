using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class DeleteCustomerPhoneNumberRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}