using System.Web.Mvc;
using WorkSphere.Repositories;
using System.Web.Security;
using System.Reflection;
using WorkSphere.Models;

namespace WorkSphere.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController()
        {
            _userRepository = new UserRepository();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user != null && _userRepository.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                Session["Username"] = user.Username;
                Session["UserRole"] = user.Role;
                return RedirectToAction("Index", "ProjectAssign"); 
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password, string fullName, string role)
        {
            if (_userRepository.GetUserByUsername(username) != null)
            {
                ViewBag.ErrorMessage = "Username already exists.";
                return View();
            }

            _userRepository.RegisterUser(username, password, fullName, role);
            ViewBag.SuccessMessage = "Registration successful. You can now login.";
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null)
            {
                string tempPassword = "Temp1234";
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(tempPassword);
                _userRepository.UpdatePassword(username, hashedPassword);

                ViewBag.Message = "Temporary password is set to 'Temp1234'. Please change it after login.";
            }
            else
            {
                ViewBag.Message = "User not found.";
            }
            return View();
        }



        public ActionResult ChangePassword()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login");
            }

            string username = Session["Username"].ToString();
            var user = _userRepository.GetUserByUsername(username);

            if (user == null || !_userRepository.ValidateUser(username, currentPassword))
            {
                ViewBag.Message = "Current password is incorrect.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Message = "New passwords do not match.";
                return View();
            }

            string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.UpdatePassword(username, newPasswordHash);

            ViewBag.Message = "Password changed successfully.";
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
