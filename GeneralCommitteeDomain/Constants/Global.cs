﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Constants
{
    public class Global
    {
        public const string Roles = "Roles";
        public const string PassCode = "TokenPassCode";
        public const string ProgramName = "MentalHealthcare";
        public const string TenantClaimType = "TenantClaimType";
        public const string ApplicationTenant = "MentalHealthcareApp";
        public const int UrlMaxLength = 250;
        public const int TitleMaxLength = 100;
        public const int MaxNameLength = 50;
        public const string AdminIdClaimType = "AdminId";
        public const string UserIdClaimType = "AdminId";
        public static string CourseThumbnailDirectory = "CoursesThumbnails";
        public static string ThumbnailFileExtension = ".jpeg";
    }
}
