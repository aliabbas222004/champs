namespace trySupa.Models
{
    public class TimetableEntry
    {
        public int TeacherId { get; set; }
        public string Day { get; set; }
        public int Timeslot { get; set; }
        public int DeptId { get; set; }
        public int ClassId { get; set; }
        public string SubId { get; set; }
    }
}
