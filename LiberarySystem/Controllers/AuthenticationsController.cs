using library_system.Business;
using library_system.Models;
using Login.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace library_system.Controllers
{
    public class AuthenticationsController : Controller
    {
        private readonly AuthenticationBO _authBO;

        public AuthenticationsController(AuthenticationBO authBO)
        {
            _authBO = authBO;
        }

        // ✅ SignUp Page
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(Authentication model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await _authBO.SignUpAsync(model);
            if (success)
                return RedirectToAction("SignIn");

            ModelState.AddModelError("", "Username or Email already exists.");
            return View(model);
        }

        // ✅ Access Denied Page
        public IActionResult AccessDenied()
        {
            return View();
        }

        // ✅ SignIn Page
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
       

            // For testing: just get the user by username/password (ignoring token)
            var user = _authBO.ValidateUser(model.Username, model.Password);

            if (user != null)
            {
                // Set session directly
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role.Name);
                HttpContext.Session.SetInt32("UserId", user.ID); // store user id

                // Redirect based on role
                return user.Role.Name switch
                {
                    "Admin" => RedirectToAction("Index", "Books", new { area = "Admin" }),
                    "Employee" => RedirectToAction("Index", "Books", new { area = "Employee" }),
                    _ => RedirectToAction("Index", "Books", new { area = "User" }),
                };
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }
        // ✅ Logout
        [HttpGet]
        [Route("Authentications/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }

        // ✅ Create user (Admin-only)
        [HttpGet]
        [Route("Authentications/Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _authBO.getRolesDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authentications/Create")]
        public async Task<IActionResult> Create(Authentication auth)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _authBO.getRolesDropDown();
                return View(auth);
            }

            var result = await _authBO.SignUpAsync(auth);
            if (result)
                return RedirectToAction("Index", "Authentications");

            ModelState.AddModelError("", "Username or Email already exists.");
            ViewBag.Roles = await _authBO.getRolesDropDown();
            return View(auth);
        }
    }
}
