using System;
using System.Collections.Generic;
using System.Text;

namespace CourseEnrollmentLib
{
    public class SpecificCourseDetails: CourseDetails
    {
        public string TeacherName { get; set; }

        public List<Student> RegisteredStudents { get; set; }
    }
}
