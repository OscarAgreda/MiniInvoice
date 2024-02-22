using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.AccountEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateAccountRequest>
    .WithActionResult<UpdateAccountResponse>
  {
    private readonly IRepository<Account> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<Account> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/accounts")]
    [SwaggerOperation(
        Summary = "Updates a Account",
        Description = "Updates a Account",
        OperationId = "accounts.update",
        Tags = new[] { "AccountEndpoints" })
    ]
    public override async Task<ActionResult<UpdateAccountResponse>> HandleAsync(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateAccountResponse(request.CorrelationId());
            try
            {
                var accToUpdate = _mapper.Map<Account>(request);
      await _repository.UpdateAsync(accToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAccount = JsonConvert.SerializeObject(accToUpdate, settings);
                AccountDto finalAccountDto = JsonConvert.DeserializeObject<AccountDto>(jsonAccount, settings);
                response.Account = finalAccountDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating account with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
