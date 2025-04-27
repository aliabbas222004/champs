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
        public async Task<IActionResult> Register(string TeacherId, string Teacher_Name, string Password, string Admin_Password, string Designation)
        {
            if (Admin_Password != "1234567890")
            {
                ViewBag.ErrorMessage = "Invalid admin password!";
                return View("Register");
            }
            TempData["TeacherToken"] = "Yes";
            TempData["tid"] = TeacherId;
            int teacherid = int.Parse(TeacherId);
            Console.WriteLine(teacherid + Teacher_Name + Password + Designation);
            var newTeacher = new TeacherModel { TeacherId = teacherid, TeacherName = Teacher_Name, Password = Password, Designation = Designation };
            var t = await _supabaseService.AddTeacher(newTeacher);
            if (t == false)
            {
                return RedirectToAction("Register");
            }
            HttpContext.Session.SetString("TeacherToken", "Yes");
            return RedirectToAction("TeacherDashboard", new { tid = int.Parse(TeacherId) });
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
                return RedirectToAction("TeacherDashboard", new { tid = t.TeacherId });
            }
        }

        Dictionary<int, string> timeSlots = new Dictionary<int, string>
            {
                {1, "11:00am - 12:00pm"},
                {2, "12:00pm - 1:00pm"},
                {3, "1:00pm - 2:00pm"},
                {4, "2:30pm - 3:30pm"},
                {5, "3:30pm - 4:30pm"},
                {6, "4:30pm - 5:30pm"}
            };

        public async Task<IActionResult> TeacherDashboard(int tid)
        {
            var token = HttpContext.Session.GetString("TeacherToken");
            if (token != "Yes")
            {
                return RedirectToAction("Index");
            }
            var teachers = await _supabaseService.GetTeachers();
            var subjects = await _supabaseService.GetSubjects();
            var depts = await _supabaseService.GetDepartments();
            var classes = await _supabaseService.GetClasses();
            var tt1 = await _supabaseService.GetTeacherSubjectsByAdmin();
            var tt = tt1.Where(t => !t.SubjectId.Contains("L"));
            var selectdSl = await _supabaseService.GetSelectedSlots();
            var SS = selectdSl.Where(a => a.TeacherId == tid).Select(a => a.SubId).Distinct();
            var teacherSubjectByAdmin = tt.Where(t => !SS.Contains(t.SubjectId));
            
                
                var allTimetable = await _supabaseService.GetTimetable();
            var result = (from ts in teacherSubjectByAdmin
                          join t in teachers on ts.TeacherId equals t.TeacherId
                          join s in subjects on ts.SubjectId equals s.SubjectId
                          join d in depts on ts.DeptId equals d.DeptId
                          join c in classes on ts.ClassId equals c.ClassId
                          where ts.TeacherId == tid
                          select new
                          {
                              TeacherName = t.TeacherName,
                              SubjectName = s.SubjectName,
                              DepartmentName = d.DeptName,
                              ClassName = c.ClassName
                          }).ToList();
            var timetable = allTimetable.Where(t => t.TeacherId == tid).ToList();

            var structuredTimetable = new Dictionary<string, string>();

            foreach (var entry in timetable)
            {
                string key = $"{entry.TimeSlot}_{entry.Day}";
                string subjectName = subjects.FirstOrDefault(s => s.SubjectId == entry.SubId)?.SubjectName ?? "Unknown";
                string className = classes.FirstOrDefault(c => c.ClassId == (entry.ClassId).ToString())?.ClassName ?? "Unknown";
                string deptName = depts.FirstOrDefault(d => d.DeptId == entry.DeptId)?.DeptName ?? "Unknown";
                structuredTimetable[key] = $"{subjectName}<br>{className}<br>{deptName}";
            }

            var subjectDict = result.Distinct().ToDictionary(
        item => $"{item.TeacherName}_{item.SubjectName}_{item.DepartmentName}_{item.ClassName}",
        item => $"{item.SubjectName} ({item.DepartmentName}, {item.ClassName})"
    );

            ViewBag.subjectsToSelect = subjectDict;
            ViewBag.TimeSlots = timeSlots;
            ViewBag.Timetable = structuredTimetable;
            ViewBag.tid = tid;

            return View();

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> TeacherInterest(string tid)
        {
            var subjects = await _supabaseService.GetSubjects();
            ViewBag.tid = tid;
            return View(subjects);
        }


        [HttpPost]
        public IActionResult SubmitInterstedSubjects(string tid, string selectedSubjects)
        {
            var selectedSubjectIds = selectedSubjects.Split(',');
            foreach (var subjectId in selectedSubjectIds)
            {
                var teacherSubjectInterest = new TeacherSubjectInterestModel
                {
                    TeacherId = Convert.ToInt32(tid),
                    SubjectId = subjectId
                };

                _supabaseService.AddTeacherSubjectInterest(teacherSubjectInterest);
            }
            ViewBag.tid = tid;
            return RedirectToAction("TeacherDashboard", new { tid = tid });
        }


        public async Task<IActionResult> SelectSubjectToTeach(string teacher_id, string Dept, string Year, string Subject)
        {

            var timetables = await _supabaseService.GetTimetable();
            var subjects = await _supabaseService.GetSubjects();
            var timetable = timetables.Where(t => t.TeacherId == int.Parse(teacher_id)).ToList();
            var no_of_hours = subjects.Where(s => s.SubjectName == (Subject)).Select(s => s.NoOfHoursPerWeek).FirstOrDefault();

            var structuredTimetable = new Dictionary<string, string>();

            foreach (var entry in timetable)
            {
                string key = $"{entry.TimeSlot}_{entry.Day}";
                structuredTimetable[key] = $"{Subject}<br>{Year}<br>{Dept}";
            }
            var depts = await _supabaseService.GetDepartments();
            var deptId = depts.Where(a => a.DeptName == Dept).Select(a => a.DeptId).FirstOrDefault();

            var subjs = await _supabaseService.GetSubjects();
            var subId = subjs.Where(a => a.SubjectName == Subject).Select(a => a.SubjectId).FirstOrDefault();

            var classes = await _supabaseService.GetClasses();
            var classId = classes.Where(a => a.ClassName == Year).Select(a => a.ClassId).FirstOrDefault();

            ViewBag.TimeSlots = timeSlots;
            ViewBag.Timetable = structuredTimetable;
            ViewBag.Subject = Subject;
            ViewBag.Dept = Dept;
            ViewBag.Year = Year;
            ViewBag.No_Of_Hours_Per_Week = no_of_hours;
            ViewBag.tid = teacher_id;

            ViewBag.DeptId = deptId;
            ViewBag.ClassId = classId;
            ViewBag.SubjId = subId;
            return View();
        }


        public async Task<IActionResult> SaveSelectedSlots([FromBody] SlotSelectionModel model)
        {
            try
            {
                if (model == null || model.SelectedSlots == null || !model.SelectedSlots.Any())
                {
                    Console.WriteLine("Hello");
                    return BadRequest("No slots selected.");
                }

                foreach (var slot in model.SelectedSlots)
                {

                    var selectedSlot = new SelectedSlotModel
                    {
                        TeacherId = model.TeacherId,
                        DeptId = int.Parse(model.DeptId),
                        ClassId = model.ClassId,
                        SubId = model.SubjId,
                        Day = slot.day,
                        TimeSlot = slot.slot
                    };
                    await _supabaseService.AddSelectedSlot(selectedSlot);
                }

                return Json(new { success = true, message = "Slots saved successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        public class SlotSelectionModel
        {
            public int TeacherId { get; set; }

            public List<SelectedSlot> SelectedSlots { get; set; }

            public string DeptId { get; set; }
            public string SubjId { get; set; }
            public string ClassId { get; set; }
        }

        public class SelectedSlot
        {
            public int slot { get; set; }
            public string day { get; set; }
        }
    }
}
