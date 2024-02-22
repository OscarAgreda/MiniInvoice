using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AccountAdjustment;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.AccountAdjustmentEndpoints
{
    public class GetByIdWithIncludes : EndpointBaseAsync.WithRequest<GetByIdAccountAdjustmentRequest>.WithActionResult<
        GetByIdAccountAdjustmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetByIdWithIncludes> _logger;
        private readonly IRepository<AccountAdjustment> _repository;
        public GetByIdWithIncludes(
            IRepository<AccountAdjustment> repository,
            IAppLoggerService<GetByIdWithIncludes> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("api/accountAdjustments/i/{AccountAdjustmentId}")]
        [SwaggerOperation(
            Summary = "Get a AccountAdjustment by Id With Includes",
            Description = "Gets a AccountAdjustment by Id With Includes",
            OperationId = "accountAdjustments.GetByIdWithIncludes",
            Tags = new[] { "AccountAdjustmentEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdAccountAdjustmentResponse>> HandleAsync(
            [FromRoute] GetByIdAccountAdjustmentRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdAccountAdjustmentResponse(request.CorrelationId());
            try
            {
                var spec = new AccountAdjustmentByIdWithIncludesSpec(request.AccountAdjustmentId);
                var accountAdjustment = await _repository.FirstOrDefaultAsync(spec,cancellationToken);
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
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching accountAdjustment with ID {request.AccountAdjustmentId} with includes.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
