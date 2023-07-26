using Contracts.Utilities.Logger;
using Contracts.Utilities.Mapper;

namespace eShopOnlineUtilities.AutoMapper
{
    public sealed class AutoMapperService : IMapService
    {
        private readonly IMapper _autoMapper;
        private readonly ILogService _logService;

        public AutoMapperService(IMapper autoMapper, ILogService logService)
        {
            _autoMapper = autoMapper;
            _logService = logService;
        }

        public TDestination Execute<TSource, TDestination>(TSource source)
        {
            _logService.LogInfo(MapLogContent.MappingInfo<TSource, TDestination>());
            return _autoMapper.Map<TSource, TDestination>(source);
        }

        public TDestination Execute<TSource, TDestination>(TSource source, TDestination destination)
        {
            _logService.LogInfo(MapLogContent.MappingInfo<TSource, TDestination>());
            return _autoMapper.Map(source, destination);
        }
    }
}
