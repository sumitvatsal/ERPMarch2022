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
    
    public partial class SaleDetails
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public double Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public long SaleId { get; set; }
        public Nullable<double> Tax { get; set; }
    
        public virtual Sales Sales { get; set; }
        public virtual Products Products { get; set; }
    }
}
