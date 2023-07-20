namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CustomerController : AbstractApiController
    {
        public CustomerController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("customers", Name = "GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            IEnumerable<CustomerDto> employeeDto = _services.Customer.GetAll();
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("customers/{id:guid}", Name = "GetCustomerById")]
        public IActionResult GetCustomerById([FromRoute]Guid id)
        {
            CustomerDto? employeeDto = _services.Customer.GetById(id);
            if (employeeDto == null)
            {
                return NotFound();
            }
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("customers", Name = "CreateCustomer")]
        public IActionResult CreateCustomer([FromBody]CustomerForCreationDto creationDto)
        {
            CustomerDto customerDto = _services.Customer.Create(creationDto);
            return CreatedAtRoute("GetCustomerById", new { id = customerDto.Id }, customerDto);
        }

        [HttpPut]
        [Route("customers/{id:guid}", Name = "UpdateCustomerFully")]
        public IActionResult UpdateCustomerFully([FromRoute]Guid id, [FromBody]CustomerForUpdateDto updateDto)
        {
            if (_services.Customer.IsValidId(id) == false)
            {
                return NotFound();
            }
            bool result = _services.Customer.UpdateFully(id, updateDto);
            return NoContent();
        }

    }
}
