using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CountryEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateCountryRequest>.WithActionResult<
        CreateCountryResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Country> _repository;
        public Create(
            IRepository<Country> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/countries")]
        [SwaggerOperation(
            Summary = "Creates a new Country",
            Description = "Creates a new Country",
            OperationId = "countries.create",
            Tags = new[] { "CountryEndpoints" })
        ]
        public override async Task<ActionResult<CreateCountryResponse>> HandleAsync(
            CreateCountryRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateCountryResponse(request.CorrelationId());
            var newCountry = new Country(
            countryId:Guid.NewGuid(),
            request.CountryName,
            request.CountryCode,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newCountry, cancellationToken);
                var dto = _mapper.Map<CountryDto>(newCountry);
                response.Country = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating country with Id {newCountry.CountryId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
