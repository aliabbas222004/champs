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
        public IActionResult AssignTeachers(Dictionary<string, int> selectedTeachers, string Dept, string Year)
        {
            Console.WriteLine(Dept + "   " + Year);
            foreach (var assignment in selectedTeachers)
            {
                string subjectId = assignment.Key;
                int teacherId = assignment.Value;
                var model = new TeacherSubjectsSelectedByAdminModel
                {
                    TeacherId = teacherId,
                    SubjectId = subjectId,
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

        [HttpPost]
        public async Task<IActionResult> GenerateTimeTableForDept(int Dept, string Year)
        {
            await CreateTimeTable(Dept, Year);

            return RedirectToAction("AdminDashboard", "Admin");
        }


        public async Task<List<TimetableModel>> CreateTimeTable(int deptId, string classId)
        {

            var timetable = new List<TimetableModel>();

            var subYearDeptList = await _supabaseService.GetSubYearDept();
            var subjectIds = subYearDeptList
                .Where(syd => syd.DeptId == deptId && syd.ClassId == classId)
                .Select(syd => syd.SubjectId)
                .Distinct()
                .ToList();

            var nOG = await _supabaseService.GetClasses();
            var noOfGroups = nOG.Where(s => s.DeptId == deptId && s.ClassId == classId).Select(s => s.NoOfGroups).FirstOrDefault();

            var allSubjects = await _supabaseService.GetSubjects();
            var subjectDetails = allSubjects.Where(s => subjectIds.Contains(s.SubjectId));

            var adminAssignments = await _supabaseService.GetTeacherSubjectsByAdmin();
            var allTeachers = await _supabaseService.GetTeachers();
            var selectedTeacherIds = adminAssignments.Where(a => !a.SubjectId.Contains("L")).Select(a => a.TeacherId).Distinct().ToList();
            var selectedTeachers = allTeachers.Where(t => selectedTeacherIds.Contains(t.TeacherId)).ToList();

            var selectedSlots = await _supabaseService.GetSelectedSlots(); // Returns List<TimetableEntry>


            var oS = await _supabaseService.GetTimetable();
            // This is for LAB ......................
            {
                var labSubjects = subjectDetails.Where(s => s.SubjectName.Contains("L"));
                var selectedLabTeacherId = adminAssignments.Where(s => s.SubjectId.Contains("L")).Select(a => a.TeacherId).Distinct();
                var occupiedSlots = oS.Where(t => selectedLabTeacherId.Contains(t.TeacherId)).ToList();

                var availableLabSlots = new List<(string Day, int Timeslot)>();
                var labSlotCandidates = new List<(string Day, int Timeslot)>
                {
                    ("Monday", 1), ("Monday", 5),
                    ("Tuesday", 1), ("Tuesday", 5),
                    ("Wednesday", 1), ("Wednesday", 5),
                    ("Thursday", 1), ("Thursday", 5),
                    ("Friday", 1), ("Friday", 5),
                    ("Saturday", 1), ("Saturday", 5),
                };
                var allocatedDays = new HashSet<string>();
                foreach (var (day, timeslot) in labSlotCandidates)
                {
                    if (allocatedDays.Contains(day))
                    {
                        continue;
                    }
                    bool isFreeForAll = selectedLabTeacherId.All(tid =>
                        !occupiedSlots.Any(o => o.TeacherId == tid && o.Day == day && o.TimeSlot == timeslot)
                    );

                    if (isFreeForAll)
                        availableLabSlots.Add((day, timeslot));

                    allocatedDays.Add(day);

                    if (availableLabSlots.Count == noOfGroups)
                        break;
                }



                foreach (var (day, timeslot) in availableLabSlots)
                {
                    foreach (var teacher in selectedLabTeacherId)
                    {
                        var sid = adminAssignments.Where(a => a.TeacherId == teacher).Select(a => a.SubjectId).FirstOrDefault();
                        timetable.Add(new TimetableModel
                        {
                            TeacherId = teacher,
                            Day = day,
                            TimeSlot = timeslot,
                            DeptId = deptId,
                            ClassId = classId,
                            SubId = sid
                        });

                        timetable.Add(new TimetableModel
                        {
                            TeacherId = teacher,
                            Day = day,
                            TimeSlot = timeslot + 1,
                            DeptId = deptId,
                            ClassId = classId,
                            SubId = sid
                        });
                    }
                }

            }

            var theorySubjects = subjectDetails.Where(s => !s.SubjectId.Contains("L"));
            theorySubjects = theorySubjects
                .Select(subject =>
                {
                    var assignedTeacherEntry = adminAssignments.FirstOrDefault(t =>
                        t.SubjectId == subject.SubjectId && t.DeptId == deptId && t.ClassId == classId);

                    var teacher = allTeachers.FirstOrDefault(t => t.TeacherId == assignedTeacherEntry?.TeacherId);
                    int priority = teacher != null ? GetDesignationPriority(teacher.Designation) : 0;

                    return new { subject, priority };
                })
                .OrderByDescending(x => x.priority)
                .Select(x => x.subject)
                .ToList();


            foreach (var subject in theorySubjects)
            {
                var allocatedDaysForTeacher = new HashSet<string>();
                var assignedTeacherEntry = adminAssignments.FirstOrDefault(t =>
                    t.SubjectId == subject.SubjectId && t.DeptId == deptId && t.ClassId == classId);

                var teacher = allTeachers.FirstOrDefault(t => t.TeacherId == assignedTeacherEntry.TeacherId);

                var teacherSlots = selectedSlots.Where(s => s.TeacherId == teacher.TeacherId).ToList();

                int hoursAssigned = 0;
                int maxHours = subject.NoOfHoursPerWeek;

                foreach (var slot in teacherSlots.OrderBy(s => s.Day).ThenBy(s => s.TimeSlot))
                {
                    Console.WriteLine(slot.Day + " " + slot.TimeSlot);
                    if (hoursAssigned >= maxHours) break;
                    bool isSlotTaken = timetable.Any(t => t.Day == slot.Day && t.TimeSlot == slot.TimeSlot);
                    Console.WriteLine(isSlotTaken + "----------------------------" + teacher.TeacherId);
                    if (!isSlotTaken && !allocatedDaysForTeacher.Contains(slot.Day))
                    {

                        timetable.Add(new TimetableModel
                        {
                            TeacherId = teacher.TeacherId,
                            Day = slot.Day,
                            TimeSlot = slot.TimeSlot,
                            DeptId = deptId,
                            ClassId = classId,
                            SubId = subject.SubjectId
                        });

                        allocatedDaysForTeacher.Add(slot.Day);

                        hoursAssigned += subject.TimeOfLecture;
                    }

                }

                if (hoursAssigned < maxHours)
                {
                    var days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

                    foreach (var day in days)
                    {
                        for (int timeslot = 1; timeslot <= 6; timeslot++)
                        {

                            bool isSlotTaken = timetable.Any(t => t.Day == day && t.TimeSlot == timeslot);
                            if (!isSlotTaken)
                            {
                                if (hoursAssigned >= maxHours) break;

                                if (!oS.Any(t => t.TeacherId == teacher.TeacherId && t.Day == day && t.TimeSlot == timeslot)
                                   )
                                {
                                    timetable.Add(new TimetableModel
                                    {
                                        TeacherId = teacher.TeacherId,
                                        Day = day,
                                        TimeSlot = timeslot,
                                        DeptId = deptId,
                                        ClassId = classId,
                                        SubId = subject.SubjectId
                                    });

                                    hoursAssigned += subject.TimeOfLecture;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var entry in timetable)
            {
                Console.WriteLine($"Subject: {entry.SubId}, Teacher: {entry.TeacherId}, Day: {entry.Day}, Time: {entry.TimeSlot}");
            }

            foreach (var entry in timetable)
            {
                await _supabaseService.AddTimetable(entry);
            }

            return timetable;
        }

        private static int GetDesignationPriority(string designation)
        {
            return designation switch
            {
                "Dean" => 5,
                "HOD" => 4,
                "Professor" => 3,
                "Associate Professor" => 2,
                "Assistant Professor" => 1,
                _ => 0
            };
        }


        private async Task<bool> IsSlotAvailable(int teacherId, string day, int timeslot, List<TimetableModel> timetable)
        {
            return !timetable.Any(t =>
                (t.TeacherId == teacherId || t.ClassId == t.ClassId) &&
                t.Day == day && t.TimeSlot == timeslot);
        }

    }
}
