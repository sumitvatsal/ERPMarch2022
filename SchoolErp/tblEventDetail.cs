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
    
    public partial class tblEventDetail
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public Nullable<long> EventType { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartdateTime { get; set; }
        public Nullable<System.DateTime> EnddateTime { get; set; }
        public string InchargeName { get; set; }
        public string EventFor { get; set; }
        public string courseId { get; set; }
        public string SectionId { get; set; }
        public Nullable<long> DepartmentId { get; set; }
        public bool Status { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> Deleted_on { get; set; }
        public string deletedby { get; set; }
    }
}
