using AirportProjectCore.Models;
using AirportProjectCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime;

namespace AirportProjectCore.Controllers
{
    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IFeedBackService _feedBackService;

        public AirportController(IAirportService services1, ICityService services2, IFeedBackService services3, IStateService services4)
        {
            _airportService = services1;
            _cityService = services2;
            _feedBackService = services3;
            _stateService = services4;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var cityList = _stateService.Get();
            var selectList = new SelectList(cityList, "CITY", "CITY");
            ViewBag.source = selectList;
            ViewBag.destination = selectList;
            return View();

        }
        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            var cityList = _cityService.Get().AsEnumerable();


            string From =  Convert.ToString( form["source"]);
            CityInfo city1 = cityList.FirstOrDefault(m => m.City == From);
            var startlocation = new Location(city1.Lat, city1.Long);
            string To = form["destination"].ToString();
            CityInfo city2 = cityList.FirstOrDefault(m => m.City == To);


            var destinationlocation = new Location(city2.Lat, city2.Long);
            if (From == To)
            {
                TempData["Error"] = "Source and destination cannot be same";
                return RedirectToAction("Create");
            }
            else
            {
                var airportsInRange = new List<AirportInfo>(); /// airports between cities
                var airinrange = new List<Airinfo>();
                var airports = _airportService.Get();

                //this is comment

                var maxDistance = HaversineDistance(startlocation, destinationlocation) + 50;
                foreach (var airport in airports)
                {
                    var airportLocation = new Location(airport.Lat, airport.Long);
                    var distance = CalculateDistance(startlocation, destinationlocation, airportLocation);

                    var dist = HaversineDistance(startlocation, airportLocation);

                    if (distance <= maxDistance)
                    {
                        airportsInRange.Add(airport);
                        Airinfo a = new Airinfo();
                        a.IataCode = airport.IataCode;
                        a.City = airport.City;
                        a.AirportName = airport.AirportName;
                        a.Distance = distance;

                        airinrange.Add(a);
                    }
                }
                airinrange = airinrange.OrderBy(a => a.Distance).ToList();
                return View("AirportDisplay", airinrange);
            }
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(IFormCollection form)
        {
            FeedBack feed = new FeedBack();
            feed.Name = form["Name"];
            feed.Email = form["Email"];
            feed.Subject = form["subject"];

            if (string.IsNullOrEmpty(feed.Name) || string.IsNullOrEmpty(feed.Email) || string.IsNullOrEmpty(feed.Subject))
            {
                TempData["Error"] = "Please fill in all the required fields.";
                return RedirectToAction("Contact");
            }

            try
            {
                _feedBackService.Add(feed);
                TempData["Success"] = "Feedback submitted successfully.";
                return RedirectToAction("Contact");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while submitting the feedback.";
                return RedirectToAction("Contact");
            }

        }
        public ActionResult Cost()
        {
            var airportlist = _airportService.Get();
            ViewBag.From = new SelectList(airportlist, "AIRPORTNAME", "AIRPORTNAME");
            ViewBag.To = new SelectList(airportlist, "AIRPORTNAME", "AIRPORTNAME");
            return View();
        }
        [HttpPost]
        public ActionResult Cost(IFormCollection form)
        {
            var airportlist = _airportService.Get();

            string From1 = form["From1"].ToString();

            AirportInfo airport1 = airportlist.Find(m => m.AirportName == From1);
            var start = new Location(airport1.Lat, airport1.Long);
            string To1 = form["To1"].ToString();
            AirportInfo airport2 = airportlist.Find(m => m.AirportName == To1);

            var destination = new Location(airport2.Lat, airport2.Long);

            var maxDistance = HaversineDistance(start, destination);
            var rph = 14.54;
            double price = rph * maxDistance;
            price = Math.Round(price, 4);
            var dist = Math.Round(maxDistance, 4);

            TempData["dist"] = $"The distance between {From1} and {To1} is {dist} Kms, Cost incurred is   ";
            TempData["Cost"] = $"INR(₹):{price}";

            return View("cost", new { From1 = From1, To1 = To1 });
        }

        public ActionResult StateWiseAirports()
        {
            var state = _stateService.Get();
            return View(state);
        }
        [HttpPost]
        public ActionResult StateWiseAirports(string state)
        {

            var slist = _stateService.Get();
            var StateList = slist.Where(x => x.State.ToLower().Contains(state.ToLower())).ToList();
            if (StateList.Count > 0)
            {
                return View(StateList);
            }
            return View();


        }

        public ActionResult AirportList(string id)
        {
            var airports = _airportService.Get();
            List<AirportInfo> list = airports.FindAll(m => m.State == id);
            return View(list);

        }
        public double CalculateDistance(Location startLocation, Location destinationLocation, Location airportLocation)
        {
            var startToAirportDistance = HaversineDistance(startLocation, airportLocation);
            var airportToDestinationDistance = HaversineDistance(airportLocation, destinationLocation);
            var totalDistance = startToAirportDistance + airportToDestinationDistance;

            return totalDistance;
        }

        public double HaversineDistance(Location location1, Location location2)
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



        public double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
        public double RadiansToDegree(double degrees)
        {
            return degrees * (180 / Math.PI);
        }
        public ActionResult Services()
        {
            return View();
        }




    }
}
