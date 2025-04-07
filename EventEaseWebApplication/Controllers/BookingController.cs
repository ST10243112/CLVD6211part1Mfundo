using EventEaseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventEaseWebApplication.Controllers
{
    public class BookingController : Controller
    {
        private readonly EventEaseDbContextcs _context;
        public BookingController(EventEaseDbContextcs context)
        {
            _context = context;
        }
        public async Task<IActionResult> index()
        {
            var Booking = await _context.Booking.ToListAsync();
            return View(Booking);
        }

        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "VenueName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking Booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Booking);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Booking created successfully!";
                return RedirectToAction(nameof(Index));

            }
            ViewData["VenueId"] = new SelectList(_context.Venue, "VenueId", "VenueName", Booking.VenueId);
            return View(Booking);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var Booking = await _context.Booking.FirstOrDefaultAsync(m => m.BookingId == id);
            if (Booking == null)
            {
                return NotFound();
            }
            return View(Booking);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            var Booking = await _context.Booking.FirstOrDefaultAsync(m => m.BookingId == id);
            if (Booking == null)
            {
                return NotFound();
            }
            return View(Booking);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(Booking);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Booking deleted successfully!";
            return RedirectToAction(nameof(Index));

        }
        private bool BookingExist(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Booking  = await _context.Booking.FindAsync(id);
            if (Booking == null)
            {
                return NotFound();
            }

            

            return View(Booking);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking Booking)
        {
            if (id != Booking.BookingId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Booking);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Booking updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExist(Booking.BookingId))
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
            
            return View(Booking);
        }

    }

}
