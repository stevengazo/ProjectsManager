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
    public class QuotationsController : Controller
    {
        private readonly DBProjectContext _context;

        public QuotationsController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Quotations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quotations.ToListAsync());
        }

        // GET: Quotations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations
                .FirstOrDefaultAsync(m => m.QuotationId == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // GET: Quotations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuotationId,Type,Description,MaterialAmount,MecsaAmountMaterial,ViaticsAmount,UnexpectedAmount,KMAmount,TotalAmount,QuantityOfDays,QuantityOfEmployees,Author,UserModification,DateCreation,LastModification")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quotation);
        }

        // GET: Quotations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation == null)
            {
                return NotFound();
            }
            return View(quotation);
        }

        // POST: Quotations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("QuotationId,Type,Description,MaterialAmount,MecsaAmountMaterial,ViaticsAmount,UnexpectedAmount,KMAmount,TotalAmount,QuantityOfDays,QuantityOfEmployees,Author,UserModification,DateCreation,LastModification")] Quotation quotation)
        {
            if (id != quotation.QuotationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotationExists(quotation.QuotationId))
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
            return View(quotation);
        }

        // GET: Quotations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations
                .FirstOrDefaultAsync(m => m.QuotationId == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // POST: Quotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var quotation = await _context.Quotations.FindAsync(id);
            _context.Quotations.Remove(quotation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotationExists(string id)
        {
            return _context.Quotations.Any(e => e.QuotationId == id);
        }
    }
}
