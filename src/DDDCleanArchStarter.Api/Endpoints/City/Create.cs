using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.City;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CityEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateCityRequest>.WithActionResult<
        CreateCityResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<City> _repository;
        public Create(
            IRepository<City> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/cities")]
        [SwaggerOperation(
            Summary = "Creates a new City",
            Description = "Creates a new City",
            OperationId = "cities.create",
            Tags = new[] { "CityEndpoints" })
        ]
        public override async Task<ActionResult<CreateCityResponse>> HandleAsync(
            CreateCityRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateCityResponse(request.CorrelationId());
            var newCity = new City(
            cityId:Guid.NewGuid(),
            request.StateId,
            request.CityName,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newCity, cancellationToken);
                var dto = _mapper.Map<CityDto>(newCity);
                response.City = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating city with Id {newCity.CityId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
