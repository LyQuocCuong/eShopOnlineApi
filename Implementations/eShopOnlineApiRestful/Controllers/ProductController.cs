namespace eShopOnlineApiRestful.Controllers
{
    public sealed class ProductController : AbstractApiController
    {
        protected override string ChildClassName => nameof(ProductController);

        public ProductController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("products", Name = "GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            LogInfoRequest();

            LogInfo(nameof(GetAllProducts), LogMessages.MessageForExecutingMethod);
            IEnumerable<ProductDto> productDtos = _services.Product.GetAll();

            LogInfoResponse();
            return Ok(productDtos);
        }

        [HttpGet]
        [Route("products/{id:guid}", Name = "GetProductById")]
        public IActionResult GetProductById([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(GetProductById), LogMessages.MessageForExecutingMethod);
            ProductDto? productDto = _services.Product.GetById(id);
            if (productDto == null)
            {
                LogInfoResponse();
                return NotFound();
            }
            LogInfoResponse();
            return Ok(productDto);
        }

        [HttpPost]
        [Route("products", Name = "CreateProduct")]
        public IActionResult CreateProduct([FromBody]ProductForCreationDto creationDto)
        {
            LogInfoRequest();

            LogInfo(nameof(CreateProduct), LogMessages.MessageForExecutingMethod);
            ProductDto productDto = _services.Product.Create(creationDto);

            LogInfoResponse();
            return CreatedAtRoute("GetProductById", new { id = productDto.Id }, productDto);
        }

        [HttpPut]
        [Route("products/{id:guid}", Name = "UpdateProductFully")]
        public IActionResult UpdateProductFully([FromRoute]Guid id, [FromBody]ProductForUpdateDto updateDto)
        {
            LogInfoRequest();

            LogInfo(nameof(UpdateProductFully), LogMessages.MessageForExecutingMethod);
            if (_services.Product.IsValidId(id) == false)
            {
                LogInfoResponse();
                return NotFound();
            }
            bool result = _services.Product.UpdateFully(id, updateDto);

            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("products/{id:guid}", Name = "DeleteProductSoftly")]
        public IActionResult DeleteProductSoftly([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteProductSoftly), LogMessages.MessageForExecutingMethod);
            bool result = _services.Product.DeleteSoftly(id);
            if (result == false)
            {
                LogInfoResponse();
                return BadRequest();
            }
            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/products/{id:guid}", Name = "DeleteProductHard")]
        public IActionResult DeleteProductHard([FromRoute] Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteProductHard), LogMessages.MessageForExecutingMethod);
            bool result = _services.Product.DeleteHard(id);
            if (result == false)
            {
                LogInfoResponse();
                return BadRequest();
            }
            LogInfoResponse();
            return NoContent();
        }

    }
}
