using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using trySupa.Models;

namespace trySupa.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SupabaseService _supabaseService;

        public TeacherController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string TeacherId, string Teacher_Name, string Password,string Admin_Password, string Designation)
        {
            if (Admin_Password != "1234567890")
            {
                ViewBag.ErrorMessage = "Invalid admin password!";
                return View("Register");
            }
            TempData["TeacherToken"] = "Yes";
            TempData["tid"] = TeacherId;
            int teacherid = int.Parse(TeacherId);
            Console.WriteLine(teacherid+ Teacher_Name+Password + Designation);
            var newTeacher = new TeacherModel { TeacherId = teacherid, TeacherName = Teacher_Name, Password = Password, Designation = Designation };
            var t=await _supabaseService.AddTeacher(newTeacher);
            if (t == false)
            {
                return RedirectToAction("Register");
            }
            HttpContext.Session.SetString("TeacherToken", "Yes");
            return RedirectToAction("TeacherDashboard","Home", new { tid = int.Parse(TeacherId) });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string TeacherID, string Password)
        {
            var teacher_info = await _supabaseService.GetTeachers();
            var t = teacher_info.FirstOrDefault(t => t.TeacherId.ToString() == TeacherID && t.Password == Password);
            if (t == null)
            {
                return View("Login");
            }
            else
            {
                HttpContext.Session.SetString("TeacherToken", "Yes");
                TempData["TeacherToken"] = "Yes";
                TempData["tid"] = TeacherID;
                var d = await _supabaseService.GetDepartments();
                TempData["Departments"] = JsonConvert.SerializeObject(d.Select(d => new { d.DeptId, d.DeptName }).ToList());
                return RedirectToAction("TeacherDashboard","Home", new { tid = t.TeacherId });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("Index","Home");
        }


        public async Task<IActionResult> TeacherInterest(string tid)
        {
            var subjects = await _supabaseService.GetSubjects();
            ViewBag.tid = tid;
            return View(subjects);
        }
    }
}
