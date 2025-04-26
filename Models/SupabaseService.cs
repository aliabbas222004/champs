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



    // ✅ CRUD Operations for Dept Table
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

    public async Task<bool> DeleteClass(string classId)
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

    public async Task<List<TimetableModel>> GetTimetable() =>
        (await _supabase.From<TimetableModel>().Select("*").Get()).Models;

    

    

    // ✅ CRUD Operations for SubYearDept Table
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

    public async Task<List<SubYearDeptModel>> GetSubYearDept() =>
        (await _supabase.From<SubYearDeptModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateSubYearDept(SubYearDeptModel model)
    {
        try
        {
            await _supabase.From<SubYearDeptModel>().Where(x => x.SubjectId == model.SubjectId && x.ClassId == model.ClassId && x.DeptId == model.DeptId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating SubYearDept: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteSubYearDept(string subjectId, string classId, int deptId)
    {
        try
        {
            await _supabase.From<SubYearDeptModel>().Where(x => x.SubjectId == subjectId && x.ClassId == classId && x.DeptId == deptId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting SubYearDept: {ex.Message}");
            return false;
        }
    }





    // CRUD for RemainingSubjectsToChoose Table
    public async Task<bool> AddRemainingSubjectToChoose(RemainingSubjectsToChooseModel model)
    {
        try
        {
            await _supabase.From<RemainingSubjectsToChooseModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding remaining subject to choose: {ex.Message}");
            return false;
        }
    }

    public async Task<List<RemainingSubjectsToChooseModel>> GetRemainingSubjectsToChoose() =>
        (await _supabase.From<RemainingSubjectsToChooseModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateRemainingSubjectToChoose(RemainingSubjectsToChooseModel model)
    {
        try
        {
            await _supabase.From<RemainingSubjectsToChooseModel>().Where(x => x.SubjectId == model.SubjectId && x.ClassId == model.ClassId && x.DeptId == model.DeptId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating remaining subject to choose: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteRemainingSubjectToChoose(string subjectId, string classId, int deptId)
    {
        try
        {
            await _supabase.From<RemainingSubjectsToChooseModel>().Where(x => x.SubjectId == subjectId && x.ClassId == classId && x.DeptId == deptId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting remaining subject to choose: {ex.Message}");
            return false;
        }
    }

    // CRUD for TeacherSubjectsSelectedByAdmin Table
    public async Task<bool> AddTeacherSubjectByAdmin(TeacherSubjectsSelectedByAdminModel model)
    {
        try
        {
            await _supabase.From<TeacherSubjectsSelectedByAdminModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding teacher subject selected by admin: {ex.Message}");
            return false;
        }
    }

    public async Task<List<TeacherSubjectsSelectedByAdminModel>> GetTeacherSubjectsByAdmin() =>
        (await _supabase.From<TeacherSubjectsSelectedByAdminModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateTeacherSubjectByAdmin(TeacherSubjectsSelectedByAdminModel model)
    {
        try
        {
            await _supabase.From<TeacherSubjectsSelectedByAdminModel>().Where(x => x.TeacherId == model.TeacherId && x.SubjectId == model.SubjectId && x.DeptId == model.DeptId && x.ClassId == model.ClassId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating teacher subject selected by admin: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteTeacherSubjectByAdmin(int teacherId, string subjectId, int deptId, string classId)
    {
        try
        {
            await _supabase.From<TeacherSubjectsSelectedByAdminModel>().Where(x => x.TeacherId == teacherId && x.SubjectId == subjectId && x.DeptId == deptId && x.ClassId == classId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting teacher subject selected by admin: {ex.Message}");
            return false;
        }
    }

    // CRUD for TeacherSubjectInterest Table
    public async Task<bool> AddTeacherSubjectInterest(TeacherSubjectInterestModel model)
    {
        try
        {
            await _supabase.From<TeacherSubjectInterestModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding teacher subject interest: {ex.Message}");
            return false;
        }
    }

    public async Task<List<TeacherSubjectInterestModel>> GetTeacherSubjectInterest() =>
        (await _supabase.From<TeacherSubjectInterestModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateTeacherSubjectInterest(TeacherSubjectInterestModel model)
    {
        try
        {
            await _supabase.From<TeacherSubjectInterestModel>().Where(x => x.TeacherId == model.TeacherId && x.SubjectId == model.SubjectId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating teacher subject interest: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteTeacherSubjectInterest(int teacherId, string subjectId)
    {
        try
        {
            await _supabase.From<TeacherSubjectInterestModel>().Where(x => x.TeacherId == teacherId && x.SubjectId == subjectId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting teacher subject interest: {ex.Message}");
            return false;
        }
    }

    // CRUD for RemainingSubjects Table
    public async Task<bool> AddRemainingSubject(RemainingSubjectsModel model)
    {
        try
        {
            await _supabase.From<RemainingSubjectsModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding remaining subject: {ex.Message}");
            return false;
        }
    }

    public async Task<List<RemainingSubjectsModel>> GetRemainingSubjects() =>
        (await _supabase.From<RemainingSubjectsModel>().Select("*").Get()).Models;

    public async Task<bool> UpdateRemainingSubject(RemainingSubjectsModel model)
    {
        try
        {
            await _supabase.From<RemainingSubjectsModel>().Where(x => x.SubjectId == model.SubjectId && x.ClassId == model.ClassId && x.DeptId == model.DeptId).Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating remaining subject: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteRemainingSubject(string subjectId, string classId, int deptId)
    {
        try
        {
            await _supabase.From<RemainingSubjectsModel>().Where(x => x.SubjectId == subjectId && x.ClassId == classId && x.DeptId == deptId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting remaining subject: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteSelectedSlot(SelectedSlotModel model)
    {
        try
        {
            await _supabase.From<SelectedSlotModel>().Delete(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting selected slot: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateSelectedSlot(SelectedSlotModel model)
    {
        try
        {
            await _supabase.From<SelectedSlotModel>().Update(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating selected slot: {ex.Message}");
            return false;
        }
    }
    public async Task<List<SelectedSlotModel>> GetSelectedSlots() =>
        (await _supabase.From<SelectedSlotModel>().Select("*").Get()).Models;
    public async Task<bool> AddSelectedSlot(SelectedSlotModel model)
    {
        try
        {
            await _supabase.From<SelectedSlotModel>().Insert(model);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding selected slot: {ex.Message}");
            return false;
        }
    }
}
