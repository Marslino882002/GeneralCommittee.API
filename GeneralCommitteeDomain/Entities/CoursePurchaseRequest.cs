using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Entities
{
    public class CoursePurchaseRequest
    {

        public int Id { get; set; }
        public User user { get; set; }
        public Course course { get; set; }  
        public string RequestStatus { get; set; } // Pending, Accepted, Rejected
        public DateTime RequestDate { get; set; }








    }
}
