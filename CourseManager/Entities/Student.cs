using System.ComponentModel.DataAnnotations;

namespace CourseManager.Entities
{
    public class Student
    {
        // PK
        public int StudentId { get; set; }


        [Required(ErrorMessage = "Student requires a name")]
        [StringLength(15, ErrorMessage = "Name must be less than 15 characters")]
        [Display(Name = "Student Name")]
        public string? StudentName { get; set; }


        [Required(ErrorMessage = "Student requires and email")]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        [Display(Name = "Student Name")]
        public string? StudentEmail { get; set; }


        [Required(ErrorMessage = "Student requires a status")]
        [Display(Name = "Student Name")]
        public StudentStatus? Status { get; set; }


        // FK
        public int CourseId { get; set; }

        public Course? Course { get; set; }

    }
}
