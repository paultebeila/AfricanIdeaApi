using System.Text.Json.Serialization;

namespace AfricanIdeasApi.Structure.Models
{
    public class Student
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public List<CourseEnrollments> Enrollments { get; set; } = new();
    }
}
