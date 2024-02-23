using System;
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
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.StateEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateStateRequest>
      .WithActionResult<UpdateStateResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<State> _repository;

        public Update(
            IRepository<State> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/states")]
        [SwaggerOperation(
            Summary = "Updates a State",
            Description = "Updates a State",
            OperationId = "states.update",
            Tags = new[] { "StateEndpoints" })
        ]
        public override async Task<ActionResult<UpdateStateResponse>> HandleAsync(UpdateStateRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateStateResponse(request.CorrelationId());
            try
            {
                var staToUpdate = _mapper.Map<State>(request);
                await _repository.UpdateAsync(staToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonState = JsonConvert.SerializeObject(staToUpdate, settings);
                StateDto finalStateDto = JsonConvert.DeserializeObject<StateDto>(jsonState, settings);
                response.State = finalStateDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating state with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}