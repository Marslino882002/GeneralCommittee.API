﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralCommittee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "AspNetRoles",
                 columns: table => new
                 {
                     Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                     Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                     NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                     ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                 });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Roles = table.Column<long>(type: "bigint", nullable: false),
                    Tenant = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "VideoUploads",
                columns: table => new
                {
                    PendingVideoUploadId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoUploads", x => x.PendingVideoUploadId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    SystemUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dof = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.SystemUserId);
                    table.ForeignKey(
                        name: "FK_SystemUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserTokenCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserTokenCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserTokenCodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 2"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedByAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Authors_Admins_AddedByAdminId",
                        column: x => x.AddedByAdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedByAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructors_Admins_AddedByAdminId",
                        column: x => x.AddedByAdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meditations",
                columns: table => new
                {
                    MeditationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "110, 10"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meditations", x => x.MeditationId);
                    table.ForeignKey(
                        name: "FK_Meditations_Admins_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PendingAdmins",
                columns: table => new
                {
                    PendingAdminsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingAdmins", x => x.PendingAdminsId);
                    table.ForeignKey(
                        name: "FK_PendingAdmins_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PodCasters",
                columns: table => new
                {
                    PodCasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedByAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodCasters", x => x.PodCasterId);
                    table.ForeignKey(
                        name: "FK_PodCasters_Admins_AddedByAdminId",
                        column: x => x.AddedByAdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });
            


            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserOFId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SystemUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_AspNetUsers_userId",
                        column: x => x.UserOFId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Logs_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "SystemUserId");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Card_Number = table.Column<long>(type: "bigint", nullable: false),
                    month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usersId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SystemUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_usersId",
                        column: x => x.usersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "SystemUserId");
                });
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 10"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Admins_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Restrict); // Changed to Restrict
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

       

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ThumbnailName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReviewsCount = table.Column<int>(type: "int", nullable: false),
                    EnrollmentsCount = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
       name: "Podcasts",
       columns: table => new
       {
           PodcastId = table.Column<int>(type: "int", nullable: false)
               .Annotation("SqlServer:Identity", "100, 20"),
           PodcastLength = table.Column<int>(type: "int", nullable: false),
           PodcastDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
           PodCasterId = table.Column<int>(type: "int", nullable: false),
           Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
           UploadedById = table.Column<int>(type: "int", nullable: false),
           CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
       },
       constraints: table =>
       {
           table.PrimaryKey("PK_Podcasts", x => x.PodcastId);
           table.ForeignKey(
               name: "FK_Podcasts_Admins_UploadedById",
               column: x => x.UploadedById,
               principalTable: "Admins",
               principalColumn: "AdminId",
               onDelete: ReferentialAction.Cascade);
           table.ForeignKey(
               name: "FK_Podcasts_PodCasters_PodCasterId",
               column: x => x.PodCasterId,
               principalTable: "PodCasters",
               principalColumn: "PodCasterId",
               onDelete: ReferentialAction.Restrict); // Changed to Restrict
       });

            migrationBuilder.CreateIndex(
                name: "IX_Podcasters_AddedByAdminId",
                table: "Podcasters",
                column: "AddedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Podcasts_PodCasterId",
                table: "Podcasts",
                column: "PodCasterId");


            migrationBuilder.CreateTable(
                name: "CategoryCourse",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    CoursesCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCourse", x => new { x.CategoriesCategoryId, x.CoursesCourseId });
                    table.ForeignKey(
                        name: "FK_CategoryCourse_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCourse_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
       name: "CourseMateriels",
       columns: table => new
       {
           CourseMaterielId = table.Column<int>(type: "int", nullable: false)
               .Annotation("SqlServer:Identity", "1, 1"),
           CourseId = table.Column<int>(type: "int", nullable: false),
           AdminId = table.Column<int>(type: "int", nullable: false),
           Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
           Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
           ItemOrder = table.Column<int>(type: "int", nullable: false),
           Url = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
           IsVideo = table.Column<bool>(type: "bit", nullable: false)
       },
       constraints: table =>
       {
           table.PrimaryKey("PK_CourseMateriels", x => x.CourseMaterielId);
           table.ForeignKey(
               name: "FK_CourseMateriels_Admins_AdminId",
               column: x => x.AdminId,
               principalTable: "Admins",
               principalColumn: "AdminId",
               onDelete: ReferentialAction.Cascade);
           table.ForeignKey(
               name: "FK_CourseMateriels_Courses_CourseId",
               column: x => x.CourseId,
               principalTable: "Courses",
               principalColumn: "CourseId",
               onDelete: ReferentialAction.Restrict); // Changed to Restrict
       });

           



            migrationBuilder.CreateTable(
         name: "CourseSystemUser",
         columns: table => new
         {
             CourseRatesCourseId = table.Column<int>(type: "int", nullable: false),
             UsersRatesSystemUserId = table.Column<int>(type: "int", nullable: false)
         },
         constraints: table =>
         {
             table.PrimaryKey("PK_CourseSystemUser", x => new { x.CourseRatesCourseId, x.UsersRatesSystemUserId });
             table.ForeignKey(
                 name: "FK_CourseSystemUser_Courses_CourseRatesCourseId",
                 column: x => x.CourseRatesCourseId,
                 principalTable: "Courses",
                 principalColumn: "CourseId",
                 onDelete: ReferentialAction.Restrict); // Changed to Restrict
             table.ForeignKey(
                 name: "FK_CourseSystemUser_SystemUsers_UsersRatesSystemUserId",
                 column: x => x.UsersRatesSystemUserId,
                 principalTable: "SystemUsers",
                 principalColumn: "SystemUserId",
                 onDelete: ReferentialAction.Cascade);
         });

        

            migrationBuilder.CreateTable(
                name: "CourseSystemUser1",
                columns: table => new
                {
                    FavCoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    UsersFavCourseSystemUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSystemUser1", x => new { x.FavCoursesCourseId, x.UsersFavCourseSystemUserId });
                    table.ForeignKey(
                        name: "FK_CourseSystemUser1_Courses_FavCoursesCourseId",
                        column: x => x.FavCoursesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict); // Changed to Restrict
                    table.ForeignKey(
                        name: "FK_CourseSystemUser1_SystemUsers_UsersFavCourseSystemUserId",
                        column: x => x.UsersFavCourseSystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "SystemUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateTable(
                name: "EnrollmentDetails",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Progress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentDetails", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_EnrollmentDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentDetails_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "SystemUserId",
                        onDelete: ReferentialAction.Restrict); // Changed to Restrict
                });


         migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UploadedById",
                table: "Articles",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email_Tenant",
                table: "AspNetUsers",
                columns: new[] { "Email", "Tenant" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NormalizedEmail_Tenant",
                table: "AspNetUsers",
                columns: new[] { "NormalizedEmail", "Tenant" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NormalizedUserName_Tenant",
                table: "AspNetUsers",
                columns: new[] { "NormalizedUserName", "Tenant" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Tenant_PhoneNumber",
                table: "AspNetUsers",
                columns: new[] { "Tenant", "PhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName_Tenant",
                table: "AspNetUsers",
                columns: new[] { "UserName", "Tenant" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AddedByAdminId",
                table: "Authors",
                column: "AddedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCourse_CoursesCourseId",
                table: "CategoryCourse",
                column: "CoursesCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMateriels_AdminId",
                table: "CourseMateriels",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMateriels_CourseId",
                table: "CourseMateriels",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSystemUser_UsersRatesSystemUserId",
                table: "CourseSystemUser",
                column: "UsersRatesSystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSystemUser1_UsersFavCourseSystemUserId",
                table: "CourseSystemUser1",
                column: "UsersFavCourseSystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDetails_CourseId",
                table: "EnrollmentDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentDetails_SystemUserId",
                table: "EnrollmentDetails",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_AddedByAdminId",
                table: "Instructors",
                column: "AddedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_SystemUserId",
                table: "Logs",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_userId",
                table: "Logs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Meditations_UploadedById",
                table: "Meditations",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SystemUserId",
                table: "Payments",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_usersId",
                table: "Payments",
                column: "usersId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingAdmins_AdminId",
                table: "PendingAdmins",
                column: "AdminId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PodCasters_AddedByAdminId",
            //    table: "PodCasters",
            //    column: "AddedByAdminId");

            migrationBuilder.CreateIndex(
               name: "IX_Podcasts_UploadedById",
                table: "Podcasts",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_UserId",
                table: "SystemUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserTokenCodes_UserId",
                table: "SystemUserTokenCodes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CategoryCourse");

            migrationBuilder.DropTable(
                name: "CourseMateriels");

            migrationBuilder.DropTable(
                name: "CourseSystemUser");

            migrationBuilder.DropTable(
                name: "CourseSystemUser1");

            migrationBuilder.DropTable(
                name: "EnrollmentDetails");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Meditations");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PendingAdmins");

            migrationBuilder.DropTable(
                name: "Podcasts");

            migrationBuilder.DropTable(
                name: "SystemUserTokenCodes");

            migrationBuilder.DropTable(
                name: "VideoUploads");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "SystemUsers");

            migrationBuilder.DropTable(
                name: "PodCasters");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
