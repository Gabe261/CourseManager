using System.ComponentModel.DataAnnotations;

namespace CourseManager.Entities
{
    public class Course
    {
        // PK
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course requires a name")]
        [StringLength(30, ErrorMessage = "Name must be less than 30 characters")]
        [Display(Name = "Course Name")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Course requires an instructor")]
        [StringLength(15, ErrorMessage = "Instructor name must be less than 15 characters")]
        [Display(Name = "Instructor")]
        public string? Instructor {  get; set; }


        [Required(ErrorMessage = "Course requires a start date")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }


        [Required(ErrorMessage = "Course requires a room number")]
        [RegularExpression("^[0-9][A-Z][0-9]{2}$", ErrorMessage = "Room number required in 1 digit, 1 capital letter and 2 digits")]
        [Display(Name = "Room Number")]
        public string? RoomNumber { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
