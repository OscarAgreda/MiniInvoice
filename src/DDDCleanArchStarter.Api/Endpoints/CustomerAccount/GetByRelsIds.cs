using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.CustomerAccount;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.CustomerAccountEndpoints
{
    public class GetByRelsIds : EndpointBaseAsync.WithRequest<GetByRelsIdsCustomerAccountRequest>.WithActionResult<
        GetByIdCustomerAccountResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CustomerAccount> _repository;

        public GetByRelsIds(
            IRepository<CustomerAccount> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/customerAccounts/{AccountId}/{CustomerId}")]
        [SwaggerOperation(
            Summary = "Get a CustomerAccount by rel Ids",
            Description = "Gets a CustomerAccount by rel Ids",
            OperationId = "customerAccounts.GetByRelsIds",
            Tags = new[] { "CustomerAccountEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdCustomerAccountResponse>> HandleAsync(
            [FromRoute] GetByRelsIdsCustomerAccountRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdCustomerAccountResponse(request.CorrelationId());
            var spec = new CustomerAccountByRelIdsSpec(request.AccountId, request.CustomerId);
            var customerAccount = await _repository.FirstOrDefaultAsync(spec);
            if (customerAccount is null)
            {
                return NotFound();
            }
            response.CustomerAccount = _mapper.Map<CustomerAccountDto>(customerAccount);
            return Ok(response);
        }
    }
}