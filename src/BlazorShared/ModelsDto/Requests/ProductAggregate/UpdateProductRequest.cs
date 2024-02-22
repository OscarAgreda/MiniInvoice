using System;
using BlazorShared.Models;
using DDDInvoicingClean.Domain.ModelsDto;
namespace BlazorMauiShared.Models.Product
{
    public class UpdateProductRequest : BaseRequest
    {
      public Guid ProductId { get; set; }
      public string ProductName { get; set; }
      public string? ProductDescription { get; set; }
      public decimal ProductUnitPrice { get; set; }
      public bool ProductIsActive { get; set; }
      public int ProductMinimumCharacters { get; set; }
      public int ProductMinimumCallMinutes { get; set; }
      public decimal ProductChargeRatePerCharacter { get; set; }
      public decimal ProductChargeRateCallPerSecond { get; set; }
      public bool? IsDeleted { get; set; }
      public Guid TenantId { get; set; }
        public static UpdateProductRequest FromDto(ProductDto productDto)
        {
            return new UpdateProductRequest
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ProductUnitPrice = productDto.ProductUnitPrice,
                ProductIsActive = productDto.ProductIsActive,
                ProductMinimumCharacters = productDto.ProductMinimumCharacters,
                ProductMinimumCallMinutes = productDto.ProductMinimumCallMinutes,
                ProductChargeRatePerCharacter = productDto.ProductChargeRatePerCharacter,
                ProductChargeRateCallPerSecond = productDto.ProductChargeRateCallPerSecond,
                IsDeleted = productDto.IsDeleted,
                TenantId = productDto.TenantId
            };
        }
    }
}
