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
    
    public partial class CustomerGroups
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime InsertDate { get; set; }
        public string InsertUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUserId { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUserId { get; set; }
        public int IsActive { get; set; }
        public Nullable<int> SchoolID { get; set; }
    }
}