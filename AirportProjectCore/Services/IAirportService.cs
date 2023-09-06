using AirportProjectCore.Models;

namespace AirportProjectCore.Services
{
    public interface IAirportService
    {
         void Add(AirportInfo Info);
         List<AirportInfo> Get();

    }
}
