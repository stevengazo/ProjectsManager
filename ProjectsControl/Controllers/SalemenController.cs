using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsControl.Models;

namespace ProjectsControl.Controllers
{
    public class SalemenController : Controller
    {
        private readonly DBProjectContext _context;

        public SalemenController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Salemen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salemans.ToListAsync());
        }

        // GET: Salemen/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleman = await _context.Salemans
                .FirstOrDefaultAsync(m => m.SalemanId == id);
            if (saleman == null)
            {
                return NotFound();
            }

            return View(saleman);
        }

        // GET: Salemen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salemen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalemanId,Name")] Saleman saleman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleman);
        }

        // GET: Salemen/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleman = await _context.Salemans.FindAsync(id);
            if (saleman == null)
            {
                return NotFound();
            }
            return View(saleman);
        }

        // POST: Salemen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SalemanId,Name")] Saleman saleman)
        {
            if (id != saleman.SalemanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalemanExists(saleman.SalemanId))
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
            return View(saleman);
        }

        // GET: Salemen/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleman = await _context.Salemans
                .FirstOrDefaultAsync(m => m.SalemanId == id);
            if (saleman == null)
            {
                return NotFound();
            }

            return View(saleman);
        }

        // POST: Salemen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var saleman = await _context.Salemans.FindAsync(id);
            _context.Salemans.Remove(saleman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Search()
        {
            return View(new List<Saleman>());
        }

        private bool SalemanExists(string id)
        {
            return _context.Salemans.Any(e => e.SalemanId == id);
        }
    }
}
