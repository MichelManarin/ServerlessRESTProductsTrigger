using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerlessRESTProductsTrigger.Infrastructure;
using ServerlessRESTProductsTrigger.Model;
using System.ComponentModel.DataAnnotations;

namespace ServerlessRESTProductsTrigger.Functions
{
    public class ProductsGetAllCreate
    {
        private readonly ConnectionContext _connectionContext;
        private readonly ILogger<ProductsGetAllCreate> _logger;

        public ProductsGetAllCreate(ILogger<ProductsGetAllCreate> logger, ConnectionContext connectionContext)
        {
            _logger = logger;
            _connectionContext = connectionContext;
        }

        [Function("ProductsGetAllCreate")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "products")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("ProductsGetAllCreate: C# HTTP trigger function processed a request.");
                if (req.Method == HttpMethods.Post)
                {
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var viewModel = JsonConvert.DeserializeObject<ProductViewModel>(requestBody);

                    if (viewModel == null)
                    {
                        return new BadRequestObjectResult(new { message = "Invalid product data" });
                    }

                    var validationResults = ProductValidator.ValidateProductViewModel(viewModel);
                    if (validationResults.Any())
                    {
                        return new BadRequestObjectResult(new { errors = validationResults });
                    }

                    var product = new Product
                    {
                        Name = viewModel.Name,
                        Price = viewModel.Price,
                        Sku = viewModel.Sku,
                        Description = viewModel.Description,
                        CompanyId = viewModel.CompanyId,
                        StoreId = viewModel.StoreId
                    };

                    _connectionContext.Products.Add(product);
                    await _connectionContext.SaveChangesAsync();
                    return new CreatedResult("/products", product);
                }

                var products = await _connectionContext.Products.ToListAsync();

                return new OkObjectResult(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while request companies");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
