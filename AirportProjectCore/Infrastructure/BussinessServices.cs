using AirportProjectCore.Services.Implementation;
using AirportProjectCore.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BussinessServices
    {
        public static IServiceCollection AddBusinessServies(this IServiceCollection Services)
        {

            Services.AddTransient<IAirportService, AirportServices>();
            Services.AddTransient<ICityService, CityService>();
            Services.AddTransient<IStateService, StateService>();
            Services.AddTransient<IFeedBackService, FeedBackService>();
            Services.AddTransient<IDistMethods, DistMethods>();
            return Services;
        }
    }
}
