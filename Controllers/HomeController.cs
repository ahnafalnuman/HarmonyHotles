using HarmonyHotles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

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


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DisplayCities(int countryId)
        {
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

        public IActionResult DisplayEventInCity(int id)
        {
            var city = _context.Cities
                .Include(c => c.Events)  // تضمين الفعاليات المرتبطة بالمدينة
                .FirstOrDefault(c => c.Cityid == id);

            if (city == null)
            {
                return NotFound();  // العودة بصفحة 404 إذا لم يتم العثور على المدينة
            }

            return View(city.Events);  // تمرير قائمة الفعاليات إلى العرض
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
