using Shared.DTOs.Outputs.EntityDtos;

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
            IEnumerable<EmployeeDto> employeeDto = _services.Employee.GetAll(isTrackChanges: false);
            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("customers/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            EmployeeDto? employeeDto = _services.Employee.GetById(isTrackChanges: false, id);
            return Ok(employeeDto);
        }

    }
}
