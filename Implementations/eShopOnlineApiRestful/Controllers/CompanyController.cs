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
        [Route("companies", Name = "GetAllCompaniesAsync")]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            IEnumerable<CompanyDto> companyDtos = await _services.Company.GetAllAsync();
            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("companies/{id:guid}", Name = "GetCompanyByIdAsync")]
        public async Task<IActionResult> GetCompanyByIdAsync([FromRoute]Guid id)
        {
            CompanyDto? companyDto = await _services.Company.GetByIdAsync(id);
            if (companyDto == null)
            {
                return NotFound("CompanyId is non-existing");
            }
            return Ok(companyDto);
        }

        [HttpPut]
        [Route("companies/{id:guid}", Name = "UpdateCompanyFullyAsync")]
        public async Task<IActionResult> UpdateCompanyFullyAsync([FromRoute]Guid id, 
                                                [FromBody]CompanyForUpdateDto? updateDto)
        {
            if (await _services.Company.IsValidIdAsync(id) == false)
            {
                return NotFound("Company ID is non-existing.");
            }
            if (updateDto == null)
            {
                return BadRequest("updateDto object is NULL.");
            }
            
            bool result = await _services.Company.UpdateFullyAsync(id, updateDto);
            return NoContent();
        }

    }
}
