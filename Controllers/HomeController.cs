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

    

    Dictionary<int, string> timeSlots = new Dictionary<int, string>
    {
        {1, "11:00am - 12:00pm"},
        {2, "12:00pm - 1:00pm"},
        {3, "1:00pm - 2:00pm"},
        {4, "2:30pm - 3:30pm"},
        {5, "3:30pm - 4:30pm"},
        {6, "4:30pm - 5:30pm"}
    };


    

    

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

                    timetable.Add(new TimetableModel
                    {
                        TeacherId = teacher,
                        Day = day,
                        TimeSlot = timeslot+1,
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

            var teacherSlots = selectedSlots .Where(s => s.TeacherId == teacher.TeacherId) .ToList();
            
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

    

    public async Task<IActionResult> Index()
    {
        var gD = await _supabaseService.GetTimetable();
        var generatedDepts = gD.Select(d => d.DeptId).Distinct();
        var d = await _supabaseService.GetDepartments();
        var departments=d.Where(t=> generatedDepts.Contains(t.DeptId));

        return View(departments);
    }

    [HttpGet]
    public async Task<IActionResult> GetClassesByDeptId(string deptId)
    {

        Console.WriteLine("Hello");
        var timtable = await _supabaseService.GetTimetable();
        var classes = await _supabaseService.GetClasses();
        var years = timtable.Where(y => y.DeptId == int.Parse(deptId)).Select(y => y.ClassId).Distinct().ToList();

        var yearNames = classes
            .Where(c => c.DeptId == int.Parse(deptId) && years.Contains(c.ClassId))
            .Select(c => new { c.ClassId, c.ClassName })
            .ToList();

        foreach(var item in years)
        {
            Console.WriteLine(item);
        }
        return Json(yearNames);

    }

    [HttpPost]
    public async Task<IActionResult> DisplayTimetable(string Department, string Class)
    {
        var timetable = await _supabaseService.GetTimetable();
        var depts = await _supabaseService.GetDepartments();
        var classes = await _supabaseService.GetClasses();
        var subjects = await _supabaseService.GetSubjects();
        var teachers = await _supabaseService.GetTeachers();
        var classId = classes.Where(c => c.ClassId == Class).Select(c => c.ClassId).FirstOrDefault();
        var deptId = depts.Where(d => d.DeptId == int.Parse(Department)).Select(d => d.DeptId).FirstOrDefault();
        var filteredTimetable = timetable
            .Where(t => t.DeptId == deptId && t.ClassId == classId)
            .OrderBy(t => t.Day)
            .ThenBy(t => t.TimeSlot)
            .ToList();

        var structuredTimetable = new Dictionary<string, List<string>>();

        foreach (var entry in filteredTimetable)
        {
            string key = $"{entry.TimeSlot}_{entry.Day}";
            string subjectName = subjects.FirstOrDefault(s => s.SubjectId == entry.SubId)?.SubjectName ?? "Unknown";
            string teacherName = teachers.FirstOrDefault(c => c.TeacherId == entry.TeacherId)?.TeacherName ?? "Unknown";

            if (!structuredTimetable.ContainsKey(key))
            {
                structuredTimetable[key] = new List<string>(); 
            }

            structuredTimetable[key].Add($"{subjectName}<br>{teacherName}");
        }

        ViewBag.TimeSlots = timeSlots;
        ViewBag.Timetable = structuredTimetable;

        return View();
    }


}




