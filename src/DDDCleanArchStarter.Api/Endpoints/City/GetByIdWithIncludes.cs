using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.City;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CityEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdCityRequest>.WithActionResult<
        GetByIdCityResponse>
    {
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<City> _repository;

        public GetByIdWithIncludes(
            IRepository<City> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("api/cities/i/{CityId}")]
        [SwaggerOperation(
            Summary = "Get a City by Id With Includes",
            Description = "Gets a City by Id With Includes",
            OperationId = "cities.GetByIdWithIncludes",
            Tags = new[] { "CityEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCityResponse>> HandleAsync(
            [FromRoute] GetByIdCityRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCityResponse(request.CorrelationId());
            try
            {
                var spec = new CityByIdWithIncludesSpec(request.CityId);
                var city = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
                if (city is null)
                {
                    var errorMsg = $"City with ID {request.CityId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCity = JsonConvert.SerializeObject(city, settings);
                CityDto finalCityDto = JsonConvert.DeserializeObject<CityDto>(jsonCity, settings);
                response.City = finalCityDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching city with ID {request.CityId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}