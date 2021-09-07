using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectsControl.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ProjectsControl.Areas.Admin.Controllers
{
    [Authorize]
    [Area(areaName: "Admin")]
    public class ManageController : Controller
    {
        private ApplicationDbContext _ContextIdentity;

        public ManageController(ApplicationDbContext context)
        {
            _ContextIdentity = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ListOfUsers()
        {           
            var query =  (from a in _ContextIdentity.Users select a).OrderBy(U => U.Id);
            return  View(query.ToList());
        }
    }
}
