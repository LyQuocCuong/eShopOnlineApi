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
        [Route("products", Name = "GetAllProductsAsync")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            IEnumerable<ProductDto> productDtos = await _services.Product.GetAllAsync();
            return Ok(productDtos);
        }

        [HttpGet]
        [Route("products/{id:guid}", Name = "GetProductByIdAsync")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute]Guid id)
        {
            ProductDto? productDto = await _services.Product.GetByIdAsync(id);
            if (productDto == null)
            {
                return NotFound();
            }
            return Ok(productDto);
        }

        [HttpPost]
        [Route("products", Name = "CreateProductAsync")]
        public async Task<IActionResult> CreateProductAsync([FromBody]ProductForCreationDto creationDto)
        {
            ProductDto productDto = await _services.Product.CreateAsync(creationDto);
            return CreatedAtRoute("GetProductById", new { id = productDto.Id }, productDto);
        }

        [HttpPut]
        [Route("products/{id:guid}", Name = "UpdateProductFullyAsync")]
        public async Task<IActionResult> UpdateProductFullyAsync([FromRoute]Guid id, [FromBody]ProductForUpdateDto updateDto)
        {
            if (await _services.Product.IsValidIdAsync(id) == false)
            {
                return NotFound();
            }
            bool result = await _services.Product.UpdateFullyAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("products/{id:guid}", Name = "DeleteProductSoftlyAsync")]
        public async Task<IActionResult> DeleteProductSoftlyAsync([FromRoute]Guid id)
        {
            bool result = await _services.Product.DeleteSoftlyAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/products/{id:guid}", Name = "DeleteProductHardAsync")]
        public async Task<IActionResult> DeleteProductHardAsync([FromRoute] Guid id)
        {
            bool result = await _services.Product.DeleteHardAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
