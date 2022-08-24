using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using ChartJSCore.Models.ChartJSCore.Models;
using ChartJSCore.Models.Polar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectsControl.Models;


namespace ProjectsControl.Controllers
{
    public class StadisticController : Controller
    {
        private readonly DBProjectContext _context;

        public StadisticController(DBProjectContext context)
        {
            _context = context;
        }


        // GET: StadisticController
        public ActionResult Index()
        {
            GetQuantityOfProjectsByYear();
            getTypesOfProjectsByYear(2022);
            return View();

        }


        public IActionResult Ventas()
        {
            return View();
        }
        #region INTERNAL METHODS

        /// <summary>
        /// Get the quantity of projects by year
        /// </summary>
        /// <returns>Dictionary, key = year, value = quantity of projects</returns>
        private Dictionary<string,int> GetQuantityOfProjectsByYear()
        {
            try
            {
                Dictionary<string,int> result = new Dictionary<string,int>();
                var tmpResult = (
                        from project in _context.Projects
                        group project by project.BeginDate.Year into projectGroup
                        select projectGroup.Key
                    ).ToList();
                foreach (var item in tmpResult)
                {
                    var tmpItem = (
                            from Project in _context.Projects
                            where Project.BeginDate.Year == item
                            select Project
                        ).Count();
                    result.Add(item.ToString(), tmpItem);
                }
                return result;
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
                throw;
            }
        }

        /// <summary>
        /// Get Quantity of projects in a specific year by the type of job
        /// </summary>
        /// <param name="year">Year to search</param>
        /// <returns>Dictionary of types, Key = type of job, Value = Quantity of Projects</returns>
        private Dictionary<string, int> getTypesOfProjectsByYear(int year)
        {
            try
            {
                Dictionary<string, int> dicResults = new();
                var tmpResult = (
                        from project in _context.Projects
                        group project by project.TypeOfJob into projectGroup
                        select projectGroup.Key
                    ).ToList();
                foreach (var item in tmpResult)
                {
                    var tmpItem = (
                           from Project in _context.Projects
                           where Project.TypeOfJob.Equals(item)
                           select Project
                       ).Count();
                    dicResults.Add(item.ToString(), tmpItem);   
                    
                }
                return dicResults;
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
                throw;
            }
        }

        #endregion

    }
}
