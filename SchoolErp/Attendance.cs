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
    
    public partial class Attendance
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public Nullable<System.DateTime> CheckIn { get; set; }
        public Nullable<System.DateTime> CheckOut { get; set; }
    
        public virtual Employees Employees { get; set; }
    }
}
