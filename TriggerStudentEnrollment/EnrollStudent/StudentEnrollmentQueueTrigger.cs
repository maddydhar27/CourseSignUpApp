using System;
using System.Threading.Tasks;
using AzureFunctions.Infrastructure;
using AzureQueueLibrary.Infrastructure;
using AzureQueueLibrary.QueueConnection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AzureQueueLibrary.Messages;
using AzureQueueLibrary.Interfaces;
using TriggerStudentEnrollment.Interface;

namespace AzureFunctions.Email
{
    public static class StudentEnrollmentQueueTrigger
    {
        [FunctionName("EmailQueueTrigger")]
        public static async Task Run(
			[QueueTrigger(RouteNames.QueueName, Connection = "AzureWebJobsStorage")]
			string message,
			ILogger log)
        {
			try
			{
				var queueCommunicator = DIContainer.Instance.GetService<IQueueCommunicator>();
                //Read message from Azure storage and reserialze to StudentEnrollmentDetails
                var studentDetails = queueCommunicator.Read<StudentEnrollmentDetails>(message);
                if (studentDetails != null)
                {
                    var handler = DIContainer.Instance.GetService<IStudentEnrollmentHandler>();
                    var courseEnrolled = await handler.EnrollStudent(studentDetails);
                    if(courseEnrolled==true)
                    {
                        //Add functionality to send success mail to Student
                        log.LogInformation("Student" + studentDetails.Name + "enrolled successfully");
                    }
                    if (courseEnrolled == false)
                    {
                        //Add functionality to send enrollment failure mail to Student
                        log.LogInformation("Student" + studentDetails.Name + "was not enrolled");
                    }
                }

            }
			catch (Exception ex)
			{
				log.LogError(ex, $"Something went wrong with the EmailQueueTrigger {message}");
				throw;
			}
        }
    }
}
