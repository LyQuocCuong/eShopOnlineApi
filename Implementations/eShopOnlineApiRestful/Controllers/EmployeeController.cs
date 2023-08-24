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
        [Route("employees", Name = nameof(GetAllEmployeesAsync))]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            IEnumerable<EmployeeDto> employeeDtos = await _services.Employee.GetAllAsync();
            return Ok(employeeDtos);
        }

        [HttpGet]
        [Route("employees/{id:guid}", Name = nameof(GetEmployeeByIdAsync))]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute]Guid id)
        {
            EmployeeDto? employeeDto = await _services.Employee.GetByIdAsync(id);
            if (employeeDto == null)
            {
                return NotFound("EmployeeId is non-existing.");
            }
            return Ok(employeeDto);
        }

        [HttpPost]
        [Route("employees", Name = nameof(CreateEmployeeAsync))]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody]EmployeeForCreationDto? creationDto)
        {
            if (creationDto == null)
            {
                return BadRequest("Object for creation is NULL.");
            }
            EmployeeDto employeeDto = await _services.Employee.CreateAsync(creationDto);
            return CreatedAtRoute(nameof(GetEmployeeByIdAsync), new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut]
        [Route("employees/{id:guid}", Name = nameof(UpdateEmployeeFullyAsync))]
        public async Task<IActionResult> UpdateEmployeeFullyAsync([FromRoute]Guid id, [FromBody]EmployeeForUpdateDto? updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("updateDto object is NULL.");
            }
            if (await _services.Employee.IsValidIdAsync(id) == false)
            {
                return NotFound("EmployeeId is non-existing.");
            }
            bool result = await _services.Employee.UpdateFullyAsync(id, updateDto);
            if (result == false)
            {
                return BadRequest("Can NOT update");
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("employees/{id:guid}", Name = nameof(DeleteEmployeeSoftlyAsync))]
        public async Task<IActionResult> DeleteEmployeeSoftlyAsync([FromRoute]Guid id)
        {
            bool result = await _services.Employee.DeleteSoftlyAsync(id);
            if (result == false)
            {
                return BadRequest("Can NOT delete Employee. There are somethings WRONG.");
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("admin/employees/{id:guid}", Name = nameof(DeleteEmployeeHardAsync))]
        public async Task<IActionResult> DeleteEmployeeHardAsync([FromRoute] Guid id)
        {
            bool result = await _services.Employee.DeleteHardAsync(id);
            if (result == false)
            {
                return BadRequest("Can NOT delete Employee. There are somethings WRONG.");
            }
            return NoContent();
        }

    }
}
