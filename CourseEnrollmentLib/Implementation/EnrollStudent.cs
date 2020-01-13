using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace CourseEnrollmentLib
{
    public class EnrollStudent : IEnrollStudent<Student>
    {
        private CourseEnrollmentDBContext _dbContext;
        public EnrollStudent(CourseEnrollmentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Enroll student with the course mentioned in Student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<bool> EnrollStudentToCourse(Student student)
        {
            try
            {
                bool courseEnrolled = false;
                if (!string.IsNullOrEmpty(student.Name) && !string.IsNullOrEmpty(student.Email) && student.DOB != null)
                {
                    Student newStudent = new Student
                    {
                        Name = student.Name,
                        Email = student.Email,
                        DOB = student.DOB,
                        Age = student.DOB.CalculateAge() //storing age for report purpose
                    };

                    int enrolledStudentCount = _dbContext.Student.Where(s => s.CourseId == student.CourseId).Count();

                    var existingStudent = await _dbContext.Student.Where(s => s.Name == student.Name && s.Email== student.Email && s.DOB == student.DOB && s.CourseId == student.CourseId).FirstOrDefaultAsync();
                  
                    int getCourseMaxAllowedStudents = await _dbContext.Course.Where(c => c.CourseId == student.CourseId).Select(c => c.MaxAllowedStudent).FirstOrDefaultAsync();
                    if (enrolledStudentCount < getCourseMaxAllowedStudents)
                    {
                        newStudent.CourseId = student.CourseId;
                        courseEnrolled = true;
                        if(existingStudent!=null)
                        {
                            _dbContext.Student.Remove(existingStudent);
                        }
                        _dbContext.Student.Add(newStudent);
                        await _dbContext.SaveChangesAsync();
                    }
                    
                }
                return courseEnrolled;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
