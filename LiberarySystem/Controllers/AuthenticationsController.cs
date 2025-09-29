using library_system.Business;
using library_system.Models;
using Login.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> SignUp(Authentication model)
        {
            if (ModelState.IsValid)
            {
                var success = await _authBO.SignUpAsync(model);
                if (success)
                    return RedirectToAction("SignIn");

                ModelState.AddModelError("", "Username or Email already exists.");
            }
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

        private string getPath(string fileName)
        {
            return "~/Views/Admin/" + fileName + ".cshtml";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _authBO.SignInAsync(model.Username, model.Password);

            if (user != null)
            {
                // Save session info
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);



                // Redirect based on Role
                switch (user.Role) { 
                    case "Admin": 
                        return RedirectToAction("Index", "Books", new { area = "Admin" });
                    case "Employee": 
                        return RedirectToAction("Index", "Books", new { area = "" }); 
                    case "User": default: 
                        return RedirectToAction("Index", "Books", new { area = "User" }); }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }

        // ✅ Example of Admin-only action
        public IActionResult AdminOnly()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("AccessDenied");

            return View();
        }
    }
}