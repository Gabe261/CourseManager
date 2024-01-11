using CourseManager.Entities;
using CourseManager.Models;
using CourseManager.Services;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController(CourseManagerDbContext courseManagerDbContext, IMailingService mailingService)
        {
            _mailingService = mailingService;
            _courseManagerDbContext = courseManagerDbContext;
        }

        // List of all courses
        [HttpGet("/courses")]
        public IActionResult GetAllCourses()
        {
            CookiesMsgUpdate();
            var courses = _courseManagerDbContext.Courses.Include(c => c.Students).ToList();
            return View("Items", courses);
        }

        // Get manage course page
        [HttpGet("/courses/{id}")]
        public IActionResult GetCourseById(int id)
        {
            CookiesMsgUpdate();
            Course? course = _courseManagerDbContext.Courses
                .Include(c => c.Students)
                .Where(p => p.CourseId == id)
                .FirstOrDefault();

            ManageCourseViewModel manageCourseViewModel = new ManageCourseViewModel()
            {
                Course = course,
                NewStudent = new Student(),
                ConfirmationMessageNotSentCount = course.Students.Count(s => s.Status == StudentStatus.ConfirmationMessageNotSent),
                ConfirmationMessageSentCount = course.Students.Count(s => s.Status == StudentStatus.ConfirmationMessageSent),
                EnrollmentConfirmedCount = course.Students.Count(s => s.Status == StudentStatus.EnrollmentConfirmed),
                EnrollmentDeniedCount = course.Students.Count(s => s.Status == StudentStatus.EnrollmentDenied)
            };
            return View("Item", manageCourseViewModel);
        }

        // Add form
        [HttpGet("/courses/add-course")]
        public IActionResult GetAddCourseForm()
        {
            CookiesMsgUpdate();
            return View("Add", new Course());
        }

        // Post from add from
        [HttpPost("/courses")]
        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseManagerDbContext.Courses.Add(course);
                _courseManagerDbContext.SaveChanges();

                return RedirectToAction("GetAllCourses");
            }
            else
            {
                return View("Add", course);
            }
        }

        // Edit form
        [HttpGet("/courses/{id}/edit-course")]
        public IActionResult GetEditCourseFormById(int id)
        {
            CookiesMsgUpdate();
            Course course = _courseManagerDbContext.Courses.Find(id);

            return View("Edit", course);
        }

        // Post from edit form
        [HttpPost("/courses/edit-requests")]
        public IActionResult ProcessEditRequest(Course course)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("here");
                _courseManagerDbContext.Courses.Update(course);
                _courseManagerDbContext.SaveChanges();

                return RedirectToAction("GetAllCourses", "Course");
            }
            else
            {
                return View("Edit", course);
            }
        }

        // Post from manage course to add a student to that course
        [HttpPost("/courses/{id}/students")]
        public IActionResult AddStudentToCourseById(int id, ManageCourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Course? course = _courseManagerDbContext.Courses
                .Include(c => c.Students)
                .Where(c => c.CourseId == id)
                .FirstOrDefault();

                viewModel.NewStudent.Status = StudentStatus.ConfirmationMessageNotSent;

                viewModel.NewStudent.CourseId = id;

                Student student = viewModel.NewStudent;

                course.Students.Add(student);

                _courseManagerDbContext.SaveChanges();

                return RedirectToAction("GetCourseById", "Course", new { id = id });
            }
            else
            {
                Course? course = _courseManagerDbContext.Courses
                .Include(c => c.Students)
                .Where(p => p.CourseId == id)
                .FirstOrDefault();

                ManageCourseViewModel manageCourseViewModel = new ManageCourseViewModel()
                {
                    Course = course,
                    NewStudent = new Student(),
                    ConfirmationMessageNotSentCount = course.Students.Count(s => s.Status == StudentStatus.ConfirmationMessageNotSent),
                    ConfirmationMessageSentCount = course.Students.Count(s => s.Status == StudentStatus.ConfirmationMessageSent),
                    EnrollmentConfirmedCount = course.Students.Count(s => s.Status == StudentStatus.EnrollmentConfirmed),
                    EnrollmentDeniedCount = course.Students.Count(s => s.Status == StudentStatus.EnrollmentDenied)
                };
                return View("Item", manageCourseViewModel);
            }
        }

        // Sends all emails to students with status ConfrimationMessageNotSent
        [HttpPost("/courses/send-email/{id}")]
        public IActionResult SendEmail(int id, ManageCourseViewModel viewModel)
        {
            _mailingService.SendEnrollmentEmailWithCourseId(id, Request.Scheme, Request.Host.ToString());

            return RedirectToAction("GetCourseById", new { id = id });
        }

        // Get the Enroll form 
        [HttpGet("/courses/{courseId:int}/enroll/{studentId:int}")]
        public IActionResult Enroll(int courseId, int studentId)
        {
            CookiesMsgUpdate();
            var student = _courseManagerDbContext.Students
                .Include(c => c.Course)
                .FirstOrDefault(e => e.CourseId == courseId && e.StudentId == studentId);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                var enrollmentStudentVM = new EnrollmentStudentViewModel()
                {
                    Student = student,
                };
                return View(enrollmentStudentVM);
            }
        }

        // Post from enroll form
        [HttpPost("/courses/{courseId:int}/enroll/{studentId:int}")]
        public IActionResult Enroll(int courseId, int studentId, EnrollmentStudentViewModel enrollStudentViewModel)
        {
            CookiesMsgUpdate();
            var student = _courseManagerDbContext.Students.Include(c => c.Course).FirstOrDefault(e => e.CourseId == courseId && e.StudentId == studentId);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    StudentStatus status = enrollStudentViewModel.Response == "Yes" ? StudentStatus.EnrollmentConfirmed : StudentStatus.EnrollmentDenied;
                    student.Status = status;
                    _courseManagerDbContext.SaveChanges();
                    return RedirectToAction("ThankYou", new { response = enrollStudentViewModel.Response });
                }
                else
                {
                    enrollStudentViewModel.Student = student;
                    return View(enrollStudentViewModel);
                }
            }
        }

        // Thank you after enroll form
        [HttpGet("/thank-you/{response}")]
        public IActionResult ThankYou(string response)
        {
            CookiesMsgUpdate();

            return View("Thankyou", response);
        }

        CourseManagerDbContext _courseManagerDbContext;
        IMailingService _mailingService;
    }
}
