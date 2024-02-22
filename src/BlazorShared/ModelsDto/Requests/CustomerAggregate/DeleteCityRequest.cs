using System;
using BlazorShared.Models;
namespace BlazorMauiShared.Models.City
{
    public class DeleteCityRequest : BaseRequest
    {
        public Guid CityId { get; set; }
    }
}
