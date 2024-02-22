using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.CustomerPhoneNumber
{
    public class GetByIdCustomerPhoneNumberRequest : BaseRequest
    {
        public int RowId { get; set; }
    }
}
