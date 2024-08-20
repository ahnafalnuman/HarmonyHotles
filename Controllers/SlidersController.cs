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
    public class SlidersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public SlidersController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        // GET: Sliders
        public async Task<IActionResult> Index()
        {
              return _context.Sliders != null ? 
                          View(await _context.Sliders.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Sliders'  is null.");
        }

        // GET: Sliders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Sliderid == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sliderid,Title,Description,ImageFile,Buttontext,Buttonlink,Isactive,Displayorder")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                ///  code insert image 
                if (slider.ImageFile != null)
                {
                    // full path
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" +slider.ImageFile.FileName;  // image name
                    string path = Path.Combine(wwwRootPath + "/images/sliders/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await slider.ImageFile.CopyToAsync(fileStream);
                    }

                    slider.Imagepath = fileName;
                }


                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Sliders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Sliderid,Title,Description,ImageFile,Buttontext,Buttonlink,Isactive,Displayorder")] Slider slider)
        {
            if (id != slider.Sliderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    ///  code insert image 
                    if (slider.ImageFile != null)
                    {
                        // full path
                        string wwwRootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + slider.ImageFile.FileName;  // image name
                        string path = Path.Combine(wwwRootPath + "/images/sliders/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await slider.ImageFile.CopyToAsync(fileStream);
                        }

                        slider.Imagepath = fileName;
                    }


                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Sliderid))
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
            return View(slider);
        }

        // GET: Sliders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Sliderid == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Sliders == null)
            {
                return Problem("Entity set 'ModelContext.Sliders'  is null.");
            }
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(decimal id)
        {
          return (_context.Sliders?.Any(e => e.Sliderid == id)).GetValueOrDefault();
        }
    }
}
