using System;
using System.Collections.Generic;
using System.Text;

namespace CourseEnrollmentLib
{
    public class CourseOverallSummary: CourseDetails
    {
        public int StudentCount { get; internal set; }
        public int StudentCapacity { get; internal set; }
    }
}
