namespace eShopOnlineUtilities.AutoMapper.Profiles
{
    public partial class MappingProfiles : Profile
    {
        private void MappingEmployeeProfile()
        {
            ReverseMapping_Employee_And_EmployeeDto();
            ReverseMapping_EmployeeForCreationDto_And_Employee();
            ReverseMapping_EmployeeForUpdateDto_And_Employee();
        }

        private void ReverseMapping_Employee_And_EmployeeDto()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap()
                .ForMember(right => right.Id, 
                           right_prop => right_prop.MapFrom(left => left.Id))
                .ForMember(right => right.WorkingStoreId,
                           right_prop => right_prop.MapFrom(left => left.WorkingStoreId))
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.FirstName,
                           right_prop => right_prop.MapFrom(left => left.FirstName))
                .ForMember(right => right.LastName, 
                           right_prop => right_prop.MapFrom(left => left.LastName))
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

        private void ReverseMapping_EmployeeForCreationDto_And_Employee()
        {
            CreateMap<EmployeeForCreationDto, Employee>().ReverseMap()
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.FirstName,
                           right_prop => right_prop.MapFrom(left => left.FirstName))
                .ForMember(right => right.LastName,
                           right_prop => right_prop.MapFrom(left => left.LastName))
                .ForMember(right => right.Address,
                           right_prop => right_prop.MapFrom(left => left.Address))
                .ForMember(right => right.Phone,
                           right_prop => right_prop.MapFrom(left => left.Phone));
        }

        private void ReverseMapping_EmployeeForUpdateDto_And_Employee()
        {
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap()
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.FirstName,
                           right_prop => right_prop.MapFrom(left => left.FirstName))
                .ForMember(right => right.LastName,
                           right_prop => right_prop.MapFrom(left => left.LastName))
                .ForMember(right => right.Address,
                           right_prop => right_prop.MapFrom(left => left.Address))
                .ForMember(right => right.Phone,
                           right_prop => right_prop.MapFrom(left => left.Phone));
        }

    }
}
