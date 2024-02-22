using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.City;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.CityEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateCityRequest>
    .WithActionResult<UpdateCityResponse>
  {
    private readonly IRepository<City> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<City> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/cities")]
    [SwaggerOperation(
        Summary = "Updates a City",
        Description = "Updates a City",
        OperationId = "cities.update",
        Tags = new[] { "CityEndpoints" })
    ]
    public override async Task<ActionResult<UpdateCityResponse>> HandleAsync(UpdateCityRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateCityResponse(request.CorrelationId());
            try
            {
                var citToUpdate = _mapper.Map<City>(request);
      await _repository.UpdateAsync(citToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCity = JsonConvert.SerializeObject(citToUpdate, settings);
                CityDto finalCityDto = JsonConvert.DeserializeObject<CityDto>(jsonCity, settings);
                response.City = finalCityDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating city with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
