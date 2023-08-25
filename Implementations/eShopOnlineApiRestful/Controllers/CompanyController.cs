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
        [Route("companies", Name = nameof(GetAllCompaniesAsync))]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            IEnumerable<CompanyDto> companyDtos = await _services.Company.GetAllAsync();
            return Ok(companyDtos);
        }

        [HttpGet]
        [Route("companies/{id:guid}", Name = nameof(GetCompanyByIdAsync))]
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
        [Route("companies/{id:guid}", Name = nameof(UpdateCompanyFullyAsync))]
        public async Task<IActionResult> UpdateCompanyFullyAsync(
            [FromRoute]Guid id, 
            [FromBody]CompanyForUpdateDto? updateDto,
            [FromServices]IValidator<CompanyForUpdateDto> updateDtoValidator)
        {
            if (updateDto == null)
            {
                return BadRequest("updateDto object is NULL.");
            }

            ValidationResult validationResult = await updateDtoValidator.ValidateAsync(updateDto);
            if (validationResult.IsValid == false)
            {
                // Copy the validation results into ModelState.
                validationResult.AddErrorsToModelStateObj(this.ModelState);

                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View (like MVC framework).
                return UnprocessableEntity(this.ModelState);
            }

            if (await _services.Company.IsValidIdAsync(id) == false)
            {
                return NotFound("Company ID is non-existing.");
            }
            
            bool result = await _services.Company.UpdateFullyAsync(id, updateDto);
            if (result == false)
            {
                return BadRequest("Can not update");
            }
            return NoContent();
        }

    }
}
