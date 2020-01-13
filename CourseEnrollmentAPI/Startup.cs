using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureQueueLibrary.Infrastructure;
using CourseEnrollmentLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CourseEnrollmentAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Injected class ConfigurationReader to IConfigurationReader        
            string connString = Configuration.GetSection("MySettings").GetSection("Storage").Value;

            //var options = new DbContextOptionsBuilder<CourseEnrollmentDBContext>()
            //           .UseSqlServer(connString)
            //           .Options;

            //CourseEnrollmentDBContext dbContext = new CourseEnrollmentDBContext(options);

            services.AddDbContext<CourseEnrollmentDBContext>((options => options.UseSqlServer(connString)),ServiceLifetime.Transient);

            services.AddSingleton(_ => new EnrollmentDetails() as IEnrollmentDetails<CourseOverallSummary,SpecificCourseDetails>);

            services.AddSingleton(_ => new EnrollStudent() as IEnrollStudent<Student>);

            services.AddAzureQueueLibrary(Configuration.GetSection("MySettings").GetSection("AzureWebJobsStorage").Value);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Dynamic configuration structure APIs" });
            });



            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen();

            //Added CORS suppo
            services.AddCors(o => o.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            app.UseMvc();
        }
    }
}
