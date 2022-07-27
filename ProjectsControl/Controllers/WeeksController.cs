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
    public class WeeksController : Controller
    {
        private readonly DBProjectContext _context;

        public WeeksController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Weeks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Week.ToListAsync());
        }

        // GET: Weeks/Details/5        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var week = await _context.Week
                .FirstOrDefaultAsync(m => m.WeekId == id);
            if (week == null)
            {
                return NotFound();
            }

            return View(week);
        }

        // GET: Weeks/Create
        [Authorize(Roles = "Admin,editor")]
        public IActionResult Create()
        {
            GetCodeOfWeek(out string code, out int eweek, out int year);
            ViewBag.Message = "";
            ViewBag.DateEnd = GetLastDateTime();
            ViewBag.Code = code;
            ViewBag.BeginDate = (from eek in _context.Week select eek.EndOfWeek).Max().AddDays(1);
            return View();
        }

        // POST: Weeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekId,NumberOfWeek,BeginOfWeek,EndOfWeek")] Week week)
        {
            ViewBag.DateEnd = GetLastDateTime().ToShortDateString();
            var counter = (week.EndOfWeek - week.BeginOfWeek).TotalDays;
            GetCodeOfWeek(out string code, out int eweek, out int year);
            if (counter >7)
            {
                ViewBag.Message = "Entre la fecha de inicio y fecha final no pueden haber más de 7 días";
                return View("Create", week);
            }
            else
            {
                ViewBag.Code = code;
                ViewBag.BeginDate = (from eek in _context.Week select eek.EndOfWeek).Max().AddDays(1);
                week.WeekId = code;
                week.NumberOfWeek = code;
                _context.Add(week);
                await _context.SaveChangesAsync();
                return View("Details", week);
            }

        }


        // GET: Weeks/Edit/5
        [Authorize(Roles = "Admin,editor")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var week = await _context.Week.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }
            return View(week);
        }

        // POST: Weeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("WeekId,NumberOfWeek,BeginOfWeek,EndOfWeek")] Week week)
        {
            if (id != week.WeekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(week);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekExists(week.WeekId))
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
            return View(week);
        }


        // GET: Weeks/Delete/5
        [Authorize(Roles = "Admin,editor")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var week = await _context.Week
                .FirstOrDefaultAsync(m => m.WeekId == id);
            if (week == null)
            {
                return NotFound();
            }

            return View(week);
        }

        // POST: Weeks/Delete/5
        [Authorize(Roles = "Admin,editor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var week = await _context.Week.FindAsync(id);
            _context.Week.Remove(week);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Check if the week exists in the Database 
        /// </summary>
        /// <param name="id">id of the week to search</param>
        /// <returns>Return true if exists, Return false if not exists</returns>
        [AllowAnonymous]
        private bool WeekExists(string id)
        {
            return _context.Week.Any(e => e.WeekId == id);
        }

        /// <summary>
        /// Search in the database and return a new week code in the actual year.
        /// The limit is 50 weeks peer year
        /// </summary>
        /// <param name="Code">Output code week</param>
        /// <param name="NWeek">Output number of Week</param>
        /// <param name="NYear">Output Year</param>
        [AllowAnonymous]
        private void GetCodeOfWeek(out string Code, out int NWeek, out int NYear)
        {
            int numberOfWeekAux = 0;
            var ActualYear = DateTime.Today.Year.ToString();
            var query = (from week in _context.Weeks
                         where week.NumberOfWeek.Contains(ActualYear)
                         select week
                         ).ToList();
            foreach (var item in query)
            {
                string[] codearray = item.NumberOfWeek.Split('-');
                // Position codearray[1] -> number of week
                int.TryParse(codearray[1], out int resultNumberWeek);
                if (resultNumberWeek > numberOfWeekAux)
                {
                    numberOfWeekAux = resultNumberWeek;
                }
            }
            int.TryParse(ActualYear, out int yresult);
            if (numberOfWeekAux >= 50)
            {
                numberOfWeekAux = 1;
                NYear = yresult + 1;
            }
            else
            {
                numberOfWeekAux = numberOfWeekAux + 1;
                NYear = yresult;
            }
            Code = NYear.ToString() + "-" + numberOfWeekAux.ToString();
            NWeek = numberOfWeekAux;
        }


        /// <summary>
        /// Search in the DB a specific week and their asistances andn display a view
        /// </summary>
        /// <param name="id">id of the week to search</param>
        /// <returns>Display view of the week</returns>
        public async Task<IActionResult> ScheduleDetails(string id)
        {
            Week week = await (from oweek in _context.Week select oweek).Where(Week => Week.WeekId == id).FirstOrDefaultAsync();
            List<Asistance> asistances = await (from asis in _context.Asistances select asis).Where(A => A.WeekId == id).Include(A => A.Employee).Include(P => P.Project).ToListAsync();
            TimeSpan aux = week.EndOfWeek - week.BeginOfWeek;
            ViewBag.Employees = (from empl in asistances select empl.Employee).Distinct().ToList();
            ViewBag.QuantityOfDays = aux.Days;
            ViewBag.Asistances = asistances;
            return View(week);
        }


        /// <summary>
        /// Get the last EndOfWeek date 
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastDateTime()
        {
            var resultQuery = (from week in _context.Weeks
                               orderby week.EndOfWeek descending
                               select week.EndOfWeek).FirstOrDefault();
            return resultQuery;
        }
    }
}
