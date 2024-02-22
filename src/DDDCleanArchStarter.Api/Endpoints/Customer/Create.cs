using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CustomerEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateCustomerRequest>.WithActionResult<
        CreateCustomerResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Customer> _repository;
        public Create(
            IRepository<Customer> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/customers")]
        [SwaggerOperation(
            Summary = "Creates a new Customer",
            Description = "Creates a new Customer",
            OperationId = "customers.create",
            Tags = new[] { "CustomerEndpoints" })
        ]
        public override async Task<ActionResult<CreateCustomerResponse>> HandleAsync(
            CreateCustomerRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateCustomerResponse(request.CorrelationId());
            var newCustomer = new Customer(
            customerId:Guid.NewGuid(),
            request.CustomerFirstName,
            request.CustomerLastName,
            request.CustomerBirthDate,
            request.CustomerWebsite,
            request.CreditLimit,
            request.Notes,
            request.CustomerMiddleName,
            request.CustomerTitle,
            request.CustomerGender,
            request.CustomerCompanyName,
            request.CustomerJobTitle,
            request.Industry,
            request.PreferredContactMethod,
            request.CustomerStatus,
            request.LoyaltyPoints,
            request.Source,
            request.LastPurchaseDate,
            request.TotalPurchases,
            request.IsDeleted,
            request.TenantId,
            request.CustomerDecimal,
            request.CustomerInt,
            request.CustomerDateTime2,
            request.CustomerDateTime,
            request.CustomerBit,
            request.CustomerFloat,
            request.CustomerReal,
            request.CustomerBigInt,
            request.CustomerSmallInt,
            request.CustomerTinyInt,
            request.CustomerChar,
            request.CustomerVarChar,
            request.CustomerText,
            request.CustomerNChar,
            request.CustomerNVarChar,
            request.CustomerNText,
            request.CustomerBinary,
            request.CustomerVarBinary,
            request.CustomerImage,
            request.CustomerMoney,
            request.CustomerSmallMoney,
            request.CustomerTimestamp,
            request.CustomerUniqueIdentifier,
            request.CustomerXml
            );
            try
            {
                await _repository.AddAsync(newCustomer, cancellationToken);
                var dto = _mapper.Map<CustomerDto>(newCustomer);
                response.Customer = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating customer with Id {newCustomer.CustomerId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
