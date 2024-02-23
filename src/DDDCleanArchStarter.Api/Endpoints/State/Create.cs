using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.State;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.StateEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateStateRequest>.WithActionResult<
        CreateStateResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<State> _repository;

        public Create(
            IRepository<State> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("api/states")]
        [SwaggerOperation(
            Summary = "Creates a new State",
            Description = "Creates a new State",
            OperationId = "states.create",
            Tags = new[] { "StateEndpoints" })
        ]
        public override async Task<ActionResult<CreateStateResponse>> HandleAsync(
            CreateStateRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateStateResponse(request.CorrelationId());
            var newState = new State(
            stateId:Guid.NewGuid(),
            request.CountryId,
            request.StateName,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newState, cancellationToken);
                var dto = _mapper.Map<StateDto>(newState);
                response.State = dto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while creating state with Id {newState.StateId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}