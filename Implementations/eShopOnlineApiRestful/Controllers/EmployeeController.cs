namespace eShopOnlineApiRestful.Controllers
{
    public sealed class EmployeeController : AbstractApiController
    {
        protected override string ChildClassName => nameof(EmployeeController);

        public EmployeeController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("employees", Name = "GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            LogInfoRequest();

            LogInfo(nameof(GetAllEmployees), LogMessages.MessageForExecutingMethod);
            IEnumerable<EmployeeDto> employeeDtos = _services.Employee.GetAll();

            LogInfoResponse();
            return Ok(employeeDtos);
        }

        [HttpGet]
        [Route("employees/{id:guid}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(GetEmployeeById), LogMessages.MessageForExecutingMethod);
            EmployeeDto? employeeDto = _services.Employee.GetById(id);
            if (employeeDto == null)
            {
                LogInfoResponse();
                return NotFound();
            }
            LogInfoResponse();
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("employees", Name = "CreateEmployee")]
        public IActionResult CreateEmployee([FromBody]EmployeeForCreationDto creationDto)
        {
            LogInfoRequest();

            LogInfo(nameof(CreateEmployee), LogMessages.MessageForExecutingMethod);
            EmployeeDto employeeDto = _services.Employee.Create(creationDto);

            LogInfoResponse();
            return CreatedAtRoute("GetEmployeeById", new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut]
        [Route("employees/{id:guid}", Name = "UpdateEmployeeFully")]
        public IActionResult UpdateEmployeeFully([FromRoute]Guid id, [FromBody]EmployeeForUpdateDto updateDto)
        {
            LogInfoRequest();

            LogInfo(nameof(UpdateEmployeeFully), LogMessages.MessageForExecutingMethod);
            if (_services.Employee.IsValidId(id) == false)
            {
                LogInfoResponse();
                return NotFound();
            }
            bool result = _services.Employee.UpdateFully(id, updateDto);

            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("employees/{id:guid}", Name = "DeleteEmployeeSoftly")]
        public IActionResult DeleteEmployeeSoftly([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteEmployeeSoftly), LogMessages.MessageForExecutingMethod);
            bool result = _services.Employee.DeleteSoftly(id);
            if (result == false)
            {
                LogInfoResponse();
                return BadRequest();
            }
            LogInfoResponse();
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/employees/{id:guid}", Name = "DeleteEmployeeHard")]
        public IActionResult DeleteEmployeeHard([FromRoute] Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(DeleteEmployeeHard), LogMessages.MessageForExecutingMethod);
            bool result = _services.Employee.DeleteHard(id);
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
