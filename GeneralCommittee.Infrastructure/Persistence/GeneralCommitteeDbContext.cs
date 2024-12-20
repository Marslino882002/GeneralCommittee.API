﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GeneralCommittee.Domain.Entities;
namespace GeneralCommittee.Infrastructure.Persistence
{
    public class GeneralCommitteeDbContext : IdentityDbContext<User>
    {

        public GeneralCommitteeDbContext(DbContextOptions<GeneralCommitteeDbContext> options)
        : base(options)
        {
        }
        #region DbSets of Tables

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseMateriel> CourseMateriels { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Meditation> Meditations { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PodCaster> PodCasters { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EnrollmentDetails> EnrollmentDetails { get; set; }
        public DbSet<SystemUserTokenCode> SystemUserTokenCodes { get; set; }
        public DbSet<PendingAdmins> PendingAdmins { get; set; }
        public DbSet<PendingVideoUpload> VideoUploads { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureAdmin(modelBuilder);
            ConfigureUserEntity(modelBuilder);
            ConfigureSystemUser(modelBuilder);
            ConfigureArticle(modelBuilder);
            ConfigurePodcast(modelBuilder);
            ConfigureMeditation(modelBuilder);
            //
            ConfigureCourse(modelBuilder);
            ConfigureCategory(modelBuilder);
            ConfigureAuthor(modelBuilder);
            ConfigurePodCaster(modelBuilder);
            ConfigureInstructor(modelBuilder);
            ConfigureLogs(modelBuilder);
            ConfigurePayments(modelBuilder);
            ConfigureEnrollmentDetails(modelBuilder);

        }

        private void ConfigureAdmin(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(a => a.AdminId);
                entity.Property(a => a.AdminId).UseIdentityColumn(1, 1);
                entity.Property(a => a.FName).IsRequired();
                entity.Property(a => a.LName).IsRequired();
            });
        }

        private void ConfigureSystemUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemUser>(U =>
            {
                U.HasKey(nameof(SystemUser.SystemUserId));
                U.Property(U => U.SystemUserId)
                    .UseIdentityColumn(1, 1);

                U.HasMany(U => U.CourseRates)
                    .WithMany(c => c.UsersRates);


                U.HasMany(u => u.FavCourses)
                    .WithMany(c => c.UsersFavCourse);
                U.Property(u => u.FName).IsRequired();
                U.Property(u => u.LName).IsRequired();
            });
        }

        private void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                // Email and Tenant combination must be unique
                entity.HasIndex(u => new { u.Email, u.Tenant })
                    .IsUnique();
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                // NormalizedEmail and Tenant combination must be unique
                entity.HasIndex(u => new { u.NormalizedEmail, u.Tenant })
                    .IsUnique();
                entity.Property(u => u.NormalizedEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                // NormalizedUserName and Tenant combination must be unique
                entity.HasIndex(u => new { u.NormalizedUserName, u.Tenant })
                    .IsUnique();
                entity.Property(u => u.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(100);

                // UserName and Tenant combination must be unique
                entity.HasIndex(u => new { u.UserName, u.Tenant })
                    .IsUnique();
                entity.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                // PhoneNumber and Tenant combination must be unique
                entity.HasIndex(u => new { u.Tenant, u.PhoneNumber })
                    .IsUnique();
                entity.Property(u => u.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                // TODO: Consider making UserName not unique within the Tenant context if needed
            });
        }

        private void ConfigureArticle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                // Primary key for Article
                entity.HasKey(a => a.ArticleId);

                // Configure ArticleId to use identity columns
                entity.Property(a => a.ArticleId).UseIdentityColumn(10, 10);

                // Configure Title property
                entity.Property(a => a.Title)
                    .IsRequired()
                    .HasAnnotation("DataType", DataType.Text);

                // Configure foreign keys
                entity.Property(a => a.UploadedById)
                    .IsRequired();

                entity.Property(a => a.AuthorId)
                    .IsRequired(); // Ensure AuthorId is required

                // Configure CreatedDate property
                entity.Property(a => a.CreatedDate)
                    .IsRequired()
                    .HasAnnotation("DataType", DataType.DateTime)
                    .HasDefaultValueSql("GETDATE()"); // Set the default value in the table instead of generating it.

                // Configure relationships
                entity.HasOne(a => a.UploadedBy) // Relationship to Admin
                    .WithMany(adm => adm.Articles)
                    .HasForeignKey(a => a.UploadedById) // Use the foreign key property
                    .OnDelete(DeleteBehavior.Cascade); // Specify delete behavior

