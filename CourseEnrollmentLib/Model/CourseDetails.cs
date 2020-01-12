using System;
using System.Collections.Generic;
using System.Text;

namespace CourseEnrollmentLib
{
    public class CourseDetails
    {
        public int? CourseId { get; set; }

        public string CourseName { get; set; }
      
        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public double AvgAge { get; set; }
     
    }
}
