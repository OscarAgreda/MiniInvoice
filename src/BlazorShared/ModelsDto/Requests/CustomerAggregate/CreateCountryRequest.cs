using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.Country
{
    public class CreateCountryRequest : BaseRequest
    {
      public string CountryName { get; set; }
      public string CountryCode { get; set; }
      public Guid TenantId { get; set; }
    }
}
