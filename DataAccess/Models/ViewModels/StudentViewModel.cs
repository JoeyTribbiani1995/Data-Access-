using System;
using System.Collections.Generic;

namespace DataAccess.Models.ViewModels
{
    public class StudentViewModel
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
