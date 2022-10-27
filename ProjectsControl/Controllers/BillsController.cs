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
    public class BillsController : Controller
    {
        private readonly DBProjectContext _context;

        public BillsController(DBProjectContext context)
        {
            _context = context;
        }


        #region Views Methods
        // GET: Bills
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Bill.Include(b => b.Project);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Bills/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create()
        {            
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            ViewBag.Projects = (from proj in _context.Projects select proj).Where(P => P.IsOver == false).OrderBy(P=>P.NumberOfProject);
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillId,NumberOfBill,DateOfCreation,Author,Amount,ProjectId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", bill.ProjectId);
            return View(bill);
        }

        // GET: Bills/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", bill.ProjectId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BillId,NumberOfBill,DateOfCreation,Author,Amount,ProjectId")] Bill bill)
        {
            if (id != bill.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.BillId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", bill.ProjectId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bill = await _context.Bill.FindAsync(id);
            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string IdToSearch=null,string AuthorToSearch = null, string ProjectIdToSearch=null)
        {
            List<Bill> Bills = new List<Bill>();
            if ((IdToSearch != null) || (AuthorToSearch != null) || (ProjectIdToSearch != null))
            {
                Bills = Consult(IdToSearch, AuthorToSearch,ProjectIdToSearch);
                if(Bills.Count > 0)
                {
                    ViewBag.Message = "";
                    return View(Bills);
                }
                else
                {
                    ViewBag.Message = "No hay coincidencias";
                    return View(Bills);
                }
            }
            return View(new List<Bill>());
        }

        #endregion

        #region Internal Methods
        [AllowAnonymous]
        private bool BillExists(string id)
        {
            return _context.Bill.Any(e => e.BillId == id);
        }
        [AllowAnonymous]
        public List<Bill> Consult(string IdToSearch = null, string AuthorToSearch = null, string ProjectIdToSearch = null)
        {
            if ((IdToSearch != null) && (AuthorToSearch != null) && (ProjectIdToSearch != null))
            {
                return _context.Bill.FromSqlInterpolated($@"SELECT * FROM Bill
                                                            WHERE	( BillId LIKE CONCAT('%',{IdToSearch},'%') )
                                                            AND		( Author LIKE CONCAT('%',{AuthorToSearch},'%') )
                                                            AND		( ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%') )").ToList();
            }
            else if ((IdToSearch != null) && (AuthorToSearch != null) && (ProjectIdToSearch == null))
            {
                return _context.Bill.FromSqlInterpolated($@"SELECT * FROM Bill
                                                            WHERE	( BillId LIKE CONCAT('%',{IdToSearch},'%') )
                                                            AND		( Author LIKE CONCAT('%',{AuthorToSearch},'%') )").ToList();
            }
            else if ((IdToSearch != null) && (AuthorToSearch == null) && (ProjectIdToSearch == null))
            {
                return _context.Bill.FromSqlInterpolated($@"SELECT * FROM Bill
                                                            WHERE	( BillId LIKE CONCAT('%',{IdToSearch},'%') )").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch != null) && (ProjectIdToSearch != null))
            {
                return _context.Bill.FromSqlInterpolated($@"SELECT * FROM Bill
                                                            WHERE	( Author LIKE CONCAT('%',{AuthorToSearch},'%') )
                                                            AND		( ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%') )").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch == null) && (ProjectIdToSearch != null))
            {
                return _context.Bill.FromSqlInterpolated($@"SELECT * FROM Bill
                                                            WHERE	( ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%') )").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch != null) && (ProjectIdToSearch == null))
            {
                return _context.Bill.FromSqlInterpolated($@"SELECT * FROM Bill
                                                            WHERE	( Author LIKE CONCAT('%',{AuthorToSearch},'%'))").ToList();
            }
            else
            {
                return (new List<Bill>());
            }
        }
        #endregion
    }
}
