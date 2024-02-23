using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.PhoneNumber;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.PhoneNumberEndpoints
{
    public class List : EndpointBaseAsync
      .WithRequest<ListPhoneNumberRequest>
      .WithActionResult<ListPhoneNumberResponse>
    {
        private readonly IAppLoggerService<List> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<PhoneNumber> _repository;

        public List(IRepository<PhoneNumber> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("api/phoneNumbers")]
        [SwaggerOperation(
            Summary = "List PhoneNumbers",
            Description = "List PhoneNumbers",
            OperationId = "phoneNumbers.List",
            Tags = new[] { "PhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<ListPhoneNumberResponse>> HandleAsync([FromQuery] ListPhoneNumberRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListPhoneNumberResponse(request.CorrelationId());
            try
            {
                var spec = new PhoneNumberGetListSpec();
                var phoneNumbers = await _repository.ListAsync(spec, cancellationToken);
                if (phoneNumbers == null || !phoneNumbers.Any())
                {
                    _logger.LogWarning("No phoneNumbers found.");
                    return NotFound();
                }
                response.PhoneNumbers = _mapper.Map<List<PhoneNumberDto>>(phoneNumbers);
                response.Count = response.PhoneNumbers.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching phoneNumber list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}