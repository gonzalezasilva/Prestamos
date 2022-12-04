using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationPrestamos.DataAccess;
using WebApplicationPrestamos.Entities;


namespace WebApplicationPrestamos.Controllers
{
    public class ThingsController : Controller
    {
        private readonly ThingsContext _context;

        public ThingsController(ThingsContext context)
        {
            _context = context;
        }

        // GET: Things
        public async Task<IActionResult> Index()
        {
            var thingsContext = _context.Things.Include(t => t.Category);
            return View(await thingsContext.ToListAsync());
        }

        // GET: Things/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Things == null)
            {
                return NotFound();
            }

            var thing = await _context.Things
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thing == null)
            {
                return NotFound();
            }

            return View(thing);
        }

        // GET: Things/Create
        public IActionResult Create()
        { 
           
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description");
            return View();
        }

        // POST: Things/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.ThingsDto request)
        {
         
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category == null)
                return NotFound();

            var newThing = new Thing
                  {
                      Description = request.Description,
                      CreateDate = request.CreateDate,
                      Category = category
                  };
         
             _context.Add(newThing);
             await _context.SaveChangesAsync();
           
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", newThing.CategoryId);
            return View(newThing);
        }


        // GET: Things/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Things == null)
            {
                return NotFound();
            }

            var thing = await _context.Things.FindAsync(id);
            if (thing == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", thing.CategoryId);
            return View(thing);
        }

        // POST: Things/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,CreateDate,CategoryId,Id")] Thing thing)
        {
            if (id != thing.Id)
            {
                return NotFound();
            }

            _context.Update(thing);
            await _context.SaveChangesAsync();
           
            return RedirectToAction(nameof(Index));
        
        }

        // GET: Things/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Things == null)
            {
                return NotFound();
            }

            var thing = await _context.Things
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thing == null)
            {
                return NotFound();
            }

            return View(thing);
        }

        // POST: Things/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Things == null)
            {
                return Problem("Entity set 'ThingsContext.Things'  is null.");
            }
            var thing = await _context.Things.FindAsync(id);
            if (thing != null)
            {
                _context.Things.Remove(thing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThingExists(int id)
        {
            return _context.Things.Any(e => e.Id == id);
        }
    }
}
