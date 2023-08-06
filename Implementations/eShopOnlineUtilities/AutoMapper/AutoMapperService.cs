using Contracts.Utilities.Mapper;
using eShopOnlineUtilities.LogMessages;

namespace eShopOnlineUtilities.AutoMapper
{
    public sealed class AutoMapperService : IMapService
    {
        private readonly IMapper _autoMapper;
        private readonly ILogger<AutoMapperService> _logger;

        public AutoMapperService(ILogger<AutoMapperService> logger, 
                                 IMapper autoMapper)
        {
            _logger = logger;
            _autoMapper = autoMapper;
        }

        public TDestination Execute<TSource, TDestination>(TSource source)
        {
            _logger.LogInformation(MappingLogs.MappingInfo<TSource, TDestination>());
            return _autoMapper.Map<TSource, TDestination>(source);
        }

        public TDestination Execute<TSource, TDestination>(TSource source, TDestination destination)
        {
            _logger.LogInformation(MappingLogs.MappingInfo<TSource, TDestination>());
            return _autoMapper.Map(source, destination);
        }
    }
}
