using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;

[Table("y")]
public class YModel : BaseModel
{
    [Column("id")]
    public long Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
}

[Table("dept")]
public class DeptModel : BaseModel
{
    [PrimaryKey("Dept_id")]
    public int DeptId { get; set; }

    [Column("Dept_Name")]
    public string DeptName { get; set; }
}

[Table("Class")]
public class ClassModel : BaseModel
{
    [PrimaryKey("Class_id")]
    public int ClassId { get; set; }

    [Column("Class_Name")]
    public string ClassName { get; set; }

    [Column("No_of_groups")]
    public int NoOfGroups { get; set; }

    [Reference(typeof(DeptModel), ReferenceAttribute.JoinType.Inner)]
    public int DeptId { get; set; }
}

[Table("Subject")]
public class SubjectModel : BaseModel
{
    [PrimaryKey("Sub_id")]
    public int SubId { get; set; }

    [Column("Sub_Name")]
    public string SubName { get; set; }

    [Column("No_of_hours_per_week")]
    public int NoOfHoursPerWeek { get; set; }

    [Column("Time_of_lecture")]
    public int TimeOfLecture { get; set; }
}

[Table("Teacher")]
public class TeacherModel : BaseModel
{
    [PrimaryKey("Teacher_id")]
    public int TeacherId { get; set; }

    [Column("Password")]
    public string Password { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Designation")]
    public string Designation { get; set; }
}

[Table("Occupied")]
public class OccupiedModel : BaseModel
{
    [PrimaryKey("Teacher_id, Day, TimeSlot")]
    public int TeacherId { get; set; }

    [Column("Day")]
    public string Day { get; set; }

    [Column("TimeSlot")]
    public int TimeSlot { get; set; }

    [Reference(typeof(DeptModel), ReferenceAttribute.JoinType.Inner)]
    public int DeptId { get; set; }

    [Reference(typeof(ClassModel), ReferenceAttribute.JoinType.Inner)]
    public int ClassId { get; set; }

    [Reference(typeof(SubjectModel), ReferenceAttribute.JoinType.Inner)]
    public int SubId { get; set; }
}

[Table("FreeSlots")]
public class FreeSlotsModel : BaseModel
{
    [PrimaryKey("Teacher_id, Day, TimeSlot")]
    public int TeacherId { get; set; }

    [Column("Day")]
    public string Day { get; set; }

    [Column("TimeSlot")]
    public int TimeSlot { get; set; }
}

[Table("Preferences")]
public class PreferencesModel : BaseModel
{
    [PrimaryKey("Teacher_id, Day, TimeSlot")]
    public int TeacherId { get; set; }

    [Column("Day")]
    public string Day { get; set; }

    [Column("TimeSlot")]
    public int TimeSlot { get; set; }

    [Reference(typeof(DeptModel), ReferenceAttribute.JoinType.Inner)]
    public int DeptId { get; set; }

    [Reference(typeof(ClassModel), ReferenceAttribute.JoinType.Inner)]
    public int ClassId { get; set; }

    [Reference(typeof(SubjectModel), ReferenceAttribute.JoinType.Inner)]
    public int SubId { get; set; }
}