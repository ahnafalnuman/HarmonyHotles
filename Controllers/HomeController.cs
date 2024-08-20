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

            var models = Tuple.Create< IEnumerable < Slider >,IEnumerable<Country>, IEnumerable<City>, IEnumerable<Hotel>, IEnumerable<Event>>
              (sliders, country, cities , hotels,events);
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
                                  .FirstOrDefault(c => c.Countryid == countryId);

            if (country == null)
            {
                return NotFound();
            }

            ViewBag.CountryName = country.Countryname;

            return View(country.Cities);
        }

        public async Task<IActionResult> DisplayAllCountries()
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
                                 .ToList();

            return View(cities);
        }





        public async Task<IActionResult> PopularDestinations()
        {
            var destinations = await _context.Destinations.ToListAsync();

            return View(destinations);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
