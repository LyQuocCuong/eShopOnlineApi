namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CustomerController : AbstractApiController<CustomerController>
    {
        public CustomerController(ILogger<CustomerController> logger, 
                                  ControllerParams controllerParams) 
            : base(logger, controllerParams)
        {
        }

        [HttpGet]
        [Route("customers", Name = nameof(GetAllCustomersAsync))]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            IEnumerable<CustomerDto> employeeDto = await _services.Customer.GetAllAsync();
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("customers/{id:guid}", Name = nameof(GetCustomerByIdAsync))]
        public async Task<IActionResult> GetCustomerByIdAsync([FromRoute]Guid id)
        {
            CustomerDto? employeeDto = await _services.Customer.GetByIdAsync(id);
            if (employeeDto == null)
            {
                return NotFound();
            }
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("customers", Name = nameof(CreateCustomerAsync))]
        public async Task<IActionResult> CreateCustomerAsync(
            [FromBody]CustomerForCreationDto? creationDto,
            [FromServices]IValidator<CustomerForCreationDto> creationDtoValidator)
        {
            if (creationDto == null)
            {
                return BadRequest("Obj is NULL");
            }

            ValidationResult validationResult = await creationDtoValidator.ValidateAsync(creationDto);
            if (validationResult.IsValid == false)
            {
                validationResult.AddErrorsToModelStateObj(this.ModelState);
                return UnprocessableEntity(this.ModelState);
            }

            CustomerDto customerDto = await _services.Customer.CreateAsync(creationDto);
            return CreatedAtRoute(nameof(GetCustomerByIdAsync), new { id = customerDto.Id }, customerDto);
        }

        [HttpPut]
        [Route("customers/{id:guid}", Name = nameof(UpdateCustomerFullyAsync))]
        public async Task<IActionResult> UpdateCustomerFullyAsync(
            [FromRoute]Guid id, 
            [FromBody]CustomerForUpdateDto? updateDto,
            [FromServices]IValidator<CustomerForUpdateDto> updateDtoValidator)
        {
            if (updateDto == null)
            {
                return BadRequest("Obj is NULL");
            }

            ValidationResult validationResult = await updateDtoValidator.ValidateAsync(updateDto);
            if (validationResult.IsValid == false)
            {
                validationResult.AddErrorsToModelStateObj(this.ModelState);
                return UnprocessableEntity(this.ModelState);
            }

            if (await _services.Customer.IsValidIdAsync(id) == false)
            {
                return NotFound();
            }
            bool result = await _services.Customer.UpdateFullyAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("customers/{id:guid}", Name = nameof(DeleteCustomerSoftlyAsync))]
        public async Task<IActionResult> DeleteCustomerSoftlyAsync([FromRoute]Guid id)
        {
            bool result = await _services.Customer.DeleteSoftlyAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/customers/{id:guid}", Name = nameof(DeleteCustomerHardAsync))]
        public async Task<IActionResult> DeleteCustomerHardAsync([FromRoute]Guid id)
        {
            bool result = await _services.Customer.DeleteHardAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
