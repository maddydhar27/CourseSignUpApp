using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourseEnrollmentLib
{
    public interface IEnrollmentDetails<O,S>
    {
        /// <summary>
        /// Get summary of all available courses 
        /// </summary>
        /// <returns></returns>
        Task<List<O>> GetCourseOverallSummary();

        /// <summary>
        /// Get details of course for given courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<S> GetSpecificCourseDetails(int courseId);
    }
}
