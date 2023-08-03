namespace eShopOnlineApiRestful.Controllers
{
    public sealed class StoreController : AbstractApiController<StoreController>
    {
        protected override string ClassName => nameof(StoreController);

        public StoreController(ILogger<StoreController> logger, 
                               ControllerParams controllerParams) 
            : base(logger, controllerParams)
        {
        }

        [HttpGet]
        [Route("stores", Name = "GetAllStores")]
        public IActionResult GetAllStores()
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetAllStores));
            IEnumerable<StoreDto> storeDtos = _services.Store.GetAll();

            LogResponseInfo();
            return Ok(storeDtos);
        }

        [HttpGet]
        [Route("stores/{id:guid}", Name = "GetStoreById")]
        public IActionResult GetStoreById([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetStoreById));
            StoreDto? storeDto = _services.Store.GetById(id);
            if (storeDto == null)
            {
                LogResponseInfo();
                return NotFound();
            }
            LogResponseInfo();
            return Ok(storeDto);
        }

        [HttpPost]
        [Route("stores", Name = "CreateStore")]
        public IActionResult CreateStore([FromBody]StoreForCreationDto creationDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(CreateStore));
            StoreDto storeDto = _services.Store.Create(creationDto);

            LogResponseInfo();
            return CreatedAtRoute("GetStoreById", new { id = storeDto.Id }, storeDto);
        }

        [HttpPut]
        [Route("stores/{id:guid}", Name = "UpdateStoreFully")]
        public IActionResult UpdateStoreFully([FromRoute]Guid id, [FromBody]StoreForUpdateDto updateDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(UpdateStoreFully));
            if (_services.Store.IsValidId(id) == false)
            {
                LogResponseInfo();
                return NotFound();
            }
            bool result = _services.Store.UpdateFully(id, updateDto);

            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("stores/{id:guid}", Name = "DeleteStoreSoftly")]
        public IActionResult DeleteStoreSoftly([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteStoreSoftly));
            bool result = _services.Store.DeleteSoftly(id);
            if (result == false)
            {
                LogResponseInfo();
                return BadRequest();
            }
            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/stores/{id:guid}", Name = "DeleteStoreHard")]
        public IActionResult DeleteStoreHard([FromRoute] Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteStoreHard));
            bool result = _services.Store.DeleteHard(id);
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
