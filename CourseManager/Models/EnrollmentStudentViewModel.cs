using CourseManager.Entities;

namespace CourseManager.Models
{
    // View model for the enrollment page
    public class EnrollmentStudentViewModel
    {
        public Student? Student { get; set; }

        public string? Response { get; set; }
    }
}
