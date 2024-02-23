using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Customer;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteCustomerRequest>.WithActionResult<
        DeleteCustomerResponse>
    {
        private readonly IRepository<CustomerAccount> _customerAccountRepository;
        private readonly IRepository<CustomerAddress> _customerAddressRepository;
        private readonly IRepository<CustomerPhoneNumber> _customerPhoneNumberRepository;
        private readonly IRepository<Customer> _customerReadRepository;
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Customer> _repository;

        public Delete(IRepository<Customer> CustomerRepository, IRepository<Customer> CustomerReadRepository,
            IAppLoggerService<Delete> logger,
               IRepository<CustomerAccount> customerAccountRepository,
               IRepository<CustomerAddress> customerAddressRepository,
               IRepository<CustomerPhoneNumber> customerPhoneNumberRepository,
            IMapper mapper)
        {
            _repository = CustomerRepository;
            _logger = logger;
            _customerReadRepository = CustomerReadRepository;
            _customerAccountRepository = customerAccountRepository;
            _customerAddressRepository = customerAddressRepository;
            _customerPhoneNumberRepository = customerPhoneNumberRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/customers/{CustomerId}")]
        [SwaggerOperation(
            Summary = "Deletes an Customer",
            Description = "Deletes an Customer",
            OperationId = "customers.delete",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCustomerResponse>> HandleAsync(
            [FromRoute] DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteCustomerResponse(request.CorrelationId());
            var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer == null)
            {
                var errorMsg = $"Customer with ID {request.CustomerId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            var customerAccountSpec = new GetCustomerAccountWithCustomerKeySpec(customer.CustomerId);
            var customerAccounts = await _customerAccountRepository.ListAsync(customerAccountSpec);
            await _customerAccountRepository.DeleteRangeAsync(customerAccounts);
            var customerAddressSpec = new GetCustomerAddressWithCustomerKeySpec(customer.CustomerId);
            var customerAddresses = await _customerAddressRepository.ListAsync(customerAddressSpec);
            await _customerAddressRepository.DeleteRangeAsync(customerAddresses);
            var customerPhoneNumberSpec = new GetCustomerPhoneNumberWithCustomerKeySpec(customer.CustomerId);
            var customerPhoneNumbers = await _customerPhoneNumberRepository.ListAsync(customerPhoneNumberSpec);
            await _customerPhoneNumberRepository.DeleteRangeAsync(customerPhoneNumbers);
            try
            {
                await _repository.DeleteAsync(customer, cancellationToken);
                _logger.LogInformation($"Successfully deleted customer with ID {request.CustomerId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting customer with ID {request.CustomerId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}