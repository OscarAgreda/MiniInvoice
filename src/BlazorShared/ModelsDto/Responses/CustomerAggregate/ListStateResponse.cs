using System;
using System.Collections.Generic;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.State
{
    public class ListStateResponse : BaseResponse
    {
        public ListStateResponse(Guid correlationId)
            : base(correlationId)
        {
        }

        public ListStateResponse()
        {
        }

        public int Count { get; set; }
        public List<StateDto> States { get; set; } = new();
    }
}