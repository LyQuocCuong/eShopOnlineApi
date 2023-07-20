using Shared.DTOs.Inputs.FromBody.CreationDtos;

namespace eShopOnlineApiRestful.Controllers
{
    public sealed class ProductController : AbstractApiController
    {
        public ProductController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("products", Name = "GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            IEnumerable<ProductDto> productDtos = _services.Product.GetAll();
            return Ok(productDtos);
        }

        [HttpGet]
        [Route("products/{id:guid}", Name = "GetProductById")]
        public IActionResult GetProductById([FromRoute]Guid id)
        {
            ProductDto? productDto = _services.Product.GetById(id);
            if (productDto == null)
            {
                return NotFound();
            }
            return Ok(productDto);
        }

        [HttpPost]
        [Route("products", Name = "CreateProduct")]
        public IActionResult CreateProduct([FromBody]ProductForCreationDto creationDto)
        {
            ProductDto productDto = _services.Product.Create(creationDto);
            return CreatedAtRoute("GetProductById", new { id = productDto.Id }, productDto);
        }

    }
}
