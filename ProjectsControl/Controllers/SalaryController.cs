using ProjectsControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsControl.Models;
using ProjectsControl.Data;
using ChartJSCore.Models;

namespace ProjectsControl.Controllers
{
    public class SalaryController : Controller
    {
        /// <summary>
        /// Projects DB
        /// </summary>
        private readonly DBProjectContext _context;
        /// <summary>
        ///  CONSTRUCTOR
        /// </summary>
        /// <param name="context">Dependeny Injeccion</param>
        public SalaryController(DBProjectContext context)
        {
            this._context = context;
        }

        #region External Methos
        public async Task<IActionResult> index(){
            return View();
        }

        #endregion


    }


}

