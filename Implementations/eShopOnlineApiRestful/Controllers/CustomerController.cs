namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CustomerController : AbstractApiController
    {
        protected override string ChildClassName => nameof(CustomerController);

        public CustomerController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("customers", Name = "GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            LogInfoRequest();

            LogInfo(nameof(GetAllCustomers), LogMessages.MessageForExecutingMethod);
            IEnumerable<CustomerDto> employeeDto = _services.Customer.GetAll();

            LogInfoResponse();
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("customers/{id:guid}", Name = "GetCustomerById")]
        public IActionResult GetCustomerById([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(GetCustomerById), LogMessages.MessageForExecutingMethod);
            CustomerDto? employeeDto = _services.Customer.GetById(id);
            if (employeeDto == null)
            {
                LogInfoResponse(); 
                return NotFound();
            }

            LogInfoResponse(); 
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("customers", Name = "CreateCustomer")]
        public IActionResult CreateCustomer([FromBody]CustomerForCreationDto creationDto)
        {
            LogInfoRequest();

            LogInfo(nameof(CreateCustomer), LogMessages.MessageForExecutingMethod);
            CustomerDto customerDto = _services.Customer.Create(creationDto);

            LogInfoResponse(); 
            return CreatedAtRoute("GetCustomerById", new { id = customerDto.Id }, customerDto);
        }

        [HttpPut]
        [Route("customers/{id:guid}", Name = "UpdateCustomerFully")]
        public IActionResult UpdateCustomerFully([FromRoute]Guid id, [FromBody]CustomerForUpdateDto updateDto)
        {
            LogInfoRequest();

            LogInfo(nameof(UpdateCustomerFully), LogMessages.MessageForExecutingMethod);
            if (_services.Customer.IsValidId(id) == false)
            {
                LogInfoResponse();
                return NotFound();
            }
            bool result = _services.Customer.UpdateFully(id, updateDto);

            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("customers/{id:guid}", Name = "DeleteCustomerSoftly")]
        public IActionResult DeleteCustomerSoftly([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteCustomerSoftly), LogMessages.MessageForExecutingMethod);
            bool result = _services.Customer.DeleteSoftly(id);
            if (result == false)
            {
                LogInfoResponse();
                return BadRequest();
            }

            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/customers/{id:guid}", Name = "DeleteCustomerHard")]
        public IActionResult DeleteCustomerHard([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteCustomerHard), LogMessages.MessageForExecutingMethod);
            bool result = _services.Customer.DeleteHard(id);
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
