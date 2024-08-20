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

        public IActionResult Countries()
        {
          
            return RedirectToAction("Index", "Countries");
        }

        public IActionResult Cities()
        {

            return RedirectToAction("Index", "cities");
        }

        public IActionResult Sliders()
        {
            return RedirectToAction("Index","Sliders");
        }



    }


    }



