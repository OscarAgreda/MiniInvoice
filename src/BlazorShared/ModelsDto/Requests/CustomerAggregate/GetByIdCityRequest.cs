using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.City
{
    public class GetByIdCityRequest : BaseRequest
    {
        public Guid CityId { get; set; }
    }
}
