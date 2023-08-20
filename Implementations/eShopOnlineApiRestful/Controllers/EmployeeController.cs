namespace eShopOnlineApiRestful.Controllers
{
    public sealed class EmployeeController : AbstractApiController<EmployeeController>
    {
        public EmployeeController(ILogger<EmployeeController> logger, 
                                  ControllerParams controllerParams) 
            : base(logger, controllerParams)
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
                return NotFound("EmployeeId is non-existing.");
            }
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("employees", Name = "CreateEmployee")]
        public IActionResult CreateEmployee([FromBody]EmployeeForCreationDto? creationDto)
        {
            if (creationDto == null)
            {
                return BadRequest("Object for creation is NULL.");
            }
            EmployeeDto employeeDto = _services.Employee.Create(creationDto);

            employeeDto.Id = Guid.Empty;    // Edited
            return CreatedAtRoute("GetEmployeeById", new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut]
        [Route("employees/{id:guid}", Name = "UpdateEmployeeFully")]
        public IActionResult UpdateEmployeeFully([FromRoute]Guid id, [FromBody]EmployeeForUpdateDto? updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("updateDto object is NULL.");
            }
            if (_services.Employee.IsValidId(id) == false)
            {
                return NotFound("EmployeeId is non-existing.");
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
                return BadRequest("Can NOT delete Employee. There are somethings WRONG.");
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
                return BadRequest("Can NOT delete Employee. There are somethings WRONG.");
            }
            return NoContent();
        }

    }
}
