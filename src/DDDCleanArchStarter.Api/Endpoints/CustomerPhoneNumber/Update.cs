using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerPhoneNumber;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace BusinessManagement.Api.CustomerPhoneNumberEndpoints
{
    public class Update : EndpointBaseAsync
      .WithRequest<UpdateCustomerPhoneNumberRequest>
      .WithActionResult<UpdateCustomerPhoneNumberResponse>
    {
        private readonly IAppLoggerService<Update> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerPhoneNumber> _repository;

        public Update(
            IRepository<CustomerPhoneNumber> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("api/customerPhoneNumbers")]
        [SwaggerOperation(
            Summary = "Updates a CustomerPhoneNumber",
            Description = "Updates a CustomerPhoneNumber",
            OperationId = "customerPhoneNumbers.update",
            Tags = new[] { "CustomerPhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<UpdateCustomerPhoneNumberResponse>> HandleAsync(UpdateCustomerPhoneNumberRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateCustomerPhoneNumberResponse(request.CorrelationId());
            try
            {
                var cpnupnspnToUpdate = _mapper.Map<CustomerPhoneNumber>(request);
                await _repository.UpdateAsync(cpnupnspnToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonCustomerPhoneNumber = JsonConvert.SerializeObject(cpnupnspnToUpdate, settings);
                CustomerPhoneNumberDto finalCustomerPhoneNumberDto = JsonConvert.DeserializeObject<CustomerPhoneNumberDto>(jsonCustomerPhoneNumber, settings);
                response.CustomerPhoneNumber = finalCustomerPhoneNumberDto;
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while updating customerPhoneNumber with request data: {request}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}