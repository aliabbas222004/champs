using Newtonsoft.Json;
using Supabase;
using Supabase.Postgrest;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SupabaseService
{
    private readonly Supabase.Client _supabase;

    public SupabaseService(string supabaseUrl, string supabaseKey)
    {
        _supabase = new Supabase.Client(supabaseUrl, supabaseKey, new SupabaseOptions());
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _supabase.InitializeAsync();
            Console.WriteLine("Supabase initialized successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Supabase: {ex.Message}");
        }
    }

    // ✅ CRUD Operations for Department Table
    public async Task<bool> AddDepartment(DeptModel dept)
    {
        try
        {
            await _supabase.From<DeptModel>().Insert(dept);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding department: {ex.Message}");
            return false;
        }
    }

    public async Task<List<DeptModel>> GetDepartments() =>
        (await _supabase.From<DeptModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateDepartment(DeptModel dept)
    {
        try
        {
            await _supabase.From<DeptModel>().Where(x => x.DeptId == dept.DeptId).Update(dept);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating department: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteDepartment(int deptId)
    {
        try
        {
            await _supabase.From<DeptModel>().Where(x => x.DeptId == deptId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting department: {ex.Message}");
            return false;
        }
    }

    // ✅ CRUD Operations for ClassInfo Table
    public async Task<bool> AddClass(ClassInfoModel model)
    {
        try
        {
            await _supabase.From<ClassInfoModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding class: {ex.Message}");
            return false;
        }
    }

    public async Task<List<ClassInfoModel>> GetClasses() =>
        (await _supabase.From<ClassInfoModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateClass(ClassInfoModel model)
    {
        try
        {
            await _supabase.From<ClassInfoModel>().Where(x => x.ClassId == model.ClassId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating class: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteClass(int classId)
    {
        try
        {
            await _supabase.From<ClassInfoModel>().Where(x => x.ClassId == classId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting class: {ex.Message}");
            return false;
        }
    }

    // ✅ CRUD Operations for Subject Table
    public async Task<bool> AddSubject(SubjectModel model)
    {
        try
        {
            await _supabase.From<SubjectModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding subject: {ex.Message}");
            return false;
        }
    }

    public async Task<List<SubjectModel>> GetSubjects() =>
        (await _supabase.From<SubjectModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateSubject(SubjectModel model)
    {
        try
        {
            await _supabase.From<SubjectModel>().Where(x => x.SubjectId == model.SubjectId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating subject: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteSubject(string subjectId)
    {
        try
        {
            await _supabase.From<SubjectModel>().Where(x => x.SubjectId == subjectId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting subject: {ex.Message}");
            return false;
        }
    }

    // ✅ CRUD Operations for Teacher Table
    public async Task<bool> AddTeacher(TeacherModel model)
    {
        try
        {
            await _supabase.From<TeacherModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding teacher: {ex.Message}");
            return false;
        }
    }

    public async Task<List<TeacherModel>> GetTeachers() =>
        (await _supabase.From<TeacherModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateTeacher(TeacherModel model)
    {
        try
        {
            await _supabase.From<TeacherModel>().Where(x => x.TeacherId == model.TeacherId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating teacher: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteTeacher(int teacherId)
    {
        try
        {
            await _supabase.From<TeacherModel>().Where(x => x.TeacherId == teacherId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting teacher: {ex.Message}");
            return false;
        }
    }

    // ✅ CRUD Operations for Timetable Table
    public async Task<List<TimetableModel>> GetTimetable() =>
    (await _supabase
        .From<TimetableModel>()
        .Select("*")
        .Get()).Models;







    public async Task<bool> AddTimetable(TimetableModel model)
    {
        try
        {
            await _supabase.From<TimetableModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding timetable: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateTimetable(TimetableModel model)
    {
        try
        {
            await _supabase.From<TimetableModel>().Where(x => x.TimetableId == model.TimetableId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating timetable: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteTimetable(int timetableId)
    {
        try
        {
            await _supabase.From<TimetableModel>().Where(x => x.TimetableId == timetableId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting timetable: {ex.Message}");
            return false;
        }
    }



    // ✅ SubYearDept CRUD
    public async Task<List<SubYearDeptModel>> GetSubYearDept()
    {
        try
        {
            var response = await _supabase.From<SubYearDeptModel>().Select("*").Get();
            return response.Models;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching SubYearDept: {ex.Message}");
            return new List<SubYearDeptModel>();
        }
    }

    public async Task<bool> AddSubYearDept(SubYearDeptModel model)
    {
        try
        {
            await _supabase.From<SubYearDeptModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding SubYearDept: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateSubYearDept(SubYearDeptModel model)
    {
        try
        {
            await _supabase.From<SubYearDeptModel>()
                .Where(x => x.SubjectId == model.SubjectId && x.ClassId == model.ClassId && x.DeptId == model.DeptId)
                .Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating SubYearDept: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteSubYearDept(string subjectId, int classId, int deptId)
    {
        try
        {
            await _supabase.From<SubYearDeptModel>()
                .Where(x => x.SubjectId == subjectId && x.ClassId == classId && x.DeptId == deptId)
                .Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting SubYearDept: {ex.Message}");
            return false;
        }
    }

    // ✅ RemainingSubjectsToChoose CRUD
    public async Task<List<RemainingSubjectsToChooseModel>> GetRemainingSubjects()
    {
        try
        {
            var response = await _supabase.From<RemainingSubjectsToChooseModel>().Select("*").Get();
            return response.Models;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching RemainingSubjectsToChoose: {ex.Message}");
            return new List<RemainingSubjectsToChooseModel>();
        }
    }

    public async Task<bool> AddRemainingSubject(RemainingSubjectsToChooseModel model)
    {
        try
        {
            await _supabase.From<RemainingSubjectsToChooseModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding RemainingSubjectsToChoose: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateRemainingSubject(RemainingSubjectsToChooseModel model)
    {
        try
        {
            await _supabase.From<RemainingSubjectsToChooseModel>()
                .Where(x => x.SubjectId == model.SubjectId && x.ClassId == model.ClassId && x.DeptId == model.DeptId)
                .Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating RemainingSubjectsToChoose: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteRemainingSubject(string subjectId, int classId, int deptId)
    {
        try
        {
            await _supabase.From<RemainingSubjectsToChooseModel>()
                .Where(x => x.SubjectId == subjectId && x.ClassId == classId && x.DeptId == deptId)
                .Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting RemainingSubjectsToChoose: {ex.Message}");
            return false;
        }
    }

}
