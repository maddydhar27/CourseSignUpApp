using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourseEnrollmentLib
{
    public interface IEnrollStudent<S>
    {
        /// <summary>
        /// Enroll student with the course mentioned in details of type S
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        Task<bool> EnrollStudentToCourse(CourseEnrollmentDBContext dBContext,S s);
      
    }
}
