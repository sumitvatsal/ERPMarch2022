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
    
    public partial class payment_details
    {
        public int id { get; set; }
        public Nullable<int> licence_id { get; set; }
        public Nullable<double> total_amount { get; set; }
        public Nullable<System.DateTime> renewal_date { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> created_on { get; set; }
        public string updated_by { get; set; }
        public Nullable<System.DateTime> updated_on { get; set; }
        public string deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_on { get; set; }
    }
}