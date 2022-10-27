using System;
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
    public class ExpensivesController : Controller
    {
        private readonly DBProjectContext _context;

        public ExpensivesController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Expensives
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Expensives.Include(e => e.Project);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Expensives/Details/5
        [Authorize(Roles = "Admin,Editor,Lector")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensive = await _context.Expensives
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.ExpensiveId == id);
            if (expensive == null)
            {
                return NotFound();
            }

            return View(expensive);
        }



        // GET: Expensives/Create        
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult CreateByProject(string id)
        {
            ViewData["ProjectId"] = id;
            return View();
        }


        // POST: Expensives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByProject(string id, [Bind("ExpensiveId,Author,LastModification,Type,Amount,Currency,Note,ProjectId")] Expensive expensive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expensive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = id;
            return View(expensive);
        }

        // GET: Expensives/Create        
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create(string id)
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            return View();
        }

        // POST: Expensives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, [Bind("ExpensiveId,Author,LastModification,Type,Amount,Currency,Note,ProjectId")] Expensive expensive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expensive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            return View(expensive);
        }

        // GET: Expensives/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensive = await _context.Expensives.FindAsync(id);
            if (expensive == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", expensive.ProjectId);
            return View(expensive);
        }

        // POST: Expensives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExpensiveId,Author,LastModification,Type,Amount,Currency,Note,ProjectId")] Expensive expensive)
        {
            if (id != expensive.ExpensiveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expensive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpensiveExists(expensive.ExpensiveId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", expensive.ProjectId);
            return View(expensive);
        }

        // GET: Expensives/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensive = await _context.Expensives
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.ExpensiveId == id);
            if (expensive == null)
            {
                return NotFound();
            }

            return View(expensive);
        }

        // POST: Expensives/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var expensive = await _context.Expensives.FindAsync(id);
            _context.Expensives.Remove(expensive);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Internal Methods
        [AllowAnonymous]
        private bool ExpensiveExists(string id)
        {
            return _context.Expensives.Any(e => e.ExpensiveId == id);
        }
        #endregion
    }
}
