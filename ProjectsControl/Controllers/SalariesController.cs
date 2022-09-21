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
    public class SalariesController : Controller
    {
        private readonly DBProjectContext _context;

        public SalariesController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Salary.Include(s => s.Employee);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Salary == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryId == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        [HttpGet]
        public IActionResult CreateByEmployee(string id)
        {
            var Employee = _context.Employees.FirstOrDefault(E => E.EmployeeId.Equals(id));
            if (Employee != null)
            {
                Salary oSalary = new(){
                    SalaryId = Guid.NewGuid().ToString(),
                    DayOfApplication= DateTime.Today,
                    EmployeeId = Employee.EmployeeId,
                    Employee = Employee
                };
                return View(oSalary);
            }
            else
            {
                RedirectToAction("Create");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateByEmployee([Bind("SalaryId,SalaryAmount,DayOfApplication,notes,EmployeeId")] Salary salary)
        {
            _context.Salary.Add(salary);
            _context.SaveChanges();
            return RedirectToAction("Details","Employees" , new {id=salary.EmployeeId});
            //return View();
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaryId,SalaryAmount,DayOfApplication,notes,EmployeeId")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", salary.EmployeeId);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Salary == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", salary.EmployeeId);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SalaryId,SalaryAmount,DayOfApplication,notes,EmployeeId")] Salary salary)
        {
            if (id != salary.SalaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.SalaryId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", salary.EmployeeId);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Salary == null)
            {
                return NotFound();
            }

            var salary = await _context.Salary
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.SalaryId == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Salary == null)
            {
                return Problem("Entity set 'DBProjectContext.Salary'  is null.");
            }
            var salary = await _context.Salary.FindAsync(id);
            if (salary != null)
            {
                _context.Salary.Remove(salary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(string id)
        {
            return _context.Salary.Any(e => e.SalaryId == id);
        }
    }
}
