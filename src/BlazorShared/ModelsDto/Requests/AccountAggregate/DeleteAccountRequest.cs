using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Account
{
    public class DeleteAccountRequest : BaseRequest
    {
        public Guid AccountId { get; set; }
    }
}