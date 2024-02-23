using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.ProductCategory;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DDDInvoicingClean.Api.ProductCategoryEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteProductCategoryRequest>.WithActionResult<
        DeleteProductCategoryResponse>
    {
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<ProductCategory> _productCategoryReadRepository;
        private readonly IRepository<ProductCategory> _repository;

        public Delete(IRepository<ProductCategory> ProductCategoryRepository, IRepository<ProductCategory> ProductCategoryReadRepository,
            IAppLoggerService<Delete> logger,
            IMapper mapper)
        {
            _repository = ProductCategoryRepository;
            _logger = logger;
            _productCategoryReadRepository = ProductCategoryReadRepository;
            _mapper = mapper;
        }

        [HttpDelete("api/productCategories/{RowId}")]
        [SwaggerOperation(
            Summary = "Deletes an ProductCategory",
            Description = "Deletes an ProductCategory",
            OperationId = "productCategories.delete",
            Tags = new[] { "ProductCategoryEndpoints" })
        ]
        public override async Task<ActionResult<DeleteProductCategoryResponse>> HandleAsync(
            [FromRoute] DeleteProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteProductCategoryResponse(request.CorrelationId());
            var productCategory = await _productCategoryReadRepository.GetByIdAsync(request.RowId, cancellationToken);
            if (productCategory == null)
            {
                var errorMsg = $"ProductCategory with ID {request.RowId} not found.";
                _logger.LogWarning(errorMsg);
                response.ErrorMessage = errorMsg;
                return NotFound(response);
            }
            try
            {
                await _repository.DeleteAsync(productCategory, cancellationToken);
                _logger.LogInformation($"Successfully deleted productCategory with ID {request.RowId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting productCategory with ID {request.RowId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}