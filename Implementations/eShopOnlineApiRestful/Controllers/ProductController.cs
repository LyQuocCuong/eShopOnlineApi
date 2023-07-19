namespace eShopOnlineApiRestful.Controllers
{
    public sealed class ProductController : AbstractApiController
    {
        public ProductController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetAll()
        {
            IEnumerable<ProductDto> productDtos = _services.Product.GetAll();
            return Ok(productDtos);
        }

        [HttpGet]
        [Route("products/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            ProductDto? productDto = _services.Product.GetById(id);
            if (productDto == null)
            {
                return NotFound();
            }
            return Ok(productDto);
        }

    }
}
