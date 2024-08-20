using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HarmonyHotles.Models;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;

namespace HarmonyHotles.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public CitiesController(ModelContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Cities.Include(c => c.Country);
            return View(await modelContext.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Cityid == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cityid,Cityname,Countryid,ImageFile")] City city)
        {
            if (ModelState.IsValid)
            {  ///  code insert image 
                if (city.ImageFile != null)
                {
                    // full path
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + city.ImageFile.FileName;  // image name
                    string path = Path.Combine(wwwRootPath + "/images/cities/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await city.ImageFile.CopyToAsync(fileStream);
                    }

                    city.Imagepath = fileName;
                }
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", city.Countryid);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname", city.Countryid);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Cityid,Cityname,Countryid,ImageFile")] City city)
        {
            if (id != city.Cityid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ///  code insert image 
                    if (city.ImageFile != null)
                    {
                        // full path
                        string wwwRootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + city.ImageFile.FileName;  // image name
                        string path = Path.Combine(wwwRootPath + "/images/cities/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await city.ImageFile.CopyToAsync(fileStream);
                        }

                        city.Imagepath = fileName;
                    }
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Cityid))
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
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryid", city.Countryid);
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Cityid == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'ModelContext.Cities'  is null.");
            }
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(decimal id)
        {
          return (_context.Cities?.Any(e => e.Cityid == id)).GetValueOrDefault();
        }
    }
}
