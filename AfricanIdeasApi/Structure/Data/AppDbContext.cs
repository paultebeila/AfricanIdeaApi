using Microsoft.EntityFrameworkCore;
using AfricanIdeasApi.Structure.Models;

namespace AfricanIdeasApi.Structure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseEnrollments> Enrollments => Set<CourseEnrollments>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseEnrollments>()
                .HasKey(x => new { x.StudentId, x.CourseId });
        }
    }
}
