using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAccount;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.CustomerAccountEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateCustomerAccountRequest>
    .WithActionResult<UpdateCustomerAccountResponse>
  {
    private readonly IRepository<CustomerAccount> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<CustomerAccount> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/customerAccounts")]
    [SwaggerOperation(
        Summary = "Updates a CustomerAccount",
        Description = "Updates a CustomerAccount",
        OperationId = "customerAccounts.update",
        Tags = new[] { "CustomerAccountEndpoints" })
    ]
    public override async Task<ActionResult<UpdateCustomerAccountResponse>> HandleAsync(UpdateCustomerAccountRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateCustomerAccountResponse(request.CorrelationId());
            try
            {
                var cauasaToUpdate = _mapper.Map<CustomerAccount>(request);
      await _repository.UpdateAsync(cauasaToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomerAccount = JsonConvert.SerializeObject(cauasaToUpdate, settings);
                CustomerAccountDto finalCustomerAccountDto = JsonConvert.DeserializeObject<CustomerAccountDto>(jsonCustomerAccount, settings);
                response.CustomerAccount = finalCustomerAccountDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating customerAccount with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
