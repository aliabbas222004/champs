using Microsoft.AspNetCore.Mvc;

namespace trySupa.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult LoginAsAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAsAdmin(string adminPassword)
        {
            if (string.IsNullOrEmpty(adminPassword) || adminPassword != "1234@4321")
            {
                ViewBag.ErrorMessage = "Incorrect password!";
                return View("LoginAsAdmin");
            }
            HttpContext.Session.SetString("AdminToken", "Yes");
            return RedirectToAction("AdminDashboard","Home");

        }
    }
}
