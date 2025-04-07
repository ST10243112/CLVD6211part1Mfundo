using EventEaseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseWebApplication.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseDbContextcs _context;

        public VenueController(EventEaseDbContextcs context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Venue = await _context.Venue.ToListAsync();
            return View(Venue);
        }
        public IActionResult Create() //creates a view 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venue Venue)//
        {
            if (ModelState.IsValid)
            {
                _context.Add(Venue);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Venue created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(Venue);
        }

        //details action
        public async Task<IActionResult> Details(int? id)
        {
            var Venue = await _context.Venue.FirstOrDefaultAsync(m => m.VenueId == id);
            if (Venue == null)
            {
                return NotFound();
            }
            return View(Venue);

        }

        //delete action 
        public async Task<IActionResult> Delete(int? id)
        {
            var Venue = await _context.Venue.FirstOrDefaultAsync(m => m.VenueId == id);
            if (Venue == null)
            {
                return NotFound();
            }
            return View(Venue);

        }

        //updtaes venue list based on delete action 
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Venue = await _context.Venue.FindAsync(id);
            _context.Venue.Remove(Venue);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Venue deleted successfully!";
            return RedirectToAction(nameof(Index));

        }

        //action for edit 
        private bool VenueExist(int id)
        {
            return _context.Venue.Any(e => e.VenueId == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Venue = await _context.Venue.FindAsync(id);
            if (Venue == null)
            {
                return NotFound();
            }
            return View(Venue);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Venue Venue)
        {
            if (id != Venue.VenueId) 
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                try
                {
                    _context.Update(Venue);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Venue updated!";
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (!VenueExist(Venue.VenueId))
                    {
                        return NotFound();
                    }else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Venue);
        }

    }
}
