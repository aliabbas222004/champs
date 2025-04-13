using Microsoft.AspNetCore.Mvc;
using static HomeController;

namespace trySupa.Controllers
{
    public class AdminController : Controller
    {
        private readonly SupabaseService _supabaseService;
        public AdminController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

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
            return RedirectToAction("AdminDashboard");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> AdminDashboard()
        {
            var token = HttpContext.Session.GetString("AdminToken");

            if (token != "Yes")
            {
                return RedirectToAction("Index");
            }
            var timetables = await _supabaseService.GetTimetable();
            var classes = await _supabaseService.GetClasses();
            var allDepts = await _supabaseService.GetDepartments();
            var years = timetables.Select(y => y.ClassId).Distinct().ToList();
            var yearNames = classes.Where(c => !years.Contains(c.ClassId)).Select(c => c.DeptId).Distinct().ToList();
            var depts = allDepts.Where(d => yearNames.Contains(d.DeptId)).Select(d => new { d.DeptId, d.DeptName }).ToList();
            ViewBag.Departments = depts;
            TempData["AdminToken"] = "Yes";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AssignSubjects(string Dept1, string Year1)
        {
            var subyeardept = await _supabaseService.GetSubYearDept();
            var subjects = await _supabaseService.GetSubjects();
            var subj = subyeardept.Where(s => s.DeptId == int.Parse(Dept1) && s.ClassId == (Year1)).Select(s => s.SubjectId);
            var subinfo = subjects.Where(s => subj.Contains(s.SubjectId)).Select(s => new { s.SubjectId, s.SubjectName });
            var teacherInterest = await _supabaseService.GetTeacherSubjectInterest();
            var teachers = await _supabaseService.GetTeachers();
            var timetable = await _supabaseService.GetTimetable();
            var subjectTeachers = teacherInterest
        .Where(ts => subinfo.Select(s => s.SubjectId).Contains(ts.SubjectId))
        .GroupBy(ts => ts.SubjectId)
        .ToDictionary(
            group => new { SubjectId = group.Key, SubjectName = subinfo.FirstOrDefault(s => s.SubjectId == group.Key)?.SubjectName ?? "Unknown" },
            group => group.Select(ts =>
            {
                var teacherData = teachers.FirstOrDefault(t => t.TeacherId == ts.TeacherId);
                return new TeacherInfo
                {
                    TeacherId = teacherData?.TeacherId ?? 0,
                    TeacherName = teacherData?.TeacherName ?? "Unknown",
                    WorkingHours = timetable.Count(t => t.TeacherId == teacherData.TeacherId),
                    Designation = teacherData?.Designation ?? "N/A"
                };
            }).ToList()
        );

            ViewBag.Dept1 = Dept1;
            ViewBag.Year1 = Year1;
            Console.WriteLine(subjectTeachers.Count);
            ViewBag.SubjectTeachers = subjectTeachers;
            return View();
        }


        [HttpPost]
        public IActionResult AssignTeachers(Dictionary<int, int> selectedTeachers, string Dept, string Year)
        {
            Console.WriteLine(Dept + "   " + Year);
            foreach (var assignment in selectedTeachers)
            {
                int subjectId = assignment.Key;
                int teacherId = assignment.Value;
                var model = new TeacherSubjectsSelectedByAdminModel
                {
                    TeacherId = teacherId,
                    SubjectId = subjectId.ToString(),
                    DeptId = int.Parse(Dept),
                    ClassId = Year
                };
                _supabaseService.AddTeacherSubjectByAdmin(model);
            }
            return RedirectToAction("AdminDashboard");
        }

        public class TeacherInfo
        {
            public int TeacherId { get; set; }
            public string TeacherName { get; set; }
            public int WorkingHours { get; set; }
            public string Designation { get; set; }
        }

    }
}
