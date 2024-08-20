using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HarmonyHotles.Models;
using Microsoft.Extensions.Hosting;

namespace HarmonyHotles.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public CountriesController(ModelContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }








        // GET: Countries
        public async Task<IActionResult> Index()
        {
          
            return _context.Countries != null ? 
                          View(await _context.Countries.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Countries'  is null.");
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Countryid == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Countryid,Countryname,ImageFile")] Country country)
        {
            if (ModelState.IsValid)
            { ///  code insert image 
                if (country.ImageFile != null)
                {
                    // full path
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + country.ImageFile.FileName;  // image name
                    string path = Path.Combine(wwwRootPath + "/images/Countries/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await country.ImageFile.CopyToAsync(fileStream);
                    }

                    country.Imagepath = fileName;
                }
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Countryid,Countryname,ImageFile")] Country country)
        {
            if (id != country.Countryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {///  code insert image 
                    if (country.ImageFile != null)
                    {
                        // full path
                        string wwwRootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + country.ImageFile.FileName;  // image name
                        string path = Path.Combine(wwwRootPath + "/images/Countries/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await country.ImageFile.CopyToAsync(fileStream);
                        }

                        country.Imagepath = fileName;
                    }
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Countryid))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Countryid == id);
            if (country == null)
            {
                return NotFound();
            }
          

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'ModelContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {

                _context.Countries.Remove(country);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(decimal id)
        {
          return (_context.Countries?.Any(e => e.Countryid == id)).GetValueOrDefault();
        }
    }
}
