using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using BlazorMauiShared.Models.Invoice;
using BlazorMauiShared.Models.InvoiceDetail;
using BlazorMauiShared.Models.Product;
using BlazorShared.Models;
using DDDCleanArchStarter.Infrastructure.Services;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.ModelsDto;
using DDDInvoicingClean.Domain.Specifications;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using static LanguageExt.Prelude;
namespace DDDCleanArchStarter.Api.Strategies
{
 public interface IInvoiceRegistrationExecuteStrategy
 {
     Task<Either<Exception, Unit>> ExecuteInvoiceRegistrationAsync(InvoiceStrategyContext context, CancellationToken cancellationToken);
 }
 public abstract class InvoiceRegistrationStrategyTemplate : IInvoiceRegistrationExecuteStrategy
 {
     public async Task<Either<Exception, Unit>> ExecuteInvoiceRegistrationAsync(InvoiceStrategyContext context, CancellationToken cancellationToken)
     {
         var preResult = await PreExecutionAsync(context, cancellationToken);
         if (preResult.IsLeft)
         {
             return preResult;
         }
 	 
         var execResult = await ExecuteSpecificAsync(context, cancellationToken);
         if (execResult.IsLeft)
         {
             return execResult;
         }
 	 
         var postResult = await PostExecutionAsync(context, cancellationToken);
         return postResult;
     }
 	 
     protected virtual Task<Either<Exception, Unit>> PreExecutionAsync(InvoiceStrategyContext context, CancellationToken cancellationToken)
     {
         return Task.FromResult(Right<Exception, Unit>(Unit.Default));
     }
 	 
     protected abstract Task<Either<Exception, Unit>> ExecuteSpecificAsync(InvoiceStrategyContext context, CancellationToken cancellationToken);
 	 
     protected virtual Task<Either<Exception, Unit>> PostExecutionAsync(InvoiceStrategyContext context, CancellationToken cancellationToken)
     {
         return Task.FromResult(Right<Exception, Unit>(Unit.Default));
     }
 }
   public interface IInvoiceNotificationChannel
   {
       void SendNotification(DDDInvoicingClean.Domain.Entities.Invoice invoice);
   }
 	 
   public interface IInvoiceObserver
   {
       void Notify(DDDInvoicingClean.Domain.Entities.Invoice invoice);
   }
 	 
   public class NewInvoiceNotifier : IInvoiceObserver, IInvoiceNotificationChannel
   {
       public void Notify(DDDInvoicingClean.Domain.Entities.Invoice invoice)
       {
           // Notify interested parties about the new invoice
           SendNotification(invoice);
       }
       public void SendNotification(DDDInvoicingClean.Domain.Entities.Invoice invoice)
       {
           // Implementation for sending notifications (e.g., Email, SMS)
       }
   }
 	 
   public class AddInvoiceCommand2 : InvoiceRegistrationStrategyTemplate
   {
       private readonly IEnumerable<IInvoiceObserver> _observers;
       private readonly IAppLoggerService<AddInvoiceCommand2> _logger;
       private readonly IRepository<DDDInvoicingClean.Domain.Entities.Invoice> _invoiceRepository;
       private readonly InvoiceFactory _invoiceFactory;
 	 
