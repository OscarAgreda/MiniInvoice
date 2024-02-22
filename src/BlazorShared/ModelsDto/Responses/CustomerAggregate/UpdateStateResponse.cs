using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.State
{
    public class UpdateStateResponse : BaseResponse
    {
        public UpdateStateResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public UpdateStateResponse()
        {
        }
        public StateDto State { get; set; } = new();
    }
}
