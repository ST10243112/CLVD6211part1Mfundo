using EventEaseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEaseWebApplication.Controllers
{
    public class EventController : Controller
    {
        private readonly EventEaseDbContextcs _context;
        public EventController(EventEaseDbContextcs context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Event = await _context.Event.ToListAsync();
            return View(Event);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Event Event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Event);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Event created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(Event);
        }
        //details action
        public async Task<IActionResult> Details(int? id)
        {
            var Event = await _context.Event.FirstOrDefaultAsync(m => m.EventId == id);
            if (Event == null)
            {
                return NotFound();
            }
            return View(Event);

        }
        //delete action
        public async Task<IActionResult> Delete(int? id)
        {
            var Event = await _context.Event.FirstOrDefaultAsync(m => m.EventId == id);
            if (Event == null)
            {
                return NotFound();
            }
            return View(Event);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Event = await _context.Event.FindAsync(id);
            _context.Event.Remove(Event);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Event deleted successfully!";
            return RedirectToAction(nameof(Index));

        }

        private bool EventExist(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Event = await _context.Event.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }
            return View(Event);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Event Event)
        {
            if (id != Event.EventId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Event);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Event updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExist(Event.EventId))
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
            return View(Event);
        }

    }
}
