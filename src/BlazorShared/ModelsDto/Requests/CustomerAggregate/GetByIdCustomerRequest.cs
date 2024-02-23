using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Customer
{
    public class GetByIdCustomerRequest : BaseRequest
    {
        public Guid CustomerId { get; set; }
    }
}