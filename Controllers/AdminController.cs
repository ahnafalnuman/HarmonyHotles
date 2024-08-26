using HarmonyHotles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HarmonyHotles.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ModelContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }



        [HttpGet]
        public IActionResult AddRoomTypeToHotel(int hotelId)
        {
            var hotel = _context.Hotels
                .Include(h => h.Rooms)
                .ThenInclude(hr => hr.Roomtype)
                .FirstOrDefault(h => h.Hotelid == hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            ViewBag.RoomTypes = new SelectList(_context.Roomtypes, "Roomtypeid", "Typename");
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoomTypeToHotel(int hotelId, int roomTypeId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null)
            {
                return NotFound();
            }

            var room = new Room { Roomtypeid = roomTypeId, Hotelid = hotelId };
            _context.Rooms.Add(room);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Room type added successfully!";
            return RedirectToAction("Details", new { id = hotelId });
        }

        [HttpGet]
        public IActionResult AddServiceToHotel(int hotelId)
        {
            var hotel = _context.Hotels
                .Include(h => h.Hotelservices)
                .ThenInclude(hs => hs.Services)
                .FirstOrDefault(h => h.Hotelid == hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            ViewBag.Services = new SelectList(_context.Services, "Servicesid", "Servicename");
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddServiceToHotel(int hotelId, int serviceId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null)
            {
                return NotFound();
            }

            var hotelService = new Hotelservice { Hotelid = hotelId, Servicesid = serviceId };
            _context.Hotelservices.Add(hotelService);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Service added successfully!";
            return RedirectToAction("Details", new { id = hotelId });
        }

        [HttpGet]
        public IActionResult AddAmenityToHotel(int hotelId)
        {
            var hotel = _context.Hotels
                .Include(h => h.Hotelamenities)
                .ThenInclude(ha => ha.Amenity)
                .FirstOrDefault(h => h.Hotelid == hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            ViewBag.Amenities = new SelectList(_context.Amenities, "Amenityid", "Amenityname");
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAmenityToHotel(int hotelId, int amenityId)
        {
            var hotel = _context.Hotels.Find(hotelId);
            if (hotel == null)
            {
                return NotFound();
            }

            var hotelAmenity = new Hotelamenity { Hotelid = hotelId, Amenityid = amenityId };
            _context.Hotelamenities.Add(hotelAmenity);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Amenity added successfully!";
            return RedirectToAction("Details", new { id = hotelId });
        }









        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Sliders()
        {
            return RedirectToAction("Index", "Sliders");
        }


        public IActionResult Countries()
        {
            return RedirectToAction("Index", "Countries");
        }

        public IActionResult Cities()
        {
            return RedirectToAction("Index", "Cities");
        }

       

        public IActionResult Amenities()
        {
            return RedirectToAction("Index", "Amenities");
        }
        public IActionResult Services()
        {
            return RedirectToAction("Index", "Services");
        }

        public IActionResult Hotels()
        {
            return RedirectToAction("Index", "Hotels");
        }

        public IActionResult Events()
        {
            return RedirectToAction("Index", "Events");
        }

        public IActionResult Roomtypes()
        {
            return RedirectToAction("Index", "Roomtypes");
        }



        public IActionResult Rooms()
        {
            return RedirectToAction("Index", "Rooms");
        }


      









    }


}



