namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CompanyController : AbstractApiController<CompanyController>
    {
        public CompanyController(ILogger<CompanyController> logger, 
                                 ControllerParams controllerParams) 
            : base(logger, controllerParams)
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
                return NotFound("CompanyId is non-existing");
            }
            return Ok(companyDto);
        }

        [HttpPut]
        [Route("companies/{id:guid}", Name = "UpdateCompanyFully")]
        public IActionResult UpdateCompanyFully([FromRoute]Guid id, 
                                                [FromBody]CompanyForUpdateDto? updateDto)
        {
            if (_services.Company.IsValidId(id) == false)
            {
                return NotFound("Company ID is non-existing.");
            }
            if (updateDto == null)
            {
                return BadRequest("updateDto object is NULL.");
            }
            
            bool result = _services.Company.UpdateFully(id, updateDto);
            return NoContent();
        }

    }
}
