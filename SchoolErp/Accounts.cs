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
    
    public partial class Accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accounts()
        {
            this.Accounts1 = new HashSet<Accounts>();
            this.Transactions = new HashSet<Transactions>();
            this.TempDebit = new HashSet<TempDebit>();
            this.TempCredit = new HashSet<TempCredit>();
        }
    
        public long Id { get; set; }
        public long HeadCode { get; set; }
        public string HeadName { get; set; }
        public Nullable<long> ParentHead { get; set; }
        public string PHeadName { get; set; }
        public int HeadLevel { get; set; }
        public string HeadType { get; set; }
        public bool IsTransaction { get; set; }
        public bool IsGL { get; set; }
        public bool IsBudget { get; set; }
        public bool IsDepreciation { get; set; }
        public Nullable<long> CustomerId { get; set; }
        public Nullable<long> SupplierId { get; set; }
        public double DepreciationRate { get; set; }
        public System.DateTime InsertDate { get; set; }
        public string InsertUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUserId { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUserId { get; set; }
        public int IsActive { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<long> ExpenseTypeId { get; set; }
        public Nullable<long> BankId { get; set; }
        public Nullable<int> SchoolID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accounts> Accounts1 { get; set; }
        public virtual Accounts Accounts2 { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TempDebit> TempDebit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TempCredit> TempCredit { get; set; }
    }
}
