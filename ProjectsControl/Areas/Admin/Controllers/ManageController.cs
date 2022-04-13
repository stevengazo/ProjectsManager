using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel;
using System.IdentityModel;
using System.Linq;
using System.Threading.Tasks;
using ProjectsControl.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ProjectsControl.Areas.Admin.Controllers
{

    [Area("Admin")] 
    [Authorize(Roles ="Admin")]
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

        [HttpGet]
        public IActionResult ListOfUsers()
        {           
            var query = (from a in _ContextIdentity.Users select a).OrderBy(U => U.Id).ToList();
            return View(query.ToList());
        }

        [HttpGet]
        public IActionResult ViewUser(string id)
        {
            IdentityUser user = GetUserById(id);
            if(user != null)
            {
                return View(user);
            }
            else
            {
                ViewBag.ErrorMessage = "No se logró encontrar al usuario"
;                return View(new IdentityUser());
            }

        }



        #region  Methods

        private IdentityUser GetUserById(string id)
        {
            var tmpResult = (
                            from user in _ContextIdentity.Users
                            where user.Id.Contains(id)
                            select user).FirstOrDefault();
            return tmpResult;
        }

        #endregion

    }
}
