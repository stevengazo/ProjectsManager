using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsControl.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
namespace ProjectsControl.Controllers
{

    /// <summary>
    /// This API  Controller is used to get commond data from the database
    /// This API can´t erase, add or update data in the DB
    /// </summary>
    /// 
    
    [Route("api/")]
    [ApiController]
    public class GenericDataController : ControllerBase
    {

        private readonly DBProjectContext _context;

        public GenericDataController(DBProjectContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Sample of connection with the API
        /// </summary>
        /// <returns>String "Hello"</returns>
        //[Route("connection")]
        [HttpGet]
        [Route("connection")]
        public string connection()
        {
            return "¡hello! You connected with my Backend";
        }

        /// <summary>
        /// Check if an exist project in the db is over 
        /// </summary>
        /// <param name="id">id of the project</param>
        /// <returns>True if the project is over or false if is incomplete or present error</returns>
        [HttpGet]
        [Route("projectisover/{id}")]
        public bool projectIsOver(string id)
        {
            try
            {
                var tmpObj = (from proj in _context.Projects select proj).FirstOrDefault(P => P.ProjectId == id);
                if (tmpObj != null)
                {
                    return tmpObj.IsOver;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception r)
            {
                Console.WriteLine($"Error in API projectIsOver, {r.Message}");
                return false;
            }
        }



        /// <summary>
        /// Search an especific project and return the data 
        /// </summary>
        /// <param name="id">Id of the project to search</param>
        /// <returns>Object type project or null</returns>
        [HttpGet]
        [Route("getProject/{id}")]
        public Project getProject(string id)
        {
            try
            {                               
                var tmpObj = (from proj in _context.Projects select proj).FirstOrDefault(P => P.ProjectId == id);
                if (tmpObj != null)
                {
                    tmpObj.Amount = 0;
                    return tmpObj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception r)
            {
                Console.WriteLine($"Error in API getProject, {r.Message}");
                return null;
            }
        }


        /// <summary>
        /// get a dictionary of projects 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDictOfProjects")]
        [Route("getDictOfProjects/{id}")]
        public Dictionary<string,string> getDictOfProjects(string id= null)
        {
            try
            {
                return _context.Projects.ToDictionary(P=>P.ProjectId,P=>P.ProjectName);
            }
            catch (Exception r)
            {
                Console.WriteLine($"Error in API getDictOfProjects, {r.Message}");
                return null;
            }

        }

        /// Ideas to build the function
        /// 1. Get the date to check and return an existan row in the Table
        /// 2. If the row not exist check the last day registered in the database
        /// 3. Get the quantity of days between the parameter and the day in the database (thursday)
        /// 4. Divide the quantity of days in 7 (week) and sum the result to the actual number of week
        /// 5. Return the new registered in the database
        /// Adicionally is necesary check the year of the dates 


        /// <summary>
        /// Check if exist and row in the DB with the date received
        /// </summary>
        /// <param name="id">Date to check. The format is "checkWeek/AAAA-MM-DD" </param>
        /// <returns>Null if presents error, week object if create a new or exist in the db</returns>
        [HttpGet]
        [Route("checkWeek")]
        [Route("checkWeek/{id}")]
        public string checkWeek(string id)
        {           
            if(id != null)
            {
                var DateToCheck =  DateTime.Parse(id);
                var tmpday = DateToCheck.DayOfWeek;
                var tmplong = $"fecha {DateToCheck.ToLongDateString()} y dia {tmpday.ToString()}";
                return tmplong;

              /*  var tmpresult = (from week in _context.Week select week).Where(W => ((W.BeginOfWeek <=DateToCheck) && (W.EndOfWeek >= DateToCheck ))).FirstOrDefault();
                if(tmpresult != null)
                {
                    var tmpDay = 
                    return tmpresult;
                }
                else
                {
                    //  the date not exist in the DB
                    // Is necessary create a new object with the range of dates 
                    return null;
                }*/
            }           
            return null;
        }
    }
}
