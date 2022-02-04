using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsControl.Models;
using System;
using System.Linq;
using System.Collections.Generic;

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
    }
}
