using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.Country
{
    public class DeleteCountryRequest : BaseRequest
    {
        public Guid CountryId { get; set; }
    }
}
