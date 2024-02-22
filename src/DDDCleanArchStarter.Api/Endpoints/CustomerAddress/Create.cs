using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAddress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CustomerAddressEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateCustomerAddressRequest>.WithActionResult<
        CreateCustomerAddressResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerAddress> _repository;
        public Create(
            IRepository<CustomerAddress> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/customerAddresses")]
        [SwaggerOperation(
            Summary = "Creates a new CustomerAddress",
            Description = "Creates a new CustomerAddress",
            OperationId = "customerAddresses.create",
            Tags = new[] { "CustomerAddressEndpoints" })
        ]
        public override async Task<ActionResult<CreateCustomerAddressResponse>> HandleAsync(
            CreateCustomerAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateCustomerAddressResponse(request.CorrelationId());
            var newCustomerAddress = new CustomerAddress(
            request.AddressId,
            request.AddressTypeId,
            request.CustomerId
            );
            try
            {
                await _repository.AddAsync(newCustomerAddress, cancellationToken);
                var dto = _mapper.Map<CustomerAddressDto>(newCustomerAddress);
                response.CustomerAddress = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating customerAddress with Id {newCustomerAddress.RowId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
