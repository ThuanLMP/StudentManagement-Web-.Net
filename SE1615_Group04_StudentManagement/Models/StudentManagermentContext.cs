using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class StudentManagermentContext : DbContext
    {
        public StudentManagermentContext()
        {
        }

        public StudentManagermentContext(DbContextOptions<StudentManagermentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<CourseClass> CourseClasses { get; set; }
        public virtual DbSet<Exercice> Exercices { get; set; }
        public virtual DbSet<RegisterSubject> RegisterSubjects { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conf = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json").Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conf.GetConnectionString("DbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_Acc");

                entity.ToTable("Account");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Rid).HasColumnName("rid");

                entity.HasOne(d => d.RidNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.Rid)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Classid)
                    .HasMaxLength(50)
                    .HasColumnName("classid");

                entity.Property(e => e.Enddate)
                    .HasColumnType("date")
                    .HasColumnName("enddate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Startdate)
                    .HasColumnType("date")
                    .HasColumnName("startdate");
            });

            modelBuilder.Entity<CourseClass>(entity =>
            {
                entity.HasKey(e => new { e.Subjectid, e.Classid });

                entity.ToTable("CourseClass");

                entity.Property(e => e.Subjectid)
                    .HasMaxLength(50)
                    .HasColumnName("subjectid");

                entity.Property(e => e.Classid)
                    .HasMaxLength(50)
                    .HasColumnName("classid");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.CourseClasses)
                    .HasForeignKey(d => d.Classid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseClass_Class");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.CourseClasses)
                    .HasForeignKey(d => d.Subjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseClass_Subject");
            });

            modelBuilder.Entity<Exercice>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.Subjectid });

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Subjectid)
                    .HasMaxLength(50)
                    .HasColumnName("subjectid");

                entity.Property(e => e.Percentage)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("percentage");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exercices)
                    .HasForeignKey(d => d.Subjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exercices_Subject");
            });

            modelBuilder.Entity<RegisterSubject>(entity =>
            {
                entity.HasKey(e => new { e.Subjectid, e.Studentid });

                entity.ToTable("RegisterSubject");

                entity.Property(e => e.Subjectid)
                    .HasMaxLength(50)
                    .HasColumnName("subjectid");

                entity.Property(e => e.Studentid)
                    .HasMaxLength(50)
                    .HasColumnName("studentid");

                entity.Property(e => e.Enddate)
                    .HasColumnType("date")
                    .HasColumnName("enddate");

                entity.Property(e => e.Startdate)
                    .HasColumnType("date")
                    .HasColumnName("startdate");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RegisterSubjects)
                    .HasForeignKey(d => d.Studentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterSubject_Student");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.RegisterSubjects)
                    .HasForeignKey(d => d.Subjectid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterSubject_Subject");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => new { e.Studentid, e.Name });

                entity.ToTable("Result");

                entity.Property(e => e.Studentid)
                    .HasMaxLength(50)
                    .HasColumnName("studentid");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Marks).HasColumnName("marks");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.Studentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Result_Student");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Rid);

                entity.ToTable("Role");

                entity.Property(e => e.Rid)
                    .ValueGeneratedNever()
                    .HasColumnName("rid");

                entity.Property(e => e.Rname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("rname");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Studentid)
                    .HasMaxLength(50)
                    .HasColumnName("studentid");

                entity.Property(e => e.Classid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("classid");

                entity.Property(e => e.Gmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("gmail");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Classid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Class");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Account");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Subjectid)
                    .HasMaxLength(50)
                    .HasColumnName("subjectid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
