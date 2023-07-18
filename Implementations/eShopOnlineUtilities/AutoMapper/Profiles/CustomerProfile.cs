namespace eShopOnlineUtilities.AutoMapper.Profiles
{
    public partial class MappingProfiles : Profile
    {
        private void MappingCustomerProfile()
        {
            ReverseMapping_Customer_And_CustomerDto();
            ReverseMapping_CustomerForCreationDto_And_Customer();
            ReverseMapping_CustomerForUpdateDto_And_Customer();
        }

        private void ReverseMapping_Customer_And_CustomerDto()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap()
                .ForMember(right => right.Id, 
                           right_prop => right_prop.MapFrom(left => left.Id))
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
                .ForMember(right => right.CreatedDate,
                           right_prop => right_prop.MapFrom(left => left.CreatedDate))
                .ForMember(right => right.UpdatedDate,
                           right_prop => right_prop.MapFrom(left => left.UpdatedDate));
        }

        private void ReverseMapping_CustomerForCreationDto_And_Customer()
        {
            CreateMap<CustomerForCreationDto, Customer>().ReverseMap()
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

        private void ReverseMapping_CustomerForUpdateDto_And_Customer()
        {
            CreateMap<CustomerForUpdateDto, Customer>().ReverseMap()
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
