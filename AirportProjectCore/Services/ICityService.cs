using AirportProjectCore.Models;

namespace AirportProjectCore.Services
{
    public interface ICityService
    {
        void Add(CityInfo Info);
        List<CityInfo> Get();
    }
}
