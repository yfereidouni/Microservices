using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;
using System.Net;
using ZstdSharp.Unsafe;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _repository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpGet("{id:length(24)}", Name = "GetProduct")]
        [Route("[action]/{id}", Name = "GetProduct")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _repository.GetProductById(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id} , not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{categoryName}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string categoryName)
        {
            try
            {
                var products = await _repository.GetProductByCategory(categoryName);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            try
            {
                var products = await _repository.GetProductByName(name);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductVM productVM)
        {
            try
            {
                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Summary = productVM.Summary,
                    ImageFile = productVM.ImageFile,
                    Category = productVM.Category,
                    Price = productVM.Price
                };

                await _repository.Create(product);

                return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                return Ok(await _repository.Update(product));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
