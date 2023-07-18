namespace eShopOnlineUtilities.AutoMapper.Profiles
{
    public partial class MappingProfiles : Profile
    {
        private void MappingStoreProfile()
        {
            ReverseMapping_Store_And_StoreDto();
            ReverseMapping_StoreForCreationDto_And_Store();
            ReverseMapping_StoreForUpdateDto_And_Store();
        }

        private void ReverseMapping_Store_And_StoreDto()
        {
            CreateMap<Store, StoreDto>().ReverseMap()
                .ForMember(right => right.Id, 
                           right_prop => right_prop.MapFrom(left => left.Id))
                .ForMember(right => right.CompanyId,
                           right_prop => right_prop.MapFrom(left => left.CompanyId))
                .ForMember(right => right.ManagerId,
                           right_prop => right_prop.MapFrom(left => left.ManagerId))
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.Name,
                           right_prop => right_prop.MapFrom(left => left.Name))
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

        private void ReverseMapping_StoreForCreationDto_And_Store()
        {
            CreateMap<StoreForCreationDto, Store>().ReverseMap()
                .ForMember(right => right.CompanyId,
                           right_prop => right_prop.MapFrom(left => left.CompanyId))
                .ForMember(right => right.ManagerId,
                           right_prop => right_prop.MapFrom(left => left.ManagerId))
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.Name,
                           right_prop => right_prop.MapFrom(left => left.Name))
                .ForMember(right => right.Address,
                           right_prop => right_prop.MapFrom(left => left.Address))
                .ForMember(right => right.Phone,
                           right_prop => right_prop.MapFrom(left => left.Phone));
        }

        private void ReverseMapping_StoreForUpdateDto_And_Store()
        {
            CreateMap<StoreForUpdateDto, Store>().ReverseMap()
                .ForMember(right => right.ManagerId,
                           right_prop => right_prop.MapFrom(left => left.ManagerId))
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.Name,
                           right_prop => right_prop.MapFrom(left => left.Name))
                .ForMember(right => right.Address,
                           right_prop => right_prop.MapFrom(left => left.Address))
                .ForMember(right => right.Phone,
                           right_prop => right_prop.MapFrom(left => left.Phone));
        }

    }
}
