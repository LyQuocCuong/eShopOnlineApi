namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CustomerController : AbstractApiController
    {
        protected override string ClassName => nameof(CustomerController);

        public CustomerController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("customers", Name = "GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetAllCustomers));
            IEnumerable<CustomerDto> employeeDto = _services.Customer.GetAll();

            LogResponseInfo();
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("customers/{id:guid}", Name = "GetCustomerById")]
        public IActionResult GetCustomerById([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetCustomerById));
            CustomerDto? employeeDto = _services.Customer.GetById(id);
            if (employeeDto == null)
            {
                LogResponseInfo(); 
                return NotFound();
            }

            LogResponseInfo(); 
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("customers", Name = "CreateCustomer")]
        public IActionResult CreateCustomer([FromBody]CustomerForCreationDto creationDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(CreateCustomer));
            CustomerDto customerDto = _services.Customer.Create(creationDto);

            LogResponseInfo(); 
            return CreatedAtRoute("GetCustomerById", new { id = customerDto.Id }, customerDto);
        }

        [HttpPut]
        [Route("customers/{id:guid}", Name = "UpdateCustomerFully")]
        public IActionResult UpdateCustomerFully([FromRoute]Guid id, [FromBody]CustomerForUpdateDto updateDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(UpdateCustomerFully));
            if (_services.Customer.IsValidId(id) == false)
            {
                LogResponseInfo();
                return NotFound();
            }
            bool result = _services.Customer.UpdateFully(id, updateDto);

            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("customers/{id:guid}", Name = "DeleteCustomerSoftly")]
        public IActionResult DeleteCustomerSoftly([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteCustomerSoftly));
            bool result = _services.Customer.DeleteSoftly(id);
            if (result == false)
            {
                LogResponseInfo();
                return BadRequest();
            }

            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/customers/{id:guid}", Name = "DeleteCustomerHard")]
        public IActionResult DeleteCustomerHard([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteCustomerHard));
            bool result = _services.Customer.DeleteHard(id);
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
