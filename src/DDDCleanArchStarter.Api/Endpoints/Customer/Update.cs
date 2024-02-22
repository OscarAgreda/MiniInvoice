using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.CustomerEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateCustomerRequest>
    .WithActionResult<UpdateCustomerResponse>
  {
    private readonly IRepository<Customer> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<Customer> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/customers")]
    [SwaggerOperation(
        Summary = "Updates a Customer",
        Description = "Updates a Customer",
        OperationId = "customers.update",
        Tags = new[] { "CustomerEndpoints" })
    ]
    public override async Task<ActionResult<UpdateCustomerResponse>> HandleAsync(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateCustomerResponse(request.CorrelationId());
            try
            {
                var cusToUpdate = _mapper.Map<Customer>(request);
      await _repository.UpdateAsync(cusToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomer = JsonConvert.SerializeObject(cusToUpdate, settings);
                CustomerDto finalCustomerDto = JsonConvert.DeserializeObject<CustomerDto>(jsonCustomer, settings);
                response.Customer = finalCustomerDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating customer with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
