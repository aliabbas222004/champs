using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using trySupa.Models;

public class HomeController : Controller
{
    private readonly SupabaseService _supabaseService;

    public HomeController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    //public async Task<IActionResult> Index()
    //{
    //    _supabaseService.GetDeptDataAsync();
    //        return View();

    //}
    List<TimetableEntry> AllTeachertimetable = new List<TimetableEntry>
        {
            new TimetableEntry { TeacherId = 1, Day = "Monday", Timeslot = 1, DeptId = 101, ClassId = 201, SubId = "301" },
            new TimetableEntry { TeacherId = 1, Day = "Monday", Timeslot = 2, DeptId = 101, ClassId = 202, SubId = "302" },
            new TimetableEntry { TeacherId = 1, Day = "Monday", Timeslot = 3, DeptId = 101, ClassId = 203, SubId = "303" },
            new TimetableEntry { TeacherId = 1, Day = "Monday", Timeslot = 4, DeptId = 101, ClassId = 204, SubId = "304" },
            new TimetableEntry { TeacherId = 1, Day = "Monday", Timeslot = 5, DeptId = 101, ClassId = 205, SubId = "305" },

            new TimetableEntry { TeacherId = 1, Day = "Tuesday", Timeslot = 1, DeptId = 101, ClassId = 201, SubId = "301" },
            new TimetableEntry { TeacherId = 1, Day = "Tuesday", Timeslot = 2, DeptId = 101, ClassId = 202, SubId = "302" },
            new TimetableEntry { TeacherId = 1, Day = "Tuesday", Timeslot = 3, DeptId = 101, ClassId = 203, SubId = "303" },
            new TimetableEntry { TeacherId = 1, Day = "Tuesday", Timeslot = 4, DeptId = 101, ClassId = 204, SubId = "304" },
            new TimetableEntry { TeacherId = 1, Day = "Tuesday", Timeslot = 5, DeptId = 101, ClassId = 205, SubId = "305" },

            new TimetableEntry { TeacherId = 1, Day = "Wednesday", Timeslot = 1, DeptId = 101, ClassId = 201, SubId = "301" },
            new TimetableEntry { TeacherId = 1, Day = "Wednesday", Timeslot = 2, DeptId = 101, ClassId = 202, SubId = "302" },
            new TimetableEntry { TeacherId = 1, Day = "Wednesday", Timeslot = 3, DeptId = 101, ClassId = 203, SubId = "303" },
            new TimetableEntry { TeacherId = 1, Day = "Wednesday", Timeslot = 4, DeptId = 101, ClassId = 204, SubId = "304" },
            new TimetableEntry { TeacherId = 1, Day = "Wednesday", Timeslot = 5, DeptId = 101, ClassId = 205, SubId = "305" },

            new TimetableEntry { TeacherId = 1, Day = "Thursday", Timeslot = 1, DeptId = 101, ClassId = 201, SubId = "301" },
            new TimetableEntry { TeacherId = 1, Day = "Thursday", Timeslot = 2, DeptId = 101, ClassId = 202, SubId = "302" },
            new TimetableEntry { TeacherId = 1, Day = "Thursday", Timeslot = 3, DeptId = 101, ClassId = 203, SubId = "303" },
            new TimetableEntry { TeacherId = 1, Day = "Thursday", Timeslot = 4, DeptId = 101, ClassId = 204, SubId = "304" },

            new TimetableEntry { TeacherId = 1, Day = "Friday", Timeslot = 2, DeptId = 101, ClassId = 202, SubId = "302" },
            new TimetableEntry { TeacherId = 1, Day = "Friday", Timeslot = 3, DeptId = 101, ClassId = 203, SubId = "303" },
            new TimetableEntry { TeacherId = 1, Day = "Friday", Timeslot = 4, DeptId = 101, ClassId = 204, SubId = "304" },
            new TimetableEntry { TeacherId = 1, Day = "Friday", Timeslot = 5, DeptId = 101, ClassId = 205, SubId = "305" },

            new TimetableEntry { TeacherId = 1, Day = "Saturday", Timeslot = 1, DeptId = 101, ClassId = 201, SubId = "301" },
            new TimetableEntry { TeacherId = 1, Day = "Saturday", Timeslot = 2, DeptId = 101, ClassId = 202, SubId = "302" },
            new TimetableEntry { TeacherId = 1, Day = "Saturday", Timeslot = 3, DeptId = 101, ClassId = 203, SubId = "303" },
            new TimetableEntry { TeacherId = 1, Day = "Saturday", Timeslot = 4, DeptId = 101, ClassId = 204, SubId = "304" },
            new TimetableEntry { TeacherId = 1, Day = "Saturday", Timeslot = 5, DeptId = 101, ClassId = 205, SubId = "305" }
        };


