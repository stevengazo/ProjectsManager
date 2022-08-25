﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsControl.Models;
using Microsoft.AspNetCore.Authorization;
namespace ProjectsControl.Controllers
{
    [Authorize]
    public class OffersController : Controller
    {
        private readonly DBProjectContext _context;

        public OffersController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Offers
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Offers.Include(o => o.Customer);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Offers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // GET: Offers/Create
        [Authorize(Roles = "Admin,Editor,Sales")]
        public IActionResult Create()
        {
            var tmplist = _context.Customers.ToList();
            ViewData["CustomerId"] = tmplist;
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor,Sales")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberOfOffer,NumberOfOffer,Title,Type,Description,Author,SaleManName,DateOfCreation,LastEdition,CustomerId")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var tmplist = _context.Customers.ToList();
            ViewData["CustomerId"] = tmplist;
            return View(offer);
        }

        // GET: Offers/Edit/5
        [Authorize(Roles = "Admin,Editor,Sales")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            var tmplist = _context.Customers.ToList();
            ViewData["CustomerId"] = tmplist;
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor,Sales")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumberOfOffer,NumberOfOffer,Title,Type,Description,Author,SaleManName,DateOfCreation,LastEdition,CustomerId")] Offer offer)
        {
            if (id != offer.OfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.OfferId))
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
            var tmplist = _context.Customers.ToList();
            ViewData["CustomerId"] = tmplist;
            return View(offer);
        }

        // GET: Offers/Delete/5
        [Authorize(Roles = "Admin,Editor,Sales")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [Authorize(Roles = "Admin,Editor,Sales")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var offer = await _context.Offers.FindAsync(id);
            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        private bool OfferExists(string id)
        {
            return _context.Offers.Any(e => e.OfferId == id);
        }
    }
}
