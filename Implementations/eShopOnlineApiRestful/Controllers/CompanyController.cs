namespace eShopOnlineApiRestful.Controllers
{
    public sealed class CompanyController : AbstractApiController
    {
        protected override string ChildClassName => nameof(CompanyController);

        public CompanyController(ControllerParams controllerParams) : base(controllerParams)
        {
        }

        [HttpGet]
        [Route("companies", Name = "GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            LogInfoRequest();

            LogInfo(nameof(GetAllCompanies), LogMessages.MessageForExecutingMethod);
            IEnumerable<CompanyDto> companyDtos = _services.Company.GetAll();

            LogInfoResponse();
            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("companies/{id:guid}", Name = "GetCompanyById")]
        public IActionResult GetCompanyById([FromRoute]Guid id)
        {
            LogInfoRequest();

            LogInfo(nameof(GetCompanyById), LogMessages.MessageForExecutingMethod);
            CompanyDto? companyDto = _services.Company.GetById(id);
            if (companyDto == null)
            {
                LogInfoResponse();
                return NotFound();
            }

            LogInfoResponse();
            return Ok(companyDto);
        }

        [HttpPut]
        [Route("companies/{id:guid}", Name = "UpdateCompanyFully")]
        public IActionResult UpdateCompanyFully([FromRoute]Guid id, [FromBody]CompanyForUpdateDto updateDto)
        {
            LogInfoRequest();

            LogInfo(nameof(UpdateCompanyFully), LogMessages.MessageForExecutingMethod);
            if (_services.Company.IsValidId(id) == false)
            {
                LogInfoResponse(); 
                return NotFound();
            }
            bool result = _services.Company.UpdateFully(id, updateDto);

            LogInfoResponse();
            return NoContent();
        }

    }
}
