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
    
    public partial class DamagedProducts
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public double PurchasePrice { get; set; }
        public double Quantity { get; set; }
        public System.DateTime Date { get; set; }
        public string Note { get; set; }
        public long ProductId { get; set; }
        public Nullable<int> SchoolID { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Products Products { get; set; }
    }
}
