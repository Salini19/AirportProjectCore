using AirportProjectCore.Models;
using AirportProjectCore.Repository;
using AirportProjectCore.Services.Implementation;
using AirportProjectCore.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryServices
    {

        public static IServiceCollection AddRepoServices(this IServiceCollection Services)
        {

            Services.AddTransient<IRepository<AirportInfo>, Repository<AirportInfo>>();
            Services.AddTransient<IRepository<CityInfo>, Repository<CityInfo>>();
            Services.AddTransient<IRepository<FeedBack>, Repository<FeedBack>>();
            Services.AddTransient<IRepository<StateImg>, Repository<StateImg>>();



            return Services;
        }
    }
}
