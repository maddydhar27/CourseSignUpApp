using System;
using System.Collections.Generic;
using System.Text;

namespace CourseEnrollmentLib
{
    static public class CommonFunction
    {
        /// <summary>
        /// Extension method to Calculate Age for given DOB
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
