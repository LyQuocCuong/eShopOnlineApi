namespace eShopOnlineUtilities.AutoMapper.Profiles
{
    public partial class MappingProfiles : Profile
    {
        private void MappingCompanyProfile()
        {
            ReverseMapping_Company_And_CompanyDto();
            ReverseMapping_CompanyForUpdateDto_And_Company();
        }

        private void ReverseMapping_Company_And_CompanyDto()
        {
            CreateMap<Company, CompanyDto>().ReverseMap()
                .ForMember(right => right.Id, 
                           right_prop => right_prop.MapFrom(left => left.Id))
                .ForMember(right => right.Name, 
                           right_prop => right_prop.MapFrom(left => left.Name))
                .ForMember(right => right.Address, 
                           right_prop => right_prop.MapFrom(left => left.Address))
                .ForMember(right => right.Phone, 
                           right_prop => right_prop.MapFrom(left => left.Phone))
                .ForMember(right => right.IsDeleted,
                           right_prop => right_prop.MapFrom(left => left.IsDeleted))
                .ForMember(right => right.CreatedDateUtcZero,
                           right_prop => right_prop.MapFrom(left => left.CreatedDateUtcZero))
                .ForMember(right => right.UpdatedDateUtcZero,
                           right_prop => right_prop.MapFrom(left => left.UpdatedDateUtcZero));
        }

        private void ReverseMapping_CompanyForUpdateDto_And_Company()
        {
            CreateMap<CompanyForUpdateDto, Company>().ReverseMap()
                .ForMember(right => right.Name,
                           right_prop => right_prop.MapFrom(left => left.Name))
                .ForMember(right => right.Address,
                           right_prop => right_prop.MapFrom(left => left.Address))
                .ForMember(right => right.Phone,
                           right_prop => right_prop.MapFrom(left => left.Phone));
        }

    }
}
