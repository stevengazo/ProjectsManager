using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel;
using ProjectsControl.Models;
using ProjectsControl.Data;


namespace ProjectsControl.Controllers
{
    [Authorize]        
    public class AsistancesController : Controller
    {
        private readonly DBProjectContext _context;
        private readonly ApplicationDbContext _applicationDb;

        public AsistancesController(DBProjectContext context, ApplicationDbContext applicationDb)
        {
            _context = context;
            _applicationDb = applicationDb;

        }

        #region Views Methods

        /// <summary>
        /// Received the list of daily asistances in the DB
        /// </summary>
        /// <param name="asistances">List of asistances </param>
        /// <returns>Same view</returns>
        [HttpPost] 
        [Authorize(Roles ="Admin,Editor")]
        public async Task<ActionResult> DailyCreate(List<Asistance> asistances )
        {
            try
            {     
                if(asistances.Count !=0)
                {
                    foreach (Asistance asistance in asistances)
                    {
                        asistance.AsistanceId = Guid.NewGuid().ToString();
                    }
                    using (var context = _context)
                    {
                        await context.Asistances.AddRangeAsync(asistances);
                        await context.SaveChangesAsync();
                    }
                    ViewBag.ErrorMessage = "Asistencias Agregadas";
                    List<Asistance> sample1 = new List<Asistance>();
                    return View(sample1);
                }
                else
                {
                    ViewBag.ErrorMessage = "No hay asistencias registradas";    
                    ViewBag.ErrorMessage.Style = "btn-danger";
                    List<Asistance> sample1 = new List<Asistance>();
                    return View(sample1);
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                List<Asistance> sample1 = new List<Asistance>();
                return View(sample1);
            }
            
        }

        /// <summary>
        ///  Return the basic information for the Daily Asistences
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin,editor")]
        [HttpGet]
        public async Task<IActionResult> DailyCreate()
        {               
            //  List of Weeks registered in the DB
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId");
            //  List of employees in the DB
            var employees  = (from employee in _context.Employees select employee).Where(E=> E.Position.Equals("Ayudante") || E.Position.Equals("tecnico")).ToList();
            //  Lista de proyectos 
            ViewBag.Projects = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).ToDictionary( P=>P.ProjectId, P=>P.ProjectName );
            //  Lista de asistencia del empleado y preparación básica
            List<Asistance> assistancesByPerson = new List<Asistance>();
            foreach (var employee in employees)
            {
                Asistance tmpAsistance = new Asistance()
                {
                    AsistanceId = Guid.NewGuid().ToString(),
                    Employee = employee,
                    EmployeeId = employee.EmployeeId,
                    DateOfBegin = DateTime.Today.AddHours(7),
                    DateOfEnd= DateTime.Today.AddHours(17)
                    
                };
                assistancesByPerson.Add(tmpAsistance);
            }
            ViewBag.ErrorMessage = "";
            //  Retorno de la vista                    
            return View(assistancesByPerson);
        }

        // GET: Asistances
        /// <summary>
        /// Main Page of the asistances
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var dBProjectContext = _context.Asistances.Include(a => a.Employee).Include(a => a.Project).Include(a => a.Week);
            return View(await dBProjectContext.ToListAsync());
        }

        // GET: Asistances/Details/5
        /// <summary>
        /// Search an especific registered of asistance in the DB
        /// </summary>
        /// <param name="id">Id to Search</param>
        /// <returns></returns>        
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistance = await _context.Asistances
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Include(a => a.Week)
                .FirstOrDefaultAsync(m => m.AsistanceId == id);
            if (asistance == null)
            {
                return NotFound();
            }
            ViewBag.Extras = (from extH in _context.ExtraHours select extH).Where(E=>E.AsistanceId== asistance.AsistanceId).ToList();

