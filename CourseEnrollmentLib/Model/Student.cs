using System;
using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentLib
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public int? CourseId { get; set; }
        public Course CourseEnrolledTo { get; set; }
    }
}
