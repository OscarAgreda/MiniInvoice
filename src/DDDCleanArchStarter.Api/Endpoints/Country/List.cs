using System;
using System.Collections.Generic;
using System.Linq;
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
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CountryEndpoints
{
    public class List : EndpointBaseAsync
      .WithRequest<ListCountryRequest>
      .WithActionResult<ListCountryResponse>
    {
        private readonly IAppLoggerService<List> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Country> _repository;

        public List(IRepository<Country> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/countries")]
        [SwaggerOperation(
            Summary = "List Countries",
            Description = "List Countries",
            OperationId = "countries.List",
            Tags = new[] { "CountryEndpoints" })
        ]
        public override async Task<ActionResult<ListCountryResponse>> HandleAsync([FromQuery] ListCountryRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListCountryResponse(request.CorrelationId());
            try
            {
                var spec = new CountryGetListSpec();
                var countries = await _repository.ListAsync(spec, cancellationToken);
                if (countries == null || !countries.Any())
                {
                    _logger.LogWarning("No countries found.");
                    return NotFound();
                }
                response.Countries = _mapper.Map<List<CountryDto>>(countries);
                response.Count = response.Countries.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching country list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}