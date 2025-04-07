using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("dept")]
public class DeptModel : BaseModel
{
    [PrimaryKey("deptid")]
    public int? DeptId { get; set; }   


    [Column("dept_name")]
    public string DeptName { get; set; }
}
[Table("classinfo")]
public class ClassInfoModel : BaseModel
{
    [PrimaryKey("classid")]
    public int ClassId { get; set; }

    [Column("class_name")]
    public string ClassName { get; set; }

    [Column("no_of_groups")]
    public int NoOfGroups { get; set; }

    [Reference(typeof(DeptModel), ReferenceAttribute.JoinType.Inner)]
    [Column("deptid")]
    public int DeptId { get; set; }
}
[Table("subject")]
public class SubjectModel : BaseModel
{
    [PrimaryKey("subjectid")]
    [Column("subjectid")]
    public string SubjectId { get; set; }

    [Column("subject_name")]
    public string SubjectName { get; set; }

    [Column("no_of_hours_per_week")]
    public int NoOfHoursPerWeek { get; set; }

    [Column("time_of_lecture")]
    public int TimeOfLecture { get; set; }
}
[Table("teacher")]
public class TeacherModel : BaseModel
{
    [PrimaryKey("teacherid")]
    [Column("teacherid")]
    public int TeacherId { get; set; }

    [Column("teacher_name")]
    public string TeacherName { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("designation")]
    public string Designation { get; set; }
}
[Table("subyeardept")]
public class SubYearDeptModel : BaseModel
{
    [PrimaryKey("subjectid")]
    [Column("subjectid")]
    public string SubjectId { get; set; }

    [PrimaryKey("classid")]
    [Column("classid")]
    public int ClassId { get; set; }

    [PrimaryKey("deptid")]
    [Column("deptid")]
    public int DeptId { get; set; }
}
[Table("timetable")]
public class TimetableModel : BaseModel
{
    [PrimaryKey("timetableid")]
    public int TimetableId { get; set; }

    [Reference(typeof(TeacherModel), ReferenceAttribute.JoinType.Inner)]
    [Column("teacherid")]
    public int TeacherId { get; set; }

    [Column("day")]
    public string Day { get; set; }

    [Column("timeslot")]
    public int TimeSlot { get; set; }

    [Reference(typeof(DeptModel), ReferenceAttribute.JoinType.Inner)]
    [Column("deptid")]
    public int DeptId { get; set; }

    [Reference(typeof(ClassInfoModel), ReferenceAttribute.JoinType.Inner)]
    [Column("classid")]
    public int ClassId { get; set; }

    [Reference(typeof(SubjectModel), ReferenceAttribute.JoinType.Inner)]
    [Column("subid")]
    public string SubId { get; set; }
}
[Table("remainingsubjectstochoose")]
public class RemainingSubjectsToChooseModel : BaseModel
{
    [PrimaryKey("subjectid")]
    [Column("subjectid")]
    public string SubjectId { get; set; }  

    [PrimaryKey("classid")]
    [Column("classid")]
    public int ClassId { get; set; }

    [PrimaryKey("deptid")]
    [Column("deptid")]
    public int DeptId { get; set; }
}

[Table("teacher_subjects_selected_by_admin")]
public class TeacherSubjectsSelectedByAdminModel : BaseModel
{
    [PrimaryKey("teacher_id")]
    [Column("teacher_id")]
    public int TeacherId { get; set; }

    [PrimaryKey("subject_id")]
    [Column("subject_id")]
    public string SubjectId { get; set; }

    [PrimaryKey("dept_id")]
    [Column("dept_id")]
    public int DeptId { get; set; }

    [PrimaryKey("class_id")]
    [Column("class_id")]
    public int ClassId { get; set; }
}
[Table("teacher_subject_interest")]
public class TeacherSubjectInterestModel : BaseModel
{
    [PrimaryKey("teacher_id")]
    [Column("teacher_id")]
    public int TeacherId { get; set; }

    [PrimaryKey("subject_id")]
    [Column("subject_id")]
    public string SubjectId { get; set; }
}

[Table("remaining_subjects")]
public class RemainingSubjectsModel : BaseModel
{
    [PrimaryKey("subject_id")]
    [Column("subject_id")]
    public string SubjectId { get; set; }

    [PrimaryKey("class_id")]
    [Column("class_id")]
    public int ClassId { get; set; }

    [PrimaryKey("dept_id")]
    [Column("dept_id")]
    public int DeptId { get; set; }
}
