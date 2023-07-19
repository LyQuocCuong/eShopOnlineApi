namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CompanyController : AbstractApiController
    {
        public CompanyController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("companies")]
        public IActionResult GetAll()
        {
            IEnumerable<CompanyDto> companyDtos = _services.Company.GetAll();
            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("companies/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            CompanyDto? companyDto = _services.Company.GetById(id);
            if (companyDto == null)
            {
                return NotFound();
            }
            return Ok(companyDto);
        }

    }
}
