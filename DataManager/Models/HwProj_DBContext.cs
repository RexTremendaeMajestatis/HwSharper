﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataManager.Models
{
    public partial class HwProj_DBContext : DbContext
    {
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CurrentHomework> CurrentHomework { get; set; }
        public virtual DbSet<CurrentTest> CurrentTest { get; set; }
        public virtual DbSet<Hometask> Hometask { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<HomeworkSolution> HomeworkSolution { get; set; }
        public virtual DbSet<Lecture> Lecture { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<OngoingCourse> OngoingCourse { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestSolution> TestSolution { get; set; }
        public virtual DbSet<TestTask> TestTask { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=HwProj_DB;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('announcement_id_seq'::regclass)");

                entity.Property(e => e.Message).IsRequired();

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Announcement)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("announcement_lectureid_fkey");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('course_id_seq'::regclass)");

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<CurrentHomework>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"CurrentHomework_id_seq\"'::regclass)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CurrentHomework)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("CurrentHomework_CourseId_fkey");

                entity.HasOne(d => d.Hw)
                    .WithMany(p => p.CurrentHomework)
                    .HasForeignKey(d => d.HwId)
                    .HasConstraintName("CurrentHomework_HwId_fkey");
            });

            modelBuilder.Entity<CurrentTest>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"CurrentTests_Id_seq\"'::regclass)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CurrentTest)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("CurrentTests_CourseId_fkey");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.CurrentTest)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("CurrentTests_TestId_fkey");
            });

            modelBuilder.Entity<Hometask>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('hometask_id_seq'::regclass)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('homework_id_seq'::regclass)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("homework_courseid_fkey");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("homework_taskid_fkey");
            });

            modelBuilder.Entity<HomeworkSolution>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('homeworksolution_id_seq'::regclass)");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.StudentId).IsRequired();

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.HomeworkSolution)
                    .HasForeignKey(d => d.HomeworkId)
                    .HasConstraintName("homeworksolution_homeworkid_fkey");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.HomeworkSolution)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("homeworksolution_studentid_fkey");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('lecture_id_seq'::regclass)");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("lecture_courseid_fkey");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('material_id_seq'::regclass)");

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("material_lectureid_fkey");
            });

            modelBuilder.Entity<OngoingCourse>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('ongoingcourse_id_seq'::regclass)");

                entity.Property(e => e.Completed).HasDefaultValueSql("false");

                entity.Property(e => e.GroupId).IsRequired();

                entity.Property(e => e.TeacherId).IsRequired();

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.OngoingCourse)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("ongoingcourse_courseid_fkey");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.OngoingCourse)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("ongoingcourse_teacherid_fkey");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.Property(e => e.Email).ValueGeneratedNever();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("studentcourse_courseid_fkey");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("studentcourse_studentid_fkey");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.Property(e => e.Email).ValueGeneratedNever();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('test_id_seq'::regclass)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("test_courseid_fkey");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("test_taskid_fkey");
            });

            modelBuilder.Entity<TestSolution>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('testsolution_id_seq'::regclass)");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.StudentId).IsRequired();

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TestSolution)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("testsolution_studentid_fkey");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestSolution)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("testsolution_testkid_fkey");
            });

            modelBuilder.Entity<TestTask>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("nextval('testtask_id_seq'::regclass)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.HasSequence("announcement_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("course_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("CurrentHomework_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("CurrentTests_Id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("hometask_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("homework_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("homeworksolution_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("lecture_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("material_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("ongoingcourse_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("test_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("testsolution_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("testtask_id_seq")
                .HasMin(1)
                .HasMax(2147483647);
        }
    }
}
