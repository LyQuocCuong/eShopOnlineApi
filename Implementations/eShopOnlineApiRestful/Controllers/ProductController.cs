namespace eShopOnlineApiRestful.Controllers
{
    public sealed class ProductController : AbstractApiController<ProductController>
    {
        public ProductController(ILogger<ProductController> logger,
                                 ControllerParams controllerParams) 
            : base(logger, controllerParams)
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

        [HttpPut]
        [Route("products/{id:guid}", Name = "UpdateProductFully")]
        public IActionResult UpdateProductFully([FromRoute]Guid id, [FromBody]ProductForUpdateDto updateDto)
        {
            if (_services.Product.IsValidId(id) == false)
            {
                return NotFound();
            }
            bool result = _services.Product.UpdateFully(id, updateDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("products/{id:guid}", Name = "DeleteProductSoftly")]
        public IActionResult DeleteProductSoftly([FromRoute]Guid id)
        {
            bool result = _services.Product.DeleteSoftly(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/products/{id:guid}", Name = "DeleteProductHard")]
        public IActionResult DeleteProductHard([FromRoute] Guid id)
        {
            bool result = _services.Product.DeleteHard(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
