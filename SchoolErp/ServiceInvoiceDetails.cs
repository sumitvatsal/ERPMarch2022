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
    
    public partial class ServiceInvoiceDetails
    {
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public long ServiceInvoiceId { get; set; }
        public Nullable<double> Tax { get; set; }
    
        public virtual ServiceInvoices ServiceInvoices { get; set; }
        public virtual Services Services { get; set; }
    }
}
