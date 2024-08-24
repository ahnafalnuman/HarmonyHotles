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
    public class RoomsController : Controller
    {
        private readonly ModelContext _context;

        public RoomsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Rooms.Include(r => r.Hotel).Include(r => r.Roomtype);
            return View(await modelContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Roomtype)
                .FirstOrDefaultAsync(m => m.Roomid == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Hotelid");
            ViewData["Roomtypeid"] = new SelectList(_context.Roomtypes, "Roomtypeid", "Roomtypeid");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roomid,Hotelid,Roomtypeid,Roomnumber,Isavailable,Bedtype,Price,Status")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Hotelid", room.Hotelid);
            ViewData["Roomtypeid"] = new SelectList(_context.Roomtypes, "Roomtypeid", "Roomtypeid", room.Roomtypeid);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Hotelid", room.Hotelid);
            ViewData["Roomtypeid"] = new SelectList(_context.Roomtypes, "Roomtypeid", "Roomtypeid", room.Roomtypeid);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roomid,Hotelid,Roomtypeid,Roomnumber,Isavailable,Bedtype,Price,Status")] Room room)
        {
            if (id != room.Roomid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Roomid))
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
            ViewData["Hotelid"] = new SelectList(_context.Hotels, "Hotelid", "Hotelid", room.Hotelid);
            ViewData["Roomtypeid"] = new SelectList(_context.Roomtypes, "Roomtypeid", "Roomtypeid", room.Roomtypeid);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Roomtype)
                .FirstOrDefaultAsync(m => m.Roomid == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'ModelContext.Rooms'  is null.");
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(decimal id)
        {
          return (_context.Rooms?.Any(e => e.Roomid == id)).GetValueOrDefault();
        }
    }
}
