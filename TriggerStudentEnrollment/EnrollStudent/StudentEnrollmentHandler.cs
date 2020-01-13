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
        private readonly CourseEnrollmentDBContext _dBContext;
        public StudentEnrollmentHandler(IEnrollStudent<Student> enrollStudent, CourseEnrollmentDBContext dBContext)
        {
            _enrollStudent = enrollStudent;
            _dBContext = dBContext;
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
                var result = await _enrollStudent.EnrollStudentToCourse(_dBContext,new Student
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
