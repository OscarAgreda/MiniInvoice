using System;
using System.Collections.Generic;
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
  public class List : EndpointBaseAsync
    .WithRequest<ListProductRequest>
    .WithActionResult<ListProductResponse>
  {
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;
        private readonly IAppLoggerService<List> _logger;
    public List(IRepository<Product> repository,
      IAppLoggerService<List> logger,
      IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet("api/products")]
    [SwaggerOperation(
        Summary = "List Products",
        Description = "List Products",
        OperationId = "products.List",
        Tags = new[] { "ProductEndpoints" })
    ]
    public override async Task<ActionResult<ListProductResponse>> HandleAsync([FromQuery] ListProductRequest request,
      CancellationToken cancellationToken)
    {
      var response = new ListProductResponse(request.CorrelationId());
            try
            {
                var spec = new ProductGetListSpec();
                var products = await _repository.ListAsync(spec, cancellationToken);
                if (products == null || !products.Any()) 
                {
                    _logger.LogWarning("No products found.");
                    return NotFound();
                }
                response.Products = _mapper.Map<List<ProductDto>>(products);
                response.Count = response.Products.Count;
            }
            catch (Exception ex)
            {
                var errorMsg = "Error while fetching product list.";
                _logger.LogError(ex, errorMsg);
                response.ErrorMessage = errorMsg;
                return BadRequest(response);
            }
      return Ok(response);
    }
  }
}
