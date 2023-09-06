using AirportProjectCore.Models;

namespace AirportProjectCore.Services
{
    public interface IStateService
    {
        void Add(StateImg Info);
        List<StateImg> Get();
    }
}
