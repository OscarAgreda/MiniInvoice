using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;

namespace BlazorMauiShared.Models.State
{
    public class UpdateStateRequest : BaseRequest
    {
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public Guid TenantId { get; set; }

        public static UpdateStateRequest FromDto(StateDto stateDto)
        {
            return new UpdateStateRequest
            {
                StateId = stateDto.StateId,
                CountryId = stateDto.CountryId,
                StateName = stateDto.StateName,
                TenantId = stateDto.TenantId
            };
        }
    }
}