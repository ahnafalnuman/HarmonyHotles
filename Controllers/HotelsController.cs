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
        private readonly IWebHostEnvironment _environment;

        public HotelsController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
       










    // GET: Hotels
    public async Task<IActionResult> Index()
        {
            var modelContext = _context.Hotels.Include(h => h.City)
                                              .Include(h => h.Country)
                                              .Include(h=>h.Images);
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
                .Include(h => h.Images)
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
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname");
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotelid,Name,Location,Rating,Hotelsdescription,Countryid,Cityid")] Hotel hotel, List<IFormFile> imageFiles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                // حفظ الصور
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    foreach (var imageFile in imageFiles)
                    {
                        if (imageFile.Length > 0)
                        {
                            // مسار الحفظ في wwwroot
                            var filePath = Path.Combine(_environment.WebRootPath, "images/hotels", imageFile.FileName);

                            // حفظ الصورة على السيرفر
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(stream);
                            }

                            // إضافة الصورة إلى قاعدة البيانات
                            var image = new Image
                            {
                                Imagepath = "/images/hotels/" + imageFile.FileName,
                                Hotelid = hotel.Hotelid
                            };

                            _context.Images.Add(image);
                        }
                    }

                    await _context.SaveChangesAsync(); // حفظ التغييرات في قاعدة البيانات
                }


                //
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname", hotel.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname", hotel.Countryid);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.Images)
                .FirstOrDefaultAsync(m => m.Hotelid == id);

            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname", hotel.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname", hotel.Countryid);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hotelid,Name,Location,Rating,Hotelsdescription,Countryid,Cityid")] Hotel hotel, List<IFormFile> imageFiles)
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

                    // التحقق من وجود صور جديدة تم رفعها
                    if (imageFiles != null && imageFiles.Count > 0)
                    {
                        // حذف الصور القديمة المرتبطة بالفندق من قاعدة البيانات
                        var oldImages = await _context.Images.Where(i => i.Hotelid == hotel.Hotelid).ToListAsync();
                        _context.Images.RemoveRange(oldImages);

                        // إضافة الصور الجديدة
                        foreach (var imageFile in imageFiles)
                        {
                            if (imageFile.Length > 0)
                            {
                                // حفظ الصور الجديدة في المجلد
                                var filePath = Path.Combine(_environment.WebRootPath, "images/hotels", imageFile.FileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await imageFile.CopyToAsync(stream);
                                }

                                // إضافة السجلات الجديدة للصور في قاعدة البيانات
                                var image = new Image
                                {
                                    Imagepath = "/images/hotels/" + imageFile.FileName,
                                    Hotelid = hotel.Hotelid
                                };

                                _context.Images.Add(image);
                            }
                        }

                        await _context.SaveChangesAsync(); // حفظ التغييرات بعد إضافة الصور الجديدة
                    }

                    //END
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
            ViewData["Cityid"] = new SelectList(_context.Cities, "Cityid", "Cityname", hotel.Cityid);
            ViewData["Countryid"] = new SelectList(_context.Countries, "Countryid", "Countryname", hotel.Countryid);
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
                return Problem("Entity set 'ModelContext.Hotels' is null.");
            }

            // العثور على الفندق مع الصور المرتبطة
            var hotel = await _context.Hotels
                .Include(h => h.Images)  // جلب الصور المرتبطة
                .FirstOrDefaultAsync(m => m.Hotelid == id);

            if (hotel != null)
            {
                // حذف الصور التابعة إذا كانت موجودة
                if (hotel.Images != null && hotel.Images.Any())
                {
                    _context.Images.RemoveRange(hotel.Images);  // حذف الصور التابعة أولاً
                }

                // حذف الفندق
                _context.Hotels.Remove(hotel);

                // حفظ التغييرات
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool HotelExists(decimal id)
        {
          return (_context.Hotels?.Any(e => e.Hotelid == id)).GetValueOrDefault();
        }
    }
}
