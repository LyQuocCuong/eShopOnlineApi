namespace eShopOnlineUtilities.AutoMapper.Profiles
{
    public partial class MappingProfiles : Profile
    {
        private void MappingProductProfile()
        {
            ReverseMapping_Product_And_ProductDto();
            ReverseMapping_ProductForCreationDto_And_Product();
            ReverseMapping_ProductForUpdateDto_And_Product();
        }

        private void ReverseMapping_Product_And_ProductDto()
        {
            CreateMap<Product, ProductDto>().ReverseMap()
                .ForMember(right => right.Id, 
                           right_prop => right_prop.MapFrom(left => left.Id))
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.Name,
                           right_prop => right_prop.MapFrom(left => left.Name))
                .ForMember(right => right.IsDeleted,
                           right_prop => right_prop.MapFrom(left => left.IsDeleted))
                .ForMember(right => right.CreatedDate,
                           right_prop => right_prop.MapFrom(left => left.CreatedDate))
                .ForMember(right => right.UpdatedDate,
                           right_prop => right_prop.MapFrom(left => left.UpdatedDate));
        }

        private void ReverseMapping_ProductForCreationDto_And_Product()
        {
            CreateMap<ProductForCreationDto, Product>().ReverseMap()
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.Name,
                           right_prop => right_prop.MapFrom(left => left.Name));
        }

        private void ReverseMapping_ProductForUpdateDto_And_Product()
        {
            CreateMap<ProductForUpdateDto, Product>().ReverseMap()
                .ForMember(right => right.Code,
                           right_prop => right_prop.MapFrom(left => left.Code))
                .ForMember(right => right.Name,
                           right_prop => right_prop.MapFrom(left => left.Name));
        }

    }
}
