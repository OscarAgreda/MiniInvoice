using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Api.ProductEndpoints
{
    public class Delete : EndpointBaseAsync.WithRequest<DeleteProductRequest>.WithActionResult<
        DeleteProductResponse>
    {
        private readonly IAppLoggerService<Delete> _logger;
        private readonly IRepository<Product> _productReadRepository;
        private readonly IRepository<InvoiceDetail> _invoiceDetailRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;
        public Delete(IRepository<Product> ProductRepository, IRepository<Product> ProductReadRepository,
            IAppLoggerService<Delete> logger,
		       IRepository<InvoiceDetail> invoiceDetailRepository,
		       IRepository<ProductCategory> productCategoryRepository,
            IMapper mapper)
        {
            _repository = ProductRepository;
            _logger = logger;
            _productReadRepository = ProductReadRepository;
			    _invoiceDetailRepository = invoiceDetailRepository;
			    _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }
        [HttpDelete("api/products/{ProductId}")]
        [SwaggerOperation(
            Summary = "Deletes an Product",
            Description = "Deletes an Product",
            OperationId = "products.delete",
            Tags = new[] { "ProductEndpoints" })
        ]
        public override async Task<ActionResult<DeleteProductResponse>> HandleAsync(
            [FromRoute] DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteProductResponse(request.CorrelationId());
            var product = await _productReadRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
            {
                    var errorMsg = $"Product with ID {request.ProductId} not found.";
                    _logger.LogWarning(errorMsg);
                    response.ErrorMessage = errorMsg;
                    return NotFound(response);
            }
            var invoiceDetailSpec = new GetInvoiceDetailWithProductKeySpec(product.ProductId);
            var invoiceDetails = await _invoiceDetailRepository.ListAsync(invoiceDetailSpec);
            await _invoiceDetailRepository.DeleteRangeAsync(invoiceDetails);
            var productCategorySpec = new GetProductCategoryWithProductKeySpec(product.ProductId);
            var productCategories = await _productCategoryRepository.ListAsync(productCategorySpec);
            await _productCategoryRepository.DeleteRangeAsync(productCategories);
            try
            {
                await _repository.DeleteAsync(product, cancellationToken);
                _logger.LogInformation($"Successfully deleted product with ID {request.ProductId}.");
            }
            catch (Exception ex)
            {
                var errorMsg = $"Error while deleting product with ID {request.ProductId}.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
