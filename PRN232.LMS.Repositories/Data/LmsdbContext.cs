using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.Entities;

namespace PRN232.LMS.Repositories.Data;

public partial class LmsdbContext : DbContext
{
    public LmsdbContext()
    {
    }

    public LmsdbContext(DbContextOptions<LmsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LMSDB;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Courseid).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Coursename)
                .HasMaxLength(100)
                .HasColumnName("coursename");
            entity.Property(e => e.Semesterid).HasColumnName("semesterid");

            entity.HasOne(d => d.Semester).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Semesterid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_semesterid_fkey");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Enrollmentid).HasName("enrollment_pkey");

            entity.ToTable("enrollment");

            entity.Property(e => e.Enrollmentid).HasColumnName("enrollmentid");
            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Enrolldate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enrolldate");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollment_courseid_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollment_studentid_fkey");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Semesterid).HasName("semester_pkey");

            entity.ToTable("semester");

            entity.Property(e => e.Semesterid).HasColumnName("semesterid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Semestername)
                .HasMaxLength(100)
                .HasColumnName("semestername");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("student_pkey");

            entity.ToTable("student");

            entity.HasIndex(e => e.Email, "student_email_key").IsUnique();

            entity.Property(e => e.Studentid).HasColumnName("studentid");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Subjectid).HasName("subject_pkey");

            entity.ToTable("subject");

            entity.HasIndex(e => e.Subjectcode, "subject_subjectcode_key").IsUnique();

            entity.Property(e => e.Subjectid).HasColumnName("subjectid");
            entity.Property(e => e.Credit).HasColumnName("credit");
            entity.Property(e => e.Subjectcode)
                .HasMaxLength(20)
                .HasColumnName("subjectcode");
            entity.Property(e => e.Subjectname)
                .HasMaxLength(100)
                .HasColumnName("subjectname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
