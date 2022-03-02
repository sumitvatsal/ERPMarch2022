using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolErp.Models
{
    public class Assignment
    {
        public long ID { get; set; }
        public tblHomeAssignment hw { get; set; }
        public string course { get; set; }
        public string section { get; set; }
        public string year { get; set; }
        public string subject { get; set; }
        public string hwDt { get; set; }
        public string subDt { get; set; }
        public string style { get; set; }
        public string teacher { get; set; }
        //public string marks { get; set; }
    }
    public class Assignment2
    {
        public long ID { get; set; }
        public tblHomeAssignment hw { get; set; }
        public string course { get; set; }
        public string section { get; set; }
        public string year { get; set; }
        public string subject { get; set; }
        public string subDt { get; set; }
        public string style { get; set; }
        public string teacher { get; set; }
        public string DateTimeCreated { get; set; }
        public string DateCreated { get; set; }
        //public string marks { get; set; }
    }

    public class Assignmentapp
    {
        public bool status { get; set; }
        public string message { get; set; }

        public List<Assignment2>  data { get; set; }

    }
}