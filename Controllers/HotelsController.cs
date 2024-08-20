using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HarmonyHotles.Models;

namespace HarmonyHotles.Controllers
{
    public class HotelsController : Controller
    {
        private readonly ModelContext _context;

        public HotelsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Hotels.Include(h => h.City).Include(h => h.Country);
            return View(await modelContext.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.City)
                .Include(h => h.Country)
                .FirstOrDefaultAsync(m => m.Hotelid == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid");
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotelid,Name,Location,Rating,Hotelsdescription,Countryid,Cityid")] Hotel hotel, List<IFormFile> HotelImages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid", hotel.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", hotel.Countryid);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid", hotel.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", hotel.Countryid);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hotelid,Name,Location,Rating,Hotelsdescription,Countryid,Cityid")] Hotel hotel)
        {
            if (id != hotel.Hotelid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Hotelid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityid", hotel.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", hotel.Countryid);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.City)
                .Include(h => h.Country)
                .FirstOrDefaultAsync(m => m.Hotelid == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Hotels == null)
            {
                return Problem("Entity set 'ModelContext.Hotels'  is null.");
            }
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(decimal id)
        {
          return (_context.Hotels?.Any(e => e.Hotelid == id)).GetValueOrDefault();
        }
    }
}
