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
    
    public partial class tblClassTiming
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> Deleted_on { get; set; }
        public string deletedby { get; set; }
        public Nullable<int> ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
    }
}
