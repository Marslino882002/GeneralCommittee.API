using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Infrastructure
{
    public class SqlServerConstants
    {
        public static string ForeignKeyViolation = "547";
        public static string CourseInstructorFkConstraint = "FK_Courses_Instructors_InstructorId";
    }
}
