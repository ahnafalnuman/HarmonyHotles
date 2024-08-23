using HarmonyHotles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace HarmonyHotles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger , ModelContext context)
        {
            _logger = logger;
            _context = context; 
        }

        [HttpGet]   
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            var country = await _context.Countries.ToListAsync();
            var cities = await _context.Cities.ToListAsync();
            var hotels = await _context.Hotels.ToListAsync();
            var events = await _context.Events.ToListAsync();
            var images = await _context.Images.ToListAsync();
            var favorites = await _context.Favorites.ToListAsync();

            /*       // الحصول على المفضلات الخاصة بالمستخدم الحالي
                   var userId = User.Identity.GetUserId(); // تأكد من استخدام الطريقة الصحيحة للحصول على معرف المستخدم
                   var favorites = await _context.Favorites.Where(f => f.UserId == userId).ToListAsync();*/

            var models = Tuple.Create<IEnumerable<Slider>, IEnumerable<Country>, IEnumerable<City>, IEnumerable<Hotel>, IEnumerable<Event>, IEnumerable<Image>, IEnumerable<Favorite>>
              (sliders, country, cities, hotels, events, images, favorites);

            return View(models);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string destination, DateTime? startDate, DateTime? endDate)
        {
            var sliders = await _context.Sliders.ToListAsync();
            var countries = await _context.Countries
                                          .Include(c => c.Cities)
                                          .ThenInclude(city => city.Hotels)
                                          .Include(c => c.Cities)
                                          .ThenInclude(city => city.Events)
                                          .ToListAsync();

            var cities = await _context.Cities
                                       .Include(city => city.Hotels)
                                       .Include(city => city.Events)
                                       .ToListAsync();

            var hotels = await _context.Hotels
                                       .Include(h => h.City)
                                       .Include(h => h.Country)
                                       .ToListAsync();

            var events = await _context.Events
                                       .Include(e => e.City)
                                       .Include(e => e.Country)
                                       .Include(e => e.Hotel)
                                       .Include(e => e.Images)
                                       .ToListAsync();

            var images = await _context.Images.ToListAsync();
            var favorites = await _context.Favorites.ToListAsync();

            // Filtering based on destination, start date, and end date
            if (!string.IsNullOrEmpty(destination))
            {
                var lowerDestination = destination.ToLower();

                countries = countries.Where(c => c.Countryname.ToLower().Contains(lowerDestination)).ToList();
                cities = cities.Where(c => c.Cityname.ToLower().Contains(lowerDestination)).ToList();
                events = events.Where(e =>
                    (e.City != null && e.City.Cityname.ToLower().Contains(lowerDestination)) ||
                    (e.Country != null && e.Country.Countryname.ToLower().Contains(lowerDestination)) ||
                    (e.Hotel != null && e.Hotel.Name.ToLower().Contains(lowerDestination)) ||
                    e.Name.ToLower().Contains(lowerDestination)).ToList();
            }

            if (startDate.HasValue || endDate.HasValue)
            {
                events = events.Where(e =>
                    (!startDate.HasValue || e.Timefrom >= startDate.Value) &&
                    (!endDate.HasValue || e.Timeto <= endDate.Value)).ToList();
            }

            var models = Tuple.Create<IEnumerable<Slider>, IEnumerable<Country>, IEnumerable<City>, IEnumerable<Hotel>, IEnumerable<Event>, IEnumerable<Image>, IEnumerable<Favorite>>
                (sliders, countries, cities, hotels, events, images, favorites);

            return View(models);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DisplayCities(int countryId)
        {
            var images = _context.Images.ToListAsync();
            var country = _context.Countries
                                  .Include(c => c.Cities)
                                  .ThenInclude(city => city.Hotels)
                                  .Include(c => c.Cities)
                                  .ThenInclude(city => city.Events) 
                                  .FirstOrDefault(c => c.Countryid == countryId);

            if (country == null)
            {
                return NotFound();
            }

            ViewBag.CountryName = country.Countryname;

            return View(country.Cities);
        }

        public async Task<IActionResult> DisplayEventInCity(int id)
        {
            var city = await _context.Cities
                .Include(c => c.Events)
                .ThenInclude(e => e.Images) 
                .FirstOrDefaultAsync(c => c.Cityid == id);

            if (city == null)
            {
                return NotFound(); 
            }

            return View(city.Events);  
        }


        public IActionResult DisplayAllCountries()
        {
            var countries = _context.Countries
                         .Include(c => c.Cities)
                         .ToList();
            return View(countries);
        }

        public IActionResult DisplayAllCities()
        {
            var cities = _context.Cities
                                 .Include(city => city.Hotels)
                                 .Include(c => c.Events)
                                 .ToList();

            return View(cities);
        }

        // GET: Display all events
        [HttpGet]
        public IActionResult DisplayAllEvent()
        {
            var events = _context.Events
                .Include(e => e.City)
                .Include(e => e.Country)
                .Include(e => e.Hotel)
                .Include(e => e.Images)
                .ToList();

            return View(events);
        }

        // POST: Apply filters to events
        [HttpPost]
        public IActionResult DisplayAllEvent(string destination, DateTime? startDate, DateTime? endDate)
        {
            var events = _context.Events
                .Include(e => e.City)
                .Include(e => e.Country)
                .Include(e => e.Hotel)
                .Include(e => e.Images)
                .AsQueryable();

            // التعامل مع البحث بالحروف الكبيرة والصغيرة
            if (!string.IsNullOrEmpty(destination))
            {
                var lowerDestination = destination.ToLower();
                events = events.Where(e =>
                    (e.City.Cityname.ToLower().Contains(lowerDestination)) ||
                    (e.Country.Countryname.ToLower().Contains(lowerDestination)) ||
                    (e.Hotel.Name.ToLower().Contains(lowerDestination)) ||
                    (e.Name.ToLower().Contains(lowerDestination)));
            }

            // التعامل مع التواريخ
            if (startDate.HasValue && endDate.HasValue)
            {
                events = events.Where(e => e.Timefrom >= startDate.Value && e.Timeto <= endDate.Value);
            }
            else if (startDate.HasValue)
            {
                events = events.Where(e => e.Timefrom >= startDate.Value);
            }
            else if (endDate.HasValue)
            {
                events = events.Where(e => e.Timeto <= endDate.Value);
            }

            return View(events.ToList());
        }



        public IActionResult Events(int id)
        {
            
            var eventDetails = _context.Events
                .Include(e => e.Images) 
                .FirstOrDefault(e => e.Eventid == id); 

            if (eventDetails == null)
            {
                return NotFound();
            }
       
            var coordinates = eventDetails.Location?.Split(',');
            string latitude = coordinates != null && coordinates.Length > 0 ? coordinates[0] : "0";
            string longitude = coordinates != null && coordinates.Length > 1 ? coordinates[1] : "0";

            ViewBag.Latitude = latitude;
            ViewBag.Longitude = longitude;

            return View(eventDetails);
        }





        /*  public async Task<IActionResult> PopularDestinations()
          {
              var destinations = await _context.Destinations.ToListAsync();

              return View(destinations);
          }
  */


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
