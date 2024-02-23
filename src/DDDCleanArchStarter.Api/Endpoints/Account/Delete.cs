using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Account;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AccountEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteAccountRequest>.WithActionResult<
        DeleteAccountResponse>
    {
        private readonly IRepository<AccountAdjustment> _accountAdjustmentRepository;
        private readonly IRepository<Account> _accountReadRepository;
        private readonly IRepository<CustomerAccount> _customerAccountRepository;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Account> _repository;

        public Delete(IRepository<Account> AccountRepository, IRepository<Account> AccountReadRepository,
            IAppLoggerService<Delete> logger,
               IRepository<AccountAdjustment> accountAdjustmentRepository,
               IRepository<CustomerAccount> customerAccountRepository,
               IRepository<Invoice> invoiceRepository,
            IMapper mapper)
        {
            _repository = AccountRepository;
            _logger = logger;
            _accountReadRepository = AccountReadRepository;
            _accountAdjustmentRepository = accountAdjustmentRepository;
            _customerAccountRepository = customerAccountRepository;
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/accounts/{AccountId}")]
        [SwaggerOperation(
            Summary = "Deletes an Account",
            Description = "Deletes an Account",
            OperationId = "accounts.delete",
            Tags = new[] { "AccountEndpoints" })
        ]
        public override async Task<ActionResult<DeleteAccountResponse>> HandleAsync(
            [FromRoute] DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteAccountResponse(request.CorrelationId());
            var account = await _accountReadRepository.GetByIdAsync(request.AccountId, cancellationToken);
            if (account == null)
            {
                var errorMsg = $"Account with ID {request.AccountId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            var accountAdjustmentSpec = new GetAccountAdjustmentWithAccountKeySpec(account.AccountId);
            var accountAdjustments = await _accountAdjustmentRepository.ListAsync(accountAdjustmentSpec);
            await _accountAdjustmentRepository.DeleteRangeAsync(accountAdjustments);
            var customerAccountSpec = new GetCustomerAccountWithAccountKeySpec(account.AccountId);
            var customerAccounts = await _customerAccountRepository.ListAsync(customerAccountSpec);
            await _customerAccountRepository.DeleteRangeAsync(customerAccounts);
            var invoiceSpec = new GetInvoiceWithAccountKeySpec(account.AccountId);
            var invoices = await _invoiceRepository.ListAsync(invoiceSpec);
            await _invoiceRepository.DeleteRangeAsync(invoices);
            try
            {
                await _repository.DeleteAsync(account, cancellationToken);
                _logger.LogInformation($"Successfully deleted account with ID {request.AccountId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting account with ID {request.AccountId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}