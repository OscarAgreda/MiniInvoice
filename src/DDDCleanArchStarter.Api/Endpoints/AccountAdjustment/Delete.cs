using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AccountAdjustment;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AccountAdjustmentEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteAccountAdjustmentRequest>.WithActionResult<
        DeleteAccountAdjustmentResponse>
    {
        private readonly IRepository<AccountAdjustment> _accountAdjustmentReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<AccountAdjustment> _repository;

        public Delete(IRepository<AccountAdjustment> AccountAdjustmentRepository, IRepository<AccountAdjustment> AccountAdjustmentReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = AccountAdjustmentRepository;
            _logger = logger;
            _accountAdjustmentReadRepository = AccountAdjustmentReadRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/accountAdjustments/{AccountAdjustmentId}")]
        [SwaggerOperation(
            Summary = "Deletes an AccountAdjustment",
            Description = "Deletes an AccountAdjustment",
            OperationId = "accountAdjustments.delete",
            Tags = new[] { "AccountAdjustmentEndpoints" })
        ]
        public override async Task<ActionResult<DeleteAccountAdjustmentResponse>> HandleAsync(
            [FromRoute] DeleteAccountAdjustmentRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteAccountAdjustmentResponse(request.CorrelationId());
            var accountAdjustment = await _accountAdjustmentReadRepository.GetByIdAsync(request.AccountAdjustmentId, cancellationToken);
            if (accountAdjustment == null)
            {
                var errorMsg = $"AccountAdjustment with ID {request.AccountAdjustmentId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(accountAdjustment, cancellationToken);
                _logger.LogInformation($"Successfully deleted accountAdjustment with ID {request.AccountAdjustmentId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting accountAdjustment with ID {request.AccountAdjustmentId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}