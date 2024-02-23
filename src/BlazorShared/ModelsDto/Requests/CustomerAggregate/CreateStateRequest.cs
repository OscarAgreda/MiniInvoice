using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.State
{
    public class CreateStateRequest : BaseRequest
    {
        public Guid CountryId { get; set; }
        public string StateName { get; set; }
        public Guid TenantId { get; set; }
    }
}