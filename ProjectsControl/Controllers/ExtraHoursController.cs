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
    public class ExtraHoursController : Controller
    {
        private readonly DBProjectContext _context;

        public ExtraHoursController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: ExtraHours
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.ExtraHours.Include(e => e.Asistance).Include(e => e.Employee);
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
                .FirstOrDefaultAsync(m => m.ExtraHourId == id);
            if (extraHour == null)
            {
                return NotFound();
            }

            return View(extraHour);
        }

        // GET: ExtraHours/Create
        [Authorize(Roles = "Editor,Admin")]
        public IActionResult Create()
        {
            ViewData["AsistanceId"] = new SelectList(_context.Asistances, "AsistanceId", "AsistanceId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
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
        [Authorize(Roles = "Editor,Admin")]
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
            return View(extraHour);
        }

        [Authorize(Roles = "Admin,Lector")]
        public async Task<IActionResult> WithoutPaid()
        {
            var aux = (from extra in _context.ExtraHours select extra).Where(E => E.IsPaid == false).OrderBy(E => E.EmployeeId).Include(e => e.Asistance).Include(e => e.Employee).ToList();
            return View(aux);
        }

        // GET: ExtraHours/Edit/5
        [Authorize(Roles = "Editor,Admin")]
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
            return View(extraHour);
        }

        [HttpPost]
        [Authorize(Roles = "Editor,Admin")]
        public async Task<IActionResult> addExtra([Bind("ExtraHourId,BeginTime,EndTime,TypeOfHour,Reason,Notes,IsPaid,EmployeeId,AsistanceId,WeekId")] ExtraHour extraHour)
        {
            try
            {
                _context.ExtraHours.Add(extraHour);
                _context.SaveChanges();
                ViewBag.Message = $"Hora extra registrada";
                // redirect to action "details" with the parameter ID
                return RedirectToAction("Details", routeValues: new { id = extraHour.ExtraHourId });
            }
            catch (Exception ef)
            {
                ViewBag.FlagExistHour = false;
                ViewBag.Message = $"Error al ingresar la hora extra. {ef.Message}";
                var tmpId = extraHour.AsistanceId;
                var tmpResult = (from asis in _context.Asistances select asis).Include(A => A.Employee).FirstOrDefault(A => A.AsistanceId.Equals(tmpId));
                ExtraHour tmpObj = extraHour;
                tmpObj.Employee = _context.Employees.FirstOrDefault(E => E.EmployeeId == tmpObj.EmployeeId);
                tmpObj.Asistance = _context.Asistances.FirstOrDefault(A => A.AsistanceId == tmpObj.AsistanceId);                
                return View(tmpObj);
            }
        }

        [Authorize(Roles = "Admin,Lector")]
        [HttpGet]
        public async Task<IActionResult> addExtra(string id)
        {
            /// Check if exist any extra linked with the asistance

            var tmpExtraHour = (from extH in _context.ExtraHours select extH).FirstOrDefault(E => E.AsistanceId == id);
            if (tmpExtraHour != null)
            {
                ViewBag.ExistHourId = tmpExtraHour.ExtraHourId;
                ViewBag.FlagExistHour = true;
            }
            else
            {
                ViewBag.FlagExistHour = false;
            }
            var tmpResult = (from asis in _context.Asistances select asis).Include(A => A.Employee).FirstOrDefault(A => A.AsistanceId == id);
            ExtraHour extraObj = new ExtraHour()
            {
                AsistanceId = tmpResult.AsistanceId,
                Employee = tmpResult.Employee,
                EmployeeId = tmpResult.EmployeeId,
                BeginTime = tmpResult.DateOfBegin,
                EndTime = tmpResult.DateOfEnd,
                Asistance = tmpResult,
                IsPaid = false
            };
            return View(extraObj);
        }
        // POST: ExtraHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Editor,Admin")]
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
            return View(extraHour);
        }

        // GET: ExtraHours/Delete/5
        [Authorize(Roles = "Editor,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extraHour = await _context.ExtraHours
                .Include(e => e.Asistance)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.ExtraHourId == id);
            if (extraHour == null)
            {
                return NotFound();
            }

            return View(extraHour);
        }

        // POST: ExtraHours/Delete/5
        [Authorize(Roles = "Editor,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var extraHour = await _context.ExtraHours.FindAsync(id);
            _context.ExtraHours.Remove(extraHour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        private bool ExtraHourExists(string id)
        {
            return _context.ExtraHours.Any(e => e.ExtraHourId == id);
        }


    }
}
