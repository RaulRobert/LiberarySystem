using Microsoft.AspNetCore.Mvc;
using library_system.Business;

namespace library_system.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminBO _adminBO;

        public AdminController(AdminBO adminBO)
        {
            _adminBO = adminBO;
        }

        // ✅ Dashboard
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            var model = _adminBO.GetDashboardSummary();
            return View(model);
        }

        // ✅ Manage Users
        public IActionResult Users()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            var users = _adminBO.GetAllUsers();
            return View(users);
        }

        // ✅ Manage Books
        public IActionResult Books()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("AccessDenied", "Authentications");

            var books = _adminBO.GetAllBooks();
            return View(books);
        }
    }
}
