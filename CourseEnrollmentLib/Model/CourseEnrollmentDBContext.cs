using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentLib
{
    public class CourseEnrollmentDBContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
        public CourseEnrollmentDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(p => p.CourseEnrolledTo)
                .WithMany(b => b.Student)
                .HasForeignKey(p => p.CourseId);
        }




    }
}
