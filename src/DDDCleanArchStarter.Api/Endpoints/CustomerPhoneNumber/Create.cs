using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerPhoneNumber;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CustomerPhoneNumberEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateCustomerPhoneNumberRequest>.WithActionResult<
        CreateCustomerPhoneNumberResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerPhoneNumber> _repository;
        public Create(
            IRepository<CustomerPhoneNumber> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/customerPhoneNumbers")]
        [SwaggerOperation(
            Summary = "Creates a new CustomerPhoneNumber",
            Description = "Creates a new CustomerPhoneNumber",
            OperationId = "customerPhoneNumbers.create",
            Tags = new[] { "CustomerPhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<CreateCustomerPhoneNumberResponse>> HandleAsync(
            CreateCustomerPhoneNumberRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateCustomerPhoneNumberResponse(request.CorrelationId());
            var newCustomerPhoneNumber = new CustomerPhoneNumber(
            request.CustomerId,
            request.PhoneNumberId,
            request.PhoneNumberTypeId,
            request.PhoneHasBeenVerified
            );
            try
            {
                await _repository.AddAsync(newCustomerPhoneNumber, cancellationToken);
                var dto = _mapper.Map<CustomerPhoneNumberDto>(newCustomerPhoneNumber);
                response.CustomerPhoneNumber = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating customerPhoneNumber with Id {newCustomerPhoneNumber.RowId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
