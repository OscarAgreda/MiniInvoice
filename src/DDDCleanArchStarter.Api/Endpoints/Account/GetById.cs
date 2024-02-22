using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AccountEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdAccountRequest>.WithActionResult<
        GetByIdAccountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<Account> _repository;
        public GetById(
            IRepository<Account> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/accounts/{AccountId}")]
        [SwaggerOperation(
            Summary = "Get a Account by Id",
            Description = "Gets a Account by Id",
            OperationId = "accounts.GetById",
            Tags = new[] { "AccountEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdAccountResponse>> HandleAsync(
            [FromRoute] GetByIdAccountRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdAccountResponse(request.CorrelationId());
            try
            {
                var account = await _repository.GetByIdAsync(request.AccountId, cancellationToken);
                if (account is null)
                {
                    var errorMsg = $"Account with ID {request.AccountId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAccount = JsonConvert.SerializeObject(account, settings);
                AccountDto finalAccountDto = JsonConvert.DeserializeObject<AccountDto>(jsonAccount, settings);
                response.Account = finalAccountDto;
                _logger.LogInformation($"Successfully fetched account with ID {request.AccountId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching account with ID {request.AccountId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