       public AddInvoiceCommand2(
           IRepository<DDDInvoicingClean.Domain.Entities.Invoice> invoiceRepository,
           InvoiceFactory invoiceFactory,
           IAppLoggerService<AddInvoiceCommand2> logger,
           IEnumerable<IInvoiceObserver> observers)
       {
           _invoiceRepository = invoiceRepository;
           _invoiceFactory = invoiceFactory;
           _logger = logger;
           _observers = observers;
       }

       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken)
       {
           try
           {
               var newInvoice = _invoiceFactory.Create(context.InvoiceRequest);
               await _invoiceRepository.AddAsync(newInvoice, cancellationToken);
 	 
               NotifyObservers(newInvoice);
 	 
               return Prelude.Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, $"Error occurred while creating invoice");
               return Prelude.Left<Exception, Unit>(ex);
           }
       }
       private void NotifyObservers(DDDInvoicingClean.Domain.Entities.Invoice invoice)
       {
           _observers.ToList().ForEach(observer => observer.Notify(invoice));
       }
   }
   public interface IRegistrationInvoiceDataStrategy
   {
       Task UpdateInvoiceAsync(
           IAppLoggerService<UpdateInvoiceStrategyDataRefactored> logger,
           UpdateInvoiceRegistrationDataStrategyRequest request,
           CancellationToken cancellationToken
       );
   }
   public class UpdateInvoiceStrategyDataRefactored
       : EndpointBaseAsync.WithRequest<UpdateInvoiceRegistrationDataStrategyRequest>.WithActionResult<UpdateInvoiceRegistrationDataResponse>
   {
       private readonly IAppLoggerService<UpdateInvoiceStrategyDataRefactored> _logger;
       private readonly IRegistrationInvoiceDataStrategy _registrationInvoiceDataStrategy;
 	 
       public UpdateInvoiceStrategyDataRefactored(
           IAppLoggerService<UpdateInvoiceStrategyDataRefactored> logger,
           IRegistrationInvoiceDataStrategy registrationInvoiceDataStrategy
       )
       {
           _logger = logger;
           _registrationInvoiceDataStrategy = registrationInvoiceDataStrategy;
       }
 	 
       [HttpPut("api/invoiceStrategyDataRegistration")]
       [SwaggerOperation(
           Summary = "Updates a invoiceStrategyDataRegistration",
           Description = "Updates a invoiceStrategyDataRegistration",
           OperationId = "invoiceStrategyDataRegistration.update",
           Tags = new[] { "invoiceStrategyDataRegistrationEndpoints" }
       )]
       public override async Task<
           ActionResult<UpdateInvoiceRegistrationDataResponse>
       > HandleAsync(
           UpdateInvoiceRegistrationDataStrategyRequest request,
           CancellationToken cancellationToken
       )
       {
           UpdateInvoiceRegistrationDataResponse response = new(request.CorrelationId());
           try
           {
               await _registrationInvoiceDataStrategy.UpdateInvoiceAsync(
                   _logger,
                   request,
                   cancellationToken
               );
               response.IsSuccess = true;
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, "An error occurred while updating invoice registration data");
               response.IsSuccess = false;
               response.ErrorMessage = ex.Message;
           }
           return Ok(response);
       }
   }
public class ProductFactory : IEntityFactory<Product>
{
   public Product Create(params object[] args)
   {
       var request = (CreateProductRequest)args[0];
       return new ProductEntityBuilder()
       .WithProductId(Guid.NewGuid())
       .WithProductName(request.ProductName)
       .WithProductDescription(request.ProductDescription)
       .WithProductUnitPrice(request.ProductUnitPrice)
       .WithProductIsActive(request.ProductIsActive)
       .WithProductMinimumCharacters(request.ProductMinimumCharacters)
       .WithProductMinimumCallMinutes(request.ProductMinimumCallMinutes)
       .WithProductChargeRatePerCharacter(request.ProductChargeRatePerCharacter)
       .WithProductChargeRateCallPerSecond(request.ProductChargeRateCallPerSecond)
       .WithIsDeleted(request.IsDeleted)
       .WithTenantId(request.TenantId)
           .Build();
   }
}
public class AddProductCommand : InvoiceRegistrationStrategyTemplate
{
   private readonly IRepository<Product> _productRepository;
   private readonly ProductFactory _productFactory;
   private readonly IAppLoggerService<AddProductCommand> _logger;
 	 
   public AddProductCommand(IRepository<Product> productRepository, ProductFactory productFactory, IAppLoggerService<AddProductCommand> logger)
   {
       _productRepository = productRepository;
       _productFactory = productFactory;
        _logger = logger;
   }

   protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(InvoiceStrategyContext context, CancellationToken cancellationToken)
  {
           try
           {
               var newProduct = _productFactory.Create(context.ProductRequest);
               _ = await _productRepository.AddAsync(newProduct, cancellationToken);
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, $"Error occurred while creating product ");
               return Left<Exception, Unit>(ex);
           }
  }
}
   public class DeleteProductCommand : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<Product> _productRepository;
       private readonly IAppLoggerService<DeleteProductCommand> _logger;
 	 
       public DeleteProductCommand(
           IRepository<Product> productRepository,
           IAppLoggerService<DeleteProductCommand> logger
       )
       {
           _productRepository = productRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               await _productRepository.DeleteAsync(context.CurrentProduct, cancellationToken);
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while deleting product with ID {context.CurrentProduct.ProductId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public class UpdateProductCommand : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<Product> _productRepository;
       private readonly IAppLoggerService<UpdateProductCommand> _logger;
 	 
       public UpdateProductCommand(
           IRepository<Product> productRepository,
           IAppLoggerService<UpdateProductCommand> logger
       )
       {
           _productRepository = productRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               _logger.LogInformation(
                   $"Attempting to update product with ID {context.CurrentProduct.ProductId}"  	 
               );
               await _productRepository.UpdateAsync(context.CurrentProduct, cancellationToken);
               _logger.LogInformation(
                   $"Successfully updated product with ID {context.CurrentProduct.ProductId}"  	 
               );
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while updating product with ID {context.CurrentProduct.ProductId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public class FetchProductByIdCommandDto : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<Product> _productRepository;
       private readonly IAppLoggerService<FetchProductByIdCommandDto> _logger;
 	 
       public FetchProductByIdCommandDto(
           IRepository<Product> productRepository,
           IAppLoggerService<FetchProductByIdCommandDto> logger
       )
       {
           _productRepository = productRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               var spec = new ProductByIdWithIncludesSpec(context.CurrentProduct.ProductId);
               var product = await _productRepository.FirstOrDefaultAsync(spec, cancellationToken);
               if (product != null)
               {
                   var settings = new JsonSerializerSettings
                   {
                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                   };
                   string jsonProduct = JsonConvert.SerializeObject(product, settings);
                   ProductDto finalProductDto = JsonConvert.DeserializeObject<ProductDto>(
                       jsonProduct,
                       settings
                   );
                   context.FetchedProductDto = finalProductDto;
               }
               else
               {
                   _logger.LogWarning($"Product with ID {context.CurrentProduct.ProductId} not found.");
                   context.FetchedProductDto = new NullProductDto();
               }
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while fetching product by ID {context.CurrentProduct.ProductId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public class FetchProductByIdCommandEntity : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<Product> _productRepository;
       private readonly IAppLoggerService<FetchProductByIdCommandEntity> _logger;
 	 
       public FetchProductByIdCommandEntity(
           IRepository<Product> productRepository,
           IAppLoggerService<FetchProductByIdCommandEntity> logger
       )
       {
           _productRepository = productRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               var spec = new ProductByIdWithIncludesSpec(context.CurrentProduct.ProductId);
               var product = await _productRepository.FirstOrDefaultAsync(spec, cancellationToken);
               if (product != null)
               {
                   context.FetchedProductEntity = product;
               }
               else
               {
                   _logger.LogWarning($"Product with ID {context.CurrentProduct.ProductId} not found.");
                   context.FetchedProductEntity = new NullProduct();
               }
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while fetching product by ID {context.CurrentProduct.ProductId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public interface IProduct
   {
       Guid ProductId { get; }
   }
 	 
   public class ProductProxy : IProduct
   {
       private readonly Product _product;
 	 
       public ProductProxy(Product product)
       {
           _product = product;
       }
 	 
       public Guid ProductId => _product?.ProductId ?? Guid.Empty;
   }
 	 
   public class NullProduct : IProduct
   {
       public Guid ProductId => Guid.Empty;
   }
 	 
   public class NullProductDto : IProduct
   {
       public Guid ProductId => Guid.Empty;
   }
public class InvoiceDetailFactory : IEntityFactory<InvoiceDetail>
{
   public InvoiceDetail Create(params object[] args)
   {
       var request = (CreateInvoiceDetailRequest)args[0];
       return new InvoiceDetailEntityBuilder()
       .WithInvoiceDetailId(Guid.NewGuid())
       .WithInvoiceId(request.InvoiceId)
       .WithProductId(request.ProductId)
       .WithTenantId(request.TenantId)
       .WithQuantity(request.Quantity)
       .WithProductName(request.ProductName)
       .WithUnitPrice(request.UnitPrice)
       .WithLineSale(request.LineSale)
       .WithLineTax(request.LineTax)
       .WithLineDiscount(request.LineDiscount)
           .Build();
   }
}

public class InvoiceFactory : IEntityFactory<Invoice>
{
    public Invoice Create(params object[] args)
    {
        var request = (CreateInvoiceRequest)args[0];
        return new InvoiceEntityBuilder()
            .WithInvoiceId(Guid.NewGuid())
            .WithAccountId(request.AccountId)
            .WithInvoiceNumber(request.InvoiceNumber)
            .WithAccountName(request.AccountName)
            .WithCustomerName(request.CustomerName)
            .WithPaymentState(request.PaymentState)
            .WithInternalComments(request.InternalComments)
            .WithInvoicedDate(request.InvoicedDate)
            .WithInvoicingNote(request.InvoicingNote)
            .WithTotalSale(request.TotalSale)
            .WithTotalSaleTax(request.TotalSaleTax)
            .WithTenantId(request.TenantId)
            .Build();
    }
}
    public class AddInvoiceDetailCommand : InvoiceRegistrationStrategyTemplate
{
   private readonly IRepository<InvoiceDetail> _invoiceDetailRepository;
   private readonly InvoiceDetailFactory _invoiceDetailFactory;
   private readonly IAppLoggerService<AddInvoiceDetailCommand> _logger;
 	 
   public AddInvoiceDetailCommand(IRepository<InvoiceDetail> invoiceDetailRepository, InvoiceDetailFactory invoiceDetailFactory, IAppLoggerService<AddInvoiceDetailCommand> logger)
   {
       _invoiceDetailRepository = invoiceDetailRepository;
       _invoiceDetailFactory = invoiceDetailFactory;
        _logger = logger;
   }

   protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(InvoiceStrategyContext context, CancellationToken cancellationToken)
  {
           try
           {
               var newInvoiceDetail = _invoiceDetailFactory.Create(context.InvoiceDetailRequest);
               _ = await _invoiceDetailRepository.AddAsync(newInvoiceDetail, cancellationToken);
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, $"Error occurred while creating invoiceDetail ");
               return Left<Exception, Unit>(ex);
           }
  }
}
   public class DeleteInvoiceDetailCommand : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<InvoiceDetail> _invoiceDetailRepository;
       private readonly IAppLoggerService<DeleteInvoiceDetailCommand> _logger;
 	 
       public DeleteInvoiceDetailCommand(
           IRepository<InvoiceDetail> invoiceDetailRepository,
           IAppLoggerService<DeleteInvoiceDetailCommand> logger
       )
       {
           _invoiceDetailRepository = invoiceDetailRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               await _invoiceDetailRepository.DeleteAsync(context.CurrentInvoiceDetail, cancellationToken);
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while deleting invoiceDetail with ID {context.CurrentInvoiceDetail.InvoiceDetailId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public class UpdateInvoiceDetailCommand : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<InvoiceDetail> _invoiceDetailRepository;
       private readonly IAppLoggerService<UpdateInvoiceDetailCommand> _logger;
 	 
       public UpdateInvoiceDetailCommand(
           IRepository<InvoiceDetail> invoiceDetailRepository,
           IAppLoggerService<UpdateInvoiceDetailCommand> logger
       )
       {
           _invoiceDetailRepository = invoiceDetailRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               _logger.LogInformation(
                   $"Attempting to update invoiceDetail with ID {context.CurrentInvoiceDetail.InvoiceDetailId}"  	 
               );
               await _invoiceDetailRepository.UpdateAsync(context.CurrentInvoiceDetail, cancellationToken);
               _logger.LogInformation(
                   $"Successfully updated invoiceDetail with ID {context.CurrentInvoiceDetail.InvoiceDetailId}"  	 
               );
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while updating invoiceDetail with ID {context.CurrentInvoiceDetail.InvoiceDetailId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public class FetchInvoiceDetailByIdCommandDto : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<InvoiceDetail> _invoiceDetailRepository;
       private readonly IAppLoggerService<FetchInvoiceDetailByIdCommandDto> _logger;
 	 
       public FetchInvoiceDetailByIdCommandDto(
           IRepository<InvoiceDetail> invoiceDetailRepository,
           IAppLoggerService<FetchInvoiceDetailByIdCommandDto> logger
       )
       {
           _invoiceDetailRepository = invoiceDetailRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               var spec = new InvoiceDetailByIdWithIncludesSpec(context.CurrentInvoiceDetail.InvoiceDetailId);
               var invoiceDetail = await _invoiceDetailRepository.FirstOrDefaultAsync(spec, cancellationToken);
               if (invoiceDetail != null)
               {
                   var settings = new JsonSerializerSettings
                   {
                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                   };
                   string jsonInvoiceDetail = JsonConvert.SerializeObject(invoiceDetail, settings);
                   InvoiceDetailDto finalInvoiceDetailDto = JsonConvert.DeserializeObject<InvoiceDetailDto>(
                       jsonInvoiceDetail,
                       settings
                   );
                   context.FetchedInvoiceDetailDto = finalInvoiceDetailDto;
               }
               else
               {
                   _logger.LogWarning($"InvoiceDetail with ID {context.CurrentInvoiceDetail.InvoiceDetailId} not found.");
                   context.FetchedInvoiceDetailDto = new NullInvoiceDetailDto();
               }
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while fetching invoiceDetail by ID {context.CurrentInvoiceDetail.InvoiceDetailId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public class FetchInvoiceDetailByIdCommandEntity : InvoiceRegistrationStrategyTemplate
   {
       private readonly IRepository<InvoiceDetail> _invoiceDetailRepository;
       private readonly IAppLoggerService<FetchInvoiceDetailByIdCommandEntity> _logger;
 	 
       public FetchInvoiceDetailByIdCommandEntity(
           IRepository<InvoiceDetail> invoiceDetailRepository,
           IAppLoggerService<FetchInvoiceDetailByIdCommandEntity> logger
       )
       {
           _invoiceDetailRepository = invoiceDetailRepository;
           _logger = logger;
       }
 	 
       protected override async Task<Either<Exception, Unit>> ExecuteSpecificAsync(
           InvoiceStrategyContext context,
           CancellationToken cancellationToken
       )
       {
           try
           {
               var spec = new InvoiceDetailByIdWithIncludesSpec(context.CurrentInvoiceDetail.InvoiceDetailId);
               var invoiceDetail = await _invoiceDetailRepository.FirstOrDefaultAsync(spec, cancellationToken);
               if (invoiceDetail != null)
               {
                   context.FetchedInvoiceDetailEntity = invoiceDetail;
               }
               else
               {
                   _logger.LogWarning($"InvoiceDetail with ID {context.CurrentInvoiceDetail.InvoiceDetailId} not found.");
                   context.FetchedInvoiceDetailEntity = new NullInvoiceDetail();
               }
               return Right<Exception, Unit>(Unit.Default);
           }
           catch (Exception ex)
           {
               _logger.LogError(
                   ex,
                   $"Error occurred while fetching invoiceDetail by ID {context.CurrentInvoiceDetail.InvoiceDetailId}"  	 
               );
               return Left<Exception, Unit>(ex);
           }
       }
   }
 	 
   public interface IInvoiceDetail
   {
       Guid InvoiceDetailId { get; }
   }
 	 
   public class InvoiceDetailProxy : IInvoiceDetail
   {
       private readonly InvoiceDetail _invoiceDetail;
 	 
       public InvoiceDetailProxy(InvoiceDetail invoiceDetail)
       {
           _invoiceDetail = invoiceDetail;
       }
 	 
       public Guid InvoiceDetailId => _invoiceDetail?.InvoiceDetailId ?? Guid.Empty;
   }
 	 
   public class NullInvoiceDetail : IInvoiceDetail
   {
       public Guid InvoiceDetailId => Guid.Empty;
   }
 	 
   public class NullInvoiceDetailDto : IInvoiceDetail
   {
       public Guid InvoiceDetailId => Guid.Empty;
   }
   public interface IInvoiceStrategyContext
   {
       Invoice CurrentInvoice { get; set; }
       Guid OperationId { get; }
       Either<NullProduct, Product> FetchedProductEntity { get; set; }
       Either<NullProductDto, ProductDto> FetchedProductDto { get; set; }
       Product CurrentProduct { get; set; }
       CreateProductRequest ProductRequest { get; set; }
       Either<NullInvoiceDetail, InvoiceDetail> FetchedInvoiceDetailEntity { get; set; }
       Either<NullInvoiceDetailDto, InvoiceDetailDto> FetchedInvoiceDetailDto { get; set; }
       InvoiceDetail CurrentInvoiceDetail { get; set; }
       CreateInvoiceDetailRequest InvoiceDetailRequest { get; set; }
       CreateInvoiceRequest InvoiceRequest { get; set; }
    }
   public class InvoiceStrategyContext : IInvoiceStrategyContext
   {
       public Invoice CurrentInvoice { get; set; }
  
       public Guid OperationId { get; private set; }
       public Either<NullProduct, Product> FetchedProductEntity { get; set; }
       public Either<NullProductDto, ProductDto> FetchedProductDto { get; set; }
       public Product CurrentProduct { get; set; }
       public CreateProductRequest ProductRequest { get; set; }
       public Either<NullInvoiceDetail, InvoiceDetail> FetchedInvoiceDetailEntity { get; set; }
       public Either<NullInvoiceDetailDto, InvoiceDetailDto> FetchedInvoiceDetailDto { get; set; }
       public InvoiceDetail CurrentInvoiceDetail { get; set; }
       public CreateInvoiceDetailRequest InvoiceDetailRequest { get; set; }
       public CreateInvoiceRequest InvoiceRequest { get; set; }
   }
 	 
   public class RegistrationInvoiceDataStrategy : IRegistrationInvoiceDataStrategy
   {
       private readonly AddProductCommand _addProductCommand;
       private readonly AddInvoiceDetailCommand _addInvoiceDetailCommand;
       private readonly IRepository<Invoice> _invoiceRepository;
 	 
       public RegistrationInvoiceDataStrategy(
           IRepository<Invoice> invoiceRepository,
           AddProductCommand addProductCommand,
           AddInvoiceDetailCommand addInvoiceDetailCommand        )
       {
           _invoiceRepository = invoiceRepository;
           _addProductCommand = addProductCommand;
           _addInvoiceDetailCommand = addInvoiceDetailCommand;
       }
 	 
       public async Task UpdateInvoiceAsync(
           IAppLoggerService<UpdateInvoiceStrategyDataRefactored> logger,
           UpdateInvoiceRegistrationDataStrategyRequest request,
           CancellationToken cancellationToken
       )
       {
           try
           {
               _invoiceRepository.BeginTransaction();
               var spec = new InvoiceByIdWithIncludesSpec(request.InvoiceId);
               var invoiceToUpdate = await _invoiceRepository.FirstOrDefaultAsync(spec,cancellationToken);
               var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
               string jsonInvoice = JsonConvert.SerializeObject(invoiceToUpdate, settings);
               InvoiceDto finalInvoiceDto = JsonConvert.DeserializeObject<InvoiceDto>(jsonInvoice, settings);
               if (invoiceToUpdate != null)
               {
                   InvoiceStrategyContext context = new InvoiceStrategyContext
                   {
                   //await ExecuteStrategies(context, cancellationToken);
                       ProductRequest = request.CreateProductRequest,
                       InvoiceDetailRequest = request.CreateInvoiceDetailRequest,
                   };
                   var updateSequenceTask =
                       from productResult in _addProductCommand.ExecuteInvoiceRegistrationAsync(context, cancellationToken)
                       from invoiceDetailResult in _addInvoiceDetailCommand.ExecuteInvoiceRegistrationAsync(context, cancellationToken)
                       select invoiceDetailResult;
                   var updateSequence = await updateSequenceTask;
 	 
                   updateSequence.Match(
                       Right: r => { /* handle right value (success case) */ },
                       Left: ex => {
                           _invoiceRepository.RollbackTransaction();
                           logger.LogError(ex.Message, "An error occurred in update sequence");
                           throw ex;
                       }
                   );
 	 
 	 
                   if (updateSequence.IsRight)
                   {
                   invoiceToUpdate.SetAccountName(request.CreateInvoiceRequest.AccountName);
                   invoiceToUpdate.SetCustomerName(request.CreateInvoiceRequest.CustomerName);
                   invoiceToUpdate.SetInternalComments(request.CreateInvoiceRequest.InternalComments);
                   invoiceToUpdate.SetInvoicingNote(request.CreateInvoiceRequest.InvoicingNote);
                   invoiceToUpdate.SetAccountId(request.CreateInvoiceRequest.AccountId);
                   await _invoiceRepository.UpdateAsync(invoiceToUpdate, cancellationToken);
                   _invoiceRepository.CommitTransaction();
                   }
               }
               else
               {
                   var errorMsg = $"Invoice with ID {request.InvoiceId} not found.";
                   logger.LogWarning(errorMsg);
               }
           }
           catch (Exception ex )
          {
              var errorMsg = $"Invoice with ID {request.InvoiceId}  throw Exception {ex.Message}";
              logger.LogError(errorMsg);
               _invoiceRepository.RollbackTransaction();
              throw;
          }
       }
   }
   public class ProductEntityBuilder
   {
       private Guid productId;
       private string productName;
       private string? productDescription;
       private decimal productUnitPrice;
       private bool productIsActive;
       private int productMinimumCharacters;
       private int productMinimumCallMinutes;
       private decimal productChargeRatePerCharacter;
       private decimal productChargeRateCallPerSecond;
       private bool? isDeleted;
       private System.Guid tenantId;
       public Product Build()
       {
       // The builders are responsible for constructing the object, and Factories are for abstracting the creation logic. While both might have validation logic, their responsibilities are different.
       // The Factory is responsible for object creation, and the Builder is responsible for object construction. Ideally, each should validate its own parameters to adhere to SRP.
           _ = Guard.Against.NullOrEmpty(productId, nameof(productId));
           _ = Guard.Against.NullOrWhiteSpace(productName, nameof(productName));
           _ = Guard.Against.Negative(productUnitPrice, nameof(productUnitPrice));
           _ = Guard.Against.Null(productIsActive, nameof(productIsActive));
           _ = Guard.Against.NegativeOrZero(productMinimumCharacters, nameof(productMinimumCharacters));
           _ = Guard.Against.NegativeOrZero(productMinimumCallMinutes, nameof(productMinimumCallMinutes));
           _ = Guard.Against.Negative(productChargeRatePerCharacter, nameof(productChargeRatePerCharacter));
           _ = Guard.Against.Negative(productChargeRateCallPerSecond, nameof(productChargeRateCallPerSecond));
           _ = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
           return new Product(
               productId: productId,
               productName: productName,
               productDescription: productDescription,
               productUnitPrice: productUnitPrice,
               productIsActive: productIsActive,
               productMinimumCharacters: productMinimumCharacters,
               productMinimumCallMinutes: productMinimumCallMinutes,
               productChargeRatePerCharacter: productChargeRatePerCharacter,
               productChargeRateCallPerSecond: productChargeRateCallPerSecond,
               isDeleted: isDeleted,
               tenantId: tenantId                );
       }
       public ProductEntityBuilder WithProductId(Guid productIdParam)
       {
           productId = productIdParam;
           return this;
       }
       public ProductEntityBuilder WithProductName(string productNameParam)
       {
           productName = productNameParam;
           return this;
       }
       public ProductEntityBuilder WithProductDescription(string? productDescriptionParam)
       {
           productDescription = productDescriptionParam;
           return this;
       }
       public ProductEntityBuilder WithProductUnitPrice(decimal productUnitPriceParam)
       {
           productUnitPrice = productUnitPriceParam;
           return this;
       }
       public ProductEntityBuilder WithProductIsActive(bool productIsActiveParam)
       {
           productIsActive = productIsActiveParam;
           return this;
       }
       public ProductEntityBuilder WithProductMinimumCharacters(int productMinimumCharactersParam)
       {
           productMinimumCharacters = productMinimumCharactersParam;
           return this;
       }
       public ProductEntityBuilder WithProductMinimumCallMinutes(int productMinimumCallMinutesParam)
       {
           productMinimumCallMinutes = productMinimumCallMinutesParam;
           return this;
       }
       public ProductEntityBuilder WithProductChargeRatePerCharacter(decimal productChargeRatePerCharacterParam)
       {
           productChargeRatePerCharacter = productChargeRatePerCharacterParam;
           return this;
       }
       public ProductEntityBuilder WithProductChargeRateCallPerSecond(decimal productChargeRateCallPerSecondParam)
       {
           productChargeRateCallPerSecond = productChargeRateCallPerSecondParam;
           return this;
       }
       public ProductEntityBuilder WithIsDeleted(bool? isDeletedParam)
       {
           isDeleted = isDeletedParam;
           return this;
       }
       public ProductEntityBuilder WithTenantId(Guid tenantIdParam)
       {
           tenantId = tenantIdParam;
           return this;
       }
   }
   public class InvoiceDetailEntityBuilder
   {
       private Guid invoiceDetailId;
       private Guid invoiceId;
       private Guid productId;
       private System.Guid tenantId;
       private decimal quantity;
       private string productName;
       private decimal? unitPrice;
       private decimal? lineSale;
       private decimal? lineTax;
       private decimal? lineDiscount;
       public InvoiceDetail Build()
       {
           _ = Guard.Against.NullOrEmpty(invoiceDetailId, nameof(invoiceDetailId));
           _ = Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));
           _ = Guard.Against.NullOrEmpty(productId, nameof(productId));
           _ = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
           _ = Guard.Against.Negative(quantity, nameof(quantity));
           _ = Guard.Against.NullOrWhiteSpace(productName, nameof(productName));
           return new InvoiceDetail(
               invoiceDetailId: invoiceDetailId,
               invoiceId: invoiceId,
               productId: productId,
               tenantId: tenantId,
               quantity: quantity,
               productName: productName,
               unitPrice: unitPrice,
               lineSale: lineSale,
               lineTax: lineTax,
               lineDiscount: lineDiscount                );
       }
       public InvoiceDetailEntityBuilder WithInvoiceDetailId(Guid invoiceDetailIdParam)
       {
           invoiceDetailId = invoiceDetailIdParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithInvoiceId(Guid invoiceIdParam)
       {
           invoiceId = invoiceIdParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithProductId(Guid productIdParam)
       {
           productId = productIdParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithTenantId(Guid tenantIdParam)
       {
           tenantId = tenantIdParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithQuantity(decimal quantityParam)
       {
           quantity = quantityParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithProductName(string productNameParam)
       {
           productName = productNameParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithUnitPrice(decimal? unitPriceParam)
       {
           unitPrice = unitPriceParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithLineSale(decimal? lineSaleParam)
       {
           lineSale = lineSaleParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithLineTax(decimal? lineTaxParam)
       {
           lineTax = lineTaxParam;
           return this;
       }
       public InvoiceDetailEntityBuilder WithLineDiscount(decimal? lineDiscountParam)
       {
           lineDiscount = lineDiscountParam;
           return this;
       }
   }

    public class InvoiceEntityBuilder
    {
        private Guid invoiceId;
        private Guid accountId;
        private int invoiceNumber;
        private string accountName;
        private string customerName;
        private int paymentState;
        private string? internalComments;
        private DateTime? invoicedDate;
        private string invoicingNote;
        private decimal? totalSale;
        private decimal? totalSaleTax;
        private System.Guid tenantId;
        public Invoice Build()
        {
            // The builders are responsible for constructing the object, and Factories are for abstracting the creation logic. While both might have validation logic, their responsibilities are different.
            // The Factory is responsible for object creation, and the Builder is responsible for object construction. Ideally, each should validate its own parameters to adhere to SRP.
            _ = Guard.Against.NullOrEmpty(invoiceId, nameof(invoiceId));
            _ = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            _ = Guard.Against.NegativeOrZero(invoiceNumber, nameof(invoiceNumber));
            _ = Guard.Against.NullOrWhiteSpace(accountName, nameof(accountName));
            _ = Guard.Against.NullOrWhiteSpace(customerName, nameof(customerName));
            _ = Guard.Against.NegativeOrZero(paymentState, nameof(paymentState));
            _ = Guard.Against.NullOrWhiteSpace(invoicingNote, nameof(invoicingNote));
            _ = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
            return new Invoice(
                invoiceId: invoiceId,
                accountId: accountId,
                invoiceNumber: invoiceNumber,
                accountName: accountName,
                customerName: customerName,
                paymentState: paymentState,
                internalComments: internalComments,
                invoicedDate: invoicedDate,
                invoicingNote: invoicingNote,
                totalSale: totalSale,
                totalSaleTax: totalSaleTax,
                tenantId: tenantId);
        }
        public InvoiceEntityBuilder WithInvoiceId(Guid invoiceIdParam)
        {
            invoiceId = invoiceIdParam;
            return this;
        }
        public InvoiceEntityBuilder WithAccountId(Guid accountIdParam)
        {
            accountId = accountIdParam;
            return this;
        }
        public InvoiceEntityBuilder WithInvoiceNumber(int invoiceNumberParam)
        {
            invoiceNumber = invoiceNumberParam;
            return this;
        }
        public InvoiceEntityBuilder WithAccountName(string accountNameParam)
        {
            accountName = accountNameParam;
            return this;
        }
        public InvoiceEntityBuilder WithCustomerName(string customerNameParam)
        {
            customerName = customerNameParam;
            return this;
        }
        public InvoiceEntityBuilder WithPaymentState(int paymentStateParam)
        {
            paymentState = paymentStateParam;
            return this;
        }
        public InvoiceEntityBuilder WithInternalComments(string? internalCommentsParam)
        {
            internalComments = internalCommentsParam;
            return this;
        }
        public InvoiceEntityBuilder WithInvoicedDate(DateTime? invoicedDateParam)
        {
            invoicedDate = invoicedDateParam;
            return this;
        }
        public InvoiceEntityBuilder WithInvoicingNote(string invoicingNoteParam)
        {
            invoicingNote = invoicingNoteParam;
            return this;
        }
        public InvoiceEntityBuilder WithTotalSale(decimal? totalSaleParam)
        {
            totalSale = totalSaleParam;
            return this;
        }
        public InvoiceEntityBuilder WithTotalSaleTax(decimal? totalSaleTaxParam)
        {
            totalSaleTax = totalSaleTaxParam;
            return this;
        }
        public InvoiceEntityBuilder WithTenantId(Guid tenantIdParam)
        {
            tenantId = tenantIdParam;
            return this;
        }
    }

    public class UpdateInvoiceRegistrationDataStrategyRequest : BaseRequest
   {
      public CreateInvoiceRequest CreateInvoiceRequest { get; set; }
      public CreateProductRequest CreateProductRequest { get; set; }
      public CreateInvoiceDetailRequest CreateInvoiceDetailRequest { get; set; }
 public System.Guid InvoiceId { get; set; }
	}
   public class UpdateInvoiceRegistrationDataResponse : BaseResponse
   {
       public UpdateInvoiceRegistrationDataResponse(Guid correlationId)
           : base(correlationId)
       {
       }
       public UpdateInvoiceRegistrationDataResponse()
       {
       }
  
   }
}
