using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.State
{
    public class CreateStateResponse : BaseResponse
    {
        public CreateStateResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public CreateStateResponse()
        {
        }
        public StateDto State { get; set; } = new();
    }
}
