using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.State
{
    public class DeleteStateResponse : BaseResponse
    {
        public DeleteStateResponse(Guid correlationId)
            : base(correlationId)
        {
        }
        public DeleteStateResponse()
        {
        }
    }
}
