using Catalog.Apis.Entities;
using Catalog.Apis.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Apis.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _ProductRepo;

        public ProductController(IProductRepo ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _ProductRepo.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _ProductRepo.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet("category")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory([FromQuery] string category)
        {
            var products = await _ProductRepo.GetProductByCategory(category);
            return Ok(products);
        }
        [HttpGet("name")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName([FromQuery] string name)
        {
            var products = await _ProductRepo.GetProductByName(name);
            return Ok(products);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateProduct([FromBody] Product product) 
        {
            await _ProductRepo.CreateProduct(product);
            return Created();
            //return CreatedAtRoute("GetProductById", new {id=product.Id},product);
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditProduct([FromBody] Product product) 
        {
            var Existproduct=await _ProductRepo.GetProduct(product.Id);
            if (Existproduct == null)
                return NotFound();
            var res = await _ProductRepo.UpdateProduct(product);
            if(!res)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpDelete("id")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProduct([FromBody] string id) 
        {
            var product=await _ProductRepo.GetProduct(id);
            if (product == null)
                return NotFound();
            var res = await _ProductRepo.DeleteProduct(id);
            if(!res)
                return BadRequest(res);
            return Ok(res);
        }




        // 18
    }
}
