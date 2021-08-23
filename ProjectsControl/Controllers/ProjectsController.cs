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
            var project = await _context.Projects
                            .Include(p => p.Customer)
                            .Include(p => p.Employee)
                            .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            // Carga la cantidad de Facturas que tenga asociado el proyecto
            ViewBag.BillsOfProyect = (from bill in _context.Bill select bill).Where(W => W.ProjectId == id).ToList();
            // Carga todas la notas que tenga asociado el proyecto
            ViewBag.Notes = (from note in _context.Notes select note).Where(N => N.ProjectId == id).ToList();
            // Carga todos los reportes asociados al proyecto
            ViewBag.Reports = (from reports in _context.Report select reports).Where(R=>R.ProjectId == id).ToList();

            //Contar cantidad de Horas en el trabajo por persona
            ViewBag.Hours = GetHoursByEmployee(id);
            // Cantidad de dias por persona            
            ViewBag.DaysOfEmployees = GetDaysByEmployee(id);
            // Contar Cantidad Extras
            ViewBag.Extras =  GetExtrasByEmployee(id);
            // Contar la cantidad de gastos registrados por categoria
            ViewBag.ExpensivesByType = GetExpensivesByProject(id);
            return View(project);
        }

        /// <summary>
        /// Search all the expensives by category and return a dictionary with the information
        /// </summary>
        /// <param name="IdOfProject">Project To Search</param>
        /// <returns></returns>
        private Dictionary<string, float> GetExpensivesByProject(string IdOfProject = "")
        {
            Dictionary<string, float> ExpensivesByType = new Dictionary<string, float>();
            var ListOfExpensives = (from exp in _context.Expensives select exp)
                                                .Where(E => E.ProjectId == IdOfProject)
                                                .ToList();
            string[] TypesOfExpensives = (from ob in ListOfExpensives select ob.Type)
                                                .Distinct()
                                                .ToArray();
            foreach (var Etype in TypesOfExpensives)
            {
                float AmountAux=0.0f;
                AmountAux = (from obj in ListOfExpensives select obj).Where(E => E.Type.Equals(Etype)).Sum(E=>E.Amount);
                ExpensivesByType.Add(Etype, AmountAux);
            }
            return ExpensivesByType;
        }
        private Dictionary<string, float>  GetHoursByEmployee (string IdofProject = "")
        {
            Dictionary<string, float> Hours = new Dictionary<string, float>();
            var aux =  (from asis in _context.Asistances select asis).Where(A => A.ProjectId == IdofProject).Include(E => E.Employee).ToList();
            var employees = (from asis in aux select asis.Employee.Name).Distinct().ToList();
            foreach (var employee in employees)
            {
                var sum = 0;
                foreach (var asistence in aux)
                {
                    if (employee.Equals(asistence.Employee.Name))
                    {
                        TimeSpan time = asistence.DateOfEnd - asistence.DateOfBegin;
                        sum = sum + time.Hours;
                    }
                }
                Hours.Add(employee, sum);
            }
            return Hours;
        }
        public Dictionary<string, int> GetDaysByEmployee(string IdOfProject = "")
        {
            Dictionary<string, int> Days = new Dictionary<string, int>();
            var aux =  (from asis in _context.Asistances select asis).Where(A => A.ProjectId == IdOfProject).Include(E => E.Employee).ToList();            
            var asistancesDays =  (from asistance in _context.Asistances select asistance).Where(D => D.ProjectId == IdOfProject).Include(E => E.Employee).ToList();
            var employees = (from asis in aux select asis.Employee.Name).Distinct().ToList();
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
                Days.Add(item, sumDay);
            }
            return Days;
        }


        public Dictionary<string, float> GetExtrasByEmployee(string IdOfProject="")
        {
            var aux =  (from asis in _context.Asistances select asis).Where(A => A.ProjectId == IdOfProject).Include(E => E.Employee).ToList();
            var employees = (from asis in aux select asis.Employee.Name).Distinct().ToList();
            Dictionary<string, float> Extras = new Dictionary<string, float>();
            var extrasAux =  _context.ExtraHours.FromSqlInterpolated($@" SELECT ExtraHours.*
                                                                        from ExtraHours 
                                                                        left join (	SELECT * FROM Asistances
                                                                        WHERE Asistances.ProjectId = '{IdOfProject.ToString()}') AS tmp
                                                                        on ExtraHours.AsistanceId = tmp.AsistanceId").Include(E => E.Employee).ToList();
            foreach (var item in employees)
            {
                var sumExtra = 0.0f;
                foreach (var e in extrasAux)
                {
                    if (item.Equals(e.Employee.Name))
                    {
                        TimeSpan etime = e.EndTime - e.BeginTime;
                        sumExtra = sumExtra + etime.Hours;
                    }
                }
                Extras.Add(item, sumExtra);
            }
            return Extras;
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
            var aux =(from proj in _context.Projects select proj.NumberOfProject).Max() + 1;
            ViewData["NumberOfProject"] = aux;
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");            
            ViewBag.Employees=(from empl in _context.Employees select empl).Where(E => E.Position.Equals("Vendedor"));            
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,NumberOfProject,NumberOfTask,ProjectName,OC,OCDate,BeginDate,EndDate,Manager,Amount,Currency,Estatus,currency,IsOver,TypeOfJob,Details,Ubication,CustomerId,EmployeeId")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsSimple", new { id = project.ProjectId });
            }           
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", project.CustomerId);            
            ViewBag.Employees = (from empl in _context.Employees select empl).Where(E => E.Position.Equals("Vendedor"));
            return View(project);
        }
        public async Task<IActionResult> DetailsSimple(string id)
        {
            Project objProject = (from pj in _context.Projects select pj).Where(P=>P.ProjectId == id).Include(P => P.Employee).Include(P => P.Customer).FirstOrDefault();
            return View(objProject);
        }

        /// GET Projects/Search 
        [HttpGet]
        public async Task<IActionResult> Search(string SearchName=null, string NumberOfProjectToSearch=null, int MonthToSearch=0, int SearchYear=0, string StatusToSearch= null)
        {
            ViewBag.YearOfProject = (from project in _context.Projects select project.OCDate.Year).Distinct().ToList();
            var consult = new List<Project>().ToList();
            ViewBag.Status = (from project in _context.Projects select project.Estatus).Distinct().ToList();
            if( (SearchName != null)||(NumberOfProjectToSearch!= null)||(SearchYear!=0)||(MonthToSearch!=0)||(StatusToSearch!= null))
            {
                consult = await Consult(SearchName, NumberOfProjectToSearch, MonthToSearch, SearchYear, StatusToSearch);
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
            ViewBag.Employees = (from empl in _context.Employees select empl).Where(E => E.Position.Equals("Vendedor"));
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
            ViewBag.Employees = (from empl in _context.Employees select empl).Where(E => E.Position.Equals("Vendedor"));
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
        /// <param name="NumberOfProject">Id To Search</param>
        /// <param name="MonthToSearch"></param>
        /// <param name="SearchYear"></param>        
        /// <param name="StatusToSearch"></param>
        /// <returns></returns>
        private async Task<List<Project>> Consult(string SearchName = null,
                                          string NumberOfProject = null,
                                          int MonthToSearch = 0,
                                          int SearchYear = 0,
                                          string StatusToSearch = null)
        {
            List<Project> LisProjects = new List<Project>();
            if ((SearchName != null) && (NumberOfProject != null) && (MonthToSearch != 0) && (SearchYear != 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(NumberOfProject like CONCAT('%',{NumberOfProject},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})
                                                                and		(YEAR(OCDate)= {SearchYear})
                                                                and		(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (NumberOfProject != null) && (MonthToSearch != 0) && (SearchYear != 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(NumberOfProject like CONCAT('%',{NumberOfProject},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})
                                                                and		(YEAR(OCDate)= {SearchYear})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (NumberOfProject != null) && (MonthToSearch != 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(NumberOfProject like CONCAT('%',{NumberOfProject},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (NumberOfProject != null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(NumberOfProject like CONCAT('%',{NumberOfProject},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName != null) && (NumberOfProject == null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(ProjectName like CONCAT('%',{SearchName.ToString()},'%'))").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (NumberOfProject != null) && (MonthToSearch != 0) && (SearchYear != 0) && (StatusToSearch != null))
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
            else if ((SearchName == null) && (NumberOfProject == null) && (MonthToSearch != 0) && (SearchYear != 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(NumberOfProject like CONCAT('%',{NumberOfProject},'%'))
                                                                and		(ProjectName like CONCAT('%',{SearchName},'%'))
                                                                and		(MONTH(OCDate) ={MonthToSearch})
                                                                and		(YEAR(OCDate)= {SearchYear})
                                                                and		(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (NumberOfProject == null) && (MonthToSearch == 0) && (SearchYear != 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(YEAR(OCDate)= {SearchYear})
                                                                and		(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (NumberOfProject == null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch != null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(Estatus = {StatusToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (NumberOfProject != null) && (MonthToSearch == 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(NumberOfProject like CONCAT('%',{NumberOfProject.ToString()},'%'))").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (NumberOfProject == null) && (MonthToSearch != 0) && (SearchYear == 0) && (StatusToSearch == null))
            {
                using (var DB = _context)
                {
                    return await DB.Projects.FromSqlInterpolated($@"SELECT * FROM Projects
                                                                WHERE	(MONTH(OCDate) ={MonthToSearch})").Include(C => C.Customer).Include(S => S.Employee).ToListAsync();
                }
            }
            else if ((SearchName == null) && (NumberOfProject == null) && (MonthToSearch == 0) && (SearchYear != 0) && (StatusToSearch == null))
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
    /*private int GetLastNumberOfReport()
        {
            var aux = (from Rp in _context.Report select Rp.NumberOfReport).Max();
            return aux;
        }*/
    }
}
