using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Inputs.FromBody.CreationDtos;

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
        [Route("employees/{id:guid}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById(Guid id)
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

    }
}
