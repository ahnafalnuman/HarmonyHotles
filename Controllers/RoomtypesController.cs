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


        public RoomtypesController(ModelContext context  , IWebHostEnvironment environment) 
        {
            _context = context;
            _environment = environment; 
        }

        // GET: Roomtypes
        public async Task<IActionResult> Index()
        {
              return _context.Roomtypes != null ? 
                          View(await _context.Roomtypes.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Roomtypes'  is null.");
        }

        // GET: Roomtypes/Details/5
        public async Task<IActionResult> Details(decimal? id)
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
        public async Task<IActionResult> Create([Bind("Roomtypeid,Typename,Roomtypesdescription,Roomsize,ImageFiles")] Roomtype roomtype)
        {
            if (ModelState.IsValid)
            {
                // إضافة نوع الغرفة في قاعدة البيانات
                _context.Add(roomtype);
                await _context.SaveChangesAsync();

                // التحقق من وجود ملفات صور مرفوعة
                if (roomtype.ImageFiles != null && roomtype.ImageFiles.Count > 0)
                {
                    // الحصول على المسار الجذري لمجلد wwwroot
                    string wwwRootPath = _environment.WebRootPath;

                    // التكرار على كل صورة مرفوعة
                    foreach (var imageFile in roomtype.ImageFiles)
                    {
                        // إنشاء اسم فريد للصورة
                        string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                        // تحديد المسار الكامل لحفظ الصورة في مجلد roomtype
                        string path = Path.Combine(wwwRootPath + "/images/roomtype/", fileName);

                        // حفظ الصورة في المجلد المحدد
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // إنشاء كائن Image وإعداد المسار
                        var image = new Image
                        {
                            Imagepath = "/images/roomtype/" + fileName,
                            Roomtypeid = roomtype.Roomtypeid
                        };

                        // إضافة الصورة إلى قاعدة البيانات
                        _context.Images.Add(image);
                    }

                    // حفظ التغييرات في قاعدة البيانات
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
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

            var roomtype = await _context.Roomtypes.FindAsync(id);
            if (roomtype == null)
            {
                return NotFound();
            }
            return View(roomtype);
        }

        // POST: Roomtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roomtypeid,Typename,Roomtypesdescription,Roomsize ,ImageFiles")] Roomtype roomtype)
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
            var roomtype = await _context.Roomtypes.FindAsync(id);
            if (roomtype != null)
            {
                _context.Roomtypes.Remove(roomtype);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomtypeExists(decimal id)
        {
          return (_context.Roomtypes?.Any(e => e.Roomtypeid == id)).GetValueOrDefault();
        }
    }
}
