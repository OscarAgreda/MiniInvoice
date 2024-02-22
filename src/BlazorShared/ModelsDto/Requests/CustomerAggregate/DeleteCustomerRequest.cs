using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.Customer
{
    public class DeleteCustomerRequest : BaseRequest
    {
        public Guid CustomerId { get; set; }
    }
}
