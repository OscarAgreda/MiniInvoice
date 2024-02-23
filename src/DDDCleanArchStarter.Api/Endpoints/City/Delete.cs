using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.City;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CityEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteCityRequest>.WithActionResult<
        DeleteCityResponse>
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<City> _cityReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<City> _repository;

        public Delete(IRepository<City> CityRepository, IRepository<City> CityReadRepository,
            IAppLoggerService<Delete> logger,
               IRepository<Address> addressRepository,
            IMapper mapper)
        {
            _repository = CityRepository;
            _logger = logger;
            _cityReadRepository = CityReadRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/cities/{CityId}")]
        [SwaggerOperation(
            Summary = "Deletes an City",
            Description = "Deletes an City",
            OperationId = "cities.delete",
            Tags = new[] { "CityEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCityResponse>> HandleAsync(
            [FromRoute] DeleteCityRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteCityResponse(request.CorrelationId());
            var city = await _cityReadRepository.GetByIdAsync(request.CityId, cancellationToken);
            if (city == null)
            {
                var errorMsg = $"City with ID {request.CityId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            var addressSpec = new GetAddressWithCityKeySpec(city.CityId);
            var addresses = await _addressRepository.ListAsync(addressSpec);
            await _addressRepository.DeleteRangeAsync(addresses);
            try
            {
                await _repository.DeleteAsync(city, cancellationToken);
                _logger.LogInformation($"Successfully deleted city with ID {request.CityId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting city with ID {request.CityId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}