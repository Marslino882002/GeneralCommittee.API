﻿using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Infrastructure.Seeders
{
    public class AdminSeeder
        (GeneralCommitteeDbContext DbContext) : IAdminSeeder
    {
        public async Task seed()
        {
            string connectionString = Environment.GetEnvironmentVariable("MY_CONNECTION_STRING");
            var x = DbContext.Database.GetConnectionString();
            if (!await DbContext.Database.CanConnectAsync())
                return;
            // var x = dbContext.Database.GetConnectionString();
            await DbContext.Database.MigrateAsync();
            await DbContext.Database.EnsureCreatedAsync();

            return;



            return;
            await DbContext.Database.MigrateAsync();
            var adminIdentity = new User
            {
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com".ToUpper(),
                Roles = 1,
                Tenant = Global.ProgramName,
                PhoneNumber = "0111111111111",
                UserName = "admin",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEFZJJnSw0p3qxEghLd+XzFTWCqqFculEJN0dq3I9VQWuR+8+PnGbnWEsOGzMF890EQ==",
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                NormalizedUserName = "admin".ToUpper(),
                PhoneNumberConfirmed = true,
            };
            var admin = new Admin
            {
                AdminId = 1,
                User = adminIdentity,
                FName = "admin",
                LName = "admin"
            };
            var adminIdentity1 = new User
            {
                Email = "admin1@admin.com",
                NormalizedEmail = "admin1@admin.com".ToUpper(),
                Roles = 1,
                Tenant = Global.ProgramName,
                PhoneNumber = "022222222222",
                UserName = "admin1",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEFZJJnSw0p3qxEghLd+XzFTWCqqFculEJN0dq3I9VQWuR+8+PnGbnWEsOGzMF890EQ==",
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                NormalizedUserName = "admin1".ToUpper(),
                PhoneNumberConfirmed = true,
            };
            var admin1 = new Admin
            {
                AdminId = 2,
                User = adminIdentity1,
                FName = "admin1",
                LName = "admin1"
            };
            var cat1 = new Category()
            {
                Name = "Dev",
                Description = "Development",
            };
            var cat2 = new Category
            {
                Name = "Think",
                Description = "Thinking",

            };
            var instructor1 = new Instructor
            {
                Name = "John Doe",
                About = "John Doe",
                AddedBy = admin,
            };
            var instructor2 = new Instructor
            {
                Name = "John Doe 2",
                About = "sadf",
                AddedBy = admin
            };
            var course = new Course()
            {
                Name = "DSP",
                Description = "cous",
                Categories = new List<Category> { cat1, cat2 },
                Instructor = instructor1,
                Price = 100,
                Rating = 2.4M,
                EnrollmentsCount = 5,
                IsFree = false,
                ReviewsCount = 2,
                IsPublic = false,
                ThumbnailUrl = "fsadfa",
                CollectionId = "28d97e2c-2561-44a9-bb55-1cb8ed14807a",

            };
            // var Matrials = new List<CourseMateriel>()
            // {
            //     new CourseMateriel()
            //     {
            //         Admin = admin,
            //         Description = "mat 1",
            //         Url = "safsadfasf.com",
            //         Title = "Mat 1",
            //         IsVideo = true,
            //         ItemOrder = 1,
            //         Course = course
            //     },
            //     new()
            //     {
            //         Admin = admin,
            //         Description = "mat 2",
            //         Url = "safsadfasf.com2",
            //         Title = "Mat 2",
            //         IsVideo = true,
            //         ItemOrder = 2,
            //         Course = course
            //
            //     }
            // };
            //
            await DbContext.AddAsync(adminIdentity);
            await DbContext.AddAsync(adminIdentity1);
            await DbContext.AddAsync(admin);
            await DbContext.AddAsync(admin1);
            await DbContext.AddAsync(cat1);
            await DbContext.AddAsync(cat2);
            await DbContext.AddAsync(instructor1);
            await DbContext.AddAsync(instructor2);
            // await dbContext.AddRangeAsync(Matrials);
            await DbContext.AddAsync(course);
            await DbContext.SaveChangesAsync();
        }

    }
}

