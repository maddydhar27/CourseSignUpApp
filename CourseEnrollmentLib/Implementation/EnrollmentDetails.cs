using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentLib
{
    public class EnrollmentDetails : IEnrollmentDetails<CourseOverallSummary,SpecificCourseDetails>
    {

        /// <summary>
        /// Get summary of all available courses 
        /// </summary>
        /// <returns></returns>
        public async Task<List<CourseOverallSummary>> GetCourseOverallSummary(CourseEnrollmentDBContext dBContext)
        {
            try
            {
                if(dBContext.Course==null || dBContext.Student == null)
                {
                    return null;
                }

                var courseSummaryList = await (from student in dBContext.Student
                                               where student.CourseId != null
                                               group student by student.CourseId into g //Pull out the unique indexes
                                               let f = g.FirstOrDefault()
                                               where f != null
                                               select new CourseOverallSummary
                                               {
                                                   CourseId = f.CourseId,
                                                   MinAge = g.Min(c => c.Age),
                                                   MaxAge = g.Max(c => c.Age),
                                                   AvgAge = g.Average(c => c.Age),
                                                   StudentCount = g.Count()
                                               }).ToListAsync();

               List<CourseOverallSummary> courseOverallsummary = new List<CourseOverallSummary>();
                foreach (var course in dBContext.Course)
                {
                    courseOverallsummary.Add(new CourseOverallSummary
                    {
                        CourseId=course.CourseId,
                        CourseName=course.CourseName,
                        StudentCapacity=course.MaxAllowedStudent,
                        StudentCount=courseSummaryList.Where(x=>x.CourseId==course.CourseId).Select(x=>x.StudentCount).FirstOrDefault(),
                        MinAge = courseSummaryList.Where(x => x.CourseId == course.CourseId).Select(x => x.MinAge).FirstOrDefault(),
                        MaxAge = courseSummaryList.Where(x => x.CourseId == course.CourseId).Select(x => x.MaxAge).FirstOrDefault(),
                        AvgAge = courseSummaryList.Where(x => x.CourseId == course.CourseId).Select(x => x.AvgAge).FirstOrDefault()

                    });
                }
                if (courseOverallsummary != null && courseOverallsummary.Count > 0)
                {
                    return courseOverallsummary;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get details of course for given courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<SpecificCourseDetails> GetSpecificCourseDetails(CourseEnrollmentDBContext dBContext,int courseId)
        {
            try
            {
                if (dBContext.Course == null || dBContext.Student == null)
                {
                    return null;
                }

                var ageSummaryData = await (from student in dBContext.Student
                                            where student.CourseId != null && student.CourseId == courseId
                                            group student by student.CourseId into g //Pull out the unique indexes
                                            let f = g.FirstOrDefault()
                                            where f != null
                                            select new SpecificCourseDetails
                                            {
                                                CourseId = f.CourseId,
                                                CourseName = dBContext.Course.Where(c => c.CourseId == f.CourseId).Select(c => c.CourseName).FirstOrDefault(),
                                                MinAge = g.Min(c => c.Age),
                                                MaxAge = g.Max(c => c.Age),
                                                AvgAge = g.Average(c => c.Age),
                                                TeacherName = dBContext.Course.Where(c => c.CourseId == f.CourseId).Select(c => c.LecturerName).FirstOrDefault(),
                                                RegisteredStudents = dBContext.Student.Where(c => c.CourseId == f.CourseId).ToList()
                                            }).FirstOrDefaultAsync();


                if (ageSummaryData != null)
                {
                    return ageSummaryData;
                }
                else
                {
                    var CourseDetails = await dBContext.Course.Where(c => c.CourseId == courseId).Select(x => new SpecificCourseDetails
                    {
                        CourseId =x.CourseId,
                        CourseName = x.CourseName,
                        TeacherName = x.LecturerName
                    }).FirstOrDefaultAsync();
                    return CourseDetails;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
