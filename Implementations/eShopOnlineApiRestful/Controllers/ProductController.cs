namespace eShopOnlineApiRestful.Controllers
{
    public sealed class ProductController : AbstractApiController
    {
        protected override string ClassName => nameof(ProductController);

        public ProductController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("products", Name = "GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetAllProducts));
            IEnumerable<ProductDto> productDtos = _services.Product.GetAll();

            LogResponseInfo();
            return Ok(productDtos);
        }

        [HttpGet]
        [Route("products/{id:guid}", Name = "GetProductById")]
        public IActionResult GetProductById([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetProductById));
            ProductDto? productDto = _services.Product.GetById(id);
            if (productDto == null)
            {
                LogResponseInfo();
                return NotFound();
            }
            LogResponseInfo();
            return Ok(productDto);
        }

        [HttpPost]
        [Route("products", Name = "CreateProduct")]
        public IActionResult CreateProduct([FromBody]ProductForCreationDto creationDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(CreateProduct));
            ProductDto productDto = _services.Product.Create(creationDto);

            LogResponseInfo();
            return CreatedAtRoute("GetProductById", new { id = productDto.Id }, productDto);
        }

        [HttpPut]
        [Route("products/{id:guid}", Name = "UpdateProductFully")]
        public IActionResult UpdateProductFully([FromRoute]Guid id, [FromBody]ProductForUpdateDto updateDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(UpdateProductFully));
            if (_services.Product.IsValidId(id) == false)
            {
                LogResponseInfo();
                return NotFound();
            }
            bool result = _services.Product.UpdateFully(id, updateDto);

            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("products/{id:guid}", Name = "DeleteProductSoftly")]
        public IActionResult DeleteProductSoftly([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteProductSoftly));
            bool result = _services.Product.DeleteSoftly(id);
            if (result == false)
            {
                LogResponseInfo();
                return BadRequest();
            }
            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/products/{id:guid}", Name = "DeleteProductHard")]
        public IActionResult DeleteProductHard([FromRoute] Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteProductHard));
            bool result = _services.Product.DeleteHard(id);
            if (result == false)
            {
                LogResponseInfo();
                return BadRequest();
            }
            LogResponseInfo();
            return NoContent();
        }

    }
}
