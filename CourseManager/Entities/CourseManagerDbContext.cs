using Microsoft.EntityFrameworkCore;

namespace CourseManager.Entities
{
    public class CourseManagerDbContext : DbContext
    {
        public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course() { CourseId = 1, Name = "See Sharp", Instructor = "David", StartDate = new DateTime(2023, 10, 20), RoomNumber = "4G15" },
                new Course() { CourseId = 2, Name = "Sequel", Instructor = "Ryan", StartDate = new DateTime(2023, 12, 31), RoomNumber = "2C09" },
                new Course() { CourseId = 3, Name = "GitHub", Instructor = "Owen", StartDate = new DateTime(2023, 11, 18), RoomNumber = "1G15" },
                new Course() { CourseId = 4, Name = "Web Dynamics", Instructor = "Liam", StartDate = new DateTime(2024, 01, 14), RoomNumber = "4G18" },
                new Course() { CourseId = 5, Name = "Game Programming", Instructor = "Eddy", StartDate = new DateTime(2024, 01, 15), RoomNumber = "3B11" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student() { StudentId = 1, StudentName = "John", StudentEmail = "gabed.siewert@hotmail.com", Status = StudentStatus.ConfirmationMessageNotSent, CourseId = 1},
                new Student() { StudentId = 2, StudentName = "Greg", StudentEmail = "gsiewert2384@conestogac.on.ca", Status = StudentStatus.ConfirmationMessageNotSent, CourseId = 1 },
                new Student() { StudentId = 3, StudentName = "Simon", StudentEmail = "gabesiewert@hotmail.com", Status = StudentStatus.ConfirmationMessageNotSent, CourseId = 2 },
                new Student() { StudentId = 4, StudentName = "Thomas", StudentEmail = "gabed.siewert@hotmail.com", Status = StudentStatus.ConfirmationMessageNotSent, CourseId = 2 },
                new Student() { StudentId = 5, StudentName = "Jason", StudentEmail = "gabed.siewert@hotmail.com", Status = StudentStatus.ConfirmationMessageNotSent, CourseId = 3 }
            );
        }
    }
}
