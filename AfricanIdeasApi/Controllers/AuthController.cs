using AfricanIdeasApi.Structure.Data;
using AfricanIdeasApi.Structure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public IActionResult Register(Student student)
        {
            if (_db.Students.Any(s => s.Username == student.Username))
            {
                return BadRequest("Username already exists.");
            }

            _db.Students.Add(student);
            _db.SaveChanges();

            return Ok(new StudentDto { Id = student.Id, Username = student.Username });
        }

        [HttpPost("login")]
        public IActionResult Login(Student student)
        {
            var match = _db.Students.FirstOrDefault(s =>
                s.Username == student.Username && s.Password == student.Password);

            if (match == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(new StudentDto { Id = match.Id, Username = match.Username });
        }
    }
}
