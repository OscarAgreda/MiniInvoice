using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.State;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.StateEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdStateRequest>.WithActionResult<
        GetByIdStateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IRepository<State> _repository;
        public GetByIdWithIncludes(
            IRepository<State> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("api/states/i/{StateId}")]
        [SwaggerOperation(
            Summary = "Get a State by Id With Includes",
            Description = "Gets a State by Id With Includes",
            OperationId = "states.GetByIdWithIncludes",
            Tags = new[] { "StateEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdStateResponse>> HandleAsync(
            [FromRoute] GetByIdStateRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdStateResponse(request.CorrelationId());
            try
            {
                var spec = new StateByIdWithIncludesSpec(request.StateId);
                var state = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching state with ID {request.StateId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
