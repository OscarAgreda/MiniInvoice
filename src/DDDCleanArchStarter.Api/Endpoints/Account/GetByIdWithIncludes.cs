using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Account;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AccountEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdAccountRequest>.WithActionResult<
        GetByIdAccountResponse>
    {
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Account> _repository;

        public GetByIdWithIncludes(
            IRepository<Account> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("api/accounts/i/{AccountId}")]
        [SwaggerOperation(
            Summary = "Get a Account by Id With Includes",
            Description = "Gets a Account by Id With Includes",
            OperationId = "accounts.GetByIdWithIncludes",
            Tags = new[] { "AccountEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdAccountResponse>> HandleAsync(
            [FromRoute] GetByIdAccountRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdAccountResponse(request.CorrelationId());
            try
            {
                var spec = new AccountByIdWithIncludesSpec(request.AccountId);
                var account = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching account with ID {request.AccountId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}