using AirportProjectCore.Models;
using AirportProjectCore.Services;
using AirportProjectCore.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime;

namespace AirportProjectCore.Controllers
{
    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly IStateService _stateService;
        private readonly IFeedBackService _feedBackService;
       

        public AirportController(IAirportService services1, IFeedBackService services3, IStateService services4)
        {
            _airportService = services1;
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
            return View();

        }
        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            string From =  Convert.ToString( form["source"]);
            string To = Convert.ToString(form["destination"]);
 
            if (From == To)
            {
                TempData["Error"] = "Source and destination cannot be same";
                return RedirectToAction("Create");
            }
            else
            {
               var list= _airportService.GetAirportsBetweenCities(From, To);
                return View("AirportDisplay", list);
            }
        }
      

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(IFormCollection form)
        {
            FeedBack feed = new FeedBack();
            feed.Name = Convert.ToString(form["Name"]);
            feed.Email = Convert.ToString(form["Email"]);
            feed.Subject = Convert.ToString(form["subject"]);

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
            var airportlist = _airportService.GetAirportList();
          
            return View();
        }
        [HttpPost]
        public ActionResult Cost(IFormCollection form)
        {
            string From1 = Convert.ToString(form["From1"]);        
            string To1 = Convert.ToString(form["To1"]);
          
            Tuple<string,string> costdetails= _airportService.GetCostDetails(From1, To1);
            TempData["dist"] = $"The distance between {From1} and {To1} is {costdetails.Item1} Kms, Cost incurred is   ";
            TempData["Cost"] = $"INR(₹):{costdetails.Item2}";

            return View();
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
            var list=_airportService.GetAirportsByState(id);
            return View(list);
        }
     
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
