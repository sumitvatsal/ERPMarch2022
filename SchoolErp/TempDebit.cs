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
    
    public partial class TempDebit
    {
        public long Id { get; set; }
        public Nullable<long> BankId { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> TotalDiscount { get; set; }
        public Nullable<double> ShippingCost { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> NetTotal { get; set; }
        public Nullable<double> PaidAmount { get; set; }
        public Nullable<double> Due { get; set; }
        public Nullable<double> Change { get; set; }
        public string Details { get; set; }
        public Nullable<long> PaymentAccount { get; set; }
        public string EmployeeId { get; set; }
        public string Voucher { get; set; }
        public Nullable<long> VNo { get; set; }
        public Nullable<int> SchoolID { get; set; }
    
        public virtual Accounts Accounts { get; set; }
    }
}
