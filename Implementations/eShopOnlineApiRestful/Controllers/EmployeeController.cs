namespace eShopOnlineApiRestful.Controllers
{
    public sealed class EmployeeController : AbstractApiController
    {
        public EmployeeController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("employees")]
        public IActionResult GetAll()
        {
            IEnumerable<EmployeeDto> employeeDtos = _services.Employee.GetAll();
            return Ok(employeeDtos);
        }

        [HttpGet]
        [Route("employees/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            EmployeeDto? employeeDto = _services.Employee.GetById(id);
            if (employeeDto == null)
            {
                return NotFound();
            }
            return Ok(employeeDto);
        }

    }
}
