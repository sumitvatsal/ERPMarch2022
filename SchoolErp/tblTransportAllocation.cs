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
    
    public partial class tblTransportAllocation
    {
        public int Id { get; set; }
        public string RouteId { get; set; }
        public string DestinationId { get; set; }
        public string UserType { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string StudentId { get; set; }
        public string DepId { get; set; }
        public string EmployeeId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public bool Status { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> Deleted_on { get; set; }
        public string deletedby { get; set; }
    }
}