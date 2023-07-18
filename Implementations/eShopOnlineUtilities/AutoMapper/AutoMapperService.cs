using AutoMapper;
using Contracts.Utilities.Mapper;

namespace eShopOnlineUtilities.AutoMapper
{
    public sealed class AutoMapperService : IMapperService
    {
        private readonly IMapper _autoMapper;

        public AutoMapperService(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public TDestination Execute<TSource, TDestination>(TSource source)
        {
            return _autoMapper.Map<TSource, TDestination>(source);
        }

        public TDestination Execute<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _autoMapper.Map(source, destination);
        }
    }
}
