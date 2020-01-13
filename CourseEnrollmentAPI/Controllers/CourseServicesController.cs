using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureQueueLibrary.Interfaces;
using AzureQueueLibrary.Messages;
using CourseEnrollmentLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;

namespace CourseEnrollmentAPI.Controllers
{
    public class CourseServicesController : Controller
    {
        private readonly IEnrollmentDetails<CourseOverallSummary,SpecificCourseDetails> _enrollmentDetails;
        private readonly IQueueCommunicator _queueCommunicator;

      
        public CourseServicesController(IEnrollmentDetails<CourseOverallSummary, SpecificCourseDetails> enrollmentDetails, IQueueCommunicator queueCommunicator)
        {
            _queueCommunicator = queueCommunicator;
            _enrollmentDetails = enrollmentDetails;
        }

        /// <summary>
        /// Enroll student to course mentioned in details
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EnrollStudentToCourse")]
        public async Task<IActionResult> EnrollStudentToCourse(StudentEnrollmentDetails details)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                        var message = string.Join(" \n ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage));                     
                   
                    return StatusCode(StatusCodes.Status400BadRequest, message);
                }
                else
                {
                    await _queueCommunicator.SendAsync(details);
                }
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }


        /// <summary>
        /// Get summary of all available courses 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCourseOverallSummary")]
        public async Task<IActionResult> GetCourseOverallSummary()
        {
            try
            {
                var result = await _enrollmentDetails.GetCourseOverallSummary();
                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, result);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get details of course for given courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSpecificCourseDetails")]
        public async Task<IActionResult> GetSpecificCourseDetails(int courseId)
        {
            try
            {
                var result = await _enrollmentDetails.GetSpecificCourseDetails(courseId);
                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, result);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
        }
    }
}