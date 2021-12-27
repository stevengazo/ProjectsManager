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
    public class AsistancesController : Controller
    {
        private readonly DBProjectContext _context;

        public AsistancesController(DBProjectContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DailyCreate()
        {
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId");
            ViewBag.ListOfEmployees = _context.Employees.ToList();

            var listProj = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).ToList();
            var dicProj = new Dictionary<string, string>();
            foreach (var item in listProj)
            {
                dicProj.Add(item.ProjectId, item.ProjectName);
            }
            ViewBag.Projects = dicProj;

            return View();
        }

        // GET: Asistances
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Asistances.Include(a => a.Employee).Include(a => a.Project).Include(a => a.Week);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Asistances/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistance = await _context.Asistances
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Include(a => a.Week)
                .FirstOrDefaultAsync(m => m.AsistanceId == id);
            if (asistance == null)
            {
                return NotFound();
            }

            return View(asistance);
        }

        // GET: Asistances/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ProjectName"] = new SelectList(_context.Employees, "ProjectName", "ProjectName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId");

            var aux = (from a in _context.Employees select a).ToList();
            var dicEmpl = new Dictionary<string, string>();
            foreach (var item in aux)
            {
                dicEmpl.Add(item.EmployeeId, item.Name);
            }

            ViewBag.ListOfEmployes = dicEmpl;

            
            
            var listProj = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).ToList();
            var dicProj = new Dictionary<string, string>();
            foreach (var item in listProj)
            {
                dicProj.Add(item.ProjectId, item.ProjectName);
            }
            ViewBag.Projects = dicProj;
            return View();
        }

        public async Task<IActionResult> Search()
        {
            return View(new List<Asistance>());
        }

        // POST: Asistances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsistanceId,DateOfBegin,DateOfEnd,EmployeeId,ProjectId,WeekId")] Asistance asistance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", asistance.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", asistance.ProjectId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", asistance.WeekId);
            var aux = (from a in _context.Employees select a).ToList();
            var dicEmpl = new Dictionary<string, string>();
            foreach (var item in aux)
            {
                dicEmpl.Add(item.EmployeeId, item.Name);
            }

            ViewBag.ListOfEmployes = dicEmpl;

            var listProj = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).ToList();
            var dicProj = new Dictionary<string, string>();
            foreach (var item in listProj)
            {
                dicProj.Add(item.ProjectId, item.ProjectName);
            }
            ViewBag.Projects = dicProj;
            return  View(asistance);
        }

        // GET: Asistances/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistance = await _context.Asistances.FindAsync(id);
            if (asistance == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", asistance.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", asistance.ProjectId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", asistance.WeekId);
            return View(asistance);
        }

        // POST: Asistances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AsistanceId,DateOfBegin,DateOfEnd,EmployeeId,ProjectId,WeekId")] Asistance asistance)
        {
            if (id != asistance.AsistanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistanceExists(asistance.AsistanceId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", asistance.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", asistance.ProjectId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", asistance.WeekId);
            return View(asistance);
        }

        // GET: Asistances/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistance = await _context.Asistances
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Include(a => a.Week)
                .FirstOrDefaultAsync(m => m.AsistanceId == id);
            if (asistance == null)
            {
                return NotFound();
            }

            return View(asistance);
        }

        // POST: Asistances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var asistance = await _context.Asistances.FindAsync(id);
            _context.Asistances.Remove(asistance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistanceExists(string id)
        {
            return _context.Asistances.Any(e => e.AsistanceId == id);
        }
    }
}
