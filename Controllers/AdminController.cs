using HarmonyHotles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HarmonyHotles.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult ManageSliders()
        {
            return RedirectToAction("Index", "Sliders");
        }


        public IActionResult ManageCountries()
        {
            return RedirectToAction("Index", "Countries");
        }

        public IActionResult ManageCities()
        {
            return RedirectToAction("Index", "Cities");
        }

        public IActionResult AddEvents()
        {
            return RedirectToAction("Index", "Events");
        }


        public IActionResult AddAmenities()
        {
            return RedirectToAction("Index", "Amenities");
        }

















    }


}



