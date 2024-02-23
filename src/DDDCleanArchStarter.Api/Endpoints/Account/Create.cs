using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Account;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AccountEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateAccountRequest>.WithActionResult<
        CreateAccountResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Account> _repository;

        public Create(
            IRepository<Account> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("api/accounts")]
        [SwaggerOperation(
            Summary = "Creates a new Account",
            Description = "Creates a new Account",
            OperationId = "accounts.create",
            Tags = new[] { "AccountEndpoints" })
        ]
        public override async Task<ActionResult<CreateAccountResponse>> HandleAsync(
            CreateAccountRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateAccountResponse(request.CorrelationId());
            var newAccount = new Account(
            accountId:Guid.NewGuid(),
            request.AccountNumber,
            request.AccountName,
            request.AccountDescription,
            request.IsDeleted,
            request.TenantId,
            request.AccountTypeId
            );
            try
            {
                await _repository.AddAsync(newAccount, cancellationToken);
                var dto = _mapper.Map<AccountDto>(newAccount);
                response.Account = dto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while creating account with Id {newAccount.AccountId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}