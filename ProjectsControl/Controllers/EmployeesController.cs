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
    public class EmployeesController : Controller
    {
        private readonly DBProjectContext _context;

        public EmployeesController(DBProjectContext context)
        {
            _context = context;
        }

        #region View Methods

        // GET: Employees
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Salary = GetActiveSalary(id);
            ViewBag.ActiveSalary = Salary;
            var actions = (from act in _context.Actions select act).Where(E => E.EmployeeId == id).OrderByDescending(E => E.DateOfCreation).ToList();
            ViewBag.actions = actions;
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeDNI,Name,Position,DateOfBirth,DateofHiring,DateOfFired,IsActive,MobileNumber,Email,Salay")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeId,ProjectName,DateofHiring,Position")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
<<<<<<< 
=======
        [Authorize(Roles ="Admin,Editor")]
>>>>>>> a6efa87b07fb1be7d7c08648edb89f1660c86054
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Internal Methods
        [AllowAnonymous]
        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
        private Salary GetActiveSalary(string id = null)
        {
            if (id != null)
            {
                var tmpResults = _context.Salary.FirstOrDefault(S => S.EmployeeId == id && S.isActive);
                return tmpResults;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
