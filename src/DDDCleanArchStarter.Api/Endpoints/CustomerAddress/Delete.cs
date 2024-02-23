using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAddress;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerAddressEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteCustomerAddressRequest>.WithActionResult<
        DeleteCustomerAddressResponse>
    {
        private readonly IRepository<CustomerAddress> _customerAddressReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerAddress> _repository;

        public Delete(IRepository<CustomerAddress> CustomerAddressRepository, IRepository<CustomerAddress> CustomerAddressReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = CustomerAddressRepository;
            _logger = logger;
            _customerAddressReadRepository = CustomerAddressReadRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/customerAddresses/{RowId}")]
        [SwaggerOperation(
            Summary = "Deletes an CustomerAddress",
            Description = "Deletes an CustomerAddress",
            OperationId = "customerAddresses.delete",
            Tags = new[] { "CustomerAddressEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCustomerAddressResponse>> HandleAsync(
            [FromRoute] DeleteCustomerAddressRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteCustomerAddressResponse(request.CorrelationId());
            var customerAddress = await _customerAddressReadRepository.GetByIdAsync(request.RowId, cancellationToken);
            if (customerAddress == null)
            {
                var errorMsg = $"CustomerAddress with ID {request.RowId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(customerAddress, cancellationToken);
                _logger.LogInformation($"Successfully deleted customerAddress with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting customerAddress with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}