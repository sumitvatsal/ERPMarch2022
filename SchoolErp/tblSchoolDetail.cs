//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolErp
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSchoolDetail
    {
        public int ID { get; set; }
        public string School { get; set; }
        public string SchoolCode { get; set; }
        public string Address { get; set; }
        public Nullable<int> State { get; set; }
        public string CityID { get; set; }
        public string District { get; set; }
        public string Board { get; set; }
        public Nullable<long> Pincode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string LogoPic { get; set; }
        public Nullable<int> PrincipalID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string Password { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> Deleted_on { get; set; }
        public string deletedby { get; set; }
        public Nullable<int> FeeDueDay { get; set; }
    }
}
