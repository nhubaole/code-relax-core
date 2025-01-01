using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.Infrastructure.DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Testcase> Testcases { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<ProblemPackage> ProblemPackages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProblemTag> ProblemTags { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SubmissionResult> SubmissionResults { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProblemPackage>()
                .HasKey(pp => new { pp.PackageId, pp.ProblemId });

            modelBuilder.Entity<ProblemTag>()
                .HasKey(pt => new { pt.ProblemId, pt.TagId });

            modelBuilder.Entity<SubmissionResult>()
                .HasKey(sr => new { sr.SubmissionId, sr.PassedTestcaseId });

            modelBuilder.Entity<Rating>()
                .HasKey(r => new { r.UserID, r.ProblemID });
        

            //notify that these properties is jsonB
            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.SubTitle)
                      .HasColumnType("jsonb");
                entity.Property(e => e.Content)
                      .HasColumnType("jsonb");
            });
    

        }
    }
}
