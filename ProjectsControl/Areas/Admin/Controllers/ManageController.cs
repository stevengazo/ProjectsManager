using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectsControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectsControl.Areas.Admin.Controllers
{

    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class ManageController : Controller
    {
        private readonly ApplicationDbContext _ContextIdentity;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ManageController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _ContextIdentity = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// View of Index
        /// </summary>
        /// <returns>Index View</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Display the view to change the password of the user
        /// </summary>
        /// <param name="id">id of the user to change the password</param>
        /// <returns></returns>
        public IActionResult ChangePassword(string id = null)
        {
            var user = GetUserById(id);
            user.PasswordHash = String.Empty;
            return View("ChangePassword", user);
        }

        /// <summary>
        /// Set the new password of the user 
        /// </summary>
        /// <param name="id">User to change the password</param>
        /// <param name="Password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IActionResult PostChangePassword(string id = null, string Password1 = null, string Password2 = null)
        {            
            if(id == null || Password2 == null || Password1 == null)
            {
                var listUsers = _ContextIdentity.Users.ToList();
                return View("ListOfUsers",listUsers);
            }
            else
            {
                var user = GetUserById(id);
                var TmpResult = _userManager.RemovePasswordAsync(user).Result;
                if(TmpResult.Succeeded)
                {
                    if (Password1.Equals(Password2))
                    {
                        var FlagTMP = _userManager.AddPasswordAsync(user, Password1).Result;
                        if(FlagTMP.Succeeded)
                        {
                            ViewBag.UserRoles = GetRoles(idUser: id);
                            ViewBag.Roles = GetRoles();
                            return View("ViewUser", user);
                        }
                    }                    
                }
                var listUsers = _ContextIdentity.Users.ToList();
                return View("ListOfUsers", listUsers);
            }

        }

        /// <summary>
        /// Return a view of the Users from the DB
        /// </summary>
        /// <returns></returns>
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
            IdentityUser user = GetUserById(id,false);
            if (user != null)
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
; return View(new IdentityUser());
            }

        }

        /// <summary>
        /// Received a user id and delete that user from the Database
        /// </summary>
        /// <param name="id">id of the user to delete</param>
        /// <returns></returns>
        public IActionResult PostDeleteUser(string id = null)
        {
            List<IdentityUser> listUsers = new List<IdentityUser>();
            if (id != null)
            {
                var user = GetUserById(id);
                _ContextIdentity.Users.Remove(user);
                _ContextIdentity.SaveChanges();
                listUsers = _ContextIdentity.Users.ToList();
                return View("ListOfUsers", listUsers);
            }
            listUsers = _ContextIdentity.Users.ToList();
            return View("ListOfUsers", listUsers);
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
            if (tmpRol != null)
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


        public IActionResult EditUser(string id = null)
        {
            var _user = GetUserById(id);
            return View(_user);
        }
        public IActionResult PostEditUser([Bind("Id,UserName,Email,PhoneNumber")] IdentityUser user)
        {
            var _tmpUser = GetUserById(user.Id,false);
            _tmpUser.PhoneNumber = user.PhoneNumber.ToString();
            _tmpUser.UserName = user.UserName.ToString();
            _tmpUser.Email = user.Email.ToString();
            _tmpUser.NormalizedUserName = user.UserName.ToUpper();
            _tmpUser.NormalizedEmail = user.Email.ToUpper();

            try
            {
                _ContextIdentity.Users.Update(_tmpUser);
                _ContextIdentity.SaveChanges();
                ViewBag.UserRoles = GetRoles(idUser: _tmpUser.Id);
                ViewBag.Roles = GetRoles();
                return View("ViewUser",user);
            }
            catch (Exception f)
            {
                Console.WriteLine(f.Message);
                throw;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// View the information of the user to delete and return the view
        /// </summary>
        /// <param name="id">id of the user to delete</param>
        /// <returns></returns>
        public IActionResult DeleteUser(string id)
        {
            var _user = GetUserById(id);
            return View(_user);
        }

        #region  Methods


        /// <summary>
        /// Search and return the information of a specific user in the DB
        /// </summary>
        /// <param name="id">Id of the user to search</param>
        /// <returns>Object with the User Information</returns>
        private IdentityUser GetUserById(string idUSER,bool isEditable = false)
        {
            var tmpResult = (
                            from user in _ContextIdentity.Users
                            where user.Id.Contains(idUSER)
                            select user).FirstOrDefault();

            if (tmpResult != null || !isEditable)
            {
                // Dont return the hash of the User Password
              // tmpResult.PasswordHash = string.Empty;
            }
            return tmpResult;
        }

        /// <summary>
        /// Get the roles in the Db
        /// </summary>
        /// <param name="idUser">Id this parameter is specific, the function only get the roles assign to the user</param>
        /// <returns>List of Roles</returns>
        private List<IdentityRole> GetRoles(string idUser = null)
        {
            if (idUser == null)
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


        #endregion

    }
}
