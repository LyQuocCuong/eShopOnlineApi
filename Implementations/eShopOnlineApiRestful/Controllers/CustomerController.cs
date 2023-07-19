namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CustomerController : AbstractApiController
    {
        public CustomerController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("customers")]
        public IActionResult GetAll()
        {
            IEnumerable<CustomerDto> employeeDto = _services.Customer.GetAll();
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("customers/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            CustomerDto? employeeDto = _services.Customer.GetById(id);
            if (employeeDto == null)
            {
                return NotFound();
            }
            return Ok(employeeDto);
        }

    }
}