    List<Subject> s1 = new List<Subject>()
        {
            new Subject{SubjectId="301",Subject_Name="DLD",No_of_Hours_per_Week=5,time_of_Lecture=1},
            new Subject{SubjectId="302",Subject_Name="OS",No_of_Hours_per_Week=5,time_of_Lecture=1},
            new Subject{SubjectId="303",Subject_Name=".NET",No_of_Hours_per_Week=5,time_of_Lecture=1},
            new Subject{SubjectId="304",Subject_Name="CO",No_of_Hours_per_Week=5,time_of_Lecture=1},
            new Subject{SubjectId="305",Subject_Name="CN",No_of_Hours_per_Week=5,time_of_Lecture=1},
            new Subject{SubjectId="306",Subject_Name="SE",No_of_Hours_per_Week=5,time_of_Lecture=1},
            new Subject{SubjectId="307",Subject_Name="AM-III",No_of_Hours_per_Week=5,time_of_Lecture=1},
        };

    List<ClassInfo> classinfo = new List<ClassInfo>()
        {
            new ClassInfo{ClassId=201,Class_Name="BE-III",No_of_groups=4,DeptId=101},
            new ClassInfo{ClassId=202,Class_Name="BE-II",No_of_groups=4,DeptId=101},
            new ClassInfo{ClassId=203,Class_Name="BE-I",No_of_groups=4,DeptId=101},
            new ClassInfo{ClassId=204,Class_Name="BE-IV",No_of_groups=4,DeptId=101},
            new ClassInfo{ClassId=205,Class_Name="BE-V",No_of_groups=4,DeptId=101},
            new ClassInfo{ClassId=210,Class_Name="BE-I",No_of_groups=4,DeptId=102},
        };

    List<Dept> dept = new List<Dept>() {
            new Dept{DeptId=101,Dept_Name="CSE"},
            new Dept{DeptId=102,Dept_Name="EE"},
        };

    List<Teacher> teacher = new List<Teacher>() {
            new Teacher{TeacherId=1,Teacher_Name="Aliabbas",Password="1234",Designation="Professor"},
        };

    List<SubYearDept> subYearDepts = new List<SubYearDept>()
    {
        new SubYearDept { SubjectId = "301", ClassId = 201, DeptId = 101 },
        new SubYearDept { SubjectId = "302", ClassId = 202, DeptId = 101 },
        new SubYearDept { SubjectId = "303", ClassId = 203, DeptId = 101 },
        new SubYearDept { SubjectId = "304", ClassId = 204, DeptId = 101 },
        new SubYearDept { SubjectId = "305", ClassId = 205, DeptId = 101 },
        new SubYearDept { SubjectId = "306", ClassId = 205, DeptId = 101 },
        new SubYearDept { SubjectId = "307", ClassId = 206, DeptId = 102 },
        new SubYearDept { SubjectId = "307", ClassId = 210, DeptId = 102 }
    };

    List<SubYearDept> remainingSubjects = new List<SubYearDept>()
    {
        new SubYearDept { SubjectId = "306", ClassId = 205, DeptId = 101 },
        new SubYearDept { SubjectId = "307", ClassId = 206, DeptId = 102 },
        new SubYearDept { SubjectId = "307", ClassId = 210, DeptId = 102 }
    };

    List<TeacherSubject> teacherSubjectsSelectedByAdmin = new List<TeacherSubject>
{
    new TeacherSubject { TeacherId = 1, SubjectId = "301", DeptId = 101, ClassId = 201 },
    new TeacherSubject { TeacherId = 1, SubjectId = "302", DeptId = 101, ClassId = 202 }
};

    List<TeachSubj> teachSubj = new List<TeachSubj>
    {
        new TeachSubj {TeacherId = 1, SubjectId = "301"},
        new TeachSubj {TeacherId = 1, SubjectId = "302"},
        new TeachSubj {TeacherId = 1, SubjectId = "307"},
    };

    Dictionary<int, string> timeSlots = new Dictionary<int, string>
    {
        {1, "11:00am - 12:00pm"},
        {2, "12:00pm - 1:00pm"},
        {3, "1:00pm - 2:00pm"},
        {4, "2:30pm - 3:30pm"},
        {5, "3:30pm - 4:30pm"},
        {6, "4:30pm - 5:30pm"}
    };

