using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Country;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.CountryEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateCountryRequest>
      .WithActionResult<UpdateCountryResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Country> _repository;

        public Update(
            IRepository<Country> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/countries")]
        [SwaggerOperation(
            Summary = "Updates a Country",
            Description = "Updates a Country",
            OperationId = "countries.update",
            Tags = new[] { "CountryEndpoints" })
        ]
        public override async Task<ActionResult<UpdateCountryResponse>> HandleAsync(UpdateCountryRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateCountryResponse(request.CorrelationId());
            try
            {
                var couToUpdate = _mapper.Map<Country>(request);
                await _repository.UpdateAsync(couToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCountry = JsonConvert.SerializeObject(couToUpdate, settings);
                CountryDto finalCountryDto = JsonConvert.DeserializeObject<CountryDto>(jsonCountry, settings);
                response.Country = finalCountryDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating country with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}