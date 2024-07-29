using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerlessRESTProductsTrigger.Infrastructure;
using ServerlessRESTProductsTrigger.Model;

namespace ServerlessRESTProductsTrigger.Functions
{
    public class ProductsGetByIdUpdateDelete
    {
        private readonly ILogger<ProductsGetByIdUpdateDelete> _logger;
        private readonly ConnectionContext _connectionContext;

        public ProductsGetByIdUpdateDelete(ILogger<ProductsGetByIdUpdateDelete> logger, ConnectionContext connectionContext)
        {
            _logger = logger;
            _connectionContext = connectionContext;
        }

        [Function("ProductsGetByIdUpdateDelete")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous,
            "get", "put", "delete", Route = "products/{id}")] HttpRequest req,
            int id)
        {

            if (req.Method == HttpMethods.Get)
            {
                var product = await _connectionContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null) return new NotFoundResult();

                return new OkObjectResult(product);
            }
            else if (req.Method == HttpMethods.Put)
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

                var productOld = await _connectionContext.Products.FindAsync(id);

                if (productOld == null)
                {
                    return new NotFoundObjectResult(new { message = "Product not found" });
                }

                productOld.Name = viewModel.Name;
                productOld.Description = viewModel.Description;
                productOld.Price = viewModel.Price;
                productOld.Sku = viewModel.Sku;

               
                _connectionContext.Products.Update(productOld);
                await _connectionContext.SaveChangesAsync();

                return new OkObjectResult(productOld);

            }
            else
            {
                var product = await _connectionContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null) return new NotFoundResult();

                _connectionContext.Products.Remove(product);
                await _connectionContext.SaveChangesAsync();

                return new NoContentResult();
            }
        }
    }
}
