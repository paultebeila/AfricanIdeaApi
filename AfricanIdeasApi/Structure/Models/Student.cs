namespace AfricanIdeasApi.Structure.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<CourseEnrollments> Enrollments { get; set; } = new();
    }
}
