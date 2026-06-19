namespace StudentGrade.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int Grade { get; set; }
        public string Remarks { get; set; }
    }
}
