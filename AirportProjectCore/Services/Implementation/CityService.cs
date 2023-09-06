using AirportProjectCore.Models;
using AirportProjectCore.Repository;

namespace AirportProjectCore.Services.Implementation
{
    
    public class CityService : ICityService
    {
        private readonly IRepository<CityInfo> _repository;
        public CityService(IRepository<CityInfo> repository)
        {
            _repository = repository;
        }
        public void Add(CityInfo Info)
        {
            _repository.Add(Info);
        }

        public List<CityInfo> Get()
        {
            return _repository.Get();
        }
    }
}
