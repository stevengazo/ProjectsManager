﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.FormattableString;
using ProjectsControl.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectsControl.Controllers
{
    [Authorize(Roles = "Lector,Editor,Admin")]
    public class ProjectsController : Controller
    {
        private readonly DBProjectContext _context;

        public ProjectsController(DBProjectContext context)
        {
            _context = context;
        }
        #region View Methods

        [AllowAnonymous]
        [HttpGet]
        // GET: Projects
        public async Task<IActionResult> Index()
        {
            int NumberOfProjectsByPage = 15;
            int NumberPage = 1;

            float tmp = _context.Projects.Count() / NumberOfProjectsByPage;
            ViewBag.TotalQuantityOfProjects = Math.Ceiling(tmp);
            ViewBag.QofProjectsByPage = NumberOfProjectsByPage;

            List<Project> projects = GetProjectsByPage(NumberOfProjectsByPage, NumberPage);

            return View(projects);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string QuantityOfProjectsView, string PageNumber)
        {
            ViewBag.TotalQuantityOfProjects = _context.Projects.Count();
            int.TryParse(QuantityOfProjectsView, out int QofProjects);
            int.TryParse(PageNumber, out int PageNumberP);
            ViewBag.QofProjectsByP = QofProjects;


            float tmp = _context.Projects.Count() / QofProjects;
            ViewBag.TotalQuantityOfProjects = Math.Ceiling(tmp);
            ViewBag.QofProjectsByPage = PageNumberP;


            List<Project> projects = GetProjectsByPage(QofProjects, PageNumberP);

            return View(projects);
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
                            .Include(p => p.Offer)
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
            ViewBag.Reports = (from reports in _context.Report select reports).Where(R => R.ProjectId == id).ToList();
            // cargar asistencias del proyecto
            ViewBag.AsistancesDetails = (from asist in _context.Asistances select asist).Include(A => A.Employee).Where(A => A.ProjectId == id).OrderBy(E => E.EmployeeId).ToList();
            //Contar cantidad de Horas en el trabajo por persona
            ViewBag.Hours = GetHoursByEmployee(id);
            // Cantidad de dias por persona            
            ViewBag.DaysOfEmployees = GetDaysByEmployee(id);
            // Contar Cantidad Extras
            ViewBag.Extras = GetExtrasByEmployee(id);
            ViewBag.ExtrasDetails = getListOfExtras(id);
            // Contar la cantidad de gastos registrados por categoria
            ViewBag.ExpensivesByType = GetExpensivesByProject(id);
            return View(project);
        }

        [Authorize(Roles = "Lector,Editor,Admin")]
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
        [Authorize(Roles = "Editor,Admin,sales")]
        public IActionResult Create()
        {
            Dictionary<string, int> Offerstmp = (from offer in _context.Offers
                                                 where offer.DateOfCreation.Year == DateTime.Today.Year
                                                 orderby offer.NumberOfOffer descending
                                                 select offer).ToDictionary(O => O.OfferId, o => o.NumberOfOffer);
            ViewBag.offers = Offerstmp;
            var aux = (from proj in _context.Projects select proj.NumberOfProject).Max() + 1;
            ViewData["NumberOfProject"] = aux;
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");

            ViewBag.Employees = (from empl in _context.Employees select empl).Where(E => E.Position.Equals("Vendedor") && E.IsActive == true);
            ViewBag.Customers = (from cust in _context.Customers select cust).OrderBy(C => C.Name);
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Editor,Admin,sales")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,NumberOfProject,NumberOfTask,ProjectName,OC,OCDate,BeginDate,EndDate,Manager,Amount,Currency,Estatus,currency,IsOver,TypeOfJob,Details,Ubication,CustomerId,EmployeeId,OfferId")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("DetailsSimple", new { id = project.ProjectId });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", project.CustomerId);
            ViewBag.Employees = (from empl in _context.Employees select empl).Where(E => E.Position.Equals("Vendedor"));
            return RedirectToAction("DetailsSimple", new { id = project.ProjectId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> DetailsSimple(string id)
        {
            Project objProject = (from pj in _context.Projects select pj).Where(P => P.ProjectId == id).Include(P => P.Employee).Include(p => p.Offer)
                .Include(P => P.Customer).FirstOrDefault();
            return View(objProject);
        }

        /// GET Projects/Search 

        [HttpGet]
        public async Task<IActionResult> Search(string SearchName = null, string NumberOfProjectToSearch = null, string TypeToSearch = null, int MonthToSearch = 0, int SearchYear = 0, string StatusToSearch = null)
        {
            ViewBag.YearOfProject = (from project in _context.Projects select project.OCDate.Year).Distinct().ToList();
            var consult = new List<Project>().ToList();
            ViewBag.Status = (from project in _context.Projects select project.Estatus).Distinct().ToList();
            if ((SearchName != null) || (NumberOfProjectToSearch != null) || (SearchYear != 0) || (MonthToSearch != 0) || (StatusToSearch != null) || (TypeToSearch != null))
            {
                consult = await Consult(SearchName, NumberOfProjectToSearch, MonthToSearch, SearchYear, StatusToSearch, TypeToSearch);
                if (consult.Count > 0)
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
        [Authorize(Roles = "Editor,Admin,sales")]
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

            Dictionary<string, int> Offerstmp = (from offer in _context.Offers
                                                 where offer.DateOfCreation.Year == DateTime.Today.Year
                                                 orderby offer.NumberOfOffer descending
                                                 select offer).ToDictionary(O => O.OfferId, o => o.NumberOfOffer);
            ViewBag.offers = Offerstmp;
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", project.CustomerId);
            ViewBag.Customers = (from cust in _context.Customers select cust).ToDictionary(I => I.CustomerId, I => I.Name);
            ViewBag.Employees = (from empl in _context.Employees select empl).Where(E => E.Position.Equals("Vendedor"));
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Editor,Admin,Sales")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProjectId,NumberOfTask,ProjectName,OC,OCDate,BeginDate,EndDate,IsOver,TypeOfJob,Details,Ubication,CustomerId,EmployeeId,OfferId")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    project.Employee = null;
                    _context.Projects.Update(project);
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
            return RedirectToAction("DetailsSimple", new { id = project.ProjectId });
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Editor,Admin")]
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
        [Authorize(Roles = "Editor,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Search");
        }

        [AllowAnonymous]
        public Dictionary<string, int> GetDaysByEmployee(string IdOfProject = "")
        {
            Dictionary<string, int> Days = new();
            var aux = (from asis in _context.Asistances select asis).Where(A => A.ProjectId == IdOfProject).Include(E => E.Employee).ToList();
            var asistancesDays = (from asistance in _context.Asistances select asistance).Where(D => D.ProjectId == IdOfProject).Include(E => E.Employee).ToList();
            var employees = (from asis in aux select asis.Employee.Name).Distinct().ToList();
            foreach (var item in employees)
            {
                var sumDay = 0;
                foreach (var aD in asistancesDays)
                {
                    if (item.Equals(aD.Employee.Name))
                    {
                        sumDay += 1;
                    }
                }
                Days.Add(item, sumDay);
            }
            return Days;
        }

        #endregion




        #region Methods

        /// <summary>
        /// Consult in the database the information by the values
        /// </summary>
        /// <param name="SearchName">ProjectName of the Project</param>
        /// <param name="NumberOfProject">Id To Search</param>
        /// <param name="MonthToSearch"></param>
        /// <param name="SearchYear"></param>        
        /// <param name="StatusToSearch"></param>
        /// <returns></returns>
        [Authorize(Roles = "Lector,Editor,Admin")]
        private async Task<List<Project>> Consult(string SearchName = null,
                                          string NumberOfProject = null,
                                          int MonthToSearch = 0,
                                          int SearchYear = 0,
                                          string StatusToSearch = null,
                                          string TypeToSearch = null)
        {
            List<Project> LisProjects = new();


            if (SearchYear == 0 || MonthToSearch == 0)
            {
                if (SearchYear == 0 && MonthToSearch != 0)
                {
                    var consult = _context.Projects.FromSqlInterpolated($@"EXEC	SearchProjects NULL, NULL, {SearchName}, {StatusToSearch},{TypeToSearch},{NumberOfProject};");

                    LisProjects = consult.ToList();

                }
                else if (SearchYear != 0 && MonthToSearch == 0)
                {
                    var consult = _context.Projects.FromSqlInterpolated($@"EXEC	SearchProjects null, {SearchYear}, {SearchName}, {StatusToSearch},{TypeToSearch},{NumberOfProject};");

                    LisProjects = consult.ToList();
                }
                else
                {
                    var consult = _context.Projects.FromSqlInterpolated($@"EXEC	SearchProjects null, NULL, {SearchName}, {StatusToSearch},{TypeToSearch},{NumberOfProject};");

                    LisProjects = consult.ToList();
                }
            }
            else
            {
                var consult = _context.Projects.FromSqlInterpolated($@"EXEC	SearchProjects {MonthToSearch}, {SearchYear}, {SearchName}, {StatusToSearch},{TypeToSearch},{NumberOfProject};");

                LisProjects = consult.ToList();
            }
            List<Project> LisPro = new();
            foreach (var item in LisProjects)
            {
                item.Customer = (from i in _context.Customers select i).Where(C => C.CustomerId == item.CustomerId).FirstOrDefault();
                item.Employee = (from i in _context.Employees select i).Where(C => C.EmployeeId == item.EmployeeId).FirstOrDefault();
            }

            if (LisProjects.Count > 0)
            {
                return LisProjects;
            }
            else
            {
                return (new List<Project>());
            }
        }

        [AllowAnonymous]
        private bool ProjectExists(string id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
        [AllowAnonymous]
        private List<Project> GetProjectsByPage(int QuantityOfProjects = 10, int PageToView = 1)
        {
            try
            {
                using (var db = _context)
                {
                    var customers = db.Customers.ToList();
                    var query = db.Projects.FromSqlInterpolated($"EXECUTE GetProjectsByPage @_PageNumber ={PageToView.ToString()} , @_QuantityOfDevices = {QuantityOfProjects.ToString()}").ToList();
                    query.ForEach(
                        delegate (Project D)
                        {
                            D.Customer = customers.FirstOrDefault(C => C.CustomerId == D.CustomerId);
                        });
                    return query;
                }
            }
            catch (Exception f)
            {
                Console.WriteLine($"Error: {f.Message}");
                return null;
            }
        }

        [AllowAnonymous]
        private int GetLastNumberOfReport()
        {
            var aux = (from Rp in _context.Report select Rp.NumberOfReport).Max();
            return aux;
        }

        /// <summary>
        /// Search all the expensives by category and return a dictionary with the information
        /// </summary>
        /// <param name="IdOfProject">Project To Search</param>
        /// <returns></returns>
        [AllowAnonymous]
        private Dictionary<string, float> GetExpensivesByProject(string IdOfProject = "")
        {
            Dictionary<string, float> ExpensivesByType = new();
            var ListOfExpensives = (from exp in _context.Expensives select exp)
                                                .Where(E => E.ProjectId == IdOfProject)
                                                .ToList();
            string[] TypesOfExpensives = (from ob in ListOfExpensives select ob.Type)
                                                .Distinct()
                                                .ToArray();
            foreach (var Etype in TypesOfExpensives)
            {
                float AmountAux = 0.0f;
                AmountAux = (from obj in ListOfExpensives select obj).Where(E => E.Type.Equals(Etype)).Sum(E => E.Amount);
                ExpensivesByType.Add(Etype, AmountAux);
            }
            return ExpensivesByType;
        }
        [AllowAnonymous]
        private Dictionary<string, float> GetHoursByEmployee(string IdofProject = "")
        {
            Dictionary<string, float> Hours = new();
            var aux = (from asis in _context.Asistances select asis).Where(A => A.ProjectId == IdofProject).Include(E => E.Employee).ToList();
            var employees = (from asis in aux select asis.Employee.Name).Distinct().ToList();
            foreach (var employee in employees)
            {
                var sum = 0;
                foreach (var asistence in aux)
                {
                    if (employee.Equals(asistence.Employee.Name))
                    {
                        TimeSpan time = asistence.DateOfEnd.TimeOfDay - asistence.DateOfBegin.TimeOfDay;
                        sum += time.Hours;
                    }
                }
                Hours.Add(employee, sum);
            }
            return Hours;
        }
        [AllowAnonymous]
        public Dictionary<string, float> GetExtrasByEmployee(string IdOfProject = "")
        {
            var aux = (from asis in _context.Asistances select asis).Where(A => A.ProjectId == IdOfProject).Include(E => E.Employee).ToList();
            var employees = (from asis in aux select asis.Employee.Name).Distinct().ToList();
            Dictionary<string, float> Extras = new();
            var extrasAux = _context.ExtraHours.FromSqlInterpolated($@"SELECT EH.*
                                                                            FROM ExtraHours AS EH
                                                                            INNER JOIN Asistances AS A ON EH.AsistanceId = A.AsistanceId
                                                                            WHERE A.ProjectId = {IdOfProject.ToString()}").Include(E => E.Employee).ToList();
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


        public List<ExtraHour> getListOfExtras(string idProject)
        {
            try
            {
                var extrasAux = _context.ExtraHours.FromSqlInterpolated($@"SELECT EH.*
                                                                            FROM ExtraHours AS EH
                                                                            INNER JOIN Asistances AS A ON EH.AsistanceId = A.AsistanceId
                                                                            WHERE A.ProjectId = {idProject.ToString()}").Include(E => E.Employee).ToList();
                return extrasAux;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

     
        #endregion

    }
}
