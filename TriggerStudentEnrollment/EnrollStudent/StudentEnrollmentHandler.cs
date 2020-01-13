using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureQueueLibrary.Messages;
using CourseEnrollmentLib;
using TriggerStudentEnrollment.Interface;

namespace TriggerStudentEnrollment.EnrollStudent
{
    public class StudentEnrollmentHandler: IStudentEnrollmentHandler
    {

        private readonly IEnrollStudent<Student> _enrollStudent;

        public StudentEnrollmentHandler(IEnrollStudent<Student> enrollStudent)
        {
            _enrollStudent = enrollStudent;
        }

        /// <summary>
        /// Enroll student to course mentioned in details
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public async Task<bool> EnrollStudent(StudentEnrollmentDetails details)
        {

            if(details!=null && !string.IsNullOrEmpty(details.Name) && !string.IsNullOrEmpty(details.Email) && details.DOB!=null && details.CourseId >0)
            {
                var result = await _enrollStudent.EnrollStudentToCourse(new Student
                {
                    Name =details.Name,
                    Email=details.Email,
                    DOB=details.DOB,
                    CourseId=details.CourseId
                });
                return result;
            }
            else
            {
                return false;
            }


            
        }
    }
}
