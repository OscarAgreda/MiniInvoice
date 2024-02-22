using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.AccountAdjustment;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.AccountAdjustmentEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateAccountAdjustmentRequest>
    .WithActionResult<UpdateAccountAdjustmentResponse>
  {
    private readonly IRepository<AccountAdjustment> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<AccountAdjustment> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/accountAdjustments")]
    [SwaggerOperation(
        Summary = "Updates a AccountAdjustment",
        Description = "Updates a AccountAdjustment",
        OperationId = "accountAdjustments.update",
        Tags = new[] { "AccountAdjustmentEndpoints" })
    ]
    public override async Task<ActionResult<UpdateAccountAdjustmentResponse>> HandleAsync(UpdateAccountAdjustmentRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateAccountAdjustmentResponse(request.CorrelationId());
            try
            {
                var aacacaToUpdate = _mapper.Map<AccountAdjustment>(request);
      await _repository.UpdateAsync(aacacaToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonAccountAdjustment = JsonConvert.SerializeObject(aacacaToUpdate, settings);
                AccountAdjustmentDto finalAccountAdjustmentDto = JsonConvert.DeserializeObject<AccountAdjustmentDto>(jsonAccountAdjustment, settings);
                response.AccountAdjustment = finalAccountAdjustmentDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating accountAdjustment with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
