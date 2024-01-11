using CourseManager.Entities;

namespace CourseManager.Models
{
    // The course and student link
    public class ManageCourseViewModel
    {
        public Course? Course { get; set; }
        public Student? NewStudent { get; set; }

        public int ConfirmationMessageNotSentCount { get; set; }
        public int ConfirmationMessageSentCount { get; set; }
        public int EnrollmentConfirmedCount { get; set; }
        public int EnrollmentDeniedCount { get; set; }

    }
}
