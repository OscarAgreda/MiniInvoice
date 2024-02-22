using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.City
{
    public class DeleteCityResponse : BaseResponse
    {
        public DeleteCityResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteCityResponse()
        {
        }
    }
}
