using System.Net.Mail;
using System.Net;
using CourseManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Services
{
    public class MailingService : IMailingService
    {
        private readonly IConfiguration _configuration;
        private readonly CourseManagerDbContext _CourseDbContext;
        public MailingService(CourseManagerDbContext courseManagerDbContext, IConfiguration configuration)
        {
            _CourseDbContext = courseManagerDbContext;
            _configuration = configuration;
        }

        public void SendEnrollmentEmailWithCourseId(int id, string scheme, string host)
        {

            var course = _CourseDbContext.Courses.Include(c => c.Students).Where(c => c.CourseId == id).FirstOrDefault();
            if (course == null)
            {
                return;
            }
            else
            {
                // Get list of students with the not sent emails
                var students = course.Students
                    .Where(student => student.Status == StudentStatus.ConfirmationMessageNotSent)
                    .ToList();
                try
                {
                    // Pull from app settings
                    var smtpHost = _configuration["SmtpSetting:Host"];
                    var smtpPort = _configuration["SmtpSetting:Port"];
                    var fromAddress = _configuration["SmtpSetting:FromAddress"];
                    var fromPassword = _configuration["SmtpSetting:FromPassword"];

                    using var smtpClient = new SmtpClient(smtpHost);
                    smtpClient.Port = string.IsNullOrWhiteSpace(smtpPort) ? 587 : Convert.ToInt32(smtpPort);
                    smtpClient.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtpClient.EnableSsl = true;
                    // Create the email for all the students in the list
                    foreach (var student in students)
                    {
                        var responseUrl = $"{scheme}://{host}/courses/{id}/enroll/{student.StudentId}";
                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress(fromAddress),
                            Subject = $"[Action Required] Confirm \"{student?.Course?.Name}\" Enrollment",
                            Body = EmailBody(student, responseUrl),
                            IsBodyHtml = true
                        };

                        if (student.StudentEmail == null)
                        {
                            return;
                        }
                        // Send created email to all students on list
                        mailMessage.To.Add(student.StudentEmail);
                        smtpClient.Send(mailMessage);

                        student.Status = StudentStatus.ConfirmationMessageSent;
                    }
                    _CourseDbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        /// <summary>
        /// Creates the body of the email
        /// </summary>
        /// <param name="student"> Student in student list </param>
        /// <param name="enrollViewURL"> The url to the enrollment view with the students ID </param>
        /// <returns></returns>
        private static string EmailBody(Student student, string enrollViewURL)
        {
            return $@"<h1>Hello {student.StudentName}:</h1>
            <p>
                Your request to enroll in the course '{student.Course?.Name}'
                in room {student.Course?.RoomNumber}
                starting on {student.Course?.StartDate:d}
                with instructor {student.Course?.Instructor}
            </p>
            <p>
                We would be pleased to hear from you. <a href={enrollViewURL}>Click here to confirm your enrollment</a>
            </p>

            <p>Sincerely,</p>
            <p>The Course Manager App</p>";
        }
    }
}
