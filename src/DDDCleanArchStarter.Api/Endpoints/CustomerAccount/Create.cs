using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAccount;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerAccountEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateCustomerAccountRequest>.WithActionResult<
        CreateCustomerAccountResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerAccount> _repository;

        public Create(
            IRepository<CustomerAccount> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("api/customerAccounts")]
        [SwaggerOperation(
            Summary = "Creates a new CustomerAccount",
            Description = "Creates a new CustomerAccount",
            OperationId = "customerAccounts.create",
            Tags = new[] { "CustomerAccountEndpoints" })
        ]
        public override async Task<ActionResult<CreateCustomerAccountResponse>> HandleAsync(
            CreateCustomerAccountRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateCustomerAccountResponse(request.CorrelationId());
            var newCustomerAccount = new CustomerAccount(
            request.AccountId,
            request.CustomerId
            );
            try
            {
                await _repository.AddAsync(newCustomerAccount, cancellationToken);
                var dto = _mapper.Map<CustomerAccountDto>(newCustomerAccount);
                response.CustomerAccount = dto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while creating customerAccount with Id {newCustomerAccount.RowId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}