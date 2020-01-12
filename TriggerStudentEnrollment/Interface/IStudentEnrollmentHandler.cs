using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureQueueLibrary.Messages;

namespace TriggerStudentEnrollment.Interface
{
    public interface IStudentEnrollmentHandler
    {
        Task<bool> EnrollStudent(StudentEnrollmentDetails studentEnrollmentDetails);
    }
}
