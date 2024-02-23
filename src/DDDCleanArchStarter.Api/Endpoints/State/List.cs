using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.State;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.StateEndpoints
{
    public class List : EndpointBaseAsync
      .WithRequest<ListStateRequest>
      .WithActionResult<ListStateResponse>
    {
        private readonly IAppLoggerService<List> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<State> _repository;

        public List(IRepository<State> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/states")]
        [SwaggerOperation(
            Summary = "List States",
            Description = "List States",
            OperationId = "states.List",
            Tags = new[] { "StateEndpoints" })
        ]
        public override async Task<ActionResult<ListStateResponse>> HandleAsync([FromQuery] ListStateRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListStateResponse(request.CorrelationId());
            try
            {
                var spec = new StateGetListSpec();
                var states = await _repository.ListAsync(spec, cancellationToken);
                if (states == null || !states.Any())
                {
                    _logger.LogWarning("No states found.");
                    return NotFound();
                }
                response.States = _mapper.Map<List<StateDto>>(states);
                response.Count = response.States.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching state list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}