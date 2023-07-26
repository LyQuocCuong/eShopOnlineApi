namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CompanyController : AbstractApiController
    {
        protected override string ClassName => nameof(CompanyController);

        public CompanyController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("companies", Name = "GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetAllCompanies));
            IEnumerable<CompanyDto> companyDtos = _services.Company.GetAll();

            LogResponseInfo();
            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("companies/{id:guid}", Name = "GetCompanyById")]
        public IActionResult GetCompanyById([FromRoute]Guid id)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(GetCompanyById));
            CompanyDto? companyDto = _services.Company.GetById(id);
            if (companyDto == null)
            {
                LogResponseInfo();
                return NotFound();
            }

            LogResponseInfo();
            return Ok(companyDto);
        }

        [HttpPut]
        [Route("companies/{id:guid}", Name = "UpdateCompanyFully")]
        public IActionResult UpdateCompanyFully([FromRoute]Guid id, [FromBody]CompanyForUpdateDto updateDto)
        {
            LogRequestInfo();

            LogMethodInfo(nameof(UpdateCompanyFully));
            if (_services.Company.IsValidId(id) == false)
            {
                LogResponseInfo(); 
                return NotFound();
            }
            bool result = _services.Company.UpdateFully(id, updateDto);

            LogResponseInfo();
            return NoContent();
        }

    }
}
