using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolErp.Models
{
    public class ScholarRegister
    {
        public tblScholarRegister tsr { get; set; }
        public tblScholarRegisterDetail tsrd { get; set; }
        public TBLStudent std { get; set; }
        public string type { get; set; }
        public string StatusName { get; set; }
        public string jDate { get; set; }
        public string SDOB { get; set; }
        public int count { get; set; }
        public string style { get; set; }
        public string fullStName { get; set; }
        public string ClassName { get; set; }
        public string Msg { get; set; }
        public int ID { get; set; }
        public List<tblScholarRegisterDetail> SRdetailsList { get; set; }
    }
}