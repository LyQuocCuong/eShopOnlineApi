namespace eShopOnlineApiRestful.Controllers
{
    public sealed class StoreController : AbstractApiController<StoreController>
    {
        public StoreController(ILogger<StoreController> logger, 
                               ControllerParams controllerParams) 
            : base(logger, controllerParams)
        {
        }

        [HttpGet]
        [Route("stores", Name = "GetAllStoresAsync")]
        public async Task<IActionResult> GetAllStoresAsync()
        {
            IEnumerable<StoreDto> storeDtos = await _services.Store.GetAllAsync();
            return Ok(storeDtos);
        }

        [HttpGet]
        [Route("stores/{id:guid}", Name = "GetStoreByIdAsync")]
        public async Task<IActionResult> GetStoreByIdAsync([FromRoute]Guid id)
        {
            StoreDto? storeDto = await _services.Store.GetByIdAsync(id);
            if (storeDto == null)
            {
                return NotFound();
            }
            return Ok(storeDto);
        }

        [HttpPost]
        [Route("stores", Name = "CreateStoreAsync")]
        public async Task<IActionResult> CreateStoreAsync([FromBody]StoreForCreationDto creationDto)
        {
            StoreDto storeDto = await _services.Store.CreateAsync(creationDto);
            return CreatedAtRoute("GetStoreById", new { id = storeDto.Id }, storeDto);
        }

        [HttpPut]
        [Route("stores/{id:guid}", Name = "UpdateStoreFullyAsync")]
        public async Task<IActionResult> UpdateStoreFullyAsync([FromRoute]Guid id, [FromBody]StoreForUpdateDto updateDto)
        {
            if (await _services.Store.IsValidIdAsync(id) == false)
            {
                return NotFound();
            }
            bool result = await _services.Store.UpdateFullyAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("stores/{id:guid}", Name = "DeleteStoreSoftlyAsync")]
        public async Task<IActionResult> DeleteStoreSoftlyAsync([FromRoute]Guid id)
        {
            bool result = await _services.Store.DeleteSoftlyAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/stores/{id:guid}", Name = "DeleteStoreHardAsync")]
        public async Task<IActionResult> DeleteStoreHardAsync([FromRoute] Guid id)
        {
            bool result = await _services.Store.DeleteHardAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
