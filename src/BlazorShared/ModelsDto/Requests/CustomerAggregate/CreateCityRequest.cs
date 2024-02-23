using System;
using BlazorShared.Models;

namespace BlazorMauiShared.Models.City
{
    public class CreateCityRequest : BaseRequest
    {
        public string CityName { get; set; }
        public Guid StateId { get; set; }
        public Guid TenantId { get; set; }
    }
}