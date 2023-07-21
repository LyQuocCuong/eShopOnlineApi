namespace eShopOnlineApiRestful.Controllers
{
    public sealed class EmployeeController : AbstractApiController
    {
        public EmployeeController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("employees", Name = "GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            IEnumerable<EmployeeDto> employeeDtos = _services.Employee.GetAll();
            return Ok(employeeDtos);
        }

        [HttpGet]
        [Route("employees/{id:guid}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById([FromRoute]Guid id)
        {
            EmployeeDto? employeeDto = _services.Employee.GetById(id);
            if (employeeDto == null)
            {
                return NotFound();
            }
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("employees", Name = "CreateEmployee")]
        public IActionResult CreateEmployee([FromBody]EmployeeForCreationDto creationDto)
        {
            EmployeeDto employeeDto = _services.Employee.Create(creationDto);
            return CreatedAtRoute("GetEmployeeById", new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut]
        [Route("employees/{id:guid}", Name = "UpdateEmployeeFully")]
        public IActionResult UpdateEmployeeFully([FromRoute]Guid id, [FromBody]EmployeeForUpdateDto updateDto)
        {
            if (_services.Employee.IsValidId(id) == false)
            {
                return NotFound();
            }
            bool result = _services.Employee.UpdateFully(id, updateDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("employees/{id:guid}", Name = "DeleteEmployeeSoftly")]
        public IActionResult DeleteEmployeeSoftly([FromRoute]Guid id)
        {
            bool result = _services.Employee.DeleteSoftly(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/employees/{id:guid}", Name = "DeleteEmployeeHard")]
        public IActionResult DeleteEmployeeHard([FromRoute] Guid id)
        {
            bool result = _services.Employee.DeleteHard(id);
            if (result == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
