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
    
    public partial class DailyClosing
    {
        public long Id { get; set; }
        public double LastDayClosing { get; set; }
        public double CashIn { get; set; }
        public double CashOut { get; set; }
        public System.DateTime Date { get; set; }
        public double Amount { get; set; }
        public double Adjustment { get; set; }
        public int Status { get; set; }
        public System.DateTime InsertDate { get; set; }
        public int InsertUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUserId { get; set; }
        public Nullable<int> SchoolId { get; set; }
    }
}
