using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Account
{
    public class CreateAccountRequest : BaseRequest
    {
        public string AccountDescription { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public Guid AccountTypeId { get; set; }
        public bool? IsDeleted { get; set; }
        public Guid TenantId { get; set; }
    }
}