using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerPhoneNumber;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.CustomerPhoneNumberEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteCustomerPhoneNumberRequest>.WithActionResult<
        DeleteCustomerPhoneNumberResponse>
    {
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IRepository<CustomerPhoneNumber> _customerPhoneNumberReadRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerPhoneNumber> _repository;
        public Delete(IRepository<CustomerPhoneNumber> CustomerPhoneNumberRepository, IRepository<CustomerPhoneNumber> CustomerPhoneNumberReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = CustomerPhoneNumberRepository;
            _logger = logger;
            _customerPhoneNumberReadRepository = CustomerPhoneNumberReadRepository;
            _mapper = mapper;
        }
        [HttpDelete("api/customerPhoneNumbers/{RowId}")]
        [SwaggerOperation(
            Summary = "Deletes an CustomerPhoneNumber",
            Description = "Deletes an CustomerPhoneNumber",
            OperationId = "customerPhoneNumbers.delete",
            Tags = new[] { "CustomerPhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<DeleteCustomerPhoneNumberResponse>> HandleAsync(
            [FromRoute] DeleteCustomerPhoneNumberRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteCustomerPhoneNumberResponse(request.CorrelationId());
            var customerPhoneNumber = await _customerPhoneNumberReadRepository.GetByIdAsync(request.RowId, cancellationToken);
            if (customerPhoneNumber == null)
            {
                    var errorMsg = $"CustomerPhoneNumber with ID {request.RowId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(customerPhoneNumber, cancellationToken);
                _logger.LogInformation($"Successfully deleted customerPhoneNumber with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting customerPhoneNumber with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
