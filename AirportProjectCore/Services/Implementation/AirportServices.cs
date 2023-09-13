using AirportProjectCore.Models;
using AirportProjectCore.Repository;

namespace AirportProjectCore.Services.Implementation
{
    public class AirportServices : IAirportService
    {
        private readonly IRepository<AirportInfo> _repository;
        private readonly ICityService _cityService;
        public AirportServices(IRepository<AirportInfo> repository, ICityService cityService)
        {
            _repository = repository;
            _cityService = cityService;
        }
        public void Add(AirportInfo Info)
        {
            _repository.Add(Info);
        }

        public List<AirportInfo> GetAirportList()
        {
            return _repository.Get();
        }

        public List<AirportInfo> GetAirportsByState(string id)
        {
            return _repository.Get().Where(x => x.State == id).ToList();
        }

        public List<AirInfo> GetAirportsBetweenCities(string from, string to)
        {
            var cityList = _cityService.Get().AsEnumerable();

            CityInfo city1 = cityList.FirstOrDefault(m => m.City == from);
            CityInfo city2 = cityList.FirstOrDefault(m => m.City == to);

            var startlocation = new Location(city1.Lat, city1.Long);
            var destinationlocation = new Location(city2.Lat, city2.Long);

            var airportsInRange = new List<AirportInfo>(); /// airports between cities
            var airinrange = new List<AirInfo>();
            var airports = _repository.Get();

            var maxDistance = HaversineDistance(startlocation, destinationlocation) + 50;
            foreach (var airport in airports)
            {
                var airportLocation = new Location(airport.Lat, airport.Long);
                var distance = CalculateDistance(startlocation, destinationlocation, airportLocation);

                var dist = HaversineDistance(startlocation, airportLocation);

                if (distance <= maxDistance)
                {
                    airportsInRange.Add(airport);
                    AirInfo a = new AirInfo();
                    a.IataCode = airport.IataCode;
                    a.City = airport.City;
                    a.AirportName = airport.AirportName;
                    a.Distance = distance;

                    airinrange.Add(a);
                }
            }
            return airinrange.OrderBy(a => a.Distance).ToList();
        }
        public Tuple<string,string> GetCostDetails(string from, string to)
        {

            var airportlist = _repository.Get().AsEnumerable();
            AirportInfo airport1 = airportlist.FirstOrDefault(m => m.AirportName == from);
            AirportInfo airport2 = airportlist.FirstOrDefault(m => m.AirportName == to);

            var start = new Location(airport1.Lat, airport2.Long);
            var destination = new Location(airport2.Lat, airport2.Long);

            var maxDistance = HaversineDistance(start, destination);
            var rph = 14.54;
            double price = rph * maxDistance;
            price = Math.Round(price, 4);
            var dist = Math.Round(maxDistance, 4);
            return Tuple.Create(dist.ToString(), price.ToString());
        }

        private double CalculateDistance(Location startLocation, Location destinationLocation, Location airportLocation)
        {
            var startToAirportDistance = HaversineDistance(startLocation, airportLocation);
            var airportToDestinationDistance = HaversineDistance(airportLocation, destinationLocation);
            var totalDistance = startToAirportDistance + airportToDestinationDistance;

            return totalDistance;
        }

        private double HaversineDistance(Location location1, Location location2)
        {
            var earthRadius = 6371; // Radius of the Earth in kilometers
            var latitudeDifference = DegreesToRadians(location2.Latitude - location1.Latitude);
            var longitudeDifference = DegreesToRadians(location2.Longitude - location1.Longitude);

            var a = Math.Sin(latitudeDifference / 2) * Math.Sin(latitudeDifference / 2) +
            Math.Cos(DegreesToRadians(location1.Latitude)) * Math.Cos(DegreesToRadians(location2.Latitude)) *
            Math.Sin(longitudeDifference / 2) * Math.Sin(longitudeDifference / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = earthRadius * c;
            return distance;
        }
        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