    public IActionResult TeacherDashboard(int tid)
    {
        var token = HttpContext.Session.GetString("TeacherToken");
        if (token != "Yes")
        {
            return RedirectToAction("Index");
        }
        var result = (from ts in teacherSubjectsSelectedByAdmin
                      join t in teacher on ts.TeacherId equals t.TeacherId
                      join s in s1 on ts.SubjectId equals s.SubjectId
                      join d in dept on ts.DeptId equals d.DeptId
                      join c in classinfo on ts.ClassId equals c.ClassId
                      where ts.TeacherId == tid
                      select new
                      {
                          TeacherName = t.Teacher_Name,
                          SubjectName = s.Subject_Name,
                          DepartmentName = d.Dept_Name,
                          ClassName = c.Class_Name
                      }).ToList();
        var timetable = AllTeachertimetable.Where(t => t.TeacherId == tid).ToList();

        var structuredTimetable = new Dictionary<string, string>();

        foreach (var entry in timetable)
        {
            string key = $"{entry.Timeslot}_{entry.Day}";
            string subjectName = s1.FirstOrDefault(s => s.SubjectId == entry.SubId)?.Subject_Name ?? "Unknown";
            string className = classinfo.FirstOrDefault(c => c.ClassId == entry.ClassId)?.Class_Name ?? "Unknown";
            string deptName = dept.FirstOrDefault(d => d.DeptId == entry.DeptId)?.Dept_Name ?? "Unknown";
            structuredTimetable[key] = $"{subjectName}<br>{className}<br>{deptName}";
        }

        var subjectDict = result.ToDictionary(
    item => $"{item.TeacherName}_{item.SubjectName}_{item.DepartmentName}_{item.ClassName}",
    item => $"{item.SubjectName} ({item.DepartmentName}, {item.ClassName})"
);

        ViewBag.subjectsToSelect = subjectDict;
        ViewBag.TimeSlots = timeSlots;
        ViewBag.Timetable = structuredTimetable;
        ViewBag.tid = tid;

        return View();

    }

    public IActionResult AdminDashboard()
    {
        var token = HttpContext.Session.GetString("AdminToken");

        if (token != "Yes")
        {
            return RedirectToAction("Index");
        }
        var years = AllTeachertimetable.Select(y => y.ClassId).Distinct().ToList();
        var yearNames = classinfo.Where(c => !years.Contains(c.ClassId)).Select(c => c.DeptId).Distinct().ToList();
        var depts = dept.Where(d => yearNames.Contains(d.DeptId)).Select(d => new { d.DeptId, d.Dept_Name }).ToList();
        ViewBag.Departments = depts;
        TempData["AdminToken"] = "Yes";

        return View();
    }


