using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAccount;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerAccountEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteCustomerAccountRequest>.WithActionResult<
        DeleteCustomerAccountResponse>
    {
        private readonly IRepository<CustomerAccount> _customerAccountReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerAccount> _repository;

        public Delete(IRepository<CustomerAccount> CustomerAccountRepository, IRepository<CustomerAccount> CustomerAccountReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = CustomerAccountRepository;
            _logger = logger;
            _customerAccountReadRepository = CustomerAccountReadRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/customerAccounts/{RowId}")]
        [SwaggerOperation(
            Summary = "Deletes an CustomerAccount",
            Description = "Deletes an CustomerAccount",
            OperationId = "customerAccounts.delete",
            Tags = new[] { "CustomerAccountEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCustomerAccountResponse>> HandleAsync(
            [FromRoute] DeleteCustomerAccountRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteCustomerAccountResponse(request.CorrelationId());
            var customerAccount = await _customerAccountReadRepository.GetByIdAsync(request.RowId, cancellationToken);
            if (customerAccount == null)
            {
                var errorMsg = $"CustomerAccount with ID {request.RowId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(customerAccount, cancellationToken);
                _logger.LogInformation($"Successfully deleted customerAccount with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting customerAccount with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}