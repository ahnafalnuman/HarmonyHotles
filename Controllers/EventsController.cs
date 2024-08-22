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
    public class EventsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;



        public EventsController(ModelContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Events.Include(x => x.City).Include(y => y.Country).Include(z => z.Hotel).Include(e => e.Images); ;
            return View(await modelContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(x => x.City)
                .Include(y => y.Country)
                .Include(z => z.Hotel)
                .FirstOrDefaultAsync(m => m.Eventid == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname");
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname");
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Eventid,Countryid,Hotelid,Cityid,Name,Eventsdescription,Location,Ticketprice,Timefrom,Timeto")] Event @event, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                // حفظ الفعالية في قاعدة البيانات أولاً
                _context.Add(@event);
                await _context.SaveChangesAsync();

                // تحقق من وجود صور مرفوعة
                if (images != null && images.Count > 0)
                {
                    // المسار الجذر لحفظ الصور
                    string wwwRootPath = _environment.WebRootPath;

                    foreach (var image in images)
                    {
                        // إنشاء اسم فريد لكل صورة
                        string fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string path = Path.Combine(wwwRootPath + "/images/events/", fileName);

                        // حفظ الصورة في المسار المحدد
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        // إضافة مسار الصورة وربطها بالفعالية
                        _context.Images.Add(new Image
                        {
                            Eventid = @event.Eventid,
                            Imagepath = fileName
                        });
                    }

                    // حفظ التغييرات في قاعدة البيانات بعد إضافة الصور
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname", @event.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname", @event.Countryid);
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Name", @event.Hotelid);
            return View(@event);
        }


        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Images) 
                .FirstOrDefaultAsync(e => e.Eventid == id);

            if (@event == null)
            {
                return NotFound();
            }

            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname", @event.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname", @event.Countryid);
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Name", @event.Hotelid);
            return View(@event);
        }


        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Eventid,Countryid,Hotelid,Cityid,Name,Eventsdescription,Location,Ticketprice,Timefrom,Timeto")] Event @event, List<IFormFile> newImages, List<decimal> deleteImageIds)
        {
            if (id != @event.Eventid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // تحديث بيانات الفعالية
                    _context.Update(@event);
                    await _context.SaveChangesAsync();

                    // حذف الصور القديمة إذا تم تحديدها
                    if (deleteImageIds != null && deleteImageIds.Count > 0)
                    {
                        foreach (var imageId in deleteImageIds)
                        {
                            var image = await _context.Images.FindAsync(imageId);
                            if (image != null)
                            {
                                // حذف الصورة من النظام
                                string imagePath = Path.Combine(_environment.WebRootPath, "images/events", image.Imagepath);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }

                                // حذف الصورة من قاعدة البيانات
                                _context.Images.Remove(image);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }

                    // إضافة الصور الجديدة إذا تم رفعها
                    if (newImages != null && newImages.Count > 0)
                    {
                        string wwwRootPath = _environment.WebRootPath;

                        foreach (var image in newImages)
                        {
                            string fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                            string path = Path.Combine(wwwRootPath + "/images/events/", fileName);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream);
                            }

                            // حفظ الصورة الجديدة في قاعدة البيانات وربطها بالفعالية
                            _context.Images.Add(new Image
                            {
                                Eventid = @event.Eventid,
                                Imagepath = fileName
                            });
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Eventid))
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

            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname", @event.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname", @event.Countryid);
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Name", @event.Hotelid);
            return View(@event);
        }



        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(x => x.City)
                .Include(y => y.Country)
                .Include(z => z.Hotel)
                .FirstOrDefaultAsync(m => m.Eventid == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ModelContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(decimal id)
        {
          return (_context.Events?.Any(e => e.Eventid == id)).GetValueOrDefault();
        }
    }
}
