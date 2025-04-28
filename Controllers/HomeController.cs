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




