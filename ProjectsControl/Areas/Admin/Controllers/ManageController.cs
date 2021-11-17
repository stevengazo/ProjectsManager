using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectsControl.Data;

namespace ProjectsControl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageController : Controller
    {
        private readonly ApplicationDbContext _ContextIdentity;

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
            var query = (from a in _ContextIdentity.Users select a).OrderBy(U => U.Id).ToList();
            return View(query.ToList());
        }
    }
}
