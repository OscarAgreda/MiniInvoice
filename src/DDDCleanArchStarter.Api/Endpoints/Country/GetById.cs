using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.Country;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CountryEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdCountryRequest>.WithActionResult<
        GetByIdCountryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<Country> _repository;
        public GetById(
            IRepository<Country> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/countries/{CountryId}")]
        [SwaggerOperation(
            Summary = "Get a Country by Id",
            Description = "Gets a Country by Id",
            OperationId = "countries.GetById",
            Tags = new[] { "CountryEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCountryResponse>> HandleAsync(
            [FromRoute] GetByIdCountryRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCountryResponse(request.CorrelationId());
            try
            {
                var country = await _repository.GetByIdAsync(request.CountryId, cancellationToken);
                if (country is null)
                {
                    var errorMsg = $"Country with ID {request.CountryId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCountry = JsonConvert.SerializeObject(country, settings);
                CountryDto finalCountryDto = JsonConvert.DeserializeObject<CountryDto>(jsonCountry, settings);
                response.Country = finalCountryDto;
                _logger.LogInformation($"Successfully fetched country with ID {request.CountryId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching country with ID {request.CountryId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
