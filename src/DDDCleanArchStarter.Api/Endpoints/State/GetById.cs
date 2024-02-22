using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.State;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.StateEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdStateRequest>.WithActionResult<
        GetByIdStateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<State> _repository;
        public GetById(
            IRepository<State> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/states/{StateId}")]
        [SwaggerOperation(
            Summary = "Get a State by Id",
            Description = "Gets a State by Id",
            OperationId = "states.GetById",
            Tags = new[] { "StateEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdStateResponse>> HandleAsync(
            [FromRoute] GetByIdStateRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdStateResponse(request.CorrelationId());
            try
            {
                var state = await _repository.GetByIdAsync(request.StateId, cancellationToken);
                if (state is null)
                {
                    var errorMsg = $"State with ID {request.StateId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonState = JsonConvert.SerializeObject(state, settings);
                StateDto finalStateDto = JsonConvert.DeserializeObject<StateDto>(jsonState, settings);
                response.State = finalStateDto;
                _logger.LogInformation($"Successfully fetched state with ID {request.StateId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching state with ID {request.StateId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
