using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.City;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CityEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdCityRequest>.WithActionResult<
        GetByIdCityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<City> _repository;
        public GetById(
            IRepository<City> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/cities/{CityId}")]
        [SwaggerOperation(
            Summary = "Get a City by Id",
            Description = "Gets a City by Id",
            OperationId = "cities.GetById",
            Tags = new[] { "CityEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCityResponse>> HandleAsync(
            [FromRoute] GetByIdCityRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCityResponse(request.CorrelationId());
            try
            {
                var city = await _repository.GetByIdAsync(request.CityId, cancellationToken);
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
                _logger.LogInformation($"Successfully fetched city with ID {request.CityId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching city with ID {request.CityId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
