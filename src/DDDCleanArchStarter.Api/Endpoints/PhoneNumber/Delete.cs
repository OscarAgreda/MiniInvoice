using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumber;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.PhoneNumberEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeletePhoneNumberRequest>.WithActionResult<
        DeletePhoneNumberResponse>
    {
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IRepository<PhoneNumber> _phoneNumberReadRepository;
        private readonly IRepository<CustomerPhoneNumber> _customerPhoneNumberRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumber> _repository;
        public Delete(IRepository<PhoneNumber> PhoneNumberRepository, IRepository<PhoneNumber> PhoneNumberReadRepository,
            IAppLoggerService<Delete> logger,
		       IRepository<CustomerPhoneNumber> customerPhoneNumberRepository,
            IMapper mapper)
        {
            _repository = PhoneNumberRepository;
            _logger = logger;
            _phoneNumberReadRepository = PhoneNumberReadRepository;
			    _customerPhoneNumberRepository = customerPhoneNumberRepository;
            _mapper = mapper;
        }
        [HttpDelete("api/phoneNumbers/{PhoneNumberId}")]
        [SwaggerOperation(
            Summary = "Deletes an PhoneNumber",
            Description = "Deletes an PhoneNumber",
            OperationId = "phoneNumbers.delete",
            Tags = new[] { "PhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<DeletePhoneNumberResponse>> HandleAsync(
            [FromRoute] DeletePhoneNumberRequest request, CancellationToken cancellationToken)
        {
            var response = new DeletePhoneNumberResponse(request.CorrelationId());
            var phoneNumber = await _phoneNumberReadRepository.GetByIdAsync(request.PhoneNumberId, cancellationToken);
            if (phoneNumber == null)
            {
                    var errorMsg = $"PhoneNumber with ID {request.PhoneNumberId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
            }
            var customerPhoneNumberSpec = new GetCustomerPhoneNumberWithPhoneNumberKeySpec(phoneNumber.PhoneNumberId);
            var customerPhoneNumbers = await _customerPhoneNumberRepository.ListAsync(customerPhoneNumberSpec);
            await _customerPhoneNumberRepository.DeleteRangeAsync(customerPhoneNumbers);
            try
            {
                await _repository.DeleteAsync(phoneNumber, cancellationToken);
                _logger.LogInformation($"Successfully deleted phoneNumber with ID {request.PhoneNumberId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting phoneNumber with ID {request.PhoneNumberId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
