using Shared.DTOs.Inputs.FromBody.CreationDtos;

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
        [Route("stores/{id:guid}", Name = "GetStoreById")]
        public IActionResult GetById(Guid id)
        {
            StoreDto? storeDto = _services.Store.GetById(id);
            if (storeDto == null)
            {
                return NotFound();
            }
            return Ok(storeDto);
        }

        [HttpPost]
        [Route("stores", Name = "CreateStore")]
        public IActionResult CreateStore([FromBody]StoreForCreationDto creationDto)
        {
            StoreDto storeDto = _services.Store.Create(creationDto);
            return CreatedAtRoute("GetStoreById", new { id = storeDto.Id }, storeDto);
        }

    }
}
