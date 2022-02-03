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
    public class ExtraHoursController : Controller
    {
        private readonly DBProjectContext _context;

        public ExtraHoursController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: ExtraHours
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.ExtraHours.Include(e => e.Asistance).Include(e => e.Employee).Include(e => e.Week);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: ExtraHours/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extraHour = await _context.ExtraHours
                .Include(e => e.Asistance)
                .Include(e => e.Employee)
                .Include(e => e.Week)
                .FirstOrDefaultAsync(m => m.ExtraHourId == id);
            if (extraHour == null)
            {
                return NotFound();
            }

            return View(extraHour);
        }

        // GET: ExtraHours/Create
        public IActionResult Create()
        {
            ViewData["AsistanceId"] = new SelectList(_context.Asistances, "AsistanceId", "AsistanceId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId");
            var aux = (from a in _context.Employees select a).ToList();
            var dicEmpl = new Dictionary<string, string>();
            foreach (var item in aux)
            {
                dicEmpl.Add(item.EmployeeId, item.Name);
            }
            ViewBag.ListOfEmployes = dicEmpl;
            return View();
        }

        // POST: ExtraHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExtraHourId,BeginTime,EndTime,TypeOfHour,Reason,Notes,IsPaid,EmployeeId,AsistanceId,WeekId")] ExtraHour extraHour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extraHour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsistanceId"] = new SelectList(_context.Asistances, "AsistanceId", "AsistanceId", extraHour.AsistanceId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", extraHour.EmployeeId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", extraHour.WeekId);
            return View(extraHour);
        }

        public async Task<IActionResult> WithoutPaid()
        {
            var aux = (from extra in _context.ExtraHours select extra).Where(E => E.IsPaid == false).OrderBy(E=>E.EmployeeId).Include(e => e.Asistance).Include(e => e.Employee).Include(e => e.Week).ToList();
            return View(aux);
        }

        // GET: ExtraHours/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extraHour = await _context.ExtraHours.FindAsync(id);
            if (extraHour == null)
            {
                return NotFound();
            }
            ViewData["AsistanceId"] = new SelectList(_context.Asistances, "AsistanceId", "AsistanceId", extraHour.AsistanceId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", extraHour.EmployeeId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", extraHour.WeekId);
            return View(extraHour);
        }

        [HttpPost]
        public async Task<IActionResult> addExtra([Bind("ExtraHourId,BeginTime,EndTime,TypeOfHour,Reason,Notes,IsPaid,EmployeeId,AsistanceId,WeekId")] ExtraHour extraHour)
        {
            try
            {               
                _context.ExtraHours.Add(extraHour);
                _context.SaveChanges();                
                ViewBag.Message = $"Hora extra registrada";
                return RedirectToAction(nameof(Details));
            }
            catch(Exception ef)
            {
                ViewBag.Message = $"Error al ingresar la hora extra. {ef.Message}";
                var tmpId = extraHour.AsistanceId;
                var tmpResult = (from asis in _context.Asistances select asis).Include(A => A.Week).Include(A => A.Employee).FirstOrDefault(A => A.AsistanceId.Equals(tmpId));
                ExtraHour tmpObj = extraHour;
                tmpObj.Employee = _context.Employees.FirstOrDefault(E => E.EmployeeId == tmpObj.EmployeeId);
                tmpObj.Asistance = _context.Asistances.FirstOrDefault(A => A.AsistanceId == tmpObj.AsistanceId);
                tmpObj.Week = _context.Week.FirstOrDefault(W => W.WeekId == tmpObj.WeekId);
                return View(tmpObj);
            }                       
        }


        [HttpGet]
        public async Task<IActionResult> addExtra(string id)
        {
            var tmpResult = (from asis in _context.Asistances select asis).Include(A => A.Week).Include(A => A.Employee).FirstOrDefault(A => A.AsistanceId == id);
            ExtraHour extraObj = new ExtraHour() {
                AsistanceId = tmpResult.AsistanceId,
                Employee = tmpResult.Employee,
                EmployeeId = tmpResult.EmployeeId,
                Week = tmpResult.Week,
                WeekId = tmpResult.WeekId,
                BeginTime= tmpResult.DateOfBegin,
                EndTime = tmpResult.DateOfEnd,
                IsPaid = false
            };
            return View(extraObj);
        }
        // POST: ExtraHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExtraHourId,BeginTime,EndTime,TypeOfHour,Reason,Notes,IsPaid,EmployeeId,AsistanceId,WeekId")] ExtraHour extraHour)
        {
            if (id != extraHour.ExtraHourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extraHour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtraHourExists(extraHour.ExtraHourId))
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
            ViewData["AsistanceId"] = new SelectList(_context.Asistances, "AsistanceId", "AsistanceId", extraHour.AsistanceId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", extraHour.EmployeeId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", extraHour.WeekId);
            return View(extraHour);
        }

        // GET: ExtraHours/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extraHour = await _context.ExtraHours
                .Include(e => e.Asistance)
                .Include(e => e.Employee)
                .Include(e => e.Week)
                .FirstOrDefaultAsync(m => m.ExtraHourId == id);
            if (extraHour == null)
            {
                return NotFound();
            }

            return View(extraHour);
        }

        // POST: ExtraHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var extraHour = await _context.ExtraHours.FindAsync(id);
            _context.ExtraHours.Remove(extraHour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraHourExists(string id)
        {
            return _context.ExtraHours.Any(e => e.ExtraHourId == id);
        }

      
    }
}
