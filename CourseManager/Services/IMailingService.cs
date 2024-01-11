namespace CourseManager.Services
{
    public interface IMailingService
    {
        public void SendEnrollmentEmailWithCourseId(int id, string scheme, string host);
    }
}
