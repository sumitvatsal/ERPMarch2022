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
    
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.DamagedProducts = new HashSet<DamagedProducts>();
            this.GoodsIssueDetails = new HashSet<GoodsIssueDetails>();
            this.GoodsReceiptDetails = new HashSet<GoodsReceiptDetails>();
            this.PurchaseDetails = new HashSet<PurchaseDetails>();
            this.QuotationDetails = new HashSet<QuotationDetails>();
            this.SaleDetails = new HashSet<SaleDetails>();
        }
    
        public long Id { get; set; }
        public string Barcode { get; set; }
        public string SN { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public double UnitPrice { get; set; }
        public double UnitsInStock { get; set; }
        public double UnitsOnOrder { get; set; }
        public double ReorderLevel { get; set; }
        public long CategoryId { get; set; }
        public long UnitId { get; set; }
        public Nullable<long> BrandId { get; set; }
        public string Image { get; set; }
        public string ProductDetails { get; set; }
        public System.DateTime InsertDate { get; set; }
        public string InsertUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUserId { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUserId { get; set; }
        public int IsActive { get; set; }
        public Nullable<int> SchoolID { get; set; }
    
        public virtual Categories Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DamagedProducts> DamagedProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsIssueDetails> GoodsIssueDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsReceiptDetails> GoodsReceiptDetails { get; set; }
        public virtual Units Units { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuotationDetails> QuotationDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetails> SaleDetails { get; set; }
    }
}
