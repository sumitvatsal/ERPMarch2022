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
    
    public partial class tblHomeAssignment
    {
        public long ID { get; set; }
        public string AssignmentNm { get; set; }
        public string AssignmentDesc { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public Nullable<int> Class { get; set; }
        public Nullable<int> Section { get; set; }
        public Nullable<long> Subject { get; set; }
        public string Marks { get; set; }
        public Nullable<System.DateTime> SubmitDt { get; set; }
        public string FilePath { get; set; }
        public Nullable<System.DateTime> HW_Dt { get; set; }
        public Nullable<int> HW_givenBy { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> SchoolID { get; set; }
    }
}