                entity.HasOne(a => a.Author) // Relationship to Author
                    .WithMany(au => au.Articles)
                    .HasForeignKey(a => a.AuthorId) // Use the foreign key property
                    .OnDelete(DeleteBehavior.Cascade); // Specify delete behavior
            });
        }

        private void ConfigurePodcast(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Podcast>(entity =>
            {
                entity.HasKey(p => p.PodcastId);
                entity.Property(p => p.PodcastId).UseIdentityColumn(100, 20);

                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasAnnotation("DataType", DataType.Text);

                entity.Property(p => p.UploadedById) // Ensure you reference UploadedById
                    .IsRequired();

                entity.Property(p => p.CreatedDate)
                    .IsRequired()
                    .HasAnnotation("DataType", DataType.DateTime)
                    .HasDefaultValueSql("GETDATE()"); // Set the default value in the table instead of generating it.

                entity.Property(p => p.PodcastLength)
                    .IsRequired(); // Make sure to require PodcastLength if needed

                // Configure relationships
                entity.HasOne(p => p.UploadedBy)
                    .WithMany(a => a.Podcasts) // Assuming Admin has a collection of Podcasts
                    .HasForeignKey(p => p.UploadedById) // Use the foreign key property
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.PodCaster)
                    .WithMany(pc => pc.Podcasts)
                    .HasForeignKey(p => p.PodCasterId) // Use the foreign key property
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureMeditation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meditation>(entity =>
            {
                entity.HasKey(m => m.MeditationId);
                entity.Property(m => m.MeditationId).UseIdentityColumn(110, 10);
                entity.Property(m => m.Title).IsRequired().HasAnnotation("DataType", DataType.Text);
                entity.Property(m => m.Content).IsRequired();
                entity.Property(m => m.CreatedDate).IsRequired().HasAnnotation("DataType", DataType.DateTime);
                entity.Property(m => m.CreatedDate)
                    .HasDefaultValueSql("GETDATE()"); // Set the default value in the table instead of generating it.

                // Use UploadedById for foreign key configuration
                entity.HasOne(m => m.UploadedBy)
                    .WithMany(a => a.Meditations) // Make sure the Admin class has a Meditations collection
                    .HasForeignKey(m => m.UploadedById) // Correctly reference the foreign key property
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }


        private void ConfigureCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);

                // Each course must have one instructor
                entity.HasOne(c => c.Instructor)
                    .WithMany(i => i.Courses)  // An instructor has many courses
                    .HasForeignKey(c => c.InstructorId)  // Foreign key is defined in the Course entity
                    .OnDelete(DeleteBehavior.Cascade);  // When the instructor is deleted, their courses will be deleted


                entity.HasMany(c => c.Categories)
                    .WithMany(cc => cc.Courses);
            });
        }

        private void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasMany(c => c.Courses)
                    .WithMany(cc => cc.Categories);
            });
        }

        private void ConfigureAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.AuthorId);
                entity.Property(a => a.AuthorId).UseIdentityColumn(10, 2);

                entity.HasMany(a => a.Articles)
                    .WithOne(ar => ar.Author)
                    .HasForeignKey(ar => ar.ArticleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurePodCaster(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PodCaster>(entity =>
            {
                entity.HasKey(p => p.PodCasterId);
                entity.Property(p => p.PodCasterId).UseIdentityColumn(100, 1);

                entity.HasMany(p => p.Podcasts)
                    .WithOne(pc => pc.PodCaster)
                    .HasForeignKey(pc => pc.PodcastId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureInstructor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasKey(i => i.InstructorId);

                // Configure auto-generated identity for InstructorId
                entity.Property(i => i.InstructorId).UseIdentityColumn(1, 1);

                // An instructor can have many courses
                entity.HasMany(i => i.Courses)
                    .WithOne(c => c.Instructor)  // One instructor for each course
                    .HasForeignKey(c => c.InstructorId) // Define the foreign key on the Course
                    .OnDelete(DeleteBehavior.Cascade);  // Deleting an instructor will cascade delete their courses
            });
        }


        private void ConfigureLogs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Id).UseIdentityColumn(1, 1);
            });
        }

        private void ConfigurePayments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(p => p.PaymentId);
                entity.Property(p => p.PaymentId).UseIdentityColumn(1, 1);
            });
        }

        private void ConfigureEnrollmentDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnrollmentDetails>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId);
                entity.Property(e => e.EnrollmentId).UseIdentityColumn(1, 1);
                // Set max length for Progress property
                entity.Property(e => e.Progress)
                    .IsRequired() // Set it as required
                    .HasMaxLength(200); // Set max length to 200

                // Configure foreign key relationships
                entity.HasOne(e => e.SystemUser)
                    .WithMany() // Assuming there's no navigation property back to EnrollmentDetails in SystemUser
                    .HasForeignKey(e => e.SystemUserId)
                    .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior

                entity.HasOne(e => e.Course)
                    .WithMany() // Assuming there's no navigation property back to EnrollmentDetails in Course
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior
            });
        }


    }
}
