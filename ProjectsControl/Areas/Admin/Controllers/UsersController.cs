using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using ProjectsControl.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _ContextIdentity;

        /// <summary>
        /// Declararación de UserController e injección de dependencia
        /// </summary>
        /// <param name="_context"></param>
        public UsersController(ApplicationDbContext context,
                                UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager
            )
        {
            _ContextIdentity = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChanceUserPassword(string idUser = null)
        {
            try
            {
                IdentityUser user = null;
                if (idUser == null)
                {
                    if (user == null)
                    {
                        ViewBag.ErrorMessage = "El usuario no fue encontrado";
                        ViewBag.UserId = idUser;
                        return View();
                    }
                }
                else
                {
                    user = GetUser(idUser);
                    ViewBag.User = user;
                    ViewBag.ErrorMessage = "";
                    ViewBag.UserId = idUser;
                    return View();
                }
                return View();
            }
            catch (System.Exception f)
            {
                Console.WriteLine($"Error: {f.Message}");
                ViewBag.ErrorMessage = "El usuario no fue encontrado";
                ViewBag.UserId = idUser;
                return View();
            }
        }


        /// <summary>
        /// View and change the actual password of an exist user
        /// </summary>
        /// <param name="idUser">id of the user to modificate</param>
        /// <param name="Password">New password to set</param>
        /// <param name="confirmPassword">confirm the new password to set</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ChanceUserPassword(string idUser, string Password, string confirmPassword)
        {
            IdentityUser user = GetUser(idUser);
            if (!Password.Equals(confirmPassword) || (user == null))
            {
                ViewBag.ErrorMessage = "Verifique la contraseña y el usuario";
                ViewBag.UserId = idUser;
                return View();
            }
            else
            {
                await _userManager.RemovePasswordAsync(user);
                var flagChangePassword = await _userManager.AddPasswordAsync(user, Password);
                if (flagChangePassword.Succeeded)
                {
                    return RedirectToPage("");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error al intentar modificar la contraseña";
                    ViewBag.UserId = idUser;
                    return View();
                }
            }
        }


        #region Internal_Methods

        /// <summary>
        /// Search an especific user by the id
        /// </summary>
        /// <param name="idUser">id To search in the DB</param>
        /// <returns>the user of the new id</returns>
        private IdentityUser GetUser(string idUser)
        {
            var tmp = (from user
                      in _ContextIdentity.Users
                       where user.Id == idUser
                       select user).FirstOrDefault();
            if (tmp == null)
            {
                return null;
            }
            else
            {
                return tmp;
            }
        }
        #endregion
    }
}
