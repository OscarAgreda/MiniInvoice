using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.AccountAdjustment;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AccountAdjustmentEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdAccountAdjustmentRequest>.WithActionResult<
        GetByIdAccountAdjustmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<AccountAdjustment> _repository;
        public GetById(
            IRepository<AccountAdjustment> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/accountAdjustments/{AccountAdjustmentId}")]
        [SwaggerOperation(
            Summary = "Get a AccountAdjustment by Id",
            Description = "Gets a AccountAdjustment by Id",
            OperationId = "accountAdjustments.GetById",
            Tags = new[] { "AccountAdjustmentEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdAccountAdjustmentResponse>> HandleAsync(
            [FromRoute] GetByIdAccountAdjustmentRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdAccountAdjustmentResponse(request.CorrelationId());
            try
            {
                var accountAdjustment = await _repository.GetByIdAsync(request.AccountAdjustmentId, cancellationToken);
                if (accountAdjustment is null)
                {
                    var errorMsg = $"AccountAdjustment with ID {request.AccountAdjustmentId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAccountAdjustment = JsonConvert.SerializeObject(accountAdjustment, settings);
                AccountAdjustmentDto finalAccountAdjustmentDto = JsonConvert.DeserializeObject<AccountAdjustmentDto>(jsonAccountAdjustment, settings);
                response.AccountAdjustment = finalAccountAdjustmentDto;
                _logger.LogInformation($"Successfully fetched accountAdjustment with ID {request.AccountAdjustmentId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching accountAdjustment with ID {request.AccountAdjustmentId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
