using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerPhoneNumber;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerPhoneNumberEndpoints
{
    public class GetByRelsIds : EndpointBaseAsync.WithRequest<GetByRelsIdsCustomerPhoneNumberRequest>.WithActionResult<
        GetByIdCustomerPhoneNumberResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerPhoneNumber> _repository;

        public GetByRelsIds(
            IRepository<CustomerPhoneNumber> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/customerPhoneNumbers/{CustomerId}/{PhoneNumberId}/{PhoneHasBeenVerified}")]
        [SwaggerOperation(
            Summary = "Get a CustomerPhoneNumber by rel Ids",
            Description = "Gets a CustomerPhoneNumber by rel Ids",
            OperationId = "customerPhoneNumbers.GetByRelsIds",
            Tags = new[] { "CustomerPhoneNumberEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerPhoneNumberResponse>> HandleAsync(
            [FromRoute] GetByRelsIdsCustomerPhoneNumberRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCustomerPhoneNumberResponse(request.CorrelationId());
            var spec = new CustomerPhoneNumberByRelIdsSpec(request.CustomerId, request.PhoneNumberId, request.PhoneHasBeenVerified);
            var customerPhoneNumber = await _repository.FirstOrDefaultAsync(spec);
            if (customerPhoneNumber is null)
            {
                return NotFound();
            }
            response.CustomerPhoneNumber = _mapper.Map<CustomerPhoneNumberDto>(customerPhoneNumber);
            return Ok(response);
        }
    }
}