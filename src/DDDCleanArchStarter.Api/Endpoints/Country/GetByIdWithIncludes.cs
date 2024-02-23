using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Country;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CountryEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdCountryRequest>.WithActionResult<
        GetByIdCountryResponse>
    {
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Country> _repository;

        public GetByIdWithIncludes(
            IRepository<Country> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("api/countries/i/{CountryId}")]
        [SwaggerOperation(
            Summary = "Get a Country by Id With Includes",
            Description = "Gets a Country by Id With Includes",
            OperationId = "countries.GetByIdWithIncludes",
            Tags = new[] { "CountryEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCountryResponse>> HandleAsync(
            [FromRoute] GetByIdCountryRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCountryResponse(request.CorrelationId());
            try
            {
                var spec = new CountryByIdWithIncludesSpec(request.CountryId);
                var country = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching country with ID {request.CountryId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}