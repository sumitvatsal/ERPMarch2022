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
    
    public partial class TariffDetail
    {
        public int ID { get; set; }
        public Nullable<int> TariffID { get; set; }
        public Nullable<int> AcademicYearid { get; set; }
        public Nullable<int> Studentid { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> amount { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> Approvedby { get; set; }
        public Nullable<int> FeeHeadID { get; set; }
        public Nullable<int> SchoolID { get; set; }
    }
}
