using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.State
{
    public class GetByIdStateResponse : BaseResponse
    {
        public GetByIdStateResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public GetByIdStateResponse()
        {
        }

        public StateDto State { get; set; } = new();
    }
}