using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Address;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.AddressEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteAddressRequest>.WithActionResult<
        DeleteAddressResponse>
    {
        private readonly IRepository<Address> _addressReadRepository;
        private readonly IRepository<CustomerAddress> _customerAddressRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Address> _repository;

        public Delete(IRepository<Address> AddressRepository, IRepository<Address> AddressReadRepository,
            IAppLoggerService<Delete> logger,
               IRepository<CustomerAddress> customerAddressRepository,
            IMapper mapper)
        {
            _repository = AddressRepository;
            _logger = logger;
            _addressReadRepository = AddressReadRepository;
            _customerAddressRepository = customerAddressRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/addresses/{AddressId}")]
        [SwaggerOperation(
            Summary = "Deletes an Address",
            Description = "Deletes an Address",
            OperationId = "addresses.delete",
            Tags = new[] { "AddressEndpoints" })
        ]
        public override async Task<ActionResult<DeleteAddressResponse>> HandleAsync(
            [FromRoute] DeleteAddressRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteAddressResponse(request.CorrelationId());
            var address = await _addressReadRepository.GetByIdAsync(request.AddressId, cancellationToken);
            if (address == null)
            {
                var errorMsg = $"Address with ID {request.AddressId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            var customerAddressSpec = new GetCustomerAddressWithAddressKeySpec(address.AddressId);
            var customerAddresses = await _customerAddressRepository.ListAsync(customerAddressSpec);
            await _customerAddressRepository.DeleteRangeAsync(customerAddresses);
            try
            {
                await _repository.DeleteAsync(address, cancellationToken);
                _logger.LogInformation($"Successfully deleted address with ID {request.AddressId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting address with ID {request.AddressId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}