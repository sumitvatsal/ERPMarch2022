using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schoolERP_BLL;

namespace SchoolErp.Models
{

    public class Loginstudent
    {
        // public string Id { get; set; }
       public string status { get; set; }
        public string message { get; set; }
       public Login data { get; set; }

    }

    public class Login
    {
       // public string Id { get; set; }
        public string userId { get; set; }
      
        public string type { get; set; }
        public bool Status { get; set; }
     //   public string msg { get; set; }   
        public string message { get; set; }
        public Student data { get; set; }
        public EmployeeEm emp { get; set; }
        public tblUser user { get; set; }
        public string password { get; set; }
        public SchoolDetails schooladmin {get;set;}
        public string msg { get; set; }
        public string SchoolCode { get; set; }


    }
    //avn
    public class Authentication
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public System.Guid UniiqueID { get; set; }
    }
    //

    public class roleType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class GetSchoolcode
    {

      public string code { get; set; }
        public string School { get; set; }
        public int guid { get; set; }
    }

    public class SchoolAdminEmail
    {

        public string emailId { get; set; }
        public string School { get; set; }
        public string SchoolCode { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }


    }

    public class EmployeeloginDetailsEmail
    {

        public string emailId { get; set; }
        public string School { get; set; }
        public string Userid { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }


    }

    public class StudentloginDetailsEmail1
    {
        public string School { get; set; }
        public string FemailId { get; set; }
        public string MemailId { get; set; }
        public string SUserid { get; set; }
        public string SPassword { get; set; }
        public string PUserid { get; set; }
        public string PPassword { get; set; }
        public string URL { get; set; }


    }


}