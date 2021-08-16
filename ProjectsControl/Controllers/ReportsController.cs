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
    public class ReportsController : Controller
    {
        private readonly DBProjectContext _context;

        public ReportsController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Report.Include(r => r.Project);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.Project)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,NumberOfReport,Author,BeginDate,EndDate,Status,Notes,ProjectId")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", report.ProjectId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", report.ProjectId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReportId,NumberOfReport,Author,BeginDate,EndDate,Status,Notes,ProjectId")] Report report)
        {
            if (id != report.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ReportId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", report.ProjectId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.Project)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var report = await _context.Report.FindAsync(id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string IdToSearch = null, string AuthorToSearch = null, string ProjectIdToSearch = null, string StatusToSearch=null)
        {
            ViewBag.Authors = (from report in _context.Report select report.Author).Distinct().ToList();
            ViewBag.status = (from report in _context.Report select report.Status).Distinct().ToList();
            ViewBag.Projects = (from project in _context.Projects select project).Where(P => P.IsOver == false).ToList();

            if ((IdToSearch != null)||(AuthorToSearch != null)||(ProjectIdToSearch != null) || (StatusToSearch != null))
            {
               var aux = Consult(IdToSearch,AuthorToSearch,ProjectIdToSearch, StatusToSearch);
                if (aux.Count > 0)
                {
                    ViewBag.Message = "";
                    return View(aux);
                }
                else
                {
                    ViewBag.Message = "No hay coincidencias";
                    return View(new List<Report>());
                }
            }
            return View(new List<Report>());
        }

        private bool ReportExists(string id)
        {
            return _context.Report.Any(e => e.ReportId == id);
        }

        /// <summary>
        /// Consult an exists report in the database
        /// </summary>
        /// <param name="IdToSearch"> </param>
        /// <param name="AuthorToSearch"></param>
        /// <param name="ProjectIdToSearch"></param>
        /// <returns>List of Reports</returns>
        private List<Report> Consult(string IdToSearch= null, string AuthorToSearch =null,string ProjectIdToSearch=null, string StatusToSearch = null)
        {
            var listaR = new List<Report>();
            if ((IdToSearch != null) && (AuthorToSearch != null) && (ProjectIdToSearch !=null)&&(StatusToSearch!=null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(ReportId LIKE CONCAT('%',{IdToSearch},'%'))
                                                                AND		(Author LIKE CONCAT('%',{AuthorToSearch},'%'))
                                                                AND		(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))
                                                                AND		(Status LIKE CONCAT('%',{StatusToSearch},'%'))").ToList();
            }
            else if ((IdToSearch != null) && (AuthorToSearch != null) && (ProjectIdToSearch != null) && (StatusToSearch == null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(ReportId LIKE CONCAT('%',{IdToSearch},'%'))
                                                                AND		(Author LIKE CONCAT('%',{AuthorToSearch},'%'))
                                                                AND		(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))").ToList();
            }
            else if ((IdToSearch != null) && (AuthorToSearch != null) && (ProjectIdToSearch == null) && (StatusToSearch == null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(ReportId LIKE CONCAT('%',{IdToSearch},'%'))
                                                                AND		(Author LIKE CONCAT('%',{AuthorToSearch},'%'))").ToList();
            }
            else if ((IdToSearch != null) && (AuthorToSearch == null) && (ProjectIdToSearch == null) && (StatusToSearch == null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(ReportId LIKE CONCAT('%',{IdToSearch},'%'))").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch != null) && (ProjectIdToSearch != null) && (StatusToSearch != null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(Author LIKE CONCAT('%',{AuthorToSearch},'%'))
                                                                AND		(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))
                                                                AND		(Status LIKE CONCAT('%',{StatusToSearch},'%'))").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch == null) && (ProjectIdToSearch != null) && (StatusToSearch != null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))
                                                                AND		(Status LIKE CONCAT('%',{StatusToSearch},'%'))").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch == null) && (ProjectIdToSearch == null) && (StatusToSearch != null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(Status LIKE CONCAT('%',{StatusToSearch},'%'))").ToList();
            }
            else if ((IdToSearch != null) && (AuthorToSearch == null) && (ProjectIdToSearch != null) && (StatusToSearch == null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(ReportId LIKE CONCAT('%',{IdToSearch},'%'))
                                                                AND		(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch != null) && (ProjectIdToSearch == null) && (StatusToSearch != null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(Author LIKE CONCAT('%',{AuthorToSearch},'%'))
                                                                AND		(Status LIKE CONCAT('%',{StatusToSearch},'%'))").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch != null) && (ProjectIdToSearch !=null)  && (StatusToSearch == null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(Author LIKE CONCAT('%',{AuthorToSearch},'%'))
                                                                AND		(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))").ToList();
            }
            else if ((IdToSearch != null) && (AuthorToSearch == null) && (ProjectIdToSearch == null) && (StatusToSearch != null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(Author LIKE CONCAT('%',{AuthorToSearch},'%'))
                                                                AND		(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch != null) && (ProjectIdToSearch == null) && (StatusToSearch == null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(Author LIKE CONCAT('%',{AuthorToSearch},'%'))").ToList();
            }
            else if ((IdToSearch == null) && (AuthorToSearch == null) && (ProjectIdToSearch != null) && (StatusToSearch == null))
            {
                listaR = _context.Report.FromSqlInterpolated($@"SELECT * FROM Report
                                                                WHERE	(ProjectId LIKE CONCAT('%',{ProjectIdToSearch},'%'))").ToList();
            }

            else
            {
                listaR = new List<Report>();
            }
            return listaR;
        }
    }
}
