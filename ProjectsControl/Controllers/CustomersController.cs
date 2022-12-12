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
    public class CustomersController : Controller
    {
        private readonly DBProjectContext _context;

        public CustomersController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ProjectsOfCustomer = _context.Projects.Where(P => P.CustomerId == id).OrderByDescending(P => P.OCDate).ToList();
            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // GET: Customers/Create
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Name,Sector")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new {id =customer.CustomerId });
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerId,ProjectName,Sector")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string IdToSearch = null, string NameToSearch = null, string TypeToSearch = null)
        {
            ViewBag.TypesOfCustomers = (from cust in _context.Customers select cust.Sector).Distinct().ToList();
            if ((IdToSearch != null) || (NameToSearch != null) || (TypeToSearch != null))
            {
                var query = Consult(IdToSearch, NameToSearch, TypeToSearch);
                if (query.Count > 0)
                {
                    ViewBag.Message = "";
                    return View(query);
                }
                else
                {
                    ViewBag.Message = "No hay coincidencias";
                    return View(query);
                }
            }
            return View(new List<Customer>());
        }



        #region Internal Methods
        [AllowAnonymous]
        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }


        /// <summary>
        /// Seach customers in the database 
        /// </summary>
        /// <param name="IdToSearch">Id of the Customer</param>
        /// <param name="NameToSearch">Customer's name</param>
        /// <param name="TypeToSearch">Type of customer</param>
        /// <returns>List of customers</returns>
        [AllowAnonymous]
        private List<Customer> Consult(string IdToSearch = null, string NameToSearch = null, string TypeToSearch = null)
        {
            var query = new List<Customer>();
            if ((IdToSearch != null) && (NameToSearch != null) && (TypeToSearch != null))
            {
                query = (_context.Customers.FromSqlInterpolated($@"SELECT * FROM Customers
                                                                WHERE	(CustomerId LIKE CONCAT('%',{IdToSearch},'%'))
                                                                AND		(Name LIKE CONCAT('%',{NameToSearch},'%'))
                                                                AND		(Sector LIKE CONCAT('%',{TypeToSearch},'%'))")).ToList();
            }
            else if ((IdToSearch != null) && (NameToSearch != null) && (TypeToSearch == null))
            {
                query = (_context.Customers.FromSqlInterpolated($@"SELECT * FROM Customers
                                                                WHERE	(CustomerId LIKE CONCAT('%',{IdToSearch},'%'))
                                                                AND		(Name LIKE CONCAT('%',{NameToSearch},'%'))")).ToList();
            }
            else if ((IdToSearch != null) && (NameToSearch == null) && (TypeToSearch == null))
            {
                query = (_context.Customers.FromSqlInterpolated($@"SELECT * FROM Customers
                                                                WHERE	(CustomerId LIKE CONCAT('%',{IdToSearch},'%'))")).ToList();
            }
            else if ((IdToSearch == null) && (NameToSearch != null) && (TypeToSearch != null))
            {
                query = (_context.Customers.FromSqlInterpolated($@"SELECT * FROM Customers
                                                                WHERE	(Name LIKE CONCAT('%',{NameToSearch},'%'))
                                                                AND		(Sector LIKE CONCAT('%',{TypeToSearch},'%'))")).ToList();
            }
            else if ((IdToSearch == null) && (NameToSearch == null) && (TypeToSearch != null))
            {
                query = (_context.Customers.FromSqlInterpolated($@"SELECT * FROM Customers
                                                                WHERE	(Sector LIKE CONCAT('%',{TypeToSearch},'%'))")).ToList();
            }
            else if ((IdToSearch == null) && (NameToSearch != null) && (TypeToSearch == null))
            {
                query = (_context.Customers.FromSqlInterpolated($@"SELECT * FROM Customers
                                                                WHERE	(Name LIKE CONCAT('%',{NameToSearch},'%'))")).ToList();
            }
            else
            {
            }
            return query;
        }

        #endregion
    }
}
