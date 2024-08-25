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
    public class RoomtypesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public RoomtypesController(ModelContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 
        }

        // GET: Roomtypes
        public async Task<IActionResult> Index()
        {
            var roomtypes = await _context.Roomtypes
                                          .Include(e => e.Images) // تضمين الصور المرتبطة بكل نوع غرفة
                                          .ToListAsync(); // تحويل النتيجة إلى قائمة وانتظار التنفيذ

            return View(roomtypes); // تمرير القائمة إلى العرض
        }


        // GET: Roomtypes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Roomtypes == null)
            {
                return NotFound();
            }

            var roomtype = await _context.Roomtypes
                .Include(rt => rt.Images)  // تضمين الصور المرتبطة بنوع الغرفة
                .FirstOrDefaultAsync(m => m.Roomtypeid == id);

            if (roomtype == null)
            {
                return NotFound();
            }

            return View(roomtype);
        }


        // GET: Roomtypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roomtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roomtypeid,Typename,Roomtypesdescription,Roomsize")] Roomtype roomtype, List<IFormFile> imageFiles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomtype);
                await _context.SaveChangesAsync();  

                // 
                if (imageFiles != null && imageFiles.Count > 0)
                {
                    foreach (var imageFile in imageFiles)
                    {
                        if (imageFile.Length > 0)
                        {
                  
                            var filePath = Path.Combine(_environment.WebRootPath, "images/Roomtypes", imageFile.FileName);

        
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(stream);
                            }

                  
                            var image = new Image
                            {
                                Imagepath = "/images/Roomtypes/" + imageFile.FileName,
                                Roomtypeid = roomtype.Roomtypeid 
                            };

                            _context.Images.Add(image);
                        }
                    }

                    await _context.SaveChangesAsync(); 
                }

                return RedirectToAction(nameof(Index));  // 
            }
            return View(roomtype);
        }

        // GET: Roomtypes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Roomtypes == null)
            {
                return NotFound();
            }

            var roomtype = await _context.Roomtypes
                                 .Include(h => h.Images)
                .FirstOrDefaultAsync(m => m.Roomtypeid == id);
            if (roomtype == null)
            {
                return NotFound();
            }
            return View(roomtype);
        }

        // POST: Roomtypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roomtypeid,Typename,Roomtypesdescription,Roomsize")] Roomtype roomtype, List<IFormFile> imageFiles)
        {
            if (id != roomtype.Roomtypeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomtype);
                    await _context.SaveChangesAsync();

                    // التحقق من وجود صور جديدة تم رفعها
                    if (imageFiles != null && imageFiles.Count > 0)
                    {
                        // حذف الصور القديمة المرتبطة بنوع الغرفة من قاعدة البيانات ومن المجلد
                        var oldImages = await _context.Images.Where(i => i.Roomtypeid == roomtype.Roomtypeid).ToListAsync();

                        foreach (var oldImage in oldImages)
                        {
                            // حذف الملف الفعلي من المجلد
                            var oldFilePath = Path.Combine(_environment.WebRootPath, oldImage.Imagepath.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // حذف السجلات القديمة من قاعدة البيانات
                        _context.Images.RemoveRange(oldImages);

                        // إضافة الصور الجديدة
                        foreach (var imageFile in imageFiles)
                        {
                            if (imageFile.Length > 0)
                            {
                                // حفظ الصور الجديدة في المجلد
                                var filePath = Path.Combine(_environment.WebRootPath, "images/Roomtypes", imageFile.FileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await imageFile.CopyToAsync(stream);
                                }

                                // إضافة السجلات الجديدة للصور في قاعدة البيانات
                                var image = new Image
                                {
                                    Imagepath = "/images/Roomtypes/" + imageFile.FileName,
                                    Roomtypeid = roomtype.Roomtypeid // استخدم Roomtypeid للربط
                                };

                                _context.Images.Add(image);
                            }
                        }

                        await _context.SaveChangesAsync(); // حفظ التغييرات بعد إضافة الصور الجديدة
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomtypeExists(roomtype.Roomtypeid))
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
            return View(roomtype);
        }


        // GET: Roomtypes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Roomtypes == null)
            {
                return NotFound();
            }

            var roomtype = await _context.Roomtypes
                .FirstOrDefaultAsync(m => m.Roomtypeid == id);
            if (roomtype == null)
            {
                return NotFound();
            }

            return View(roomtype);
        }

        // POST: Roomtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Roomtypes == null)
            {
                return Problem("Entity set 'ModelContext.Roomtypes'  is null.");
            }
            var roomtype = await _context.Roomtypes
                                .Include(h => h.Images)  // جلب الصور المرتبطة
                      .FirstOrDefaultAsync(m => m.Roomtypeid == id);
            if (roomtype != null)
            {
                _context.Images.RemoveRange(roomtype.Images);
              
            }
            _context.Roomtypes.Remove(roomtype);


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomtypeExists(decimal id)
        {
          return (_context.Roomtypes?.Any(e => e.Roomtypeid == id)).GetValueOrDefault();
        }
    }
}
