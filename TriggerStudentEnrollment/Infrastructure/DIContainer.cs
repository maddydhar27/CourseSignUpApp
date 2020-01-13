using AzureQueueLibrary.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CourseEnrollmentLib;
using Microsoft.EntityFrameworkCore;
using TriggerStudentEnrollment.Interface;
using TriggerStudentEnrollment.EnrollStudent;

namespace AzureFunctions.Infrastructure
{
	public sealed class DIContainer
	{
		private static readonly IServiceProvider _instance = Build();
		public static IServiceProvider Instance => _instance;


        static DIContainer()
        { }

        private DIContainer()
        { }

        private static IServiceProvider Build()
		{
			var services = new ServiceCollection();
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

            string connString = configuration["Storage"];

            //var options = new DbContextOptionsBuilder<CourseEnrollmentDBContext>()
            //           .UseSqlServer(connString)
            //           .Options;

            //CourseEnrollmentDBContext dbContext = new CourseEnrollmentDBContext(options);
            services.AddDbContext<CourseEnrollmentDBContext>((options => options.UseSqlServer(connString)), ServiceLifetime.Transient);
            services.AddSingleton(_ => new EnrollStudent() as IEnrollStudent<Student>);
            services.AddSingleton<IStudentEnrollmentHandler, StudentEnrollmentHandler>();
            services.AddAzureQueueLibrary(configuration["AzureWebJobsStorage"]);
			return services.BuildServiceProvider();
		}

	}
}
