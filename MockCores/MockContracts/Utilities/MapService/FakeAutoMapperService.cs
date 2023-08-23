using AutoMapper;
using eShopOnlineUtilities.AutoMapper;
using MockContracts.Utilities.LogService;

namespace MockContracts.Utilities.MapService
{
    public sealed class FakeAutoMapperService
    {
        private static IMapper GetMapperInstance()
        {
            var mappingProfile = new eShopOnlineUtilities.AutoMapper.Profiles.MappingProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        public static AutoMapperService GetInstance()
        {
            var mockILogger = MockILogger<AutoMapperService>.GetInstance();
            return new AutoMapperService(mockILogger.Object, GetMapperInstance());
        }
    }
}
