using AirportProjectCore.Models;
using AirportProjectCore.Repository;

namespace AirportProjectCore.Services.Implementation
{
    public class AirportServices : IAirportService
    {
        private readonly IRepository<AirportInfo> _repository;
        public AirportServices(IRepository<AirportInfo> repository)
        {
            _repository = repository;
        }
        public void Add(AirportInfo Info)
        {
            _repository.Add(Info);
        }

        public List<AirportInfo> Get()
        {
            return _repository.Get();
        }
    }
}
