using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.ProductCategoryEndpoints
{
    public class Create : EndpointBaseAsync.WithRequest<CreateProductCategoryRequest>.WithActionResult<
        CreateProductCategoryResponse>
    {
        private readonly IAppLoggerService<Create> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductCategory> _repository;
        public Create(
            IRepository<ProductCategory> repository,
            IMapper mapper,
            IAppLoggerService<Create> logger
            )
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }
        [HttpPost("api/productCategories")]
        [SwaggerOperation(
            Summary = "Creates a new ProductCategory",
            Description = "Creates a new ProductCategory",
            OperationId = "productCategories.create",
            Tags = new[] { "ProductCategoryEndpoints" })
        ]
        public override async Task<ActionResult<CreateProductCategoryResponse>> HandleAsync(
            CreateProductCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var response = new CreateProductCategoryResponse(request.CorrelationId());
            var newProductCategory = new ProductCategory(
            request.ProductId,
            request.TenantId
            );
            try
            {
                await _repository.AddAsync(newProductCategory, cancellationToken);
                var dto = _mapper.Map<ProductCategoryDto>(newProductCategory);
                response.ProductCategory = dto;
            }
            catch (Exception ex)  
            {
                var errorMsg = $"Error while creating productCategory with Id {newProductCategory.RowId.ToString("D", CultureInfo.InvariantCulture)}";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
