using System;
using System.Linq;
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
    public class Delete : EndpointBaseAsync.WithRequest<DeleteStateRequest>.WithActionResult<
        DeleteStateResponse>
    {
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IRepository<State> _stateReadRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<State> _repository;
        public Delete(IRepository<State> StateRepository, IRepository<State> StateReadRepository,
            IAppLoggerService<Delete> logger,
		       IRepository<Address> addressRepository,
		       IRepository<City> cityRepository,
            IMapper mapper)
        {
            _repository = StateRepository;
            _logger = logger;
            _stateReadRepository = StateReadRepository;
			    _addressRepository = addressRepository;
			    _cityRepository = cityRepository;
            _mapper = mapper;
        }
        [HttpDelete("api/states/{StateId}")]
        [SwaggerOperation(
            Summary = "Deletes an State",
            Description = "Deletes an State",
            OperationId = "states.delete",
            Tags = new[] { "StateEndpoints" })
        ]
        public override async Task<ActionResult<DeleteStateResponse>> HandleAsync(
            [FromRoute] DeleteStateRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteStateResponse(request.CorrelationId());
            var state = await _stateReadRepository.GetByIdAsync(request.StateId, cancellationToken);
            if (state == null)
            {
                    var errorMsg = $"State with ID {request.StateId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
            }
            var addressSpec = new GetAddressWithStateKeySpec(state.StateId);
            var addresses = await _addressRepository.ListAsync(addressSpec);
            await _addressRepository.DeleteRangeAsync(addresses);
            var citySpec = new GetCityWithStateKeySpec(state.StateId);
            var cities = await _cityRepository.ListAsync(citySpec);
            await _cityRepository.DeleteRangeAsync(cities);
            try
            {
                await _repository.DeleteAsync(state, cancellationToken);
                _logger.LogInformation($"Successfully deleted state with ID {request.StateId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting state with ID {request.StateId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
