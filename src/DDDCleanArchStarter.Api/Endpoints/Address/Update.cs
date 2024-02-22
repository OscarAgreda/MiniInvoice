using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Address;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.AddressEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateAddressRequest>
    .WithActionResult<UpdateAddressResponse>
  {
    private readonly IRepository<Address> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<Address> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/addresses")]
    [SwaggerOperation(
        Summary = "Updates a Address",
        Description = "Updates a Address",
        OperationId = "addresses.update",
        Tags = new[] { "AddressEndpoints" })
    ]
    public override async Task<ActionResult<UpdateAddressResponse>> HandleAsync(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateAddressResponse(request.CorrelationId());
            try
            {
                var addToUpdate = _mapper.Map<Address>(request);
      await _repository.UpdateAsync(addToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAddress = JsonConvert.SerializeObject(addToUpdate, settings);
                AddressDto finalAddressDto = JsonConvert.DeserializeObject<AddressDto>(jsonAddress, settings);
                response.Address = finalAddressDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating address with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
