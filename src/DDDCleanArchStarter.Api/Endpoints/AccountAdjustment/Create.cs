using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AccountAdjustment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AccountAdjustmentEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateAccountAdjustmentRequest>.WithActionResult<
        CreateAccountAdjustmentResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<AccountAdjustment> _repository;
        public Create(
            IRepository<AccountAdjustment> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/accountAdjustments")]
        [SwaggerOperation(
            Summary = "Creates a new AccountAdjustment",
            Description = "Creates a new AccountAdjustment",
            OperationId = "accountAdjustments.create",
            Tags = new[] { "AccountAdjustmentEndpoints" })
        ]
        public override async Task<ActionResult<CreateAccountAdjustmentResponse>> HandleAsync(
            CreateAccountAdjustmentRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateAccountAdjustmentResponse(request.CorrelationId());
            var newAccountAdjustment = new AccountAdjustment(
            accountAdjustmentId:Guid.NewGuid(),
            request.AccountId,
            request.AdjustmentReason,
            request.TotalChargeCredited,
            request.TotalTaxCredited,
            request.IsDeleted
            );
            try
            {
                await _repository.AddAsync(newAccountAdjustment, cancellationToken);
                var dto = _mapper.Map<AccountAdjustmentDto>(newAccountAdjustment);
                response.AccountAdjustment = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating accountAdjustment with Id {newAccountAdjustment.AccountAdjustmentId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
