using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Country
{
    public class DeleteCountryResponse : BaseResponse
    {
        public DeleteCountryResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteCountryResponse()
        {
        }
    }
}
