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
    
    public partial class tblScholarRegisterDetail
    {
        public long ID { get; set; }
        public long Student_ID { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public string ActionTaken { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
    }
}