using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel;
using System.IdentityModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ProjectsControl.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ProjectsControl.Areas.Admin.Controllers
{

    [Area("admin")] 
    [Authorize(Roles ="admin")]
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


        /// <summary>
        /// View the information of a specific user
        /// </summary>
        /// <param name="id">Id of the user to search</param>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public IActionResult ViewUser(string id)
        {
            IdentityUser user = GetUserById(id);
            if(user != null)
            {               
                ViewBag.UserRoles = new List<IdentityRole>();
                ViewBag.UserRoles = GetRoles(idUser: id);
                ViewBag.Roles = new List<IdentityRole>();
                ViewBag.Roles = GetRoles();
                return View(user);
            }
            else
            {
                ViewBag.ErrorMessage = "No se logró encontrar al usuario"
;                return View(new IdentityUser());
            }

        }

        private List<IdentityRole> GetRoles(string idUser= null)
        {
            if(idUser == null)
            {
                // Trae todos los roles
                List<IdentityRole> roles = _ContextIdentity.Roles.ToList();
                return roles;
            }
            else
            {
                // Trae los roles de un usuaior
                var tmpResult = _ContextIdentity.Roles.FromSqlInterpolated(@$"
                                    select id,Name,NormalizedName,ConcurrencyStamp from AspNetRoles
                                    inner join AspNetUserRoles on AspNetUserRoles.RoleId = AspNetRoles.Id
                                    where AspNetUserRoles.UserId = {idUser}").ToList();
                return tmpResult;
            }
        }


        /// <summary>
        /// Received a specific user id and role id and set the rol to the user in the DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="RoleId"></param>
        /// <returns>Return view User Page</returns>
        public IActionResult SetRoleUser(string id, string rolid)
        {
            var tmpRol = (from rol in _ContextIdentity.Roles where rol.Id.Contains(rolid) select rol).FirstOrDefault();           
            if(tmpRol != null)
            {
                var tmpConsult = (
                        from userRol in _ContextIdentity.UserRoles
                        select userRol
                    ).Where(U => U.UserId.Contains(id) && U.RoleId.Contains(tmpRol.Id)).FirstOrDefault();
                if (tmpConsult == null)
                {
                    IdentityUserRole<string> identityUsertpm = new IdentityUserRole<string>();
                    identityUsertpm.UserId = id;
                    identityUsertpm.RoleId = rolid;
                    _ContextIdentity.UserRoles.Add(identityUsertpm);
                    _ContextIdentity.SaveChanges();
                }
            }
            var _user = GetUserById(id);
            ViewBag.UserRoles = GetRoles(idUser: id);
            ViewBag.Roles = GetRoles();
            return View("ViewUser", _user);
        }


        #region  Methods


        /// <summary>
        /// Search and return the information of a specific user in the DB
        /// </summary>
        /// <param name="id">Id of the user to search</param>
        /// <returns>Object with the User Information</returns>
        private IdentityUser GetUserById(string idUSER)
        {
            var tmpResult = (
                            from user in _ContextIdentity.Users
                            where user.Id.Contains(idUSER)
                            select user).FirstOrDefault();

            if(tmpResult != null)
            {
                // Dont return the hash of the User Password
                tmpResult.PasswordHash = string.Empty;
            }
            return tmpResult;
        }

     



        #endregion

    }
}
