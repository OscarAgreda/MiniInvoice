using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Country;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CountryEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteCountryRequest>.WithActionResult<
        DeleteCountryResponse>
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Country> _countryReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Country> _repository;
        private readonly IRepository<State> _stateRepository;

        public Delete(IRepository<Country> CountryRepository, IRepository<Country> CountryReadRepository,
            IAppLoggerService<Delete> logger,
               IRepository<Address> addressRepository,
               IRepository<State> stateRepository,
            IMapper mapper)
        {
            _repository = CountryRepository;
            _logger = logger;
            _countryReadRepository = CountryReadRepository;
            _addressRepository = addressRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/countries/{CountryId}")]
        [SwaggerOperation(
            Summary = "Deletes an Country",
            Description = "Deletes an Country",
            OperationId = "countries.delete",
            Tags = new[] { "CountryEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCountryResponse>> HandleAsync(
            [FromRoute] DeleteCountryRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteCountryResponse(request.CorrelationId());
            var country = await _countryReadRepository.GetByIdAsync(request.CountryId, cancellationToken);
            if (country == null)
            {
                var errorMsg = $"Country with ID {request.CountryId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            var addressSpec = new GetAddressWithCountryKeySpec(country.CountryId);
            var addresses = await _addressRepository.ListAsync(addressSpec);
            await _addressRepository.DeleteRangeAsync(addresses);
            var stateSpec = new GetStateWithCountryKeySpec(country.CountryId);
            var states = await _stateRepository.ListAsync(stateSpec);
            await _stateRepository.DeleteRangeAsync(states);
            try
            {
                await _repository.DeleteAsync(country, cancellationToken);
                _logger.LogInformation($"Successfully deleted country with ID {request.CountryId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting country with ID {request.CountryId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}