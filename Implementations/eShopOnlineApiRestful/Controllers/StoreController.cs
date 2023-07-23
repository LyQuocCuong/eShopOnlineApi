namespace eShopOnlineApiRestful.Controllers
{
    public sealed class StoreController : AbstractApiController
    {
        protected override string ChildClassName => nameof(StoreController);

        public StoreController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("stores", Name = "GetAllStores")]
        public IActionResult GetAllStores()
        {
            LogInfoRequest();

            LogInfo(nameof(GetAllStores), LogMessages.MessageForExecutingMethod);
            IEnumerable<StoreDto> storeDtos = _services.Store.GetAll();

            LogInfoResponse();
            return Ok(storeDtos);
        }

        [HttpGet]
        [Route("stores/{id:guid}", Name = "GetStoreById")]
        public IActionResult GetStoreById([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(GetStoreById), LogMessages.MessageForExecutingMethod);
            StoreDto? storeDto = _services.Store.GetById(id);
            if (storeDto == null)
            {
                LogInfoResponse();
                return NotFound();
            }
            LogInfoResponse();
            return Ok(storeDto);
        }

        [HttpPost]
        [Route("stores", Name = "CreateStore")]
        public IActionResult CreateStore([FromBody]StoreForCreationDto creationDto)
        {
            LogInfoRequest();

            LogInfo(nameof(CreateStore), LogMessages.MessageForExecutingMethod);
            StoreDto storeDto = _services.Store.Create(creationDto);

            LogInfoResponse();
            return CreatedAtRoute("GetStoreById", new { id = storeDto.Id }, storeDto);
        }

        [HttpPut]
        [Route("stores/{id:guid}", Name = "UpdateStoreFully")]
        public IActionResult UpdateStoreFully([FromRoute]Guid id, [FromBody]StoreForUpdateDto updateDto)
        {
            LogInfoRequest();

            LogInfo(nameof(UpdateStoreFully), LogMessages.MessageForExecutingMethod);
            if (_services.Store.IsValidId(id) == false)
            {
                LogInfoResponse();
                return NotFound();
            }
            bool result = _services.Store.UpdateFully(id, updateDto);

            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("stores/{id:guid}", Name = "DeleteStoreSoftly")]
        public IActionResult DeleteStoreSoftly([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteStoreSoftly), LogMessages.MessageForExecutingMethod);
            bool result = _services.Store.DeleteSoftly(id);
            if (result == false)
            {
                LogInfoResponse();
                return BadRequest();
            }
            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/stores/{id:guid}", Name = "DeleteStoreHard")]
        public IActionResult DeleteStoreHard([FromRoute] Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteStoreHard), LogMessages.MessageForExecutingMethod);
            bool result = _services.Store.DeleteHard(id);
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
