using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseEnrollmentLib
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string LecturerName { get; set; }
        public int MaxAllowedStudent { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
