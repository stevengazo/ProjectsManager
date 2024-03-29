﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsControl.Models;

namespace ProjectsControl.Controllers
{
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
        public IActionResult Create()
        {
            GetCodeOfWeek(out string code, out int eweek, out int year);
            ViewBag.Code = code;
            ViewBag.BeginDate = (from eek in _context.Week select eek.EndOfWeek).Max().AddDays(1);
            return View();
        }

        // POST: Weeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekId,NumberOfWeek,BeginOfWeek,EndOfWeek")] Week week)
        {
            GetCodeOfWeek(out string code, out int eweek, out int year);
            ViewBag.Code = code;
            ViewBag.BeginDate = (from eek in _context.Week select eek.EndOfWeek).Max().AddDays(1);
            if (ModelState.IsValid)
            {
                _context.Add(week);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(week);
        }

        // GET: Weeks/Edit/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var week = await _context.Week.FindAsync(id);
            _context.Week.Remove(week);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeekExists(string id)
        {
            return _context.Week.Any(e => e.WeekId == id);
        }


        private void GetCodeOfWeek(out string Code, out int NWeek, out int NYear)
        {
            int aux = 0;
            var ActualYear = DateTime.Today.Year.ToString();
            var query = (from week in _context.Weeks
                         where week.NumberOfWeek.Contains(ActualYear)
                         select week
                         ).ToList();
            foreach (var item in query)
            {
                string[] codearray = item.NumberOfWeek.Split('-');
                int.TryParse(codearray[1], out int result);
                if (result > aux)
                {
                    aux = result;
                }      
            }
            int.TryParse(ActualYear, out int yresult);
            if (aux >= 50)
            {
                aux = 1;                
                NYear = yresult + 1;
            }
            else
            {
                aux = aux + 1;
                NYear = yresult;
            }
            Code = NYear.ToString() + "-" + aux.ToString();
            NWeek = aux;
        }

        
        public async Task<IActionResult> ScheduleDetails(string id)
        {
            Week week =await (from oweek in _context.Week select oweek).Where(Week =>Week.WeekId == id).FirstOrDefaultAsync();
            List<Asistance> asistances = await (from asis in _context.Asistances select asis).Where(A => A.WeekId == id).Include(A=>A.Employee).Include(P=>P.Project).ToListAsync();             
            TimeSpan aux = week.EndOfWeek - week.BeginOfWeek;
            ViewBag.Employees= (from empl in asistances select empl.Employee).Distinct().ToList();
            ViewBag.QuantityOfDays = aux.Days;
            ViewBag.Asistances = asistances;
            return View(week);
        }
    }
}
