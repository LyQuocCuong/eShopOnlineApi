﻿namespace eShopOnlineApiRestful.Controllers
{
    public sealed class StoreController : AbstractApiController<StoreController>
    {
        public StoreController(ILogger<StoreController> logger, 
                               ControllerParams controllerParams) 
            : base(logger, controllerParams)
        {
        }

        [HttpGet]
        [Route("stores", Name = "GetAllStores")]
        public IActionResult GetAllStores()
        {
            IEnumerable<StoreDto> storeDtos = _services.Store.GetAll();
            return Ok(storeDtos);
        }

        [HttpGet]
        [Route("stores/{id:guid}", Name = "GetStoreById")]
        public IActionResult GetStoreById([FromRoute]Guid id)
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

        [HttpPut]
        [Route("stores/{id:guid}", Name = "UpdateStoreFully")]
        public IActionResult UpdateStoreFully([FromRoute]Guid id, [FromBody]StoreForUpdateDto updateDto)
        {
            if (_services.Store.IsValidId(id) == false)
            {
                return NotFound();
            }
            bool result = _services.Store.UpdateFully(id, updateDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("stores/{id:guid}", Name = "DeleteStoreSoftly")]
        public IActionResult DeleteStoreSoftly([FromRoute]Guid id)
        {
            bool result = _services.Store.DeleteSoftly(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/stores/{id:guid}", Name = "DeleteStoreHard")]
        public IActionResult DeleteStoreHard([FromRoute] Guid id)
        {
            bool result = _services.Store.DeleteHard(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
