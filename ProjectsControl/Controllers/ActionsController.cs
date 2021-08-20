using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsControl.Models;
using Action = ProjectsControl.Models.Action;

namespace ProjectsControl.Controllers
{
    public class ActionsController : Controller
    {
        private readonly DBProjectContext _context;

        public ActionsController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Actions
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Actions.Include(a => a.Employee);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Actions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var action = await _context.Actions
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ActionId == id);
            if (action == null)
            {
                return NotFound();
            }

            return View(action);
        }

        // GET: Actions/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Actions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActionId,Title,DateOfCreation,Author,TypeOfAction,Description,IsActive,EmployeeId")]Action oaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", oaction.EmployeeId);
            return View(oaction);
        }

        // GET: Actions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var action = await _context.Actions.FindAsync(id);
            if (action == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", action.EmployeeId);
            return View(action);
        }

        // POST: Actions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ActionId,Title,DateOfCreation,Author,TypeOfAction,Description,IsActive,EmployeeId")] Action oaction)
        {
            if (id != oaction.ActionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionExists(oaction.ActionId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", oaction.EmployeeId);
            return View(oaction);
        }

        // GET: Actions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var action = await _context.Actions
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ActionId == id);
            if (action == null)
            {
                return NotFound();
            }

            return View(action);
        }

        // POST: Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var action = await _context.Actions.FindAsync(id);
            _context.Actions.Remove(action);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActionExists(string id)
        {
            return _context.Actions.Any(e => e.ActionId == id);
        }
    }
}
