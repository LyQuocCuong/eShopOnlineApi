namespace eShopOnlineApiRestful.Controllers
{
    public sealed class StoreController : AbstractApiController
    {
        public StoreController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("stores")]
        public IActionResult GetAll()
        {
            IEnumerable<StoreDto> storeDtos = _services.Store.GetAll();
            return Ok(storeDtos);
        }

        [HttpGet]
        [Route("stores/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            StoreDto? storeDto = _services.Store.GetById(id);
            if (storeDto == null)
            {
                return NotFound();
            }
            return Ok(storeDto);
        }

    }
}