    [HttpPost]
    public IActionResult SubmitInterstedSubjects(string tid,string selectedSubjects)
    {
        Console.WriteLine(selectedSubjects);
        var selectedSubjectIds = selectedSubjects.Split(',');
        ViewBag.tid = tid;
        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> DisplayTimetable(string Department, string Class)
    {
        var timetable = await _supabaseService.GetTimetable();
        //var timetable = AllTeachertimetable.ToList();
        var classId = classinfo.Where(c => c.Class_Name == Class).Select(c => c.ClassId).FirstOrDefault();
        var deptId = dept.Where(d => d.Dept_Name == Department).Select(d => d.DeptId).FirstOrDefault();
        var filteredTimetable = timetable
        .Where(t => t.DeptId == deptId && t.ClassId == classId)
        .OrderBy(t => t.Day)
        .ThenBy(t => t.TimeSlot)
        .ToList();


        var structuredTimetable = new Dictionary<string, string>();

        foreach (var entry in filteredTimetable)
        {
            string key = $"{entry.TimeSlot}_{entry.Day}";
            string subjectName = s1.FirstOrDefault(s => s.SubjectId == entry.SubId)?.Subject_Name ?? "Unknown";
            string teacherName = teacher.FirstOrDefault(c => c.TeacherId == entry.TeacherId)?.Teacher_Name ?? "Unknown";
            structuredTimetable[key] = $"{subjectName}<br>{teacherName}";
        }

        ViewBag.TimeSlots = timeSlots;
        ViewBag.Timetable = structuredTimetable;



        return View();
    }

    



    //[HttpPost]
    public IActionResult SelectSubjectToTeach(string teacher_id, string Dept, string Year, string Subject)
    {

        var timetable = AllTeachertimetable.Where(t => t.TeacherId == int.Parse(teacher_id)).ToList();
        var no_of_hours = s1.Where(s => s.Subject_Name == (Subject)).Select(s => s.No_of_Hours_per_Week).FirstOrDefault();

        var structuredTimetable = new Dictionary<string, string>();

        foreach (var entry in timetable)
        {
            string key = $"{entry.Timeslot}_{entry.Day}";
            //string subjectName = s1.FirstOrDefault(s => s.SubjectId == entry.SubId)?.Subject_Name ?? "Unknown";
            //string className = classinfo.FirstOrDefault(c => c.ClassId == entry.ClassId)?.Class_Name ?? "Unknown";
            //string deptName = dept.FirstOrDefault(d => d.DeptId == entry.DeptId)?.Dept_Name ?? "Unknown";
            structuredTimetable[key] = $"{Subject}<br>{Year}<br>{Dept}";
        }

        ViewBag.TimeSlots = timeSlots;
        ViewBag.Timetable = structuredTimetable;
        ViewBag.Subject = Subject;
        ViewBag.Dept = Dept;
        ViewBag.Year = Year;
        ViewBag.No_Of_Hours_Per_Week = no_of_hours;
        ViewBag.tid = teacher_id;

        return View();

        //var timetable = AllTeachertimetable.Where(t => t.TeacherId == int.Parse(teacher_id)).ToList();
        //var no_of_hours = s1.Where(s => s.SubjectId == (Subject)).Select(s => s.No_of_Hours_per_Week).FirstOrDefault();

        //var structuredTimetable = new Dictionary<string, string>();

        //foreach (var entry in timetable)
        //{
        //    string key = $"{entry.Timeslot}_{entry.Day}";
        //    string subjectName = s1.FirstOrDefault(s => s.SubjectId == entry.SubId)?.Subject_Name ?? "Unknown";
        //    string className = classinfo.FirstOrDefault(c => c.ClassId == entry.ClassId)?.Class_Name ?? "Unknown";
        //    string deptName = dept.FirstOrDefault(d => d.DeptId == entry.DeptId)?.Dept_Name ?? "Unknown";
        //    structuredTimetable[key] = $"{subjectName}<br>{className}<br>{deptName}";
        //}

        //ViewBag.TimeSlots = timeSlots;
        //ViewBag.Timetable = structuredTimetable;
        //ViewBag.Subject = s1.Where(s => s.SubjectId == (Subject)).Select(s => s.Subject_Name).FirstOrDefault();
        //ViewBag.Dept = dept.Where(d => d.DeptId == int.Parse(Dept)).Select(s => s.Dept_Name).FirstOrDefault();
        //ViewBag.Year = classinfo.Where(c => c.ClassId == int.Parse(Year)).Select(c => c.Class_Name).FirstOrDefault();
        //ViewBag.No_Of_Hours_Per_Week = no_of_hours;
        //ViewBag.tid = teacher_id;

        //return View();
    }

    public IActionResult SaveSelectedSlots([FromBody] SlotSelectionModel model)
    {
        try
        {
            if (model == null || model.SelectedSlots == null || !model.SelectedSlots.Any())
            {
                Console.WriteLine("Hello");
                return BadRequest("No slots selected.");
            }

            Console.WriteLine($"Teacher ID: {model.TeacherId}");
            foreach (var slot in model.SelectedSlots)
            {
                Console.WriteLine($"Selected Slot: {slot.Day} - {slot.TimeSlot}");
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
        public string TeacherId { get; set; }
        public List<SelectedSlot> SelectedSlots { get; set; }
    }

    public class SelectedSlot
    {
        public string Day { get; set; }
        public int TimeSlot { get; set; }
    }


    //[HttpGet]
    //public JsonResult GetYears(string deptId)
    //{
    //    Console.WriteLine(deptId);
    //    var years = remainingSubjects.Where(y => y.DeptId == int.Parse(deptId)).Select(y => y.ClassId).ToList();
    //    var yearNames = classinfo.Where(y => years.Contains(y.ClassId)).Select(y => new { y.ClassId, y.Class_Name }).ToList();
    //    foreach (var year in yearNames)
    //    {
    //        Console.WriteLine($"ClassId: {year.ClassId}, Class_Name: {year.Class_Name}");
    //    }

    //    return Json(yearNames);
    //}

    [HttpGet]
    public async Task<JsonResult> GetYears(string deptId)
    {
        Console.WriteLine(deptId);
        var remainingSubs = await _supabaseService.GetRemainingSubjects();
        var classes = await _supabaseService.GetClasses();
        var years = remainingSubs.Where(y => y.DeptId == int.Parse(deptId)).Select(y => y.ClassId).ToList();
        var yearNames = classes.Where(y => years.Contains(y.ClassId)).Select(y => new { y.ClassId, y.ClassName}).ToList();
        foreach (var year in yearNames)
        {
            Console.WriteLine($"ClassId: {year.ClassId}, Class_Name: {year.ClassName}");
        }

        return Json(yearNames);
    }

    [HttpGet]
    public JsonResult GetSubjects(int deptId, int yearId)
    {
        var subjects = remainingSubjects.Where(s => s.DeptId == deptId && s.ClassId == yearId)
                        .Select(s => s.SubjectId).ToList();
        var subNames = s1.Where(s => subjects.Contains(s.SubjectId)).Select(s => new { s.SubjectId, s.Subject_Name }).ToList();
        return Json(subNames);
    }

    public class TeacherInfo
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int WorkingHours { get; set; }
        public string Designation { get; set; }
    }

    [HttpPost]
    public IActionResult AssignSubjects(string Dept1, string Year1)
    {
        var subj = subYearDepts.Where(s => s.DeptId == int.Parse(Dept1) && s.ClassId == int.Parse(Year1)).Select(s => s.SubjectId);
        var subinfo = s1.Where(s => subj.Contains(s.SubjectId)).Select(s => new { s.SubjectId, s.Subject_Name });
        var subjectTeachers = teachSubj
    .Where(ts => subinfo.Select(s => s.SubjectId).Contains(ts.SubjectId))
    .GroupBy(ts => ts.SubjectId)
    .ToDictionary(
        group => new { SubjectId = group.Key, SubjectName = subinfo.FirstOrDefault(s => s.SubjectId == group.Key)?.Subject_Name ?? "Unknown" },
        group => group.Select(ts =>
        {
            var teacherData = teacher.FirstOrDefault(t => t.TeacherId == ts.TeacherId);
            return new TeacherInfo
            {
                TeacherId = teacherData?.TeacherId ?? 0,
                TeacherName = teacherData?.Teacher_Name ?? "Unknown",
                WorkingHours = AllTeachertimetable.Count(t => t.TeacherId == teacherData.TeacherId),
                Designation = teacherData?.Designation ?? "N/A"
            };
        }).ToList()
    );


        Console.WriteLine(subjectTeachers.Count);
        ViewBag.SubjectTeachers = subjectTeachers;
        return View();
    }

    [HttpPost]
    public IActionResult GenerateTimeTable(string Dept, string Year)
    {

        var remainingSubjectsToSelect = remainingSubjects.Where(s => s.DeptId == int.Parse(Dept) && s.ClassId == int.Parse(Year)).Select(s => s.SubjectId);
        TempData["Success"] = false;
        if (remainingSubjectsToSelect.Count() == 0) {
            TempData["AlertMessage"] = "Timetable generated successfully!";
            TempData["Success"] = true;

        }
        else
        {
            var subinfo = s1.Where(s => remainingSubjectsToSelect.Contains(s.SubjectId)).Select(s => new { s.SubjectId, s.Subject_Name });
            var message = "";
            foreach (var item in subinfo)
            {
                message += " " + item.Subject_Name;
            }
            TempData["AlertMessage"] = "The subject -->" + message + "  are not selected by any teacher";
        }
        return RedirectToAction("AdminDashboard");
    }


    [HttpPost]
    public IActionResult AssignTeachers(Dictionary<int, int> selectedTeachers)
    {
        foreach (var assignment in selectedTeachers)
        {
            int subjectId = assignment.Key;
            int teacherId = assignment.Value;

            Console.WriteLine($"Assigned Teacher {teacherId} to Subject {subjectId}");
        }
        return RedirectToAction("AdminDashboard");
    }

    [HttpGet]
    public JsonResult GetYearsForRemainingDept(string deptId)
    {
        var years = AllTeachertimetable.Where(y => y.DeptId == int.Parse(deptId)).Select(y => y.ClassId).Distinct().ToList();

        var yearNames = classinfo
            .Where(c => c.DeptId == int.Parse(deptId) && !years.Contains(c.ClassId))
            .Select(c => new { c.ClassId, c.Class_Name })
            .ToList();
        return Json(yearNames);
    }





    //Time table generation algo..................



    public void GenerateTimetabl1(List<TimetableEntry> selectedSlots)
    {
        List<TimetableEntry> timetable = new List<TimetableEntry>();

        foreach (var sub in s1.Where(s => s.Subject_Name.ToString().EndsWith("L")))
        {
            AssignSubject(sub, subYearDepts, teacher, AllTeachertimetable, selectedSlots, timetable);
        }

        foreach (var sub in s1.Where(s => !s.Subject_Name.ToString().EndsWith("L")))
        {
            AssignSubject(sub, subYearDepts, teacher, AllTeachertimetable, selectedSlots, timetable);
        }

    }

    private static void AssignSubject(Subject subject, List<SubYearDept> subYearDepts, List<Teacher> teachers,
        List<TimetableEntry> allTeacherTimetable, List<TimetableEntry> selectedSlots, List<TimetableEntry> timetable)
    {
        var assignedClasses = subYearDepts.Where(syd => syd.SubjectId == subject.SubjectId).ToList();

        foreach (var classInfo in assignedClasses)
        {
            var availableTeachers = teachers.OrderByDescending(t => GetDesignationPriority(t.Designation)).ToList();

            foreach (var teacher in availableTeachers)
            {
                var preferredSlots = selectedSlots.Where(s => s.TeacherId == teacher.TeacherId).ToList();
                int hoursAssigned = 0;

                foreach (var slot in preferredSlots)
                {
                    if (hoursAssigned < subject.No_of_Hours_per_Week &&
                        IsSlotAvailable(teacher.TeacherId, slot.Day, slot.Timeslot, allTeacherTimetable, timetable))
                    {
                        timetable.Add(new TimetableEntry
                        {
                            TeacherId = teacher.TeacherId,
                            Day = slot.Day,
                            Timeslot = slot.Timeslot,
                            DeptId = classInfo.DeptId,
                            ClassId = classInfo.ClassId,
                            SubId = subject.SubjectId
                        });
                        hoursAssigned += subject.time_of_Lecture;

                        if (hoursAssigned >= subject.No_of_Hours_per_Week)
                            break;
                    }
                }
            }
        }
    }

    private static bool IsSlotAvailable(int teacherId, string day, int timeslot, List<TimetableEntry> allTeacherTimetable,
        List<TimetableEntry> timetable)
    {
        return !allTeacherTimetable.Any(t => t.TeacherId == teacherId && t.Day == day && t.Timeslot == timeslot)
            && !timetable.Any(t => t.TeacherId == teacherId && t.Day == day && t.Timeslot == timeslot);
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

    // ✅ Read All Departments
    public async Task<IActionResult> Index()
    {
        var departments = await _supabaseService.GetDepartments();
        ViewBag.Departments = departments;
        return View();
    }

    // ✅ Add New Department
    [HttpPost]
    public async Task<IActionResult> AddDepartment(string deptName)
    {
        var newDept = new DeptModel { DeptName = deptName };
        await _supabaseService.AddDepartment(newDept);
        return RedirectToAction("Index");
    }

    // ✅ Update Department
    [HttpPost]
    public async Task<IActionResult> UpdateDepartment(int deptId, string deptName)
    {
        var updatedDept = new DeptModel { DeptId = deptId, DeptName = deptName  };
        await _supabaseService.UpdateDepartment(updatedDept);
        return RedirectToAction("Index");
    }

    // ✅ Delete Department
    public async Task<IActionResult> DeleteDepartment(int deptId)
    {
        await _supabaseService.DeleteDepartment(deptId);
        return RedirectToAction("Index");
    }


    // ✅ ClassInfo CRUD
    public async Task<IActionResult> GetAllClasses()
    {
        var classes = await _supabaseService.GetClasses();
        return View(classes);
    }

    [HttpPost]
    public async Task<IActionResult> AddClass(string className, int noOfGroups, int deptId)
    {
        var newClass = new ClassInfoModel { ClassName = className, NoOfGroups = noOfGroups, DeptId = deptId };
        await _supabaseService.AddClass(newClass);
        return RedirectToAction("GetAllClasses");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateClass(int classId, string className, int noOfGroups, int deptId)
    {
        var updatedClass = new ClassInfoModel { ClassId = classId, ClassName = className, NoOfGroups = noOfGroups, DeptId = deptId };
        await _supabaseService.UpdateClass(updatedClass);
        return RedirectToAction("GetAllClasses");
    }

    public async Task<IActionResult> DeleteClass(int classId)
    {
        await _supabaseService.DeleteClass(classId);
        return RedirectToAction("GetAllClasses");
    }

    // ✅ Subject CRUD
    public async Task<IActionResult> GetAllSubjects()
    {
        var subjects = await _supabaseService.GetSubjects();
        return View(subjects);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubject(string subjectName, int noOfHoursPerWeek, int timeOfLecture)
    {
        var newSubject = new SubjectModel { SubjectName = subjectName, NoOfHoursPerWeek = noOfHoursPerWeek, TimeOfLecture = timeOfLecture };
        await _supabaseService.AddSubject(newSubject);
        return RedirectToAction("GetAllSubjects");
    }

    [HttpPost]
    //public async Task<IActionResult> UpdateSubject(int subjectId, string subjectName, int noOfHoursPerWeek, int timeOfLecture)
    //{
    //    var updatedSubject = new SubjectModel { SubjectId = subjectId, SubjectName = subjectName, NoOfHoursPerWeek = noOfHoursPerWeek, TimeOfLecture = timeOfLecture };
    //    await _supabaseService.UpdateSubject(updatedSubject);
    //    return RedirectToAction("GetAllSubjects");
    //}

    //public async Task<IActionResult> DeleteSubject(int subjectId)
    //{
    //    await _supabaseService.DeleteSubject(subjectId);
    //    return RedirectToAction("GetAllSubjects");
    //}

    // ✅ Teacher CRUD
    public async Task<IActionResult> GetAllTeachers()
    {
        var teachers = await _supabaseService.GetTeachers();
        return View(teachers);
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacher(string teacherName, string password, string designation)
    {
        var newTeacher = new TeacherModel { TeacherName = teacherName, Password = password, Designation = designation };
        await _supabaseService.AddTeacher(newTeacher);
        return RedirectToAction("GetAllTeachers");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTeacher(int teacherId, string teacherName, string password, string designation)
    {
        var updatedTeacher = new TeacherModel { TeacherId = teacherId, TeacherName = teacherName, Password = password, Designation = designation };
        await _supabaseService.UpdateTeacher(updatedTeacher);
        return RedirectToAction("GetAllTeachers");
    }

    public async Task<IActionResult> DeleteTeacher(int teacherId)
    {
        await _supabaseService.DeleteTeacher(teacherId);
        return RedirectToAction("GetAllTeachers");
    }

    // ✅ Timetable CRUD
    public async Task<IActionResult> GetAllTimetables()
    {
        var timetables = await _supabaseService.GetTimetable();
        return View(timetables);
    }

    //[HttpPost]
    //public async Task<IActionResult> AddTimetable(int teacherId, string day, int timeSlot, int deptId, int classId, int subId)
    //{
    //    var newTimetable = new TimetableModel { TeacherId = teacherId, Day = day, TimeSlot = timeSlot, DeptId = deptId, ClassId = classId, SubId = subId };
    //    await _supabaseService.AddTimetable(newTimetable);
    //    return RedirectToAction("GetAllTimetables");
    //}

    //[HttpPost]
    //public async Task<IActionResult> UpdateTimetable(int timetableId, int teacherId, string day, int timeSlot, int deptId, int classId, int subId)
    //{
    //    var updatedTimetable = new TimetableModel { TimetableId = timetableId, TeacherId = teacherId, Day = day, TimeSlot = timeSlot, DeptId = deptId, ClassId = classId, SubId = subId };
    //    await _supabaseService.UpdateTimetable(updatedTimetable);
    //    return RedirectToAction("GetAllTimetables");
    //}

    public async Task<IActionResult> DeleteTimetable(int timetableId)
    {
        await _supabaseService.DeleteTimetable(timetableId);
        return RedirectToAction("GetAllTimetables");
    }

        // ✅ SubYearDept CRUD
        public async Task<IActionResult> GetAllSubYearDepts()
        {
            var subYearDepts = await _supabaseService.GetSubYearDept();
            return View(subYearDepts);
        }

    //[HttpPost]
    //public async Task<IActionResult> AddSubYearDept(int subjectId, int classId, int deptId)
    //{
    //    var newSubYearDept = new SubYearDeptModel { SubjectId = subjectId, ClassId = classId, DeptId = deptId };
    //    await _supabaseService.AddSubYearDept(newSubYearDept);
    //    return RedirectToAction("GetAllSubYearDepts");
    //}

    //[HttpPost]
    //public async Task<IActionResult> UpdateSubYearDept(int subjectId, int classId, int deptId)
    //{
    //    var updatedSubYearDept = new SubYearDeptModel { SubjectId = subjectId, ClassId = classId, DeptId = deptId };
    //    await _supabaseService.UpdateSubYearDept(updatedSubYearDept);
    //    return RedirectToAction("GetAllSubYearDepts");
    //}

    //public async Task<IActionResult> DeleteSubYearDept(int subjectId, int classId, int deptId)
    //{
    //    await _supabaseService.DeleteSubYearDept(subjectId, classId, deptId);
    //    return RedirectToAction("GetAllSubYearDepts");
    //}
    public async Task<IActionResult> GetAllTeacherSubjectsSelectedByAdmin()
    {
        var list = await _supabaseService.GetTeacherSubjectsSelectedByAdmin();
        return View(list);
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacherSubjectsSelectedByAdmin(int teacherId, string subjectId, int deptId, int classId)
    {
        var model = new TeacherSubjectsSelectedByAdminModel { TeacherId = teacherId, SubjectId = subjectId, DeptId = deptId, ClassId = classId };
        await _supabaseService.AddTeacherSubjectsSelectedByAdmin(model);
        return RedirectToAction("GetAllTeacherSubjectsSelectedByAdmin");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTeacherSubjectsSelectedByAdmin(int teacherId, string subjectId, int deptId, int classId)
    {
        var model = new TeacherSubjectsSelectedByAdminModel { TeacherId = teacherId, SubjectId = subjectId, DeptId = deptId, ClassId = classId };
        await _supabaseService.UpdateTeacherSubjectsSelectedByAdmin(model);
        return RedirectToAction("GetAllTeacherSubjectsSelectedByAdmin");
    }

    public async Task<IActionResult> DeleteTeacherSubjectsSelectedByAdmin(int teacherId, string subjectId, int deptId, int classId)
    {
        await _supabaseService.DeleteTeacherSubjectsSelectedByAdmin(teacherId, subjectId, deptId, classId);
        return RedirectToAction("GetAllTeacherSubjectsSelectedByAdmin");
    }
    public async Task<IActionResult> GetAllTeacherSubjectInterests()
    {
        var list = await _supabaseService.GetTeacherSubjectInterests();
        return View(list);
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacherSubjectInterest(int teacherId, string subjectId)
    {
        var model = new TeacherSubjectInterestModel { TeacherId = teacherId, SubjectId = subjectId };
        await _supabaseService.AddTeacherSubjectInterest(model);
        return RedirectToAction("GetAllTeacherSubjectInterests");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTeacherSubjectInterest(int teacherId, string subjectId)
    {
        var model = new TeacherSubjectInterestModel { TeacherId = teacherId, SubjectId = subjectId };
        await _supabaseService.UpdateTeacherSubjectInterest(model);
        return RedirectToAction("GetAllTeacherSubjectInterests");
    }

    public async Task<IActionResult> DeleteTeacherSubjectInterest(int teacherId, string subjectId)
    {
        await _supabaseService.DeleteTeacherSubjectInterest(teacherId, subjectId);
        return RedirectToAction("GetAllTeacherSubjectInterests");
    }
    public async Task<IActionResult> GetAllRemainingSubjects()
    {
        var list = await _supabaseService.GetRemainingSubjectsList();
        return View(list);
    }

    [HttpPost]
    public async Task<IActionResult> AddRemainingSubject(string subjectId, int classId, int deptId)
    {
        var model = new RemainingSubjectsModel { SubjectId = subjectId, ClassId = classId, DeptId = deptId };
        await _supabaseService.AddRemainingSubject(model);
        return RedirectToAction("GetAllRemainingSubjects");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRemainingSubject(string subjectId, int classId, int deptId)
    {
        var model = new RemainingSubjectsModel { SubjectId = subjectId, ClassId = classId, DeptId = deptId };
        await _supabaseService.UpdateRemainingSubject(model);
        return RedirectToAction("GetAllRemainingSubjects");
    }

    public async Task<IActionResult> DeleteRemainingSubject2(string subjectId, int classId, int deptId)
    {
        await _supabaseService.DeleteRemainingSubject2(subjectId, classId, deptId);
        return RedirectToAction("GetAllRemainingSubjects");
    }


}














