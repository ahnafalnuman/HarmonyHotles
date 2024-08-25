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
    public class ServicesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;


        public ServicesController(ModelContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
              return _context.Services != null ? 
                          View(await _context.Services.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Services'  is null.");
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Servicesid == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Servicesid,Servicename,Serviceprice,Imagepath,Status ,ImageFile")] Service service)
        {
            if (ModelState.IsValid)
            {

                ///  code insert image 
                if (service.ImageFile != null)
                {
                    // full path
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + service.ImageFile.FileName;  // image name
                    string path = Path.Combine(wwwRootPath + "/images/Services/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await service.ImageFile.CopyToAsync(fileStream);
                    }

                    service.Imagepath = fileName;
                }
         
              
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Servicesid,Servicename,Serviceprice,Imagepath,Status ,ImageFile")] Service service)
        {
            if (id != service.Servicesid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {   ///  code insert image 
                    if (service.ImageFile != null)
                    {
                        // full path
                        string wwwRootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + service.ImageFile.FileName;  // image name
                        string path = Path.Combine(wwwRootPath + "/images/Services/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await service.ImageFile.CopyToAsync(fileStream);
                        }

                        service.Imagepath = fileName;
                    }

                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Servicesid))
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
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Servicesid == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'ModelContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(decimal id)
        {
          return (_context.Services?.Any(e => e.Servicesid == id)).GetValueOrDefault();
        }
    }
}
