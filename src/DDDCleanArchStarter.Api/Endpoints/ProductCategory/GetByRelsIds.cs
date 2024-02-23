using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.ProductCategory;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.ProductCategoryEndpoints
{
    public class GetByRelsIds : EndpointBaseAsync.WithRequest<GetByRelsIdsProductCategoryRequest>.WithActionResult<
        GetByIdProductCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ProductCategory> _repository;

        public GetByRelsIds(
            IRepository<ProductCategory> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/productCategories/{TenantId}/{ProductId}")]
        [SwaggerOperation(
            Summary = "Get a ProductCategory by rel Ids",
            Description = "Gets a ProductCategory by rel Ids",
            OperationId = "productCategories.GetByRelsIds",
            Tags = new[] { "ProductCategoryEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdProductCategoryResponse>> HandleAsync(
            [FromRoute] GetByRelsIdsProductCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetByIdProductCategoryResponse(request.CorrelationId());
            var spec = new ProductCategoryByRelIdsSpec(request.TenantId, request.ProductId);
            var productCategory = await _repository.FirstOrDefaultAsync(spec);
            if (productCategory is null)
            {
                return NotFound();
            }
            response.ProductCategory = _mapper.Map<ProductCategoryDto>(productCategory);
            return Ok(response);
        }
    }
}