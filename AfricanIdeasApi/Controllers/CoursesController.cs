using AfricanIdeasApi.Structure.Data;
using AfricanIdeasApi.Structure.Models;
using CourseEnrollment.Structure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            return await _db.Courses
                .Select(c => new CourseDto { Id = c.Id, Name = c.Name })
                .ToListAsync();
        }

        [HttpGet("student/{studentId}")]
        public async Task<IEnumerable<CourseDto>> GetStudentCourses(int studentId)
        {
            return await _db.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => new CourseDto { Id = e.Course.Id, Name = e.Course.Name })
                .ToListAsync();
        }
        [HttpPost("Create Course")]
        public IActionResult CreateCourse(CourseCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Course name is required.");

            // Check if course name already exists
            if (_db.Courses.Any(c => c.Name == dto.Name))
                return BadRequest("Course already exists.");

            var course = new Course
            {
                Name = dto.Name
            };

            _db.Courses.Add(course);
            _db.SaveChanges();

            return Ok(new CourseDto { Id = course.Id, Name = course.Name });
        }

        [HttpPost("{courseId}/enroll/{studentId}")]
        public IActionResult Enroll(int courseId, int studentId)
        {
            if (_db.Enrollments.Any(e => e.CourseId == courseId && e.StudentId == studentId))
            {
                return BadRequest("Already enrolled.");
            }

            _db.Enrollments.Add(new CourseEnrollments
            {
                CourseId = courseId,
                StudentId = studentId
            });

            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{courseId}/unenroll/{studentId}")]
        public IActionResult Unenroll(int courseId, int studentId)
        {
            var enrollment = _db.Enrollments.Find(studentId, courseId);
            if (enrollment == null)
            {
                return NotFound();
            }

            _db.Enrollments.Remove(enrollment);
            _db.SaveChanges();

            return Ok();
        }
    }
}
