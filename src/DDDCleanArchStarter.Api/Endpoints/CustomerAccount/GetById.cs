using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Newtonsoft.Json;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAccount;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CustomerAccountEndpoints
{
    public class GetById : EndpointBaseAsync.WithRequest<GetByIdCustomerAccountRequest>.WithActionResult<
        GetByIdCustomerAccountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppLoggerService<GetById> _logger;
        private readonly IRepository<CustomerAccount> _repository;
        public GetById(
            IRepository<CustomerAccount> repository,
            IAppLoggerService<GetById> logger,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("api/customerAccounts/{RowId}")]
        [SwaggerOperation(
            Summary = "Get a CustomerAccount by Id",
            Description = "Gets a CustomerAccount by Id",
            OperationId = "customerAccounts.GetById",
            Tags = new[] { "CustomerAccountEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerAccountResponse>> HandleAsync(
            [FromRoute] GetByIdCustomerAccountRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCustomerAccountResponse(request.CorrelationId());
            try
            {
                var customerAccount = await _repository.GetByIdAsync(request.RowId, cancellationToken);
                if (customerAccount is null)
                {
                    var errorMsg = $"CustomerAccount with ID {request.RowId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
                }
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomerAccount = JsonConvert.SerializeObject(customerAccount, settings);
                CustomerAccountDto finalCustomerAccountDto = JsonConvert.DeserializeObject<CustomerAccountDto>(jsonCustomerAccount, settings);
                response.CustomerAccount = finalCustomerAccountDto;
                _logger.LogInformation($"Successfully fetched customerAccount with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while fetching customerAccount with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
