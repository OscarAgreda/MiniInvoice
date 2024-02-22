using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.Account
{
    public class GetByIdAccountRequest : BaseRequest
    {
        public Guid AccountId { get; set; }
    }
}
