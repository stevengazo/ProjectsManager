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
    public class ProjectsController : Controller
    {
        private readonly DBProjectContext _context;

        public ProjectsController(DBProjectContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Projects.Include(p => p.Customer).Include(p => p.Employee);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ExtraHour> extras = new List<ExtraHour>();         
            ViewBag.Extras = extras;
            ViewBag.BillsOfProyect = await (from bill in _context.Bill select bill).Where(W => W.ProjectId == id).ToListAsync();
            ViewBag.Reports =  await (from reports in _context.Report select reports).Where(R=>R.ProjectId == id).ToListAsync();
            var project = await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.ProjectId == id);


            #region Contar cantidad de Horas en el trabajo por persona
            Dictionary<string, float> Hours = new Dictionary<string, float>();
            var aux = (from asis in _context.Asistances select asis).Where(A => A.ProjectId == id).Include(E => E.Employee);
            var employees = (from asis in aux select asis.Employee.Name).Distinct().ToList();
            foreach (var employee in employees)
            {
                var sum = 0;
                foreach (var asistence in aux)
                {
                    if(employee.Equals(asistence.Employee.Name))
                    {
                        TimeSpan time = asistence.DateOfEnd - asistence.DateOfBegin;
                        sum = sum + time.Hours;
                    }
                }
                Hours.Add(employee, sum);
            }
            ViewBag.Hours = Hours;
            #endregion
           

            Dictionary<string, int> Days = new Dictionary<string, int>();
            var asistancesDays = (from asistance in _context.Asistances select asistance).Where(D => D.ProjectId == id).Include(E=>E.Employee);
            foreach (var item in employees)
            {
                var sumDay = 0;
                foreach (var aD in asistancesDays)
                {
                    if (item.Equals(aD.Employee.Name))
                    {
                        sumDay = sumDay + 1;
                    }
                }
                Days.Add(item,sumDay);
            }
            ViewBag.DaysOfEmployees = Days;


            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        public async Task<IActionResult> WithoutReports()
        {
            var AUX = _context.Projects.FromSqlInterpolated($@"
                                                                Select Projects.*
                                                                FROM Projects LEFT JOIN (SELECT Report.ProjectId FROM Report
                                                                GROUP BY Report.ProjectId) AS R
                                                                ON Projects.ProjectId = R.ProjectId
                                                                Where r.ProjectId is null
                                                                ").Include(p => p.Customer).Include(p => p.Employee);
            return View(AUX);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,NumberOfTask,ProjectName,OC,OCDate,BeginDate,EndDate,Manager,Amount,Estatus,currency,IsOver,TypeOfJob,Details,Ubication,CustomerId,SalemanId")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }           
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", project.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", project.EmployeeId);
            return View(project);
        }

        /// GET Projects/Search
        [HttpGet]
        public async Task<IActionResult> Search(string SearchName=null, string IdToSearch=null, int MonthToSearch=0, int SearchYear=0, string StatusToSearch= null)
        {
            ViewBag.YearOfProject = (from project in _context.Projects select project.OCDate.Year).Distinct().ToList();
            var consult = new List<Project>().ToList();
            ViewBag.Status = (from project in _context.Projects select project.Estatus).Distinct().ToList();
            if( (SearchName != null)||(IdToSearch!= null)||(SearchYear!=0)||(MonthToSearch!=0)||(StatusToSearch!= null))
            {
                consult = await Consult(SearchName, IdToSearch, MonthToSearch, SearchYear, StatusToSearch);
                if(consult.Count > 0)
                {
                    ViewBag.Message = "";
                    return View(consult);
                }
                else
                {
                    ViewBag.Message = "No hay coincidencias"; 
                    return View(new List<Project>());

                }
            }
            else 
            {
                return View(new List<Project>());
            }
            

        }
        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", project.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", project.Employee);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProjectId,NumberOfTask,ProjectName,OC,OCDate,BeginDate,EndDate,IsOver,TypeOfJob,Details,Ubication,CustomerId,EmployeeId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", project.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", project.EmployeeId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(string id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

        /// <summary>
        /// Consult in the database the information by the values
        /// </summary>
        /// <param name="SearchName">ProjectName of the Project</param>
        /// <param name="IdToSearch">Id To Search</param>
        /// <param name="MonthToSearch"></param>
        /// <param name="SearchYear"></param>        
        /// <param name="StatusToSearch"></param>
        /// <returns></returns>
      private async Task<List<Project>> Consult(  string SearchName= null,
                                        string IdToSearch = null,
                                        int MonthToSearch=0, 
                                        int SearchYear= 0, 
                                        string StatusToSearch= null)
        {
            List<Project> LisProjects = new List<Project>();
            if(  (SearchName!=null) && (IdToSearch!= null) && (MonthToSearch!=0) && (SearchYear!=0) && (StatusToSearch != null))
            {
               using( var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectId like CONCAT('%',{IdToSearch},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})
                                                                and		(YEAR(OCDate)= {SearchYear})
                                                                and		(Estatus = {StatusToSearch})").Include(C=>C.Customer).Include(S=>S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (IdToSearch != null) && (MonthToSearch != 0) && (SearchYear != 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectId like CONCAT('%',{IdToSearch},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})
                                                                and		(YEAR(OCDate)= {SearchYear})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (IdToSearch != null) && (MonthToSearch != 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectId like CONCAT('%',{IdToSearch},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (IdToSearch != null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectId like CONCAT('%',{IdToSearch},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (IdToSearch == null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectName like CONCAT('%',{SearchName},'%'))").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (IdToSearch != null) && (MonthToSearch != 0) && (SearchYear != 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})
                                                                and		(YEAR(OCDate)= {SearchYear})
                                                                and		(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (IdToSearch == null) && (MonthToSearch != 0) && (SearchYear != 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectId like CONCAT('%',{IdToSearch},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})
                                                                and		(YEAR(OCDate)= {SearchYear})
                                                                and		(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (IdToSearch == null) && (MonthToSearch == 0) && (SearchYear != 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(YEAR(OCDate)= {SearchYear})
                                                                and		(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (IdToSearch == null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (IdToSearch != null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch == null))  
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectId like CONCAT('%',{IdToSearch},'%'))").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (IdToSearch == null) && (MonthToSearch != 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(MONTH(OCDate) ={MonthToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (IdToSearch == null) && (MonthToSearch == 0) && (SearchYear != 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(YEAR(OCDate)= {SearchYear})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else
            {
                return (new List<Project>());
            }
         
        }
    }
}
