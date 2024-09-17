﻿using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Models;

namespace PerformanceSurvey.Context
{
    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Department> departments { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<QuestionOption> question_Option { get; set; }
        public DbSet<AssignmentQuestion> assignment_Question { get; set; }
        public DbSet<Response> responses { get; set; }
        public DbSet<Token> Tokens { get; set; } // Add this line
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure UserId is configured correctly if needed
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd(); // Ensure auto-increment is set if it's an identity column

            // Other configurations if necessary

            base.OnModelCreating(modelBuilder);
        }
    }

}
