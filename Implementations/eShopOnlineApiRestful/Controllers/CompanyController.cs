namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CompanyController : AbstractApiController
    {
        public CompanyController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("companies", Name = "GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            IEnumerable<CompanyDto> companyDtos = _services.Company.GetAll();
            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("companies/{id:guid}", Name = "GetCompanyById")]
        public IActionResult GetCompanyById([FromRoute]Guid id)
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
