using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
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


    //List<Subject> s1 = new List<Subject>()
    //    {
    //        new Subject{SubjectId="301",Subject_Name="DLD",No_of_Hours_per_Week=5,time_of_Lecture=1},
    //        new Subject{SubjectId="302",Subject_Name="OS",No_of_Hours_per_Week=5,time_of_Lecture=1},
    //        new Subject{SubjectId="303",Subject_Name=".NET",No_of_Hours_per_Week=5,time_of_Lecture=1},
    //        new Subject{SubjectId="304",Subject_Name="CO",No_of_Hours_per_Week=5,time_of_Lecture=1},
    //        new Subject{SubjectId="305",Subject_Name="CN",No_of_Hours_per_Week=5,time_of_Lecture=1},
    //        new Subject{SubjectId="306",Subject_Name="SE",No_of_Hours_per_Week=5,time_of_Lecture=1},
    //        new Subject{SubjectId="307",Subject_Name="AM-III",No_of_Hours_per_Week=5,time_of_Lecture=1},
    //    };

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
            //foreach (var slot in model.SelectedSlots)
            //{

            //    Console.WriteLine($"Selected Slot: {slot.day} - {slot.slot}");
            //}

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

    //public class SlotSelectionModel
    //{
    //    public string TeacherId { get; set; }
    //    public List<SelectedSlot> SelectedSlots { get; set; }
    //}

    //public class SelectedSlot
    //{
    //    public string Day { get; set; }
    //    public int TimeSlot { get; set; }
    //}

    [HttpGet]
    public async Task<JsonResult> GetYears(string deptId)
    {
        Console.WriteLine(deptId);
        var remainingSubs = await _supabaseService.GetRemainingSubjects();
        var adminSelectedClasses = await _supabaseService.GetTeacherSubjectsByAdmin();
        var asC = adminSelectedClasses.Select(a => a.ClassId).Distinct();
        foreach (var item in asC)
        {
            Console.WriteLine(item);
        }
        var allClasses = await _supabaseService.GetClasses();
        var classes=allClasses.Where(c => !asC.Contains(c.ClassId)).ToList();
        foreach (var item in classes)
        {
            Console.WriteLine(item);
        }
        var years = remainingSubs.Where(y => y.DeptId == int.Parse(deptId)).Select(y => y.ClassId).ToList();
        var yearNames = classes.Where(y => years.Contains(y.ClassId)).Select(y => new { y.ClassId, y.ClassName }).ToList();
        foreach (var year in yearNames)
        {
            Console.WriteLine($"ClassId: {year.ClassId}, Class_Name: {year.ClassName}");
        }

        return Json(yearNames);
    }

    //[HttpGet]
    //public JsonResult GetSubjects(int deptId, int yearId)
    //{
    //    var subjects = remainingSubjects.Where(s => s.DeptId == deptId && s.ClassId == yearId)
    //                    .Select(s => s.SubjectId).ToList();
    //    var subNames = s1.Where(s => subjects.Contains(s.SubjectId)).Select(s => new { s.SubjectId, s.Subject_Name }).ToList();
    //    return Json(subNames);
    //}



    //[HttpPost]
    //public async Task<IActionResult> GenerateTimeTable(string Dept, string Year)
    //{

    //    var remainingSubjectsToSelect = remainingSubjects.Where(s => s.DeptId == int.Parse(Dept) && s.ClassId == int.Parse(Year)).Select(s => s.SubjectId);
    //    TempData["Success"] = false;
    //    if (remainingSubjectsToSelect.Count() == 0)
    //    {
    //        TempData["AlertMessage"] = "Timetable generated successfully!";
    //        TempData["Success"] = true;

    //    }
    //    else
    //    {
    //        var 
    //        var subinfo = s1.Where(s => remainingSubjectsToSelect.Contains(s.SubjectId)).Select(s => new { s.SubjectId, s.Subject_Name });
    //        var message = "";
    //        foreach (var item in subinfo)
    //        {
    //            message += " " + item.Subject_Name;
    //        }
    //        TempData["AlertMessage"] = "The subject -->" + message + "  are not selected by any teacher";
    //    }
    //    return RedirectToAction("AdminDashboard");
    //}


    [HttpGet]
    public async Task<JsonResult> GetYearsForRemainingDept(string deptId)
    {
        var timtable = await _supabaseService.GetTimetable();
        var allClasses = await _supabaseService.GetClasses();


        var adminSelectedClasses = await _supabaseService.GetTeacherSubjectsByAdmin();
        var asC = adminSelectedClasses.Select(a => a.ClassId).Distinct();
        foreach (var item in asC)
        {
            Console.WriteLine(item);
        }
        var classes = allClasses.Where(c => !asC.Contains(c.ClassId)).ToList();
        foreach (var item in classes)
        {
            Console.WriteLine(item);
        }



        var years = timtable.Where(y => y.DeptId == int.Parse(deptId)).Select(y => y.ClassId).Distinct().ToList();

        var yearNames = classes
            .Where(c => c.DeptId == int.Parse(deptId) && !years.Contains(c.ClassId))
            .Select(c => new { c.ClassId, c.ClassName })
            .ToList();
        return Json(yearNames);
    }

    [HttpGet]
    public async Task<JsonResult> GetYearsForRemainingDept1(string deptId)
    {
        Console.WriteLine("Hello");
        var timtable = await _supabaseService.GetTimetable();
        var classes = await _supabaseService.GetClasses();
        var years = timtable.Where(y => y.DeptId == int.Parse(deptId)).Select(y => y.ClassId).Distinct().ToList();

        var yearNames = classes
            .Where(c => c.DeptId == int.Parse(deptId) && !years.Contains(c.ClassId))
            .Select(c => new { c.ClassId, c.ClassName })
            .ToList();
        return Json(yearNames);
    }

    //Time table generation algo..................




    //private static void AssignSubject(Subject subject, List<SubYearDept> subYearDepts, List<Teacher> teachers,
    //    List<TimetableEntry> allTeacherTimetable, List<TimetableEntry> selectedSlots, List<TimetableEntry> timetable)
    //{
    //    var assignedClasses = subYearDepts.Where(syd => syd.SubjectId == subject.SubjectId).ToList();

    //    foreach (var classInfo in assignedClasses)
    //    {
    //        var availableTeachers = teachers.OrderByDescending(t => GetDesignationPriority(t.Designation)).ToList();

    //        foreach (var teacher in availableTeachers)
    //        {
    //            var preferredSlots = selectedSlots.Where(s => s.TeacherId == teacher.TeacherId).ToList();
    //            int hoursAssigned = 0;

    //            foreach (var slot in preferredSlots)
    //            {
    //                if (hoursAssigned < subject.No_of_Hours_per_Week &&
    //                    IsSlotAvailable(teacher.TeacherId, slot.Day, slot.Timeslot, allTeacherTimetable, timetable))
    //                {
    //                    timetable.Add(new TimetableEntry
    //                    {
    //                        TeacherId = teacher.TeacherId,
    //                        Day = slot.Day,
    //                        Timeslot = slot.Timeslot,
    //                        DeptId = classInfo.DeptId,
    //                        ClassId = classInfo.ClassId,
    //                        SubId = subject.SubjectId
    //                    });
    //                    hoursAssigned += subject.time_of_Lecture;

    //                    if (hoursAssigned >= subject.No_of_Hours_per_Week)
    //                        break;
    //                }
    //            }
    //        }
    //    }
    //}

    public async Task<List<TimetableModel>> GenerateTimetableAsync(int deptId, string classId)
    {

        var timetable = new List<TimetableModel>();

        var subYearDeptList = await _supabaseService.GetSubYearDept();
        var subjectIds = subYearDeptList
            .Where(syd => syd.DeptId == deptId && syd.ClassId == classId)
            .Select(syd => syd.SubjectId)
            .Distinct()
            .ToList();
        
        var nOG = await _supabaseService.GetClasses();
        var noOfGroups=nOG.Where(s=>s.DeptId==deptId && s.ClassId==classId).Select(s=>s.NoOfGroups).FirstOrDefault();
        
        var allSubjects = await _supabaseService.GetSubjects();
        var subjectDetails = allSubjects.Where(s => subjectIds.Contains(s.SubjectId));

        var adminAssignments = await _supabaseService.GetTeacherSubjectsByAdmin();
        var allTeachers = await _supabaseService.GetTeachers();
        var selectedTeacherIds = adminAssignments.Where(a=>!a.SubjectId.Contains("L")).Select(a => a.TeacherId).Distinct().ToList();
        var selectedTeachers = allTeachers.Where(t => selectedTeacherIds.Contains(t.TeacherId)).ToList();

        var selectedSlots = await _supabaseService.GetSelectedSlots(); // Returns List<TimetableEntry>


        var oS = await _supabaseService.GetTimetable();
        // This is for LAB ......................
        {
            var labSubjects = subjectDetails.Where(s => s.SubjectName.Contains("L"));
            var selectedLabTeacherId = adminAssignments.Where(s=>s.SubjectId.Contains("L")).Select(a => a.TeacherId).Distinct();
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

            foreach (var (day, timeslot) in labSlotCandidates)
            {
                bool isFreeForAll = selectedLabTeacherId.All(tid =>
                    !occupiedSlots.Any(o => o.TeacherId == tid && o.Day == day && o.TimeSlot == timeslot)
                );

                if (isFreeForAll)
                    availableLabSlots.Add((day, timeslot));

                if (availableLabSlots.Count == noOfGroups)
                    break;
            }

            foreach(var (day, timeslot) in availableLabSlots)
            {
                foreach (var teacher in selectedLabTeacherId)
                {
                    var sid = adminAssignments.Where(a => a.TeacherId == teacher).Select(a=>a.SubjectId).FirstOrDefault();
                    timetable.Add(new TimetableModel
                    {
                        TeacherId = teacher,
                        Day = day,
                        TimeSlot = timeslot,
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
            var assignedTeacherEntry = adminAssignments.FirstOrDefault(t =>
                t.SubjectId == subject.SubjectId && t.DeptId == deptId && t.ClassId == classId);

            var teacher = allTeachers.FirstOrDefault(t => t.TeacherId == assignedTeacherEntry.TeacherId);

            var teacherSlots = selectedSlots .Where(s => s.TeacherId == teacher.TeacherId) .ToList();

            int hoursAssigned = 0;
            int maxHours = subject.NoOfHoursPerWeek;

            foreach (var slot in teacherSlots.OrderBy(s => s.Day).ThenBy(s => s.TimeSlot))
            {
                if (hoursAssigned >= maxHours) break;
                bool isSlotTaken = timetable.Any(t => t.Day == slot.Day && t.TimeSlot == slot.TimeSlot);
                if (!isSlotTaken)
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
                            }
                        }
                    }
                }
            }
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

    

    // GET: Home/Index
    public async Task<IActionResult> Index()
    {
        // Get the list of departments from the database
        var departments = await _supabaseService.GetDepartments();

        // Pass the list of departments to the view
        return View(departments);
    }

    // POST: Home/AddDepartment
    [HttpPost]
    public async Task<IActionResult> AddDepartment(DeptModel department)
    {
        if (ModelState.IsValid)
        {
            bool success = await _supabaseService.AddDepartment(department);
            if (success)
            {

                var classModel = new ClassInfoModel
                {
                    DeptId = 3,
                    ClassId = "Class_"  , // Generate class ID
                    ClassName = "Class for "  ,
                    NoOfGroups = 3
                };
                bool classAdded = await _supabaseService.AddClass(classModel);
                if (classAdded) Console.WriteLine($"Class '{classModel.ClassName}' added successfully.");
                else Console.WriteLine("Failed to add class.");


                // Redirect back to the index view after successful addition
                return RedirectToAction("Index");
            }
            else
            {
                // Return an error message if adding the department failed
                ModelState.AddModelError(string.Empty, "An error occurred while adding the department.");
            }
        }

        return View("Index");
    }

    // POST: Home/UpdateDepartment
    [HttpPost]
    public async Task<IActionResult> UpdateDepartment(DeptModel department)
    {
        if (ModelState.IsValid)
        {
            bool success = await _supabaseService.UpdateDepartment(department);
            if (success)
            {
                // Redirect back to the index view after successful update
                return RedirectToAction("Index");
            }
            else
            {
                // Return an error message if updating the department failed
                ModelState.AddModelError(string.Empty, "An error occurred while updating the department.");
            }
        }

        return View("Index");
    }

    // POST: Home/DeleteDepartment
    [HttpPost]
    public async Task<IActionResult> DeleteDepartment(int deptId)
    {
        bool success = await _supabaseService.DeleteDepartment(deptId);
        if (success)
        {
            // Redirect back to the index view after successful deletion
            return RedirectToAction("Index");
        }
        else
        {
            // Return an error message if deleting the department failed
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the department.");
        }

        return View("Index");
    }

    // Add Class
    //[HttpPost]
    //public async Task<IActionResult> AddClass(ClassInfoModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var result = await _supabaseService.AddClass(model);
    //        return result ? RedirectToAction("Index") : View("Error");
    //    }
    //    return View("Error");
    //}

    //// Edit Class (GET)
    //[HttpGet]
    //public async Task<IActionResult> Edit(string id)
    //{
    //    var classes = await _supabaseService.GetClasses();
    //    var classToEdit = classes?.FirstOrDefault(c => c.ClassId == id);
    //    return classToEdit != null ? View(classToEdit) : View("Error");
    //}

    //// Update Class (POST)
    //[HttpPost]
    //public async Task<IActionResult> UpdateClass(ClassInfoModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var result = await _supabaseService.UpdateClass(model);
    //        return result ? RedirectToAction("Index") : View("Error");
    //    }
    //    return View("Error");
    //}

    //// Delete Class
    //[HttpPost]
    //public async Task<IActionResult> DeleteClass(string classId)
    //{
    //    if (!string.IsNullOrEmpty(classId))
    //    {
    //        var result = await _supabaseService.DeleteClass(classId);
    //        return result ? RedirectToAction("Index") : View("Error");
    //    }
    //    return View("Error");
    //}


}




