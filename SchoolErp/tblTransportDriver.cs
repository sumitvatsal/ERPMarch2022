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
    
    public partial class tblTransportDriver
    {
        public int Id { get; set; }
        public Nullable<int> VehicleId { get; set; }
        public int DriveId { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string phone { get; set; }
        public string LicenseNo { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> Deleted_on { get; set; }
        public string deletedby { get; set; }
        public string LicenseExpDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
