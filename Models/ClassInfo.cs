using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trySupa.Models
{
    public class Dept
    {
        [Key]
        public int DeptId { get; set; }

        public string Dept_Name {  get; set; }
    }
    public class ClassInfo
    {
        public int ClassId { get; set; }
        public string Class_Name { get; set; }

        public int No_of_groups { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public virtual Dept Department { get; set; }

    }

    public class Subject
    {
        [Key]
        public string SubjectId { get; set; }
        public string Subject_Name { get; set; }

        public int No_of_Hours_per_Week { get; set; }

        public int time_of_Lecture { get; set; }

    }

    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string Teacher_Name { get; set; }
        public string Password {  get; set; }  
        public string Designation {  get; set; }
    }

    public class SubYearDept
    {
        [Key, Column(Order = 1)]
        public string SubjectId { get; set; }

        [Key, Column(Order = 2)]
        public int ClassId { get; set; }

        [Key, Column(Order = 3)]
        public int DeptId { get; set; }

        // Navigation Properties
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        [ForeignKey("ClassId")]
        public ClassInfo Year { get; set; }

        [ForeignKey("DeptId")]
        public Dept Department { get; set; }
    }


    public class TeacherSubject
    {
        [Key, Column(Order = 1)]
        public int TeacherId { get; set; }

        [Key, Column(Order = 2)]
        public string SubjectId { get; set; }

        [Key, Column(Order = 3)]
        public int DeptId { get; set; }

        [Key, Column(Order = 4)]
        public int ClassId { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher Teacher { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; }

        [ForeignKey(nameof(DeptId))]
        public virtual Dept Department { get; set; }

        [ForeignKey(nameof(ClassId))]
        public virtual ClassInfo Year { get; set; }
    }


    public class TeachSubj
    {
        [Key, Column(Order = 1)]
        public int TeacherId { get; set; }

        [Key, Column(Order = 2)]
        public string SubjectId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
    }
}
