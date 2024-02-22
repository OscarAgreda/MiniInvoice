using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.City;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CityEndpoints
{
  public class List : EndpointBaseAsync
    .WithRequest<ListCityRequest>
    .WithActionResult<ListCityResponse>
  {
    private readonly IRepository<City> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<City> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/cities")]
    [SwaggerOperation(
        Summary = "List Cities",
        Description = "List Cities",
        OperationId = "cities.List",
        Tags = new[] { "CityEndpoints" })
    ]
    public override async Task<ActionResult<ListCityResponse>> HandleAsync([FromQuery] ListCityRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListCityResponse(request.CorrelationId());
            try
            {
                var spec = new CityGetListSpec();
                var cities = await _repository.ListAsync(spec, cancellationToken);
                if (cities == null || !cities.Any()) 
                {
                    _logger.LogWarning("No cities found.");
                    return NotFound();
                }
                response.Cities = _mapper.Map<List<CityDto>>(cities);
                response.Count = response.Cities.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching city list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
