using AzureQueueLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AzureQueueLibrary.Messages
{
	public class StudentEnrollmentDetails : BaseQueueMessage
	{
        [Required(ErrorMessage = "Student Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Student Email Address is required")]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Student Date of birth is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Course Id is required")]
        public int CourseId { get; set; }

        public StudentEnrollmentDetails() 
			: base(RouteNames.QueueName)
		{
		}
	}
}
