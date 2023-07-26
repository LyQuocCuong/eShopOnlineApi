namespace eShopOnlineApiRestful.Controllers
{
    public sealed class EmployeeController : AbstractApiController
    {
        protected override string ClassName => nameof(EmployeeController);

        public EmployeeController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("employees", Name = "GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetAllEmployees));
            IEnumerable<EmployeeDto> employeeDtos = _services.Employee.GetAll();

            LogResponseInfo();
            return Ok(employeeDtos);
        }

        [HttpGet]
        [Route("employees/{id:guid}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetEmployeeById));
            EmployeeDto? employeeDto = _services.Employee.GetById(id);
            if (employeeDto == null)
            {
                LogResponseInfo();
                return NotFound();
            }
            LogResponseInfo();
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("employees", Name = "CreateEmployee")]
        public IActionResult CreateEmployee([FromBody]EmployeeForCreationDto creationDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(CreateEmployee));
            EmployeeDto employeeDto = _services.Employee.Create(creationDto);

            LogResponseInfo();
            return CreatedAtRoute("GetEmployeeById", new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut]
        [Route("employees/{id:guid}", Name = "UpdateEmployeeFully")]
        public IActionResult UpdateEmployeeFully([FromRoute]Guid id, [FromBody]EmployeeForUpdateDto updateDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(UpdateEmployeeFully));
            if (_services.Employee.IsValidId(id) == false)
            {
                LogResponseInfo();
                return NotFound();
            }
            bool result = _services.Employee.UpdateFully(id, updateDto);

            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("employees/{id:guid}", Name = "DeleteEmployeeSoftly")]
        public IActionResult DeleteEmployeeSoftly([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteEmployeeSoftly));
            bool result = _services.Employee.DeleteSoftly(id);
            if (result == false)
            {
                LogResponseInfo();
                return BadRequest();
            }
            LogResponseInfo();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/employees/{id:guid}", Name = "DeleteEmployeeHard")]
        public IActionResult DeleteEmployeeHard([FromRoute] Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(DeleteEmployeeHard));
            bool result = _services.Employee.DeleteHard(id);
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