            return View(asistance);
        }

        // GET: Asistances/Create        
        [Authorize(Roles = "admin,editor")]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ProjectName"] = new SelectList(_context.Employees, "ProjectName", "ProjectName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId");

            var aux = (from a in _context.Employees select a).ToList();
            var dicEmpl = new Dictionary<string, string>();
            foreach (var item in aux)
            {
                dicEmpl.Add(item.EmployeeId, item.Name);
            }

            ViewBag.ListOfEmployes = dicEmpl;

            
            
            var listProj = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).ToList();
            var dicProj = new Dictionary<string, string>();
            foreach (var item in listProj)
            {
                dicProj.Add(item.ProjectId, item.ProjectName);
            }
            ViewBag.Projects = dicProj;
            return View();
        }

        // POST: Asistances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin,editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsistanceId,DateOfBegin,DateOfEnd,EmployeeId,ProjectId,WeekId")] Asistance asistance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", asistance.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", asistance.ProjectId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", asistance.WeekId);
            var aux = (from a in _context.Employees select a).ToList();
            var dicEmpl = new Dictionary<string, string>();
            foreach (var item in aux)
            {
                dicEmpl.Add(item.EmployeeId, item.Name);
            }

            ViewBag.ListOfEmployes = dicEmpl;

            var listProj = (from proj in _context.Projects select proj).Where(P => P.IsOver != true).ToList();
            var dicProj = new Dictionary<string, string>();
            foreach (var item in listProj)
            {
                dicProj.Add(item.ProjectId, item.ProjectName);
            }
            ViewBag.Projects = dicProj;
            return  View(asistance);
        }

        // GET: Asistances/Edit/5
        [Authorize(Roles = "admin,editor")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistance = await _context.Asistances.FindAsync(id);
            if (asistance == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", asistance.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", asistance.ProjectId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", asistance.WeekId);
            return View(asistance);
        }

        // POST: Asistances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin,editor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AsistanceId,DateOfBegin,DateOfEnd,EmployeeId,ProjectId,WeekId")] Asistance asistance)
        {
            if (id != asistance.AsistanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistanceExists(asistance.AsistanceId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", asistance.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", asistance.ProjectId);
            ViewData["WeekId"] = new SelectList(_context.Set<Week>(), "WeekId", "WeekId", asistance.WeekId);
            return View(asistance);
        }

        // GET: Asistances/Delete/5
        [Authorize(Roles = "admin,editor")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistance = await _context.Asistances
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Include(a => a.Week)
                .FirstOrDefaultAsync(m => m.AsistanceId == id);
            if (asistance == null)
            {
                return NotFound();
            }

            return View(asistance);
        }

        // POST: Asistances/Delete/5
        [Authorize(Roles = "admin,editor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var asistance = await _context.Asistances.FindAsync(id);
            _context.Asistances.Remove(asistance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.Projects = (from proj in _context.Projects select proj).Where(P => P.IsOver == false).ToList();
            ViewBag.Weeks = _context.Week.ToList();
            return  View(new List<Asistance>());
        }*/
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search( string DateToSearch= null, string NameToSearch= null, string ProjectToSearch= null,string WeekToSearch= null )
        {
            if (NameToSearch != null)
            {
                NameToSearch = (NameToSearch.Equals("0") || NameToSearch == null) ? null : NameToSearch;
            }
            if(ProjectToSearch != null)
            {
                ProjectToSearch = (ProjectToSearch.Equals("0") || ProjectToSearch == null) ? null : ProjectToSearch;
            }
            if(WeekToSearch != null)
            {
                WeekToSearch = (WeekToSearch.Equals("0")) ? null : WeekToSearch;
            }
                       
            ViewBag.Employees = _context.Employees.ToList();
            ViewBag.Projects = (from proj in _context.Projects select proj).ToList();
            ViewBag.Weeks = _context.Week.ToList();
            var tmpResult = await SearchInDb(DateToSearch, NameToSearch, ProjectToSearch, WeekToSearch);
            if (tmpResult != null)
            {
                if( NameToSearch == null && DateToSearch == null && ProjectToSearch == null && WeekToSearch == null) 
                {
                    ViewBag.ErrorMessage = "";
                }
                else
                {
                    if (tmpResult.Count == 0)
                    {
                        ViewBag.ErrorMessage = "No hay coincidencias";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "";
                    }
                }
                return View(tmpResult);
            }
            else
            {                
                return View(new List<Asistance>());
            }
            
        }

        #endregion


        #region Internal Methods

        /// <summary>
        /// This function is focus to consult the asistances in the database with a stored procedure
        /// </summary>
        /// <param name="DateToSearch"></param>
        /// <param name="NameToSearch"></param>
        /// <param name="ProjectToSearch"></param>
        /// <param name="WeekToSearch"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<Asistance>> SearchInDb(string DateToSearch = null, string NameToSearch = null, string ProjectToSearch = null, string WeekToSearch = null)
        {
            try
            {
                /// Check if the parameters exists
                bool bandName = (NameToSearch != null) ? true : false;
                bool bandProject = (ProjectToSearch != null) ? true : false;
                bool bandWeek = (WeekToSearch != null) ? true : false;
                bool bandDate = (DateToSearch != null) ? true : false;
                if (bandDate || bandName || bandProject || bandWeek)
                {
                    var result = _context.Asistances.FromSqlInterpolated($" EXECUTE SearchAsistances @_EmployeeId = {NameToSearch}, @_ProjectId = {ProjectToSearch}, @_DateToSearch ={DateToSearch}, @_WeekId = {WeekToSearch}");
                    var employees = _context.Employees.ToList();
                    var projects = _context.Projects.ToList();
                    foreach (var item in result)
                    {
                        item.Employee = employees.FirstOrDefault(E => E.EmployeeId == item.EmployeeId);
                        item.Project = projects.FirstOrDefault(P => P.ProjectId == item.ProjectId);
                    }
                    return await result.ToListAsync();
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ed)
            {
                Console.WriteLine($"Message {ed.Message}");
                Console.WriteLine($"Inner Exception {ed.InnerException.Message}");
                return null;
            }
            return null;
        }
        [AllowAnonymous]
        private bool AsistanceExists(string id)
        {
            return _context.Asistances.Any(e => e.AsistanceId == id);
        }
        #endregion
    }
}
