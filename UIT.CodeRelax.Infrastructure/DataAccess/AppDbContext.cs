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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProblemPackage>()
                .HasKey(pp => new { pp.Package_Id, pp.Problem_id });

            modelBuilder.Entity<ProblemTag>()
                .HasKey(pt => new { pt.Problem_id, pt.Tag_id });

            modelBuilder.Entity<SubmissionResult>()
                .HasKey(sr => new { sr.SubmissionId, sr.PassedTestcaseId });

            modelBuilder.Entity<Rating>()
                .HasKey(r => new { r.User_Id, r.Problem_Id });
        }


    }
}
