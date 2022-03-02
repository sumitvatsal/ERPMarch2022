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
    
    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            this.SaleDetails = new HashSet<SaleDetail>();
        }
    
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public System.DateTime Date { get; set; }
        public double Discount { get; set; }
        public double TotalDiscount { get; set; }
        public double Vat { get; set; }
        public double TotalTax { get; set; }
        public double ShippingCost { get; set; }
        public double GrandTotal { get; set; }
        public double NetTotal { get; set; }
        public double PaidAmount { get; set; }
        public double Due { get; set; }
        public double Change { get; set; }
        public string Details { get; set; }
        public Nullable<long> PaymentAccount { get; set; }
        public long VNo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}