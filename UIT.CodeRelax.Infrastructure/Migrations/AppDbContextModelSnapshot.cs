﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UIT.CodeRelax.Infrastructure.DataAccess;

#nullable disable

namespace UIT.CodeRelax.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cover");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("subtitle");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("summary");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("articles");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Discussion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("ProblemID")
                        .HasColumnType("integer")
                        .HasColumnName("problem_id");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserID")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ID");

                    b.HasIndex("ProblemID");

                    b.HasIndex("UserID");

                    b.ToTable("discussions");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("packages");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer")
                        .HasColumnName("difficulty");

                    b.Property<string>("Explaination")
                        .HasColumnType("text")
                        .HasColumnName("explaination");

                    b.Property<string>("FunctionName")
                        .HasColumnType("text")
                        .HasColumnName("function_name");

                    b.Property<int>("NumOfAcceptance")
                        .HasColumnType("integer")
                        .HasColumnName("num_of_acceptance");

                    b.Property<int>("NumOfSubmission")
                        .HasColumnType("integer")
                        .HasColumnName("num_of_submission");

                    b.Property<string>("ReturnType")
                        .HasColumnType("text")
                        .HasColumnName("return_type");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("problems");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.ProblemPackage", b =>
                {
                    b.Property<int>("PackageId")
                        .HasColumnType("integer")
                        .HasColumnName("package_id");

                    b.Property<int>("ProblemId")
                        .HasColumnType("integer")
                        .HasColumnName("problem_id");

                    b.HasKey("PackageId", "ProblemId");

                    b.HasIndex("ProblemId");

                    b.ToTable("problem_packages");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.ProblemTag", b =>
                {
                    b.Property<int>("ProblemId")
                        .HasColumnType("integer")
                        .HasColumnName("problem_id");

                    b.Property<int>("TagId")
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    b.HasKey("ProblemId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("problem_tags");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Article_id")
                        .HasColumnType("integer")
                        .HasColumnName("article_id");

                    b.Property<string>("CorrectOption")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("correct_option");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Explanation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("explanation");

                    b.Property<string>("OptionA")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("option_a");

                    b.Property<string>("OptionB")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("option_b");

                    b.Property<string>("OptionC")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("option_c");

                    b.Property<string>("OptionD")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("option_d");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("question_text");

                    b.Property<int?>("articleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("articleId");

                    b.ToTable("quizzes");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Rating", b =>
                {
                    b.Property<int>("User_Id")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("Problem_Id")
                        .HasColumnType("integer")
                        .HasColumnName("problem_id");

                    b.Property<int>("NumberOfStar")
                        .HasColumnType("integer");

                    b.HasKey("User_Id", "Problem_Id");

                    b.HasIndex("Problem_Id");

                    b.ToTable("ratings");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("language");

                    b.Property<int>("ProblemId")
                        .HasColumnType("integer")
                        .HasColumnName("problem_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.HasIndex("UserId");

                    b.ToTable("submissions");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.SubmissionResult", b =>
                {
                    b.Property<int>("SubmissionId")
                        .HasColumnType("integer")
                        .HasColumnName("submission_id");

                    b.Property<int>("PassedTestcaseId")
                        .HasColumnType("integer")
                        .HasColumnName("passed_testcase_id");

                    b.HasKey("SubmissionId", "PassedTestcaseId");

                    b.HasIndex("PassedTestcaseId");

                    b.ToTable("submission_results");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Testcase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("input");

                    b.Property<bool>("IsExample")
                        .HasColumnType("boolean")
                        .HasColumnName("is_example");

                    b.Property<string>("Output")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("output");

                    b.Property<int>("ProblemId")
                        .HasColumnType("integer")
                        .HasColumnName("problem_id");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.ToTable("testcases");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Article", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.User", "User")
                        .WithMany("articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Discussion", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Problem", "Problem")
                        .WithMany("Discussions")
                        .HasForeignKey("ProblemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UIT.CodeRelax.Core.Entities.User", "User")
                        .WithMany("Discussions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.ProblemPackage", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Package", "Package")
                        .WithMany("ProblemPackages")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UIT.CodeRelax.Core.Entities.Problem", "Problem")
                        .WithMany("ProblemPackages")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.ProblemTag", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Problem", "Problem")
                        .WithMany("ProblemTags")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UIT.CodeRelax.Core.Entities.Tag", "Tag")
                        .WithMany("ProblemTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Quiz", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Article", "article")
                        .WithMany()
                        .HasForeignKey("articleId");

                    b.Navigation("article");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Rating", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Problem", "Problem")
                        .WithMany("ratings")
                        .HasForeignKey("Problem_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UIT.CodeRelax.Core.Entities.User", "User")
                        .WithMany("ratings")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Submission", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Problem", "Problem")
                        .WithMany("Submissions")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UIT.CodeRelax.Core.Entities.User", "User")
                        .WithMany("Submissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.SubmissionResult", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Testcase", "Testcase")
                        .WithMany("SubmissionResults")
                        .HasForeignKey("PassedTestcaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UIT.CodeRelax.Core.Entities.Submission", "Submission")
                        .WithMany("SubmissionResults")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");

                    b.Navigation("Testcase");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Testcase", b =>
                {
                    b.HasOne("UIT.CodeRelax.Core.Entities.Problem", "Problem")
                        .WithMany("Testcases")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Package", b =>
                {
                    b.Navigation("ProblemPackages");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Problem", b =>
                {
                    b.Navigation("Discussions");

                    b.Navigation("ProblemPackages");

                    b.Navigation("ProblemTags");

                    b.Navigation("Submissions");

                    b.Navigation("Testcases");

                    b.Navigation("ratings");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Submission", b =>
                {
                    b.Navigation("SubmissionResults");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Tag", b =>
                {
                    b.Navigation("ProblemTags");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.Testcase", b =>
                {
                    b.Navigation("SubmissionResults");
                });

            modelBuilder.Entity("UIT.CodeRelax.Core.Entities.User", b =>
                {
                    b.Navigation("Discussions");

                    b.Navigation("Submissions");

                    b.Navigation("articles");

                    b.Navigation("ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
