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
    public class Of_QuoController : Controller
    {
        private readonly DBProjectContext _context;

        public Of_QuoController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Of_Quo
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Of_Quos.Include(o => o.Offer).Include(o => o.Quotation);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Of_Quo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var of_Quo = await _context.Of_Quos
                .Include(o => o.Offer)
                .Include(o => o.Quotation)
                .FirstOrDefaultAsync(m => m.Of_QuoId == id);
            if (of_Quo == null)
            {
                return NotFound();
            }

            return View(of_Quo);
        }

        // GET: Of_Quo/Create
        public IActionResult Create()
        {
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId");
            ViewData["QuotationId"] = new SelectList(_context.Quotations, "QuotationId", "QuotationId");
            return View();
        }

        // POST: Of_Quo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Of_QuoId,IsModicable,OfferId,QuotationId")] Of_Quo of_Quo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(of_Quo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", of_Quo.OfferId);
            ViewData["QuotationId"] = new SelectList(_context.Quotations, "QuotationId", "QuotationId", of_Quo.QuotationId);
            return View(of_Quo);
        }

        // GET: Of_Quo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var of_Quo = await _context.Of_Quos.FindAsync(id);
            if (of_Quo == null)
            {
                return NotFound();
            }
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", of_Quo.OfferId);
            ViewData["QuotationId"] = new SelectList(_context.Quotations, "QuotationId", "QuotationId", of_Quo.QuotationId);
            return View(of_Quo);
        }

        // POST: Of_Quo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Of_QuoId,IsModicable,OfferId,QuotationId")] Of_Quo of_Quo)
        {
            if (id != of_Quo.Of_QuoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(of_Quo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Of_QuoExists(of_Quo.Of_QuoId))
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
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", of_Quo.OfferId);
            ViewData["QuotationId"] = new SelectList(_context.Quotations, "QuotationId", "QuotationId", of_Quo.QuotationId);
            return View(of_Quo);
        }

        // GET: Of_Quo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var of_Quo = await _context.Of_Quos
                .Include(o => o.Offer)
                .Include(o => o.Quotation)
                .FirstOrDefaultAsync(m => m.Of_QuoId == id);
            if (of_Quo == null)
            {
                return NotFound();
            }

            return View(of_Quo);
        }

        // POST: Of_Quo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var of_Quo = await _context.Of_Quos.FindAsync(id);
            _context.Of_Quos.Remove(of_Quo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Of_QuoExists(string id)
        {
            return _context.Of_Quos.Any(e => e.Of_QuoId == id);
        }
    }
}
