using LinqKit;
using SchoolErp.Models;
using schoolERP_BLL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;

namespace SchoolErp.Controllers.WebApi
{
    public class LicenceController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();

        [System.Web.Http.Route("api/Licence/SaveLicence")]
        [System.Web.Http.HttpPost]
        public string SaveLicence(Licence AppObj)
        {
            try
            {
                string b = LicenceBLL.saveLicence(AppObj);
                if (b != "")
                {
                    return b;
                }
                else
                {

                    return "";
                }

            }
            catch (Exception) { return "No"; }
        }

        [System.Web.Http.Route("api/Licence/matchcountofstudents")]
        [System.Web.Http.HttpGet]
        public CHECKLICENCE matchcountofstudents(string usrname)
        {
            
            int SID = Convert.ToInt32( usrname);
         
            sqlHelper obj = new sqlHelper();
            CHECKLICENCE usr = new CHECKLICENCE();
            DataTable dt = obj.getDataTable("select B.no_of_students from licence_details B where school_id='"+ SID + "' and deleted_on is null and active='1'");
            foreach (DataRow dr in dt.Rows)
            {
               
                int a = db.TBLStudents.Where(s => s.SchoolID == SID).Count();
               
                int b =Convert.ToInt32(dr["no_of_students"]);
                if (a<b)
                {
                 usr.alertmsg  = "1";
                }
               else if (a==b)
                {
                    usr.alertmsg = "2";
                }
                else if (a > b)
                {
                    usr.alertmsg = "2";
                }

            }
            return usr;
        }


        [System.Web.Http.Route("api/Licence/getAllLicence")]
        [System.Web.Http.HttpPost]
        public Licence[] getAllLicence()
        {
            List<Licence> list = new List<Licence>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable("select id,(select School from tblSchoolDetails where ID=ld.school_id)school_id,licence_no,no_of_students,per_student_charge,valid_to,(case when active='1' then 'Active' when active='0' then '' else '' end)active from licence_details ld where deleted_on is null order by school_id,active desc");
            foreach (DataRow dr in dt.Rows)
            {
                Licence usr = new Licence();
                usr.Id = Convert.ToInt32(dr["id"]);
                usr.SchoolName = dr["school_id"].ToString();
                usr.LicenceNo = dr["licence_no"].ToString();
                usr.NoofStudent = dr["no_of_students"].ToString();
                usr.Charges = dr["per_student_charge"].ToString();
                usr.ValidTo = Convert.ToDateTime(dr["valid_to"]).ToString("MM/dd/yyyy");
                usr.Active = dr["active"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }
        //[System.Web.Http.Route("api/Licence/getAllLicenceByID")]
        //[System.Web.Http.HttpPost]
        //public Licence[] getAllLicence(string id)
        //{
        //    List<Licence> list = new List<Licence>();
        //    sqlHelper obj = new sqlHelper();
        //    DataTable dt = obj.getDataTable("select id,(select School from tblSchoolDetails where ID=ld.school_id)school_id,licence_no,no_of_students,per_student_charge,valid_to,(case when active='1' then 'Active' when active='0' then '' else '' end)active from licence_details ld where deleted_on is null and id='"+id+"' order by school_id,active desc ");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Licence usr = new Licence();
        //        usr.Id = Convert.ToInt32(dr["id"]);
        //        usr.SchoolName = dr["school_id"].ToString();
        //        usr.LicenceNo = dr["licence_no"].ToString();
        //        usr.NoofStudent = dr["no_of_students"].ToString();
        //        usr.Charges = dr["per_student_charge"].ToString();
        //        usr.ValidTo = Convert.ToDateTime(dr["valid_to"]).ToString("MM/dd/yyyy");
        //        usr.Active = dr["active"].ToString();
        //        list.Add(usr);
              
        //    }
        //    return list.ToArray();
        //}
        [System.Web.Http.Route("api/Licence/DeleteLicence")]
        [System.Web.Http.HttpGet]
        public string DeleteLicence(string id, string username)
        {
            bool b = LicenceBLL.deleteLicence(id, username);
            if (b)
            {
                return "Licence Deleted Successfully";
            }
            else
            {
                return "Licence Not Deleted Successfully";
            }

        }

        [System.Web.Http.Route("api/Licence/EditLicence")]
        [System.Web.Http.HttpGet]
        public Licence EditLicence(string id)
        {
            Licence usr = new Licence();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable("select id,school_id,no_of_students,per_student_charge,valid_to from licence_details ld where id=" + id);
            foreach (DataRow dr in dt.Rows)
            {
                usr.SchoolID = Convert.ToInt32(dr["school_id"]);
                usr.NoofStudent = dr["no_of_students"].ToString();
                usr.Charges = dr["per_student_charge"].ToString();
                usr.ValidTo = Convert.ToDateTime(dr["valid_to"]).ToString("MM/dd/yyyy");
            }
            return usr;
        }

        [System.Web.Http.Route("api/Licence/getSchoolLicences")]
        [System.Web.Http.HttpPost]
        public Licence[] getSchoolLicences(List<string> usrname)
        {
            List<Licence> list = new List<Licence>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable("select id,renewal_date,(select School from tblSchoolDetails where ID=ld.school_id)school_id,licence_no,no_of_students,per_student_charge,valid_to,(case when active='1' then 'Active' when active='0' then '' else '' end)active from licence_details ld where deleted_on is null and school_id=(select id from tblSchoolDetails where schoolcode='" + usrname[0] + "') order by school_id,active desc");
            foreach (DataRow dr in dt.Rows)
            {
                Licence usr = new Licence();
                usr.Id = Convert.ToInt32(dr["id"]);
                usr.SchoolName = dr["school_id"].ToString();
                usr.LicenceNo = dr["licence_no"].ToString();
                usr.NoofStudent = dr["no_of_students"].ToString();
                usr.Charges = dr["per_student_charge"].ToString();
                usr.ValidTo = Convert.ToDateTime(dr["valid_to"]).ToString("MM/dd/yyyy");
                var str = Convert.ToString(dr["renewal_date"]);
                if (Convert.ToString(dr["renewal_date"])=="" || Convert.ToString(dr["renewal_date"]) == null)
                {
                    usr.RenewalDate = "";
                }
                else
                {

                    usr.RenewalDate = Convert.ToDateTime(dr["renewal_date"]).ToString("MM/dd/yyyy");
                }
             
                usr.Active = dr["active"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Licence/PaymentDetails")]
        [System.Web.Http.HttpGet]
        public Licence PaymentDetails(string usrname)
        {
            Licence usr = new Licence();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable("select no_of_students,per_student_charge from licence_details where deleted_on is null and active=1 and school_id=(select id from tblschooldetails where schoolcode='" + usrname + "')");
            foreach (DataRow dr in dt.Rows)
            {
                usr.NoofStudent = dr["no_of_students"].ToString();
                usr.Charges = dr["per_student_charge"].ToString();
            }
            return usr;
        }

        [System.Web.Http.Route("api/Licence/upgradelicence")]
        [System.Web.Http.HttpGet]
        public string upgradelicence(string usrname, string totalamount)
        {
            try
            {
                string flg = "";
                sqlHelper obj = new sqlHelper();
                DataTable dt = obj.getDataTable("select id,valid_to from licence_details where deleted_on is null and school_id=(select id from tblSchoolDetails where schoolcode='" + usrname + "') and active='1'");
                int licenceid = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    licenceid = Convert.ToInt32(dr["id"]);
                }

                string b = LicenceBLL.savepaymentdetals(usrname, totalamount, licenceid);
                if (b != "")
                {
                    return b;
                }
                else
                {

                    return "";
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
