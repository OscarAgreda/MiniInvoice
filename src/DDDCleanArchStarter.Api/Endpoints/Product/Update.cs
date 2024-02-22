using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ardalis.ApiEndpoints;
using AutoMapper;
using BlazorMauiShared.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace BusinessManagement.Api.ProductEndpoints
{
  public class Update : EndpointBaseAsync
    .WithRequest<UpdateProductRequest>
    .WithActionResult<UpdateProductResponse>
  {
    private readonly IRepository<Product> _repository;
        private readonly IAppLoggerService<Update> _logger;
    private readonly IMapper _mapper;
        public Update(
            IRepository<Product> repository,
            IAppLoggerService<Update> logger,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
    [HttpPut("api/products")]
    [SwaggerOperation(
        Summary = "Updates a Product",
        Description = "Updates a Product",
        OperationId = "products.update",
        Tags = new[] { "ProductEndpoints" })
    ]
    public override async Task<ActionResult<UpdateProductResponse>> HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken)
    {
      var response = new UpdateProductResponse(request.CorrelationId());
            try
            {
                var proToUpdate = _mapper.Map<Product>(request);
      await _repository.UpdateAsync(proToUpdate, cancellationToken);
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string jsonProduct = JsonConvert.SerializeObject(proToUpdate, settings);
                ProductDto finalProductDto = JsonConvert.DeserializeObject<ProductDto>(jsonProduct, settings);
                response.Product = finalProductDto;
                }
                catch (Exception ex)
                {
                  var errorMsg = $"Error while updating product with request data: {request}.";
                  _logger.LogError(ex, errorMsg);
                  response.ErrorMessage = errorMsg;
                  return BadRequest(response);
                }
                return Ok(response);
    }
  }
}
