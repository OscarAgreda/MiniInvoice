using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.Country
{
    public class GetByIdCountryRequest : BaseRequest
    {
        public Guid CountryId { get; set; }
    }
}