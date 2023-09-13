using AirportProjectCore.Models;

namespace AirportProjectCore.Services
{
    public interface IAirportService
    {
        void Add(AirportInfo Info);
        List<AirportInfo> GetAirportList();
        List<AirportInfo> GetAirportsByState(string id);

        List<AirInfo> GetAirportsBetweenCities(string from, string to);
        Tuple<string, string> GetCostDetails(string from, string to);
    }
}
