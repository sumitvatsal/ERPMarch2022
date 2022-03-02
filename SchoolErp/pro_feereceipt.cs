namespace SchoolErp
{
    using System;
    public partial class pro_feereceipt
    {
        public string Refund { get; set; }
        public Nullable<int> StudentiD { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<int> remark { get; set; }
        public string Fee_Type { get; set; }
        public string remark1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> per { get; set; }
        public Nullable<decimal> concession { get; set; }
        public Nullable<decimal> nettotal { get; set; }
        public Nullable<decimal> paid { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<long> FeeHeadID { get; set; }
        public Nullable<int> AcademicYear { get; set; }
    }
}
