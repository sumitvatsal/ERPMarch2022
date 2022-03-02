using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolErp.Models;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using schoolERP_BLL;
using System.Web.Helpers;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using System.Web.Security;
    namespace SchoolErp.Controllers.WebApi
{
    public class DefaultController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();


        //

        [System.Web.Http.Route("api/Default/getAllSchoolName")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllSchoolName(List<string> loginuser)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Usertype == "2")
            {
                query = "select * from tblSchoolDetails where IsDeleted is null ";
                CountyMaster usr = new CountyMaster();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            if (Usertype == "3")
            {
                query = "select * from tblSchoolDetails where id=(select SchoolID  from tblemployee  where UserID='" + loginusername + "'  and IsDeleted is null ) ";
            }
            else if (Usertype== "1")
            {
                query = "select * from tblSchoolDetails where id=(select SchoolID  from tblemployee  where UserID='" + loginusername + "'  and IsDeleted is null ) ";

            }
            else if (Usertype == "6")
            {
                query = " select * from tblSchoolDetails  where SchoolCode='" + loginusername + "' and IsDeleted is null  ";
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["School"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }



        [System.Web.Http.Route("api/Default/getRegionName")]
        [System.Web.Http.HttpPost]
        public RegionMaster[] getRegionName(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<RegionMaster> list = new List<RegionMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Regions where Deletedate is null and SchoolID='" + SchoolID + "' ";
                RegionMaster usr = new RegionMaster();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                RegionMaster usr = new RegionMaster();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getCustomerGroup")]
        [System.Web.Http.HttpPost]
        public CgroupMaster[] getCustomerGroup(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<CgroupMaster> list = new List<CgroupMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from CustomerGroups where Deletedate is null and SchoolID='" + SchoolID + "' ";
                CgroupMaster usr = new CgroupMaster();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CgroupMaster usr = new CgroupMaster();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getservices")]
        [System.Web.Http.HttpPost]
        public ServiceMaster[] getservices(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<ServiceMaster> list = new List<ServiceMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Services where DeleteDate is null and SchoolId='"+SchoolID+"' ";
                ServiceMaster usr = new ServiceMaster();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                ServiceMaster usr = new ServiceMaster();
                usr.Name = dr["ServiceName"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/Default/getEmployee")]
        [System.Web.Http.HttpPost]
        public Employeemaster[] getEmployee(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<Employeemaster> list = new List<Employeemaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from tblEmployee where IsDeleted is null and SchoolID='" + SchoolID + "' ";
                Employeemaster usr = new Employeemaster();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Employeemaster usr = new Employeemaster();
                //usr.Name = dr["Name"].ToString();
                usr.FirstName = dr["FirstName"].ToString();
                usr.MiddleName = dr["MiddleName"].ToString();
                usr.LastName = dr["LastName"].ToString();
                usr.Name = usr.FirstName + " " + usr.MiddleName + "" + usr.LastName;
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }



        [System.Web.Http.Route("api/Default/getSuppliers")]
        [System.Web.Http.HttpPost]
        public getSuppliers[] getSuppliers(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getSuppliers> list = new List<getSuppliers>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Suppliers where Deletedate is null and SchoolID='" + SchoolID + "' ";
                getSuppliers usr = new getSuppliers();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getSuppliers usr = new getSuppliers();
                usr.Name = dr["CompanyName"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getCustomer")]
        [System.Web.Http.HttpPost]
        public getCustomer[] getCustomer(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getCustomer> list = new List<getCustomer>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Customers where Deletedate is null and SchoolID='" + SchoolID + "'";
                getCustomer usr = new getCustomer();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getCustomer usr = new getCustomer();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getBankAccounts")]
        [System.Web.Http.HttpPost]
        public getBanks[] getBankAccounts(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getBanks> list = new List<getBanks>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Accounts where PHeadName='Cash At Bank' and DeleteDate is NULL and SchoolID='" + SchoolID + "'";
                getBanks usr = new getBanks();
                usr.Name = "Cash In Hand";
                usr.Id = "14";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getBanks usr = new getBanks();
                usr.Name = dr["HeadName"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/tttt")]
        [System.Web.Http.HttpPost]
        public getBanks[] tttt(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getBanks> list = new List<getBanks>();
            sqlHelper obj = new sqlHelper();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "SELECT SUM(debit)as Debit, SUM(Credit)as credit,SUM(debit)-SUM(Credit) as Balance FROM Transactions WHERE (VDate = { fn CURDATE() }) and COA='1020204' and SchoolId='" + SchoolID+"'";
                getBanks usr = new getBanks();
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getBanks usr = new getBanks();
                usr.Name = dr["Debit"].ToString();
                usr.Id = dr["Credit"].ToString();
                
                usr.HType = "0";
                DataTable dt1 = obj.getDataTable(@" SELECT TOP 1 isnull([Amount],0) as Amount FROM [dbo].[DailyClosing] WHERE [Date] = (SELECT MAX([Date]) FROM [dbo].[DailyClosing] Where SchoolId='"+SchoolID+ "' ) and  SchoolId='" + SchoolID + "'  order by Id desc ");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    usr.HType = dr1["Amount"].ToString();
                }
                if (usr.Name =="")
                {
                    usr.Name = "0";
                }
                if (usr.Id == "")
                {
                    usr.Id = "0";
                }
                var bal = Convert.ToDouble(usr.Name) - Convert.ToDouble(usr.Id) + Convert.ToDouble(usr.HType);
                usr.PName = bal.ToString();

                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/Default/getBanks")]
        [System.Web.Http.HttpPost]
        public getBanks[] getBanks(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getBanks> list = new List<getBanks>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from banks where DeleteDate is NULL and SchoolID='" + SchoolID + "'";
                getBanks usr = new getBanks();
                usr.Name = "Cash In Hand";
                usr.Id = "14";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getBanks usr = new getBanks();
                usr.Name = dr["BankName"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getBanks1")]
        [System.Web.Http.HttpPost]
        public getBanks[] getBanks1(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getBanks> list = new List<getBanks>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "Select a.Id,a.HeadName from Accounts a where a.PHeadName in ('Cash & Cash Equivalent') and a.HeadName!='Cash At Bank' and a.SchoolID='" + SchoolID + "' union select a.id,a.HeadName from  accounts a right join Banks b on b.Id = a.BankId and b.SchoolID=a.SchoolID where a.schoolid='" + SchoolID +"' and b.DeleteUserId is null";

                //query1 = "Select a.Id,a.HeadName from Accounts a where a.PHeadName in ('Cash & Cash Equivalent') and a.HeadName!='Cash At Bank' and a.SchoolID='" + SchoolID + "'";
                //getBanks usr = new getBanks();
                //usr.Name = "Cash In Hand";
                //usr.Id = "14";
                ////  usr.Status = bool.Parse(dr["Status"].ToString());
                //list.Add(usr);
            }
           
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getBanks usr = new getBanks();
                usr.Name = dr["HeadName"].ToString();
                usr.Id = dr["Id"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }

            

            con.Close();
            return list.ToArray();
        }



        [System.Web.Http.Route("api/Default/getAccounts")]
        [System.Web.Http.HttpPost]
        public getBanks[] getAccounts(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getBanks> list = new List<getBanks>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from (select   CONVERT(varchar, HeadCode, 101) +'--'+HeadName as List,ID,HeadCode,HeadName  from Accounts where  SchoolID='" + SchoolID + "' ) as MemberNo ";
                getBanks usr = new getBanks();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getBanks usr = new getBanks();
                usr.Name = dr["List"].ToString();
                usr.Coa = dr["HeadName"].ToString();
                usr.Coaid = dr["HeadCode"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getAccounts1")]
        [System.Web.Http.HttpPost]
        public getBanks[] getAccounts1(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getBanks> list = new List<getBanks>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //string loginusername = loginuser[0].ToString();
            //string Usertype = loginuser[1].ToString();
            string query = "";
            //if (Convert.ToInt32(Usertype) >= 1)
            //{

            query = "select Id,HeadCode,HeadName,isnull(parenthead,0) as ParentHead,PHeadName,HeadLevel,HeadType from accounts where  SchoolID='" + SchoolID + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);



            foreach (DataRow dr in dt.Rows)
            {
                var i = Convert.ToInt32(dr["ParentHead"].ToString());
                var Name = "";
                while(i>0)
                {
                    var data = db.Accounts.Where(a => a.Id == i).ToList();
                    Name  = data[0].HeadName + " >> " + Name;
                    if (data[0].ParentHead == null)
                    {
                        i = 0;
                    }
                    else 
                    { 
                        i = Convert.ToInt32(data[0].ParentHead); 
                    
                    }

                }
                getBanks usr = new getBanks();
                string query1 = " select Max(HeadCode) + 1 as HeadCode,HeadLevel from accounts where [PHeadName] = '"+ dr["HeadName"].ToString() + "' and SchoolID='" + SchoolID + "' group by HeadLevel";

                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adap1.Fill(dt1);
                foreach (DataRow dr1 in dt1.Rows)
                {
                    usr.Code = dr1["HeadCode"].ToString();
                    usr.HLevel = dr1["HeadLevel"].ToString();
                }
                    
                usr.PName = dr["PHeadName"].ToString();
                usr.Name = Name + dr["HeadName"].ToString();
                usr.HType = dr["HeadType"].ToString();
                usr.Id = dr["Id"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/Default/getExpenses")]
        [System.Web.Http.HttpPost]
        public getBanks[] getExpenses(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);


            List<getBanks> list = new List<getBanks>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from ExpenseTypes where Deletedate is null and SchoolID='" + SchoolID + "' ";
                getBanks usr = new getBanks();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getBanks usr = new getBanks();
                usr.Name = dr["Type"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/Default/getpurch")]
        [System.Web.Http.HttpPost]
        public getpurch[] getpurch(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getpurch> list = new List<getpurch>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Purchases where  SchoolID='" + SchoolID + "' ";
                getpurch usr = new getpurch();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getpurch usr = new getpurch();
                var dd = "BILL-";
                usr.Id = dr["ID"].ToString();
                usr.Name = dd + usr.Id;
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }
        [System.Web.Http.Route("api/Default/getpurch1")]
        [System.Web.Http.HttpPost]
        public getpurch[] getpurch1(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<getpurch> list = new List<getpurch>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Sales where SchoolID='" + SchoolID + "'";
                getpurch usr = new getpurch();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                getpurch usr = new getpurch();
                var dd = "INV-";
                usr.Id = dr["ID"].ToString();
                usr.Name = dd + usr.Id;
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/GetUnitByProductId")]
        [System.Web.Mvc.HttpGet]
        public string GetUnitByProductId(int ProductId)
        {
            
                if(ProductId == 0)
                {
                    return string.Empty;
                }
                long unitId = db.Products.Where(x => x.Id == ProductId).FirstOrDefault().UnitId;
            var unitrecords = db.Units.Where(x => x.Id == unitId).FirstOrDefault();
            string unitname = unitrecords.Name;
            long unitid = unitrecords.Id;
            return unitname + "," + unitid ;

        }


        [System.Web.Http.Route("api/Default/GetUnit")]
        [System.Web.Mvc.HttpGet]
        public string GetUnit()
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query1 = "(SELECT Max(HeadCode) + 1 as HeadCode FROM [dbo].[Accounts] where [PHeadName] ='COA')";

            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adap1.Fill(dt1);
            string Code = "";
            foreach (DataRow dr1 in dt1.Rows)
            {
                Code = dr1["HeadCode"].ToString();
               // HLevel = dr1["HeadLevel"].ToString();
            }

           
            return Code ;

        }




        [System.Web.Http.Route("api/Default/GetUnitByProductId1")]
        [System.Web.Mvc.HttpGet]
        public string GetUnitByProductId1(int ProductId)
        {

            if (ProductId == 0)
            {
                return string.Empty;
            }
            var data = db.Accounts.Where(a => a.Id == ProductId).ToList();
            String Code = "", HLevel="", PName, HType;
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
               string query1 = " select Max(HeadCode) + 1 as HeadCode,HeadLevel from accounts where [PHeadName] = '" + data[0].HeadName + "' group by HeadLevel";

                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                adap1.Fill(dt1);
                foreach (DataRow dr1 in dt1.Rows)
                {
                    Code = dr1["HeadCode"].ToString();
                    HLevel = dr1["HeadLevel"].ToString();
                }

                PName = data[0].HeadName;
                HType = data[0].HeadType;
                
            
            con.Close();

            return Code + "," + HLevel + "," + PName + "," + HType;

        }





        [System.Web.Http.Route("api/Default/GetBankCOaid")]
        [System.Web.Mvc.HttpGet]
        public string GetBankCOaid(int ProductId)
        {

            if (ProductId == 0)
            {
                return string.Empty;
            }
            long unitId = db.Accounts.Where(x => x.Id == ProductId).FirstOrDefault().Id;
            var unitrecords = db.Accounts.Where(x => x.Id == unitId).FirstOrDefault();
            long unitname = unitrecords.HeadCode;
            long unitid = unitrecords.Id;
            return unitname + "," + unitid;

        } 

        [System.Web.Http.Route("api/Default/GetBiilNo")]
        [System.Web.Mvc.HttpGet]
        public string GetBiilNo(int ProductId)
        {

            if (ProductId == 0)
            {
                return string.Empty;
            }
            long unitId = db.Purchases.Where(x => x.Id == ProductId).FirstOrDefault().SupplierId;
            var unitrecords = db.Suppliers.Where(x => x.Id == unitId).FirstOrDefault();
            string unitname = unitrecords.CompanyName;
            long unitid = unitrecords.Id;
            return unitname + "," + unitid ;

        }

        [System.Web.Http.Route("api/Default/GetInvoiceNo")]
        [System.Web.Mvc.HttpGet]
        public string GetInvoiceNo(int ProductId)
        {

            if (ProductId == 0)
            {
                return string.Empty;
            }
            long unitId = db.Sales.Where(x => x.Id == ProductId).FirstOrDefault().CustomerId;
            var unitrecords = db.Customers.Where(x => x.Id == unitId).FirstOrDefault();
            string unitname = unitrecords.Name;
            long unitid = unitrecords.Id;
            return unitname + "," + unitid;

        }

        [System.Web.Http.Route("api/Default/GetServicecharge")]
        [System.Web.Mvc.HttpGet]
        public string GetServicecharge(int ProductId)
        {

            if (ProductId == 0)
            {
                return string.Empty;
            }
            long unitId = db.Services.Where(x => x.Id == ProductId).FirstOrDefault().Id;
            var unitrecords = db.Services.Where(x => x.Id == unitId).FirstOrDefault();
            Double unitname = unitrecords.Charge;
            long unitid = unitrecords.Id;
            return unitname + "," + unitid ;

        }


        [System.Web.Http.Route("api/Default/GetDamageProduct")]
        [System.Web.Mvc.HttpGet]
        public string GetDamageProduct(int ProductId)
        {

            if (ProductId == 0)
            {
                return string.Empty;
            }
            long unitId = db.PurchaseDetails.Where(x => x.Id == ProductId).FirstOrDefault().Id;
            var unitrecords = db.PurchaseDetails.Where(x => x.Id == unitId).FirstOrDefault();
            Double unitname = unitrecords.UnitPrice;
            Double unitid = unitrecords.Quantity;
            return unitname + "," + unitid;

        }


        [System.Web.Http.Route("api/Default/getUnits")]
        [System.Web.Http.HttpPost]
        public GetUnits[] getUnits(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<GetUnits> list = new List<GetUnits>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Units where Deletedate is null and SchoolID='" + SchoolID + "' ";
                GetUnits usr = new GetUnits();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                GetUnits usr = new GetUnits();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getBrands")]
        [System.Web.Http.HttpPost]
        public GetBrands[] getBrands(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<GetBrands> list = new List<GetBrands>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Brands where Deletedate is null and SchoolID='" + SchoolID + "' ";
                GetBrands usr = new GetBrands();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                GetBrands usr = new GetBrands();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/GetProducts")]
        [System.Web.Http.HttpPost]
        public GetProducts[] GetProducts(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<GetProducts> list = new List<GetProducts>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Products where Deletedate is null and SchoolID='" + SchoolID + "' ";
                GetProducts usr = new GetProducts();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                GetProducts usr = new GetProducts();
                usr.Name = dr["ProductName"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/GetCategory")]
        [System.Web.Http.HttpPost]
        public GetProducts[] GetCategory(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<GetProducts> list = new List<GetProducts>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "Select * from Categories where DeleteDate  is  NULL and SchoolID='" + SchoolID + "' ";
                GetProducts usr = new GetProducts();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                GetProducts usr = new GetProducts();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/GetWareHouses")]
        [System.Web.Http.HttpPost]
        public GetProducts[] GetWareHouses(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<GetProducts> list = new List<GetProducts>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Warehouses where  SchoolID='" + SchoolID + "' ";
                GetProducts usr = new GetProducts();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                GetProducts usr = new GetProducts();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getCategories")]
        [System.Web.Http.HttpPost]
        public GetCategories[] getCategories(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<GetCategories> list = new List<GetCategories>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from Categories where Deletedate is null and SchoolID='" + SchoolID + "'";
                GetCategories usr = new GetCategories();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                GetCategories usr = new GetCategories();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }



        [System.Web.Http.Route("api/Default/getSupplierGroup")]
        [System.Web.Http.HttpPost]
        public SgroupMaster[] getSupplierGroup(List<string> loginuser)
        {
            int SchoolID = Convert.ToInt32(loginuser[2]);

            List<SgroupMaster> list = new List<SgroupMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string loginusername = loginuser[0].ToString();
            string Usertype = loginuser[1].ToString();
            string query = "";
            if (Convert.ToInt32(Usertype) >= 1)
            {
                query = "select * from SupplierGroups where Deletedate is null and SchoolID='" + SchoolID + "' ";
                SgroupMaster usr = new SgroupMaster();
                usr.Name = "--Select--";
                usr.Id = "0";
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SgroupMaster usr = new SgroupMaster();
                usr.Name = dr["Name"].ToString();
                usr.Id = dr["ID"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }




        [System.Web.Http.Route("api/Default/getDDlType")]
        [System.Web.Http.HttpPost]
        public roleType[] getDDlType()
        {
            // var r= db.tblUserTypes.ToList();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            List<roleType> list = new List<roleType>();

            string query = "select * from tblUserType where status=1 and id <>2";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleType usr = new roleType();
                usr.Id = dt.Rows[i]["id"].ToString();
                usr.Name = dt.Rows[i]["Name"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();



        }

        [System.Web.Http.Route("api/Default/getDDlType1")]
        [System.Web.Http.HttpPost]
        public roleType[] getDDlType1(List<string> aa)
        {
            var type = Convert.ToInt32(aa[0].ToString());

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            List<roleType> list = new List<roleType>();
            string query = "";
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adap = new SqlDataAdapter();
            DataTable dt = new DataTable();

            if (type == 2)
            {
                query = "select * from tblUserType where status=1 and id=1";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);

            }
            else {
                query = "select * from tblUserType where status=1 and id =3";
                cmd = new SqlCommand(query, con);
                adap = new SqlDataAdapter(cmd);
            }



            adap.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleType usr = new roleType();
                usr.Id = dt.Rows[i]["id"].ToString();
                usr.Name = dt.Rows[i]["Name"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();



        }




        [System.Web.Http.Route("api/Default/SendEmailStuLogin")]
        [System.Web.Http.HttpPost]
        public string SendEmailStuLogin(StudentloginDetailsEmail1 val)
        {

            string avi = "";
            try
            {
                int SchoolID = Convert.ToInt32(val.School);
                sqlHelper obj = new sqlHelper();
                string SchoolName = obj.ExecuteScaler("select School from tblSchoolDetails where IsDeleted is null and ID='" + SchoolID + "'");
                string SchoolCODE = obj.ExecuteScaler(" select SchoolCode from tblSchoolDetails where IsDeleted is null and ID='" + SchoolID + "'");

                SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string toFmail = val.FemailId.ToString();
                string toMmail = val.MemailId.ToString();
                using (MailMessage mm1 = new MailMessage())
                {
                    StringBuilder st = new StringBuilder();




                    st.AppendLine("Hi Sir/mam,");
                    st.AppendLine("your Web Login Information");

                    st.AppendLine("Login URL :" + val.URL);
                    st.AppendLine("SCHOOL CODE :" + SchoolCODE);
                    st.AppendLine("Student UserID :" + val.SUserid);
                    st.AppendLine("Student Password :" + val.SPassword);
                    st.AppendLine("Parents UserID :" + val.PUserid);
                    st.AppendLine("Parents Password :" + val.PPassword);
                    st.AppendLine("your App Login Information");
                    st.AppendLine("SCHOOL CODE :" + SchoolCODE);
                    st.AppendLine("UserID :" + val.SUserid);
                    st.AppendLine("Password :" + val.SPassword);
                    st.AppendLine("");
                    st.AppendLine("Regards");
                    st.AppendLine(SchoolName);
                    mm1.From = new MailAddress(secObj.From); //--- Email address of the sender
                    mm1.To.Add(toFmail); //---- Email address of the recipient.
                    mm1.CC.Add(toMmail);                  
                    mm1.Subject = "Your Login Details"; //---- Subject of email.
                    mm1.Body = (st.ToString()); //---- Content of email.
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                    smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                    NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                    //---Your Email and password
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25; //---- SMTP Server port number. This varies from host to host. 
                    smtp.Send(mm1);
                    avi = "-1";





                }   


                //---Your Email and password


            }
            catch
            {
                avi = "1";
            }

            return avi;


        }

        [System.Web.Http.Route("api/Default/SendEmployeeloginDetails")]
        [System.Web.Http.HttpPost]
        public string SendEmployeeloginDetails(EmployeeloginDetailsEmail val)
        {
            string avi = "";
            try
            {
                int SchoolID = Convert.ToInt32(val.School);
                //var schoolname1 = db.tblSchoolDetails.Where(x => x.ID == SchoolID).FirstOrDefault();
                //string SchoolName = schoolname1.School;
                sqlHelper obj = new sqlHelper();
                string SchoolName = obj.ExecuteScaler("select School from tblSchoolDetails where IsDeleted is null and ID='" + SchoolID + "'");
                string SchoolCode = obj.ExecuteScaler(" select SchoolCode from tblSchoolDetails where IsDeleted is null and ID='" + SchoolID + "'");
                SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage())
                {
                    StringBuilder st = new StringBuilder();
                    st.AppendLine("Hi Sir/mam,");
                    st.AppendLine("Your Login Information");
                    st.AppendLine("SchoolCode :"+SchoolCode);
                    st.AppendLine("UserID :" + val.Userid);
                    st.AppendLine("Password :" + val.Password);
                    st.AppendLine("Login URL :" + val.URL);
                    st.AppendLine("");
                    st.AppendLine("Regards");
                    st.AppendLine(SchoolName);
                    mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                    mm.To.Add(val.emailId); //---- Email address of the recipient.
                    mm.Subject = "School Login Details"; //---- Subject of email.
                    mm.Body = (st.ToString()); //---- Content of email.

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                    smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                    NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                    //---Your Email and password
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25; //---- SMTP Server port number. This varies from host to host. 
                    smtp.Send(mm);
                    avi = "-1";
                }

               

            }
            catch 
            {
                //avi = "Email not Sent !";
                avi = "1";
            }

            return avi;
        }

        //SendEmailStuLogin
        [System.Web.Http.Route("api/Default/SendDetailsSchoolAdmin")]
        [System.Web.Http.HttpPost]
        public string SendDetailsSchoolAdmin(SchoolAdminEmail val)
        {
            string avi = "";
            try
            {

                SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage())
                {
                    StringBuilder st = new StringBuilder();
                    st.AppendLine("Hi Sir/mam,");
                    st.AppendLine("Your Login Information");
                    st.AppendLine("SchoolCode :" + val.SchoolCode);
                    st.AppendLine("UserID :" + val.SchoolCode);
                    st.AppendLine("Password :" + val.Password);
                    st.AppendLine("Login URL :" + val.URL);
                    st.AppendLine("");
                    st.AppendLine("Regards");
                    st.AppendLine("(Zeusadsolutions)");
                    mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                    mm.To.Add(val.emailId); //---- Email address of the recipient.
                    mm.Subject = "School Login Details"; //---- Subject of email.
                    mm.Body =( st.ToString()); //---- Content of email.

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                    smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                    NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                    //---Your Email and password
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25; //---- SMTP Server port number. This varies from host to host. 
                    smtp.Send(mm);
                    avi = "-1";



                  
                    avi = "-1";



                }

                //WebMail.SmtpServer = "smtpout.asia.secureserver.net";
                ////gmail port to send emails
                //WebMail.SmtpPort = 25;
                //WebMail.SmtpUseDefaultCredentials = false;
                ////sending emails with secure protocol
                //WebMail.EnableSsl = false;
                ////EmailId used to send emails from application
                //WebMail.UserName = "erp@smartvidhya.com";
                //WebMail.Password = "A9557676148@";

                ////Sender email address.
                //WebMail.From = "erp@smartvidhya.com";

                ////Send email
                //WebMail.Send(to: val.emailId.ToString().Trim(), subject: "School Login Details", body: "Your UserID :" + val.SchoolCode + " & Password :" + val.Password, isBodyHtml: true);
               
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                avi = "1";
            }
           
            return avi;

        }

        [System.Web.Http.Route("api/Default/GenrateSchoolCode")]
        [System.Web.Http.HttpPost]
        public string GenrateSchoolCode(GetSchoolcode val)
        {
            string SchoolName = val.School;
            List<GetSchoolcode> list = new List<GetSchoolcode>();
            GetSchoolcode ab = new GetSchoolcode();
            //ab.guid = Convert.ToInt32(Guid.NewGuid());

            string SchoolCode = "";

            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            int code = _rdm.Next(_min, _max);
            int codelenth = SchoolName.Length;
            if (codelenth < 3)
            {
                SchoolCode = "";
            }
            else
            {
                string schoolnamenotspace = SchoolName.Replace(" ", string.Empty);
                string schoolname1 = schoolnamenotspace.Substring(0, 3).ToUpper();

                //string SchoolnameAndCode = schoolname1.Replace(" ", string.Empty);           
                SchoolCode = schoolname1 + code;

                ab.code = SchoolCode;
            }


            return SchoolCode;








        }


        [System.Web.Http.Route("api/Default/getDDlTypeSuperAdmin")]
        [System.Web.Http.HttpPost]
        public roleType[] getDDlTypeSuperAdmin()
        {
            // var r= db.tblUserTypes.ToList();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            List<roleType> list = new List<roleType>();

            string query = "select * from tblUserType where status=1 and id=2";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roleType usr = new roleType();
                usr.Id = dt.Rows[i]["id"].ToString();
                usr.Name = dt.Rows[i]["Name"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();



        }

        //

        [System.Web.Http.Route("get/tocken")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public string gettocken(tblUser user)
        {
            bool isuserexist = db.tblUsers.Any(a => a.UserID == user.UserID && a.Pwd == user.Pwd);
            if (user != null)
            {
                Guid guid = Guid.NewGuid();
                return guid.ToString();
            }
            else
            {
                return "User Does not exist";
            }

        }



        //

        //[System.Web.Http.Route("api/Default/login")]
        //[System.Web.Http.HttpPost]
        //public Login login1([FromBody]Login person)
        //{

        //    return person;
        //}

        [System.Web.Http.Route("api/Default/Studentlogin")]
        [System.Web.Http.HttpPost]

        public Loginstudent loginStudent([FromBody]Login person)
        {

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "";
            Loginstudent obj = new Loginstudent();
            if (person.type == "4")
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(person.password));

                //get hash result after compute it  
                byte[] result = md5.Hash;

                StringBuilder strBuilderPPWD = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilderPPWD.Append(result[i].ToString("x2"));
                }
                string StudentPassword = strBuilderPPWD.ToString();

                //query = "select * from TBLStudent inner join  where SUserID='" + person.userId + "' and SPwd='" + person.password + "' and Status=3";
                query = @"select st.ID,RegNo,c.CourseName Class,FirstName,MiddleName,LastName,PicPath,
                        RollNo,s.SectionName,st.Status,st.SectionID,st.ClassId,st.SchoolID,st.AcademicYear from TBLStudent st
                        inner join tblCourses c on c.Id = st.ClassID
                        inner join tblSections s on s.Id = st.SectionID where SUserID='" + person.userId + "' and SPwd='" + StudentPassword + "' and st.Status=3";
                //select* from  tblCourses a left outer join tblSections b on a.SchoolID = b.SchoolID and b.IsDeleted is null where a.Code = '0009'
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Student std = new Student();
                    std.FirstName = dt.Rows[0]["FirstName"].ToString();
                    std.MiddleName = dt.Rows[0]["MiddleName"].ToString();
                    std.LastName = dt.Rows[0]["LastName"].ToString();
                    std.Marks = person.type;
                    std.Marks2 = person.userId;
                    std.PicPath = dt.Rows[0]["PicPath"].ToString();
                    std.Class = dt.Rows[0]["Class"].ToString() + " - " + dt.Rows[0]["SectionName"].ToString();
                    std.RegNo = dt.Rows[0]["RegNo"].ToString();
                    std.ID = Convert.ToInt16(dt.Rows[0]["ID"].ToString());
                    std.Section = dt.Rows[0]["SectionID"].ToString();
                    std.ClassID = Convert.ToInt16(dt.Rows[0]["ClassId"].ToString());
                    std.SchoolID = dt.Rows[0]["SchoolID"].ToString();
                    std.AcademicYear = dt.Rows[0]["AcademicYear"].ToString();

                    //avn
                    //var tocken = db.tblAuthentications.Create();
                    //tblAuthentication au = new tblAuthentication();
                    //au.UserID = person.userId;
                    //au.DateTime = DateTime.Now;
                    //au.UniiqueID = Guid.NewGuid();
                    //db.tblAuthentications.Add(au);
                    //db.SaveChanges();
                    //
                    person.message = "login Sucess";
                    person.Status = true;
                    person.password = "";
                    person.data = std;
                    obj.status = "200";
                    obj.message = "Sucess";
                    obj.data = person;

                    // string cls = dt.Rows[0]["Class"].ToString() + " - " + dt.Rows[0]["SectionName"].ToString();
                    //return dt.Rows[0]["FirstName"].ToString() + "***" + dt.Rows[0]["MiddleName"].ToString() + "***" + dt.Rows[0]["LastName"].ToString() + "***" + person.type + "***" + person.userId + "***" + dt.Rows[0]["PicPath"].ToString() + "***" + cls + "***" + dt.Rows[0]["RegNo"].ToString() + "***" + dt.Rows[0]["ID"].ToString() + "***" + dt.Rows[0]["SectionID"].ToString() + "***" + dt.Rows[0]["ClassId"].ToString();
                }
                else
                {
                    person.message = "Invalid User";
                    person.password = "";
                    obj.status = "200";
                    obj.message = "Invalid User";
                    obj.data = person;
                    return obj;
                }
            }
            con.Close();
            return obj;
        }

        [System.Web.Http.Route("api/Default/login")]
        [System.Web.Http.HttpPost]
        //  [WebMethod(EnableSession =true)]
        public Login login([FromBody]Login person)
        {
            try
            {
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

                SqlConnection con = new SqlConnection(constr);
                con.Open();
                string query = "";
                if (person.type == "1")
                {
                    //avneesh aswal
                    sqlHelper obj = new sqlHelper();

                    //var schoolid = db.tblSchoolDetails.Where(x => x.SchoolCode == person.SchoolCode).FirstOrDefault();
                    //int School = Convert.ToInt32( schoolid.ID);
                    string schoolid = obj.ExecuteScaler("select ID from tblSchoolDetails where IsDeleted is null and SchoolCode='" + person.SchoolCode + "' ");
                    int School = Convert.ToInt32(schoolid);

                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(person.password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilderPPWD = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilderPPWD.Append(result[i].ToString("x2"));
                    }
                    string EmpPassword = strBuilderPPWD.ToString();


                    //query = "select * from tblUser where UserID='" + person.userId + "' and Pwd='" + person.password + "' and Status=1 ";
                    //SqlCommand cmd = new SqlCommand(query, con);
                    //SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //DataTable dt = new DataTable();
                    //adap.Fill(dt);
                    //var userss = db.tblUsers.SingleOrDefault(s => s.UserID == person.userId && s.Pwd == person.password && s.Status == 1);
                    ////var userss = db.tblEmployees.SingleOrDefault(s => s.UserID == person.userId && s.Pwd == person.password);

                    //if (userss != null)
                    //{

                    //    person.user = userss;
                    //    //avn
                    //    //var tocken = db.tblAuthentications.Create();
                    //    //tblAuthentication au = new tblAuthentication();
                    //    //au.UserID = person.userId;
                    //    //au.DateTime = DateTime.Now;
                    //    //au.UniiqueID = Guid.NewGuid();
                    //    //db.tblAuthentications.Add(au);
                    //    //db.SaveChanges();
                    //    //
                    //    return person;

                    //    // return Request.CreateResponse(HttpStatusCode.OK, person); 
                    //}



                    //else
                    //{
                    //    person.msg = "Invalid User";
                    //    return person;
                    //    //return person;
                    //}
                    query = @"
select ut.Name,d.DepartmentName,desg.Designation,qualf.QualificationName,sc.Name staff
                                , g.GenderName, cit.CityName, st.StateName, c.CountryName, ms.Name Marital,
                                rg.ReligionName, em.* from tblEmployee em
                                left outer join tblDepartmnet d on d.DepartmentId = em.DeptID
                                left outer join tblDesignation desg on desg.DesigID = em.DesigID
                                left outer join tblQualifications qualf on qualf.QualificationId = em.Qualification
                                left outer join tblStaffCategory sc on sc.Id = em.StaffCategory
                                left outer join tblGender g on g.gender_id = em.GenderID
                                left outer join tblCity cit on cit.Id = em.City
                                left outer join tblState st on st.StateId = em.State
                                left outer join tblCountry c on c.CountryID = em.Country
                                left outer join tblMaritalStatus ms on ms.Id = em.MaritalSatus
                                left outer join tblReligion rg on rg.ReligionId = em.Religion 
								left outer join tblUserType ut on ut.id=em.UserType
                                where em.UserID='" + person.userId + "' and  em.Pwd='" + EmpPassword + "' and em.SchoolID='"+ School + "' and em.Status=1 and em.UserType=1 and em.isdeleted is null";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // var session = HttpContext.Current.Session;
                        //session["rahul"] = dt.Rows[0]["FirstName"].ToString();
                        EmployeeEm emusr = new EmployeeEm();
                        emusr.FName = dt.Rows[0]["FirstName"].ToString();
                        emusr.MName = dt.Rows[0]["MiddleName"].ToString();
                        emusr.LName = dt.Rows[0]["LastName"].ToString();
                        emusr.Religon = person.type;
                        emusr.UserType = person.userId;
                        emusr.ImageUpload = dt.Rows[0]["Image"].ToString();
                        emusr.Department = dt.Rows[0]["DepartmentName"].ToString();
                        emusr.Designation = dt.Rows[0]["Designation"].ToString();
                        emusr.Id = dt.Rows[0]["Id"].ToString();
                        emusr.DesigId = dt.Rows[0]["DesigID"].ToString();
                        emusr.departmentID = dt.Rows[0]["DeptID"].ToString();
                        emusr.TypeName = dt.Rows[0]["Name"].ToString();
                        emusr.SchoolID = Convert.ToInt32(dt.Rows[0]["SchoolID"]);
                        //avn
                        //var tocken = db.tblAuthentications.Create();
                        //tblAuthentication au = new tblAuthentication();
                        //au.UserID = person.userId;
                        //au.DateTime = DateTime.Now;
                        //au.UniiqueID = Guid.NewGuid();
                        //db.tblAuthentications.Add(au);
                        //db.SaveChanges();
                        //
                        person.emp = emusr;
                        string scid = Convert.ToString(emusr.SchoolID);
                        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        HttpContext.Current.Session["schoolid"] = scid;
                        //   person.msg = "Invalid User";
                        string cookies = person.userId;
                        //FormsAuthentication.SetAuthCookie(cookies,false);
                        FormsAuthentication.Initialize();
                        DateTime expires = DateTime.Now.AddHours(1);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                          cookies,
                          DateTime.Now,
                          expires, // value of time out property
                          true, // Value of IsPersistent property
                          String.Empty,
                          FormsAuthentication.FormsCookiePath);

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie authCookie = new HttpCookie(
                              FormsAuthentication.FormsCookieName,
                              encryptedTicket);
                        authCookie.Expires = expires;
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                        //
                     

                        return person;



                        //return dt.Rows[0]["FirstName"].ToString() + "***" + dt.Rows[0]["MiddleName"].ToString() + "***" + dt.Rows[0]["LastName"].ToString() + "***" + person.type + "***" + person.userId
                        //   + "***" + dt.Rows[0]["Image"].ToString() + "***" + dt.Rows[0]["DepartmentName"].ToString() + "***" + dt.Rows[0]["Designation"].ToString() + "***" + dt.Rows[0]["Id"].ToString() + "***" + dt.Rows[0]["DesigID"].ToString() + "***" + dt.Rows[0]["DeptID"].ToString();

                    }
                    else
                    {

                      

                        person.message = "Invalid User";
                        person.msg = "Invalid username & password";
                        return person;
                    }


                }

                else if (person.type == "2")
                {

                    var userss = db.tblUsers.SingleOrDefault(s => s.UserID == person.userId && s.Pwd == person.password && s.Status == 1);

                    if (userss != null)
                    {
                        //avneesh aswal
                        //SuperAdminDetails schoolsuperadmin = new SuperAdminDetails();
                        //schoolsuperadmin.userid = userss.
                        person.user = userss;
                        string cookies = person.userId;
                        //FormsAuthentication.SetAuthCookie(cookies, false); before

                        FormsAuthentication.Initialize();
                        DateTime expires = DateTime.Now.AddHours(1);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                          cookies,
                          DateTime.Now,
                          expires, // value of time out property
                          true, // Value of IsPersistent property
                          String.Empty,
                          FormsAuthentication.FormsCookiePath);

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie authCookie = new HttpCookie(
                              FormsAuthentication.FormsCookieName,
                              encryptedTicket);
                        authCookie.Expires = expires;
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                        return person;
                        // return Request.CreateResponse(HttpStatusCode.OK, person); 
                    }



                    else
                    {
                        person.message = "Invalid User";
                        person.msg = "Invalid username & password";
                        return person;
                        //return person;
                    }

                }
                else if (person.type == "3")
                {
                    //avneesh aswal
                    //var schoolid = db.tblSchoolDetails.Where(x => x.SchoolCode == person.SchoolCode).FirstOrDefault();
                    //int School = Convert.ToInt32(schoolid.ID);

                    sqlHelper obj = new sqlHelper();
                    string schoolid = obj.ExecuteScaler("select ID from tblSchoolDetails where IsDeleted is null and SchoolCode='" + person.SchoolCode+"' ");
                    int School = Convert.ToInt32(schoolid);
                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(person.password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilderPPWD = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilderPPWD.Append(result[i].ToString("x2"));
                    }
                    string EmpPassword = strBuilderPPWD.ToString();

                    query = @"select d.DepartmentName,desg.Designation,qualf.QualificationName,sc.Name staff
                                , g.GenderName, cit.CityName, st.StateName, c.CountryName, ms.Name Marital,
                                rg.ReligionName, em.* from tblEmployee em
                                left outer join tblDepartmnet d on d.DepartmentId = em.DeptID
                                left outer join tblDesignation desg on desg.DesigID = em.DesigID
                                left outer join tblQualifications qualf on qualf.QualificationId = em.Qualification
                                left outer join tblStaffCategory sc on sc.Id = em.StaffCategory
                                left outer join tblGender g on g.gender_id = em.GenderID
                                left outer join tblCity cit on cit.Id = em.City
                                left outer join tblState st on st.StateId = em.State
                                left outer join tblCountry c on c.CountryID = em.Country
                                left outer join tblMaritalStatus ms on ms.Id = em.MaritalSatus
                                left outer join tblReligion rg on rg.ReligionId = em.Religion 
                                where em.UserID='" + person.userId + "' and  em.Pwd='" + EmpPassword + "' and em.SchoolID='"+ School + "' and em.Status=1 and em.UserType=3 and em.isdeleted is null";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        // var session = HttpContext.Current.Session;
                        //session["rahul"] = dt.Rows[0]["FirstName"].ToString();
                        EmployeeEm emusr = new EmployeeEm();
                        emusr.FName = dt.Rows[0]["FirstName"].ToString();
                        emusr.MName = dt.Rows[0]["MiddleName"].ToString();
                        emusr.LName = dt.Rows[0]["LastName"].ToString();
                        emusr.Religon = person.type;
                        emusr.UserType = person.userId;
                        emusr.ImageUpload = dt.Rows[0]["Image"].ToString();
                        emusr.Department = dt.Rows[0]["DepartmentName"].ToString();
                        emusr.Designation = dt.Rows[0]["Designation"].ToString();
                        emusr.Id = dt.Rows[0]["Id"].ToString();
                        emusr.DesigId = dt.Rows[0]["DesigID"].ToString();
                        emusr.departmentID = dt.Rows[0]["DeptID"].ToString();
                        emusr.SchoolID = Convert.ToInt32(dt.Rows[0]["SchoolID"]);
                        //avn
                        //var tocken = db.tblAuthentications.Create();
                        //tblAuthentication au = new tblAuthentication();
                        //au.UserID = person.userId;
                        //au.DateTime = DateTime.Now;
                        //au.UniiqueID = Guid.NewGuid();
                        //db.tblAuthentications.Add(au);
                        //db.SaveChanges();
                        //
                        person.emp = emusr;
                        string scid = Convert.ToString(emusr.SchoolID);
                        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        HttpContext.Current.Session["schoolid"] = scid;
                        //   person.msg = "Invalid User";
                        string cookies = person.userId;
                        //FormsAuthentication.SetAuthCookie(cookies, true); before
                        FormsAuthentication.Initialize();
                        DateTime expires = DateTime.Now.AddHours(1);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                          cookies,
                          DateTime.Now,
                          expires, // value of time out property
                          true, // Value of IsPersistent property
                          String.Empty,
                          FormsAuthentication.FormsCookiePath);

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie authCookie = new HttpCookie(
                              FormsAuthentication.FormsCookieName,
                              encryptedTicket);
                        authCookie.Expires = expires;
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                        return person;
                        //return dt.Rows[0]["FirstName"].ToString() + "***" + dt.Rows[0]["MiddleName"].ToString() + "***" + dt.Rows[0]["LastName"].ToString() + "***" + person.type + "***" + person.userId
                        //   + "***" + dt.Rows[0]["Image"].ToString() + "***" + dt.Rows[0]["DepartmentName"].ToString() + "***" + dt.Rows[0]["Designation"].ToString() + "***" + dt.Rows[0]["Id"].ToString() + "***" + dt.Rows[0]["DesigID"].ToString() + "***" + dt.Rows[0]["DeptID"].ToString();

                    }
                    else
                    {
                        person.message = "Invalid User";
                        person.msg = "Invalid username & password";
                        return person;
                    }
                }
                else if (person.type == "4")
                {
                    //avneesh aswal
                    if (person.userId.Equals(null) || "".Equals(person.userId))
                    {
                        person.message = "please Enter UserID";
                        person.Status = false;
                        person.password = "";
                        return person;
                    }
                    else if (person.password.Equals(null) || "".Equals(person.password))
                    {
                        person.message = "please Enter  Password";
                        person.Status = false;
                        person.password = "";
                        return person;
                    }
                    else if (person.type.Equals(null) || "".Equals(person.type))
                    {
                        person.message = "please Enter type";
                        person.Status = false;
                        person.password = "";
                        return person;
                    }
                    else if (person.SchoolCode.Equals(null) || "".Equals(person.SchoolCode))
                    {
                        person.message = "please Enter SchoolCode";
                        person.Status = false;
                        person.password = "";
                        return person;
                    }
                    else
                    {
                        //var SchoolID = db.tblSchoolDetails.Where(X => X.SchoolCode == person.SchoolCode).FirstOrDefault();
                        //int School = Convert.ToInt32( SchoolID.ID);
                        sqlHelper obj = new sqlHelper();
                        string schoolid = obj.ExecuteScaler("select ID from tblSchoolDetails where IsDeleted is null and SchoolCode='" + person.SchoolCode + "' ");
                        int School = Convert.ToInt32(schoolid);

                        MD5 md5 = new MD5CryptoServiceProvider();

                        //compute hash from the bytes of text  
                        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(person.password));

                        //get hash result after compute it  
                        byte[] result = md5.Hash;

                        StringBuilder strBuilderPPWD = new StringBuilder();
                        for (int i = 0; i < result.Length; i++)
                        {
                            //change it into 2 hexadecimal digits  
                            //for each byte  
                            strBuilderPPWD.Append(result[i].ToString("x2"));
                        }
                        string StudentPassword = strBuilderPPWD.ToString();

                        //query = "select * from TBLStudent inner join  where SUserID='" + person.userId + "' and SPwd='" + person.password + "' and Status=3";
                        //query = @"select st.ID,RegNo,c.CourseName Class,FirstName,MiddleName,LastName,PicPath,
                        //RollNo,s.SectionName,st.Status,st.SectionID,st.ClassId,st.SchoolID,st.AcademicYear from TBLStudent st
                        //inner join tblCourses c on c.Id = st.ClassID
                        //inner join tblSections s on s.Id = st.SectionID where SUserID='" + person.userId + "' and SPwd='" + StudentPassword + "' and st.Status=3";
                        query = @"select sc.School, st.ID,RegNo,c.CourseName Class,FirstName,MiddleName,LastName,PicPath,st.AadharNo,st.FatherName,st.MotherName,st.DOB,g.GenderName,
                                 RollNo,s.SectionName,st.Status,st.SectionID,st.ClassId,st.SchoolID,st.AcademicYear from TBLStudent st
                                 inner join tblCourses c on c.Id = st.ClassID
                                 inner join tblGender g on g.gender_id = st.GenderID
                                 inner join tblSections s on s.Id = st.SectionID
                                 join tblSchoolDetails sc on st.SchoolID=sc.ID where SUserID='" + person.userId + "' and SPwd='" + StudentPassword + "' and st.SchoolID='"+ School + "' and st.Status=3 and st.IsDeleted is null";

                        //select* from  tblCourses a left outer join tblSections b on a.SchoolID = b.SchoolID and b.IsDeleted is null where a.Code = '0009'
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adap.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Student std = new Student();
                            std.FirstName = dt.Rows[0]["FirstName"].ToString();
                            std.MiddleName = dt.Rows[0]["MiddleName"].ToString();
                            std.LastName = dt.Rows[0]["LastName"].ToString();
                            std.Marks = person.type;
                            std.Marks2 = person.userId;
                            std.PicPath = dt.Rows[0]["PicPath"].ToString();
                            std.Class = dt.Rows[0]["Class"].ToString() + " - " + dt.Rows[0]["SectionName"].ToString();
                            std.RegNo = dt.Rows[0]["RegNo"].ToString();
                            std.ID = Convert.ToInt16(dt.Rows[0]["ID"].ToString());
                            std.Section = dt.Rows[0]["SectionID"].ToString();
                            std.ClassID = Convert.ToInt16(dt.Rows[0]["ClassId"].ToString());
                            std.SchoolID = dt.Rows[0]["SchoolID"].ToString();
                            std.AcademicYear = dt.Rows[0]["AcademicYear"].ToString();
                            std.School = dt.Rows[0]["School"].ToString();
                            std.FatherName = dt.Rows[0]["FatherName"].ToString();
                            std.MotherName = dt.Rows[0]["MotherName"].ToString();
                            if (dt.Rows[0]["DOB"] !=null)
                            {
                                std.DOB =Convert.ToDateTime(dt.Rows[0]["DOB"]);
                            }                           
                            std.Gender = dt.Rows[0]["GenderName"].ToString();
                            std.AadharNo= dt.Rows[0]["AadharNo"].ToString();
                            //avn
                            //var tocken = db.tblAuthentications.Create();
                            //tblAuthentication au = new tblAuthentication();
                            //au.UserID = person.userId;
                            //au.DateTime = DateTime.Now;
                            //au.UniiqueID = Guid.NewGuid();
                            //db.tblAuthentications.Add(au);
                            //db.SaveChanges();
                            //
                            person.message = "login Sucess";
                            person.Status = true;
                            person.password = "";
                            string cookies = person.userId;
                            //FormsAuthentication.SetAuthCookie(cookies, false); before
                            FormsAuthentication.Initialize();
                            DateTime expires = DateTime.Now.AddHours(1);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                              cookies,
                              DateTime.Now,
                              expires, // value of time out property
                              true, // Value of IsPersistent property
                              String.Empty,
                              FormsAuthentication.FormsCookiePath);

                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            HttpCookie authCookie = new HttpCookie(
                                  FormsAuthentication.FormsCookieName,
                                  encryptedTicket);
                            authCookie.Expires = expires;
                            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                            person.data = std;

                            // string cls = dt.Rows[0]["Class"].ToString() + " - " + dt.Rows[0]["SectionName"].ToString();
                            //return dt.Rows[0]["FirstName"].ToString() + "***" + dt.Rows[0]["MiddleName"].ToString() + "***" + dt.Rows[0]["LastName"].ToString() + "***" + person.type + "***" + person.userId + "***" + dt.Rows[0]["PicPath"].ToString() + "***" + cls + "***" + dt.Rows[0]["RegNo"].ToString() + "***" + dt.Rows[0]["ID"].ToString() + "***" + dt.Rows[0]["SectionID"].ToString() + "***" + dt.Rows[0]["ClassId"].ToString();
                        }

                        else
                        {
                            person.msg = "Invalid username & password";
                            person.message = "Invalid User";
                            person.password = "";
                            return person;
                        }
                    }
                }

                else if (person.type == "5")
                { //avneesh aswal
                    //var SchoolID = db.tblSchoolDetails.Where(X => X.SchoolCode == person.SchoolCode).FirstOrDefault();
                    //int School = Convert.ToInt32( SchoolID.ID);
                    sqlHelper obj1 = new sqlHelper();
                    string schoolid = obj1.ExecuteScaler("select ID from tblSchoolDetails where IsDeleted is null and SchoolCode='" + person.SchoolCode + "' ");
                    int School = Convert.ToInt32(schoolid);
                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(person.password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilderPPWD = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilderPPWD.Append(result[i].ToString("x2"));
                    }
                    string ParentsPassword = strBuilderPPWD.ToString();

                    //query = @"select st.ID,RegNo,c.CourseName Class,FirstName,MiddleName,LastName,PicPath,
                    //            RollNo,s.SectionName,st.Status,st.SectionID,st.ClassId,st.FatherName,st.FMobile,st.FJob,st.FDesig
                    //            ,st.FDOB,st.FOfficeAddress,st.Fmail,st.FQualification,st.AadharNo,st.MotherName,st.Mmobile,st.MJob,
                    //            st.MDesig,st.MDOB,st.MOfficeAddress,st.FIncome,st.MIncome,st.SchoolID,st.AcademicYear from TBLStudent st
                    //            inner join tblCourses c on c.Id = st.ClassID
                    //            inner join tblSections s on s.Id = st.SectionID
                    //            where PUserID='" + person.userId + "' and PPwd='" + ParentsPassword + "' and st.Status=3 and st.isdeleted is null";
                    query = @"select sc.School,st.ID,RegNo,c.CourseName Class,FirstName,MiddleName,LastName,PicPath,
                                RollNo,s.SectionName,st.Status,st.SectionID,st.ClassId,st.FatherName,st.FMobile,st.FJob,st.FDesig
                                ,st.FDOB,st.FOfficeAddress,st.Fmail,st.FQualification,st.AadharNo,st.MotherName,st.Mmobile,st.MJob,
                                st.MDesig,st.MDOB,st.MOfficeAddress,st.FIncome,st.MIncome,st.SchoolID,st.AcademicYear from TBLStudent st
                                inner join tblCourses c on c.Id = st.ClassID
                                inner join tblSections s on s.Id = st.SectionID
								join tblSchoolDetails sc on sc.ID=st.SchoolID
                                where PUserID='" + person.userId + "' and PPwd='" + ParentsPassword + "' and st.SchoolID='"+ School + "' and st.Status=3 and st.isdeleted is null";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Student obj = new Student();

                        Student std = new Student();
                        std.FirstName = dt.Rows[0]["FirstName"].ToString();
                        std.MiddleName = dt.Rows[0]["MiddleName"].ToString();
                        std.LastName = dt.Rows[0]["LastName"].ToString();
                        std.Marks = person.type;
                        std.Marks2 = person.userId;
                        std.PicPath = dt.Rows[0]["PicPath"].ToString();
                        std.Class = dt.Rows[0]["Class"].ToString() + " - " + dt.Rows[0]["SectionName"].ToString();
                        std.RegNo = dt.Rows[0]["RegNo"].ToString();
                        std.ID = Convert.ToInt16(dt.Rows[0]["ID"].ToString());
                        std.Section = dt.Rows[0]["SectionID"].ToString();
                        std.ClassID = Convert.ToInt16(dt.Rows[0]["ClassId"].ToString());
                        std.FatherName = dt.Rows[0]["FatherName"].ToString();
                        std.MotherName = dt.Rows[0]["MotherName"].ToString();
                        std.SchoolID = dt.Rows[0]["SchoolID"].ToString();
                        std.AcademicYear = dt.Rows[0]["AcademicYear"].ToString();
                        std.School = dt.Rows[0]["School"].ToString();
                        //avn
                        //var tocken = db.tblAuthentications.Create();
                        //tblAuthentication au = new tblAuthentication();
                        //au.UserID = person.userId;
                        //au.DateTime = DateTime.Now;
                        //au.UniiqueID = Guid.NewGuid();
                        //db.tblAuthentications.Add(au);
                        //db.SaveChanges();
                        //
                        string cookies = person.userId;
                        //FormsAuthentication.SetAuthCookie(cookies, true); before
                        FormsAuthentication.Initialize();
                        DateTime expires = DateTime.Now.AddHours(1);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                          cookies,
                          DateTime.Now,
                          expires, // value of time out property
                          true, // Value of IsPersistent property
                          String.Empty,
                          FormsAuthentication.FormsCookiePath);

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie authCookie = new HttpCookie(
                              FormsAuthentication.FormsCookieName,
                              encryptedTicket);
                        authCookie.Expires = expires;
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                        person.data = std;


                        //string cls = dt.Rows[0]["Class"].ToString() + " - " + dt.Rows[0]["SectionName"].ToString();
                        //return dt.Rows[0]["FirstName"].ToString() + "***" + dt.Rows[0]["MiddleName"].ToString() + "***" + dt.Rows[0]["LastName"].ToString() + "***" + person.type + "***" + person.userId +
                        //    "***" + dt.Rows[0]["PicPath"].ToString() + "***" + cls + "***" + dt.Rows[0]["RegNo"].ToString() + "***" + dt.Rows[0]["ID"].ToString() +
                        //    "***" + dt.Rows[0]["SectionID"].ToString() + "***" + dt.Rows[0]["ClassId"].ToString() + "***" + dt.Rows[0]["FatherName"].ToString() + "***" + dt.Rows[0]["MotherName"].ToString();
                    }
                    else
                     {
                        person.message = "Invalid User";
                        person.msg = "Invalid username & password";
                        return person;
                    }
                }

                else if (person.type == "6")
                {
                    //avneesh aswal
                    //var SchoolID = db.tblSchoolDetails.Where(X => X.SchoolCode == person.SchoolCode).FirstOrDefault();
                    //int School = Convert.ToInt32(SchoolID.ID);
                    sqlHelper obj = new sqlHelper();
                    string schoolid = obj.ExecuteScaler("select ID from tblSchoolDetails where IsDeleted is null and SchoolCode='" + person.SchoolCode + "' ");
                    int School = Convert.ToInt32(schoolid);
                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(person.password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilderPPWD = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilderPPWD.Append(result[i].ToString("x2"));
                    }
                    string SchoolAdminPassword = strBuilderPPWD.ToString();

                    query = @"select * from tblSchoolDetails 
                             where SchoolCode='" + person.userId + "' and  Password='" + SchoolAdminPassword + "' and ID='"+ School + "' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        // var session = HttpContext.Current.Session;
                        //session["rahul"] = dt.Rows[0]["FirstName"].ToString();
                        SchoolDetails schadmin = new SchoolDetails();
                        schadmin.School = dt.Rows[0]["School"].ToString();
                        schadmin.SchoolCode = dt.Rows[0]["SchoolCode"].ToString();
                        schadmin.ID = dt.Rows[0]["ID"].ToString();
                        string cookies = person.userId;
                        //FormsAuthentication.SetAuthCookie(cookies, true); before 
                        FormsAuthentication.Initialize();
                        DateTime expires = DateTime.Now.AddHours(1);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                          cookies,
                          DateTime.Now,
                          expires, // value of time out property
                          true, // Value of IsPersistent property
                          String.Empty,
                          FormsAuthentication.FormsCookiePath);

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        HttpCookie authCookie = new HttpCookie(
                              FormsAuthentication.FormsCookieName,
                              encryptedTicket);
                        authCookie.Expires = expires;
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                        person.schooladmin = schadmin;



                    }
                    else
                    {
                        person.message = "Invalid User";
                        person.msg = "Invalid username & password";
                        return person;
                    }

                }

                else
                {

                }


                //  return person;
                con.Close();
            }
            catch (Exception ex)
            {
                person.Status = false;
                person.message = "Something Went Wrong";
                person.msg = ex.Message;

            }
            return person;

        }

        [System.Web.Http.Route("api/Default/checklicence")]
        [System.Web.Http.HttpGet]
        public string checklicence(string username, string usertype)
        {
            string chkflag = "";
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string str = "select valid_to from licence_details where deleted_on is null and school_id=(select id from tblSchoolDetails where schoolcode='" + username + "') and active='1'";
            SqlCommand cmd = new SqlCommand(str, con);
            string validto = Convert.ToString(cmd.ExecuteScalar());
            if (validto == "")
            {
                chkflag = "";
            }
            else if (Convert.ToDateTime(validto) > DateTime.Now)
            {
                chkflag = "1";
            }
            con.Close();
            return chkflag;
        }

        [System.Web.Http.Route("api/Default/getTotalStudentsbyschool")]
        [System.Web.Http.HttpPost]
        public string getTotalStudentsbyschool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            sqlHelper obj = new sqlHelper();
            string studentCount = obj.ExecuteScaler("select count(*) from TBLStudent st inner join tblCourses c on c.Id = st.ClassID inner join tblSections s on s.Id = st.SectionID left outer join tblStatus ss on ss.StatusID = st.Status left outer join tblschooldetails scd on scd.ID = st.SchoolID where st.IsDeleted is null and st.SchoolID = '"+SchoolID+"'");
            string totaEmployeeCount = obj.ExecuteScaler("select count(*) from tblEmployee where IsDeleted is null and SchoolID='" + SchoolID + "'");
            string totalSchoolCount = obj.ExecuteScaler("select count(*) from tblSchoolDetails where IsDeleted is null and SchoolID='" + SchoolID + "'");

            //string totaEmployeeCount = obj.ExecuteScaler("select count(*)  from tblEmployee em left outer join tblDepartmnet td on td.DepartmentId = em.DeptID left outer join tblDesignation desg on desg.DesigID = em.DesigID left outer join tblStaffCategory sc on sc.Id = em.StaffCategory left outer join tblQualifications qc on qc.QualificationId = em.Qualification left outer join tblGender gen on gen.gender_id = em.GenderID left outer join tblSchoolDetails s on em.SchoolID = s.ID left outer join tblusertype ut on ut.id = em.UserType where em.IsDeleted is null");

            return studentCount + "***" + totaEmployeeCount + "***" + totalSchoolCount;
        }



        [System.Web.Http.Route("api/Default/getTotalStudents")]
        [System.Web.Http.HttpPost]
        public string getTotalStudents()
        {
            sqlHelper obj = new sqlHelper();
            string studentCount = obj.ExecuteScaler("select count(*) from TBLStudent where IsDeleted is null and schoolid in (select id from tblschooldetails where isdeleted is null)");
            string totaEmployeeCount = obj.ExecuteScaler("select count(*) from tblEmployee where Status=1 and IsDeleted is null and schoolid in (select id from tblschooldetails where isdeleted is null)");
            string totalSchoolCount = obj.ExecuteScaler("select count(*) from tblSchoolDetails where IsDeleted is null");

            //string totaEmployeeCount = obj.ExecuteScaler("select count(*)  from tblEmployee em left outer join tblDepartmnet td on td.DepartmentId = em.DeptID left outer join tblDesignation desg on desg.DesigID = em.DesigID left outer join tblStaffCategory sc on sc.Id = em.StaffCategory left outer join tblQualifications qc on qc.QualificationId = em.Qualification left outer join tblGender gen on gen.gender_id = em.GenderID left outer join tblSchoolDetails s on em.SchoolID = s.ID left outer join tblusertype ut on ut.id = em.UserType where em.IsDeleted is null");

            return studentCount + "***" + totaEmployeeCount + "***" + totalSchoolCount;
        }


        [System.Web.Http.Route("api/Default/sendEmailForAdmin")]
        [System.Web.Http.HttpPost]
        public string sendEmailForAdmin(SendEmail SchoolADMIN)
        {

               


            string avi = "";
            try
            {    
                SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage())
                {
                    StringBuilder st = new StringBuilder();
                    st.AppendLine (SchoolADMIN.Messsage);
                   
                   
                    mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                    mm.To.Add(SchoolADMIN.EmailTo); //---- Email address of the recipient.
                  
                    mm.Subject = SchoolADMIN.Subject; //---- Subject of email.
                    mm.Body = (st.ToString()); //---- Content of email.

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                    smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                    NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                    //---Your Email and password
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25; //---- SMTP Server port number. This varies from host to host. 
                    smtp.Send(mm);
                    //avi = "Email Sent Successfully Thank u!";
                    avi = "1";
                }



            }
            catch
            {
                //avi = "Email not Sent !";
                avi = "-1";
            }

            return avi;


            //string[] Email = employye.EmailTo.Split(',');

            //for (int i = 0; i < Email.Length; i++)
            //{

            //    if (!string.IsNullOrEmpty(Email[i].ToString().Trim()))
            //    {
            //        WebMail.SmtpServer = "webmail.smartvidhya.com";
            //        WebMail.SmtpPort = 25;
            //        WebMail.SmtpUseDefaultCredentials = false;
            //        WebMail.EnableSsl = false;
            //        WebMail.UserName = "Info@smartvidhya.com";
            //        WebMail.Password = "smartvidhya123@";
            //        WebMail.From = "Info@smartvidhya.com";

            //        //Send email
            //        WebMail.Send(to: Email[i].Trim(), subject: employye.Subject, body: employye.Messsage, cc: "", bcc: "", isBodyHtml: true);
            //    }

            //}

            
        }

        [System.Web.Http.Route("api/Default/saveTaskToDo")]
        [System.Web.Http.HttpPost]
        public string saveTaskToDo(SendEmail employye)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            if (string.IsNullOrEmpty(employye.Id))
            {
                tblTaskToDo td = new tblTaskToDo();
                td.Subject = employye.Subject;
                td.Message = employye.Messsage;
                td.TaskDate = employye.date;
                td.UserType = "1";
                td.SchoolID = employye.SchoolID;
                db.tblTaskToDoes.Add(td);
                db.SaveChanges();
                return "To Do Saved Successfully";
            }
            else
            {
                int id = Convert.ToInt32(employye.Id);
                var result = db.tblTaskToDoes.SingleOrDefault(s => s.Id == id);
                result.Subject = employye.Subject;
                result.Message = employye.Messsage;
                result.TaskDate = employye.date;
                result.UserType = "1";
                result.SchoolID = employye.SchoolID;
                //  db.tblTaskToDoes.Add(result);
                db.SaveChanges();
                return "To Do Updated Successfully";



            }
        }

        [System.Web.Http.Route("api/Default/getAllToDoList")]
        [System.Web.Http.HttpPost]
        public SendEmail[] getAllToDoList([FromBody]List<string> val)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<SendEmail> list = new List<SendEmail>();
            DateTime date = Convert.ToDateTime(val[0]);
            int SchoolID = Convert.ToInt32(val[1]);
            var result = db.tblTaskToDoes.Where(s => s.TaskDate >= date && s.SchoolID == SchoolID && s.IsDeleted == null).OrderByDescending(s => s.TaskDate).ToList();
            foreach (var a in result)
            {
                SendEmail usr = new SendEmail();
                usr.Subject = a.Subject;
                usr.Messsage = a.Message;
                usr.Id = a.Id.ToString();
                usr.TaskDate = ((DateTime)a.TaskDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); ;
                list.Add(usr);

            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/Default/deleteTaskToDoById")]
        [System.Web.Http.HttpPost]
        public string deleteTaskToDoById(List<string> val)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            int Id = Convert.ToInt32(val[0]);
            //   var result = new tblTaskToDo{ Id = Id };
            // db.Entry(result).State = System.Data.Entity.EntityState.Deleted;
            //var resul = db.tblTaskToDoes.Where(s => s.Id == Id);
            //foreach (var r in resul)
            //{
            //    db.tblTaskToDoes.Remove(r);
            //}
            var result = db.tblTaskToDoes.Where(a => a.Id == Id).ToList();
            result.ForEach(a =>
            {
                a.IsDeleted = 1;
                a.Deleted_on = DateTime.Now;
            });
            db.SaveChanges();

            return "Task To Do Deleted Successfully thank u!";
        }

        [System.Web.Http.Route("api/Default/getTeacherBirthday")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getTeacherBirthday(List<string> val)
        {

            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<EmployeeEm> list = new List<EmployeeEm>();
            DateTime date = Convert.ToDateTime(val[0]); //Convert.ToDateTime(val[0]);
            sqlHelper obj = new sqlHelper();
            int? schid = Convert.ToInt32(obj.ExecuteScaler("select schoolid from tblemployee where isdeleted is null and userid='" + val[1] + "'"));
            if (schid == null)
            {
                schid = 0;
            }
            var result = (from e in db.tblEmployees
                          join d in db.tblDepartmnets on e.DeptID equals d.DepartmentId
                          where e.Status == true && e.DOB.Day == date.Day && e.DOB.Month == date.Month && e.SchoolID == schid && e.IsDeleted==null
                          select new
                          {
                              d.DepartmentName,
                              e
                          }
                          ).ToList();
            foreach (var rs in result)
            {
                EmployeeEm usr = new EmployeeEm();
                usr.FName = rs.e.FirstName + " " + rs.e.MiddleName + " " + rs.e.LastName;
                usr.Department = rs.DepartmentName;

                usr.DOB = ((DateTime)rs.e.DOB).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                list.Add(usr);
            }

            return list.ToArray();
        }


        [System.Web.Http.Route("api/Default/saveTaskToDoForEmployee")]
        [System.Web.Http.HttpPost]
        public string saveTaskToDoForEmployee(SendEmail employye)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            if (string.IsNullOrEmpty(employye.Id))
            {
                tblTaskToDoForEmployee td = new tblTaskToDoForEmployee();
                td.Subject = employye.Subject;
                td.Message = employye.Messsage;
                td.TaskDate = employye.date;
                td.UserType = "3";
                td.EmployeeId = employye.EmpId;
                td.SchoolID = employye.SchoolID;
                db.tblTaskToDoForEmployees.Add(td);
                db.SaveChanges();
                return "To Do Saved Successfully";
            }
            else
            {
                int id = Convert.ToInt32(employye.Id);
                var result = db.tblTaskToDoForEmployees.SingleOrDefault(s => s.Id == id);
                result.Subject = employye.Subject;
                result.Message = employye.Messsage;
                result.TaskDate = employye.date;
                result.EmployeeId = employye.EmpId;
                result.SchoolID = employye.SchoolID;
                result.UserType = "3";
                //  db.tblTaskToDoes.Add(result);
                db.SaveChanges();
                return "To Do Updated Successfully";



            }
        }


        [System.Web.Http.Route("api/Default/getAllToDoListForEmployee")]
        [System.Web.Http.HttpPost]
        public SendEmail[] getAllToDoListForEmployee(SendEmail se)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<SendEmail> list = new List<SendEmail>();
            DateTime date = Convert.ToDateTime(se.date);

            var result = db.tblTaskToDoForEmployees.Where(s => s.TaskDate >= date && s.EmployeeId == se.EmpId && s.SchoolID == se.SchoolID && s.IsDeleted == null).OrderByDescending(s => s.TaskDate).ToList();
            foreach (var a in result)
            {
                SendEmail usr = new SendEmail();
                usr.Subject = a.Subject;
                usr.Messsage = a.Message;
                usr.Id = a.Id.ToString();
                usr.TaskDate = ((DateTime)a.TaskDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); ;
                list.Add(usr);

            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/Default/deleteTaskToDoForEmployeById")]
        [System.Web.Http.HttpPost]
        public string deleteTaskToDoForEmployeById(SendEmail se)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            int Id = Convert.ToInt32(se.Id);
            //   var result = new tblTaskToDo{ Id = Id };
            // db.Entry(result).State = System.Data.Entity.EntityState.Deleted;
            //var resul = db.tblTaskToDoForEmployees.Where(s => s.Id == Id && s.EmployeeId==se.EmpId);
            //foreach (var r in resul)
            //{
            //    db.tblTaskToDoForEmployees.Remove(r);
            //}
            //var result = db.tblTaskToDoes.SingleOrDefault(a => a.Id == Id);
            //result.IsDeleted = 1;
            //result.Deleted_on = DateTime.Now;
            //db.SaveChanges();
            var resul = db.tblTaskToDoForEmployees.Where(a => a.Id == Id && a.EmployeeId == se.EmpId).FirstOrDefault();

            resul.IsDeleted = 1;
            resul.Deleted_on = DateTime.Now;
            db.SaveChanges();
            //  resul.ForEach(s => { s.IsDeleted = 1;s.Deleted_on = DateTime.Now; });



            return "Task To Do Deleted Successfully thank u!";
        }

        [System.Web.Http.Route("api/Default/sendEmailForEmployee")]
        [System.Web.Http.HttpPost]
        public string sendEmailForEmployee(SendEmail employye)
        {
            sqlHelper obj = new sqlHelper();
            //string EmailFrom = obj.ExecuteScaler("select Email from tblEmployee where SchoolID='"+employye.SchoolID+"' and Id=" + employye.EmpId);
           
            //if (string.IsNullOrEmpty(EmailFromname))
            //{
            //    EmailFromname = "smartvidhya.com";
            //}
            string avi = "";
            try
            {



                SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage())
                {
                    StringBuilder st = new StringBuilder();
                    st.AppendLine(employye.Messsage);


                    mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                    mm.To.Add(employye.EmailTo); //---- Email address of the recipient.

                    mm.Subject = employye.Subject; //---- Subject of email.
                    mm.Body = (st.ToString()); //---- Content of email.

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                    smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                    NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                    //---Your Email and password
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25; //---- SMTP Server port number. This varies from host to host. 
                    smtp.Send(mm);
                    //avi = "Email Sent Successfully Thank u!";
                    avi = "1";
                }



            }
            catch
            {
                avi = "Email not Sent !";
                avi = "-1";
               
            }

            return avi;

            //string[] Email = employye.EmailTo.Split(',');

            //for (int i = 0; i < Email.Length; i++)
            //{

            //    if (!string.IsNullOrEmpty(Email[i].ToString().Trim()))
            //    {
            //        WebMail.SmtpServer = "webmail.smartvidhya.com";
            //        WebMail.SmtpPort = 25;
            //        WebMail.SmtpUseDefaultCredentials = false;
            //        WebMail.EnableSsl = false;
            //        WebMail.UserName = "Info@smartvidhya.com";
            //        WebMail.Password = "smartvidhya123@";
            //        WebMail.From = EmailFrom;

            //        //Send email
            //        WebMail.Send(to: Email[i].Trim(), subject: employye.Subject, body: employye.Messsage, cc: "", bcc: "", isBodyHtml: true);
            //    }

            //}

            //return "Email Sent Successfully Thank u!";
        }

        [System.Web.Http.Route("api/Default/getStudentsBirthday")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getStudentsBirthday(SendEmail se)
        {
            sqlHelper obj = new sqlHelper();
            //List<EmployeeEm> list = new List<EmployeeEm>();
            //DateTime date = Convert.ToDateTime(se.date);
            //string classId = obj.ExecuteScaler("select ClassID from tblClassTeacherAllocation where EmpID=" + se.EmpId);
            //string sectionId = obj.ExecuteScaler("select SectionID from tblClassTeacherAllocation where EmpID=" + se.EmpId);
            //if (!string.IsNullOrEmpty(classId))
            //{

            //    DataTable dt = obj.getDataTable(@"select * from TBLStudent  where ClassID=" + classId + "  and SectionID=" + sectionId + "  and DOB='" + date + "' and Status=3");
            //    if (dt.Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            EmployeeEm usr = new EmployeeEm();
            //            usr.FName = dr["FirstName"] + " " + dr["MiddleName"] + " " + dr["LastName"].ToString();
            //            usr.DOB = ((DateTime)dr["DOB"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); ;
            //            list.Add(usr);
            //        }
            //    }

            //}
            //return list.ToArray();

            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<EmployeeEm> list = new List<EmployeeEm>();
            DateTime date = se.date;
            string classId = obj.ExecuteScaler("select ClassID from tblClassTeacherAllocation where intEmpID=" + se.EmpId);
            int cls = Convert.ToInt32(classId);
            string sectionId = obj.ExecuteScaler("select SectionID from tblClassTeacherAllocation where intEmpID=" + se.EmpId);
            int sec = Convert.ToInt32(sectionId);
            if (!string.IsNullOrEmpty(classId))
            {
                var reslt = db.TBLStudents.Where(x => x.ClassID == cls && x.SectionID == sec && x.DOB.Value.Day == date.Day && x.DOB.Value.Month == date.Month && x.Status == 3).ToList();

                foreach (var r in reslt)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.FName = r.FirstName + " " + r.MiddleName + " " + r.LastName;
                    usr.DOB = ((DateTime)r.DOB).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); ;
                    list.Add(usr);
                }

            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getSchoolDetailsId")]
        [System.Web.Http.HttpPost]
        public string getSchoolDetailsId()
        {
            sqlHelper obj = new sqlHelper();
            string SchoolId = obj.ExecuteScaler("select top 1 ID from tblSchoolDetails order by DateCreated desc");
            if (string.IsNullOrEmpty(SchoolId))
            {
                return "0";
            }
            else
            {

                return SchoolId;
            }





        }

        [System.Web.Http.Route("api/Default/getSchoolDetailsBySchoolID")] //getSchoolDetails
        [System.Web.Http.HttpPost]
        public SchoolDetails getSchoolDetailsBySchoolID(List<string> id)
        {
            List<SchoolDetails> list = new List<SchoolDetails>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@"select u.Signature,s.* from  tblSchoolDetails s
                                                left outer join tblUser u on u.ID=s.PrincipalID
												where s.ID='"+id[0]+"' and IsDeleted is null");


            SchoolDetails usr = new SchoolDetails();
            if (dr.Read())
            {
                // usr.ID = Int64.Parse(dr["Id"].ToString());
                usr.LogoPic = dr["LogoPic"].ToString();
                usr.School = dr["School"].ToString();
                usr.Address = dr["Address"].ToString();
                usr.SchoolCode = dr["SchoolCode"].ToString();
                usr.Country = dr["CountryID"].ToString();
                usr.State = dr["State"].ToString();
                usr.City = dr["CityID"].ToString();
                usr.District = dr["District"].ToString();
                usr.Phone = dr["Phone"].ToString();
                usr.Pincode = dr["Pincode"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                usr.Email = dr["Email"].ToString();
                usr.Fax = dr["Fax"].ToString();
                usr.Board = dr["Board"].ToString();
                usr.Website = dr["Website"].ToString();
                usr.Feedueday = Convert.ToInt32(dr["FeeDueDay"]);
                //usr.PrincipalID = int.Parse(dr["PrincipalID"].ToString());
                // string sign= obj.ExecuteScaler("select Signature from tblUser where ID=" + dr["PrincipalID"].ToString());
                //usr.principleSign = dr["Signature"].ToString();


            }

            return usr;

        }

        [System.Web.Http.Route("api/Default/getSchoolDetails")]
        [System.Web.Http.HttpPost]
        public SchoolDetails getSchoolDetails()
        {
            List<SchoolDetails> list = new List<SchoolDetails>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader(@"select u.Signature,s.* from  tblSchoolDetails s
                                                left outer join tblUser u on u.ID=s.PrincipalID
												where IsDeleted is null");


            SchoolDetails usr = new SchoolDetails();
            if (dr.Read())
            {
                // usr.ID = Int64.Parse(dr["Id"].ToString());
                usr.LogoPic = dr["LogoPic"].ToString();
                usr.School = dr["School"].ToString();
                usr.Address = dr["Address"].ToString();
                usr.SchoolCode = dr["SchoolCode"].ToString();
                usr.State = dr["State"].ToString();
                usr.City = dr["City"].ToString();
                usr.District = dr["District"].ToString();
                usr.Phone = dr["Phone"].ToString();
                usr.Pincode = dr["Pincode"].ToString();
                usr.Mobile = dr["Mobile"].ToString();
                usr.Email = dr["Email"].ToString();
                usr.Fax = dr["Fax"].ToString();
                usr.Board = dr["Board"].ToString();
                usr.Website = dr["Website"].ToString();
                usr.PrincipalID = int.Parse(dr["PrincipalID"].ToString());
                // string sign= obj.ExecuteScaler("select Signature from tblUser where ID=" + dr["PrincipalID"].ToString());
                usr.principleSign = dr["Signature"].ToString();


            }

            return usr;

        }


        [System.Web.Http.Route("api/Default/getAllState")]
        [System.Web.Http.HttpPost]
        public List<StateMaster> getAllState()
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<StateMaster> list = new List<StateMaster>();
            var result = db.tblStates.Where(s => s.status == true).ToList();
            foreach (var a in result)
            {
                StateMaster usr = new StateMaster();
                usr.Id = a.StateId.ToString();
                usr.Name = a.StateName;
                list.Add(usr);
            }
            return list;

        }

        [System.Web.Http.Route("api/Default/SaveSchoolDetailsEdit")]
        [System.Web.Http.HttpPost]
        public string SaveSchoolDetailsEdit(SchoolDetails sd)

        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();         
            double SchoolID = Convert.ToInt64( sd.ID);
            var result = db.tblSchoolDetails.SingleOrDefault(s => s.ID == SchoolID);
            result.School = sd.School;
            result.SchoolCode = sd.SchoolCode;
            result.Board = sd.Board;
            result.Website = sd.Website;
            result.Email = sd.Email;
            result.Fax = sd.Fax;
            result.Address = sd.Address;
            //result.CountryID = Convert.ToInt32(sd.Country);
            result.CountryID = Convert.ToInt32( sd.Country);
            result.State = Convert.ToInt32(sd.State);
            result.CityID = sd.City;
            result.District = sd.District;
            result.Pincode = Convert.ToInt64(sd.Pincode);
            result.Mobile = sd.Mobile;
            result.Phone = sd.Phone;
            result.DateCreated = DateTime.Now;
            result.FeeDueDay = sd.Feedueday;
            db.SaveChanges();
            return result.ID.ToString();
            


        }

        //[System.Web.Http.Route("api/Default/SaveSchoolImage")]
        //[System.Web.Http.HttpPost]
        //public string updateSchoolDetails(SchoolDetails sd)

        //{
        //    SCHOOLERPEntities db = new SCHOOLERPEntities();
        //    sqlHelper obj = new sqlHelper();
        //    string principleId = obj.ExecuteScaler("select top 1 ID  from tblUser");
        //    if (string.IsNullOrEmpty(sd.ID.ToString()) || sd.ID == 0)
        //    {
        //        tblSchoolDetail usr = new tblSchoolDetail();
        //        usr.School = sd.School;
        //        usr.SchoolCode = sd.SchoolCode;
        //        usr.Board = sd.Board;
        //        usr.Website = sd.Website;
        //        usr.Email = sd.Email;
        //        usr.Fax = sd.Fax;
        //        usr.Address = sd.Address;
        //        usr.CountryID = Convert.ToInt32(sd.Country);
        //        usr.State = Convert.ToInt32(sd.State);
        //        usr.CityID = Convert.ToInt32(sd.City);
        //        usr.District = sd.District;
        //        usr.Pincode = Convert.ToInt64(sd.Pincode);
        //        usr.Mobile = sd.Mobile;
        //        usr.Phone = sd.Phone;
        //        usr.PrincipalID = Convert.ToInt32(principleId);
        //        usr.DateCreated = DateTime.Now;
        //        db.tblSchoolDetails.Add(usr);
        //       // db.tblSchoolDetails.Add(usr);
        //        db.SaveChanges();
        //        return usr.ID.ToString();

        //    }
        //    else
        //    {
        //       var result = db.tblSchoolDetails.SingleOrDefault(s => s.ID == sd.ID);
        //        result.School = sd.School;
        //        result.SchoolCode = sd.SchoolCode;
        //        result.Board = sd.Board;
        //        result.Website = sd.Website;
        //        result.Email = sd.Email;
        //        result.Fax = sd.Fax;
        //        result.Address = sd.Address;
        //        result.CountryID = Convert.ToInt32(sd.Country);
        //        result.State = Convert.ToInt32(sd.State);
        //        result.CityID = Convert.ToInt32(sd.City);
        //        result.District = sd.District;
        //        result.Pincode = Convert.ToInt64(sd.Pincode);
        //        result.Mobile = sd.Mobile;
        //        result.Phone = sd.Phone;
        //        result.DateCreated = DateTime.Now;
        //        db.SaveChanges();
        //        return "School Details Updated Successfully";

        //    }
        //}
        //Gallery Image
        [System.Web.Http.Route("api/Default/SaveGalleyDetails")]
        [System.Web.Http.HttpPost]
        public string SaveGalleyDetails(List<string> Id)
        {
            string a = "-1";
            try
            {
                if (Id[4] == "0")
                {
                    tblGalleryImage gall = new tblGalleryImage();
                    gall.SchoolID = Convert.ToInt32(Id[0]);
                    gall.ImageTitle = Id[1].ToString();
                    gall.ImageDescription = Id[2].ToString();
                    gall.status = Convert.ToBoolean(Id[3]);
                    db.tblGalleryImages.Add(gall);

                    db.SaveChanges();
                    a = gall.ID.ToString();
                }
                else
                {
                    int idd = Convert.ToInt32(Id[4]);
                    tblGalleryImage gall = new tblGalleryImage();
                    var result = db.tblGalleryImages.Where(g => g.ID == idd).FirstOrDefault();
                    result.ImageTitle = Id[1];
                    result.ImageDescription = Id[2];
                    result.status = Convert.ToBoolean(Id[3]);
                    db.SaveChanges();
                    a = idd.ToString();


                }
            }
            catch
            {
                a = "-1";
            }
         
               
            
        
            //else
            //{
            //    a = "-1";

            //}

         


           
           
            return a;

        }

        [System.Web.Http.Route("api/Default/GetAllGalleryImage")]
        [System.Web.Http.HttpPost]
        public GalleryImage[] GetAllGalleryImage(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            int avi = 0;
            List<GalleryImage> list = new List<GalleryImage>();

            var gallery = db.tblGalleryImages.Where(X => X.SchoolID == SchoolID && X.IsDeleted==null).ToList();
            foreach (var a in gallery)
            {
                avi++;
                GalleryImage img = new GalleryImage();
                img.ID = a.ID;
                img.Sno = avi;
                img.image = a.GalleryImage;
                img.SchoolID = Convert.ToInt32(a.SchoolID);
                img.ImageTitle = a.ImageTitle;
                img.ImageDesc = a.ImageDescription;
                img.status = a.status;
                if ( img.status==true)
                {
                    img.StatusShow = "Active";
                }
                else
                {
                    img.StatusShow = "DeActive";
                }
                list.Add(img);
            }

            return list.ToArray();
        }




        [System.Web.Http.Route("api/Default/deleteGalleryImageById")]
        [System.Web.Http.HttpGet]
        public string deleteGalleryImageById(Int32 id)
        {
            GalleryImage gall = new GalleryImage();
            int idd = Convert.ToInt32(id);
            var gallery = db.tblGalleryImages.Where(x => x.ID == idd).FirstOrDefault();
            gallery.IsDeleted = 1;
            gallery.Deleted_on = DateTime.Now;
            db.SaveChanges();          

            return "Gallery Image Deleted Successfully";

        }

        [System.Web.Http.Route("api/StudentApi/SchoolGalleryapi")]
        [System.Web.Http.HttpPost]
        public GalleryImageAPP SchoolGalleryapi(Gallery VAL)
        {
            GalleryImageAPP OBJ = new GalleryImageAPP();
            Gallery ga = new Models.Gallery();
            ga.GalleryImages = new List<GalleryImagess>();
            
            
            ga.SchoolID = VAL.SchoolID;
            try
            {
                if (VAL.SchoolID.Equals(0) || VAL.SchoolID.Equals(null) || "".Equals(VAL.SchoolID))
                {
                    OBJ.status = false;
                    OBJ.message = "Please Enter SchoolID";

                }
                else
                {
                    int a1 = 0;
                    int SchoolID = Convert.ToInt32(VAL.SchoolID);
                    var gallery = db.tblGalleryImages.Where(g => g.SchoolID == SchoolID && g.IsDeleted == null).ToList();
                    foreach (var GAL in gallery)
                    {
                        GalleryImagess a = new GalleryImagess();
                        a1++;
                        a.imagePath = GAL.GalleryImage;
                        a.ImageTitle = GAL.ImageTitle;
                        a.ImageDescription = GAL.ImageDescription;
                        a.status = GAL.status;
                        if (a.imagePath != null && a.imagePath != "")
                        {
                            ga.GalleryImages.Add(a);
                        }
                    }
                    if (a1 != 0)
                    {
                        OBJ.status = true;
                        OBJ.message = "success";
                        OBJ.data = ga;
                    }
                    else
                    {
                        OBJ.status = true;
                        OBJ.message = "Data Not Found";
                        OBJ.data = ga;
                    }


                }


            }
            catch
            {
                OBJ.status = false;
                OBJ.message = "Something Went Wrong";
                OBJ.data = ga;

            }


            return (OBJ);

        }

        [System.Web.Http.Route("api/Default/SaveSchoolDetails")]
        [System.Web.Http.HttpPost]
        public string SaveSchoolDetails(SchoolDetails School)
        {
            sqlHelper obj = new sqlHelper();
           

            string b = SchoolDetails.saveSchoolDetails(School);
            if (b != "")
            {
                
                return b;
            }
            else
            {
                return "";
            }
        }

        [System.Web.Http.Route("api/Default/SaveGalleryImage1")]
        [System.Web.Http.HttpPost]
        public string SaveGalleryImage()
        {

            try
            {
                int jk = 0;

                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {

                    string Id = HttpContext.Current.Request.Params["Id"];
                    //  string EmployeeCode = HttpContext.Current.Request.Params["EmployeeCode"];
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    string ImageExtensions = "";
                    string ImageExtensions1 = "";
                    string ImageFileName = "";
                    string ImageFileName1 = "";
                    string ImageFile = "";
                    string ImageFile1 = "";
                    string ImageFileSTRING = "";
                    string FullImagePath = "/Images/SchoolGalleryImage/";
                    //string FullImagePath2 = "/Images/Employee/Documents/";
                    string serverPath = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath);
                    //string serverPath2 = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath2);
                    if (httpPostedFile != null)
                    {

                        // Validate the uploaded image(optional) 

                        // Get the complete file path
                        ImageExtensions = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage"].FileName).ToString();
                        if (ImageExtensions.ToLower() == ".jpg" || ImageExtensions.ToLower() == ".png" || ImageExtensions.ToLower() == ".jpeg" /*||*/ /*ImageExtensions.ToLower() == ".gif" ||*/ /*ImageExtensions.ToLower() == ".pdf"*/)
                        {
                            ImageFile = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage"].FileName);
                            System.IO.FileInfo filename1 = new System.IO.FileInfo(serverPath + ImageFile);

                            //Guid gB = Guid.NewGuid();
                            //string imagenamestring = Convert.ToBase64String(gB.ToByteArray());
                            //imagenamestring = imagenamestring.Replace("=", "");
                            //imagenamestring = imagenamestring.Replace("+", "");

                            string imagenamestring = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + System.IO.Path.GetRandomFileName().Replace(".", string.Empty) + ImageExtensions;
                            //  ImageFileSTRING = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString()+  imagenamestring + ImageExtensions;

                            ImageFileName = imagenamestring;
                            ImageFile = FullImagePath + imagenamestring;
                            HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                            sqlHelper obj = new sqlHelper();

                            //  ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

                            //ImageFileName = ImageFile;
                            //ImageFile = FullImagePath + ImageFile;
                            //HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                            //sqlHelper obj = new sqlHelper();
                            string fileimage = "http:/" + "/www.smartvidhya.com" + ImageFile;
                            string[] cols = { "GalleryImage" };
                            object[] vals = { fileimage };
                            obj.updateValIntoTable("tblgalleryImages", cols, vals, "Id", Id);
                            jk++;
                        }

                    }

                }

                if (jk == 0)
                {
                    return "-1";
                }
                else
                {
                    return "1";
                   
                }
              

            }
            catch (Exception)
            {

                throw;
            }
        }





        [System.Web.Http.Route("api/Default/SaveSchoolImage")]
        [System.Web.Http.HttpPost]
        public string SaveSchoolImage()
        {

            try
            {
                int jk = 0;

                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {

                    string Id = HttpContext.Current.Request.Params["Id"];
                    //  string EmployeeCode = HttpContext.Current.Request.Params["EmployeeCode"];
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    string ImageExtensions = "";
                    string ImageExtensions1 = "";
                    string ImageFileName = "";
                    string ImageFileName1 = "";
                    string ImageFile = "";
                    string ImageFile1 = "";
                    string ImageFileSTRING = "";
                    string FullImagePath = "/Images/School/SchoolImage/";
                    //string FullImagePath2 = "/Images/Employee/Documents/";
                    string serverPath = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath);
                    //string serverPath2 = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath2);
                    if (httpPostedFile != null)
                    {

                        // Validate the uploaded image(optional) 

                        // Get the complete file path
                        ImageExtensions = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage"].FileName).ToString();
                        if (ImageExtensions.ToLower() == ".jpg" || ImageExtensions.ToLower() == ".png" || ImageExtensions.ToLower() == ".jpeg" /*||*/ /*ImageExtensions.ToLower() == ".gif" ||*/ /*ImageExtensions.ToLower() == ".pdf"*/)
                        {
                            ImageFile = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage"].FileName);
                            System.IO.FileInfo filename1 = new System.IO.FileInfo(serverPath + ImageFile);

                            //Guid gB = Guid.NewGuid();
                            //string imagenamestring = Convert.ToBase64String(gB.ToByteArray());
                            //imagenamestring = imagenamestring.Replace("=", "");
                            //imagenamestring = imagenamestring.Replace("+", "");

                            string imagenamestring = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + System.IO.Path.GetRandomFileName().Replace(".", string.Empty) + ImageExtensions;
                            //  ImageFileSTRING = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString()+  imagenamestring + ImageExtensions;

                            ImageFileName = imagenamestring;
                            ImageFile = FullImagePath + imagenamestring;
                            HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                            sqlHelper obj = new sqlHelper();

                            //  ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

                            //ImageFileName = ImageFile;
                            //ImageFile = FullImagePath + ImageFile;
                            //HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                            //sqlHelper obj = new sqlHelper();
                            string fileimage = "http:/"+"/www.smartvidhya.com"+ ImageFile;
                            string[] cols = { "LogoPic" };
                            object[] vals = { fileimage };
                            obj.updateValIntoTable("tblSchoolDetails", cols, vals, "Id", Id);
                        }

                    }

                }

                if (jk == 0)
                {
                    return "School Inserted Successfully";
                }
                else
                {
                    return "School Updated Successfully";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }







        //[System.Web.Http.Route("api/Default/SaveSchoolImage")]
        //[System.Web.Http.HttpPost]
        //public string SaveSchoolImage()
        //{

        //    try
        //    {
        //        int jk = 0;

        //        if (HttpContext.Current.Request.Files.AllKeys.Any())
        //        {

        //            string Id = HttpContext.Current.Request.Params["Id"];
        //            //  string EmployeeCode = HttpContext.Current.Request.Params["EmployeeCode"];
        //            // Get the uploaded image from the Files collection
        //            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
        //            string ImageExtensions = "";
        //            string ImageExtensions1 = "";
        //            string ImageFileName = "";
        //            string ImageFileName1 = "";
        //            string ImageFile = "";
        //            string ImageFile1 = "";
        //            string FullImagePath = "/Images/School/SchoolImage/";
        //            //string FullImagePath2 = "/Images/Employee/Documents/";
        //            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath);
        //            //string serverPath2 = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath2);
        //            if (httpPostedFile != null)
        //            {

        //                // Validate the uploaded image(optional) 

        //                // Get the complete file path
        //                ImageExtensions = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage"].FileName).ToString();
        //                if (ImageExtensions.ToLower() == ".jpg" || ImageExtensions.ToLower() == ".png" || ImageExtensions.ToLower() == ".jpeg" || ImageExtensions.ToLower() == ".gif" || ImageExtensions.ToLower() == ".pdf")
        //                {
        //                    ImageFile = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage"].FileName);
        //                    System.IO.FileInfo filename1 = new System.IO.FileInfo(serverPath + ImageFile);

        //                    ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

        //                    ImageFileName = ImageFile;
        //                    ImageFile = FullImagePath + ImageFile;
        //                    HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
        //                    sqlHelper obj = new sqlHelper();
        //                    string[] cols = { "LogoPic" };
        //                    object[] vals = { ImageFile };
        //                    obj.updateValIntoTable("tblSchoolDetails", cols, vals, "Id", Id);
        //                }

        //            }

        //        }

        //        if (jk == 0)
        //        {
        //            return "School Inserted Successfully";
        //        }
        //        else
        //        {
        //            return "School Updated Successfully";
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



        [System.Web.Http.Route("api/Default/GetAllSchoolDetailBySchool")]
        [System.Web.Http.HttpPost]
        public SchoolDETAILSAPP GetAllSchoolDetailBySchool(SchoolDetails1 val)
        {
            int avi = 0;
            sqlHelper obj = new sqlHelper();
            SchoolDETAILSAPP OBJ = new SchoolDETAILSAPP();
            List<SchoolDetails1> list = new List<SchoolDetails1>();
            try
            {
                if (!string.IsNullOrEmpty(val.SchoolCode))
                {
                    string[] cols = { "SchoolCode" };
                    object[] vals = { val.SchoolCode };

                    DataTable dt = obj.sp_GetDataTable("Sp_GetSchoolDeatilsBySchoolID", cols, vals);

                    foreach (DataRow dr in dt.Rows)
                    {
                        avi++;
                        SchoolDetails1 Sch = new SchoolDetails1();

                        Sch.ID = dr["ID"].ToString();
                        Sch.School = dr["School"].ToString();
                        Sch.SchoolCode = dr["SchoolCode"].ToString();
                        Sch.Address = dr["Address"].ToString();
                        Sch.Board = dr["Board"].ToString();
                        Sch.Pincode = dr["Pincode"].ToString();
                        Sch.Phone = dr["Phone"].ToString();
                        Sch.Fax = dr["Fax"].ToString();
                        Sch.Mobile = dr["Mobile"].ToString();
                        Sch.Email = dr["Email"].ToString();
                        Sch.Website = dr["Website"].ToString();
                        Sch.Country = dr["CountryName"].ToString();
                        Sch.State = dr["StateName"].ToString();
                        Sch.City = dr["CityName"].ToString();
                        Sch.District = dr["District"].ToString();
                        //WHILE SAVING DATE CREATE NOT INSERTED FROM SERVER
                        Sch.DateCreated = Convert.ToDateTime(dr["DateCreated"]).ToString("dd MMMM yyyy ");
                        Sch.DateTimeCreated = Convert.ToDateTime(dr["DateCreated"]).ToString("yyyy-MM-dd T HH:mm:ss");
                        if (dr["LogoPic"].ToString() == "")
                        {

                            Sch.ImageUpload = "/Images/no-image.png";
                        }
                        else
                        {
                            Sch.ImageUpload = dr["LogoPic"].ToString();
                        }
                        //   Sch.Password = dr["Password"].ToString();
                        list.Add(Sch);
                    }
                    if (avi != 0)
                    {
                        OBJ.status = true;
                        OBJ.message = "Sucess";
                        OBJ.data = list;
                    }
                    else
                    {
                        OBJ.status = false;
                        OBJ.message = "No School Found";
                        OBJ.data = list;
                    }
                }
                else
                {
                    OBJ.status = false;
                    OBJ.message = "Please Enter SchoolCode";
                    OBJ.data = list;
                }
            }
            catch 
            {

                OBJ.status = false;
                OBJ.message = "Something Went Wrong";
                OBJ.data = list;

            }

            return OBJ;

        }


        [System.Web.Http.Route("api/Default/DeleteSchool")]
        [System.Web.Http.HttpGet]
        public string DeleteSchool(string id)
        {
            bool b = AdminMaster.deleteSchoolById(id);
            if (b)
            {
                return "School Deleted Successfully";
            }
            else
            {

                return "School Not Deleted Successfully";

            }

        }

        [System.Web.Http.Route("api/Default/GetAllSchoolDetail")]
        [System.Web.Http.HttpPost]
        public SchoolDetails[] GetAllSchoolDetail()
        {
            sqlHelper obj = new sqlHelper();
            List<SchoolDetails> list = new List<SchoolDetails>();


            DataTable dt = obj.sp_GetDataTableNoParam("Sp_GetSchoolDeatils");
            foreach (DataRow dr in dt.Rows)
            {
                SchoolDetails Sch = new SchoolDetails();

                Sch.ID = dr["ID"].ToString();
                Sch.School = dr["School"].ToString();
                Sch.SchoolCode = dr["SchoolCode"].ToString();
                Sch.Address = dr["Address"].ToString();
                Sch.Board = dr["Board"].ToString();
                Sch.Pincode = dr["Pincode"].ToString();
                Sch.Phone = dr["Phone"].ToString();
                Sch.Fax = dr["Fax"].ToString();
                Sch.Mobile = dr["Mobile"].ToString();
                Sch.Email = dr["Email"].ToString();
                Sch.Website = dr["Website"].ToString();
                Sch.Country = dr["CountryName"].ToString();
                Sch.State = dr["StateName"].ToString();
                Sch.City = dr["CityName"].ToString();
                Sch.District = dr["District"].ToString();


                if (dr["LogoPic"].ToString() == "")
                {

                    Sch.ImageUpload = "/Images/no-image.png";
                }
                else
                {
                    Sch.ImageUpload = dr["LogoPic"].ToString();
                }
                Sch.Password = dr["Password"].ToString();
                list.Add(Sch);
            }
            return list.ToArray();
        }

      


        [System.Web.Http.Route("api/Default/GetDetailUpdateSchool")]
        [System.Web.Http.HttpPost]
        public SchoolDetails GetDetailUpdateSchool(List<string> id)
        {
            List<SchoolDetails> list = new List<SchoolDetails>();

            sqlHelper obj = new sqlHelper();

            SqlDataReader dr = obj.GetReader("select * from tblSchoolDetails where Id=" + id[0]);


            SchoolDetails Sch = new SchoolDetails();
            if (dr.Read())
            {
                Sch.ID = dr["ID"].ToString();
                Sch.School = dr["School"].ToString();
                Sch.SchoolCode = dr["SchoolCode"].ToString();
                Sch.Address = dr["Address"].ToString();
                Sch.Board = dr["Board"].ToString();
                Sch.Pincode = dr["Pincode"].ToString();
                Sch.Phone = dr["Phone"].ToString();
                Sch.Fax = dr["Fax"].ToString();
                Sch.Mobile = dr["Mobile"].ToString();
                Sch.Email = dr["Email"].ToString();
                Sch.Website = dr["Website"].ToString();
                Sch.Country = dr["CountryID"].ToString();
                Sch.State = dr["State"].ToString();
                Sch.City = dr["CityID"].ToString();
                Sch.District = dr["District"].ToString();
                Sch.ImageUpload = dr["LogoPic"].ToString();
                Sch.Password = dr["Password"].ToString();


            }
            return Sch;
        }

        //[System.Web.Http.Route("api/Default/getAllSchoolName")]
        //[System.Web.Http.HttpPost]
        //public CountyMaster[] getAllSchoolName()
        //{
        //    List<CountyMaster> list = new List<CountyMaster>();
        //    string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        //    SqlConnection con = new SqlConnection(constr);
        //    con.Open();


        //    string query = "select * from tblSchoolDetails";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter adap = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    adap.Fill(dt);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        CountyMaster usr = new CountyMaster();
        //        usr.Name = dr["School"].ToString();
        //        usr.Id = dr["ID"].ToString();
        //      //  usr.Status = bool.Parse(dr["Status"].ToString());
        //        list.Add(usr);
        //    }
        //    con.Close();



        //    return list.ToArray();


        //}







        [System.Web.Http.Route("api/Default/SchoolLogoUpload")]
        [System.Web.Http.HttpPost]
        public string SchoolLogoUpload()
        {

            //  int jk = 0;

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {

                string EmployeeID = HttpContext.Current.Request.Params["Id"];


                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                string ImageExtensions = "";
                //   string ImageExtensions1 = "";
                string ImageFileName = "";
                //   string ImageFileName1 = "";
                string ImageFile = "";
                // string ImageFile1 = "";
                string FullImagePath = "/Images/Admin/AdmitCards/";
                string FullImagePath2 = "/Images/Employee/Documents/";
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath);
                string serverPath2 = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath2);
                if (httpPostedFile != null)
                {

                    // Validate the uploaded image(optional) 

                    // Get the complete file path
                    ImageExtensions = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage"].FileName).ToString();
                    if (ImageExtensions.ToLower() == ".jpg" || ImageExtensions.ToLower() == ".png" || ImageExtensions.ToLower() == ".jpeg" || ImageExtensions.ToLower() == ".gif" || ImageExtensions.ToLower() == ".pdf")
                    {
                        ImageFile = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage"].FileName);
                        System.IO.FileInfo filename1 = new System.IO.FileInfo(serverPath + ImageFile);

                        ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

                        ImageFileName = ImageFile;
                        ImageFile = FullImagePath + ImageFile;
                        HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                        sqlHelper obj = new sqlHelper();
                        string[] cols = { "LogoPic" };
                        object[] vals = { ImageFile };
                        obj.updateValIntoTable("tblSchoolDetails", cols, vals, "Id", EmployeeID);
                    }

                }





            }
            return "";

        }

        [System.Web.Http.Route("api/Default/SchoolPrincipleUpload")]
        [System.Web.Http.HttpPost]
        public string SchoolPrincipleUpload()
        {


            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {

                string EmployeeID = HttpContext.Current.Request.Params["Id"];


                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                string ImageExtensions = "";
                // string ImageExtensions1 = "";
                string ImageFileName = "";
                //string ImageFileName1 = "";
                string ImageFile = "";
                //string ImageFile1 = "";
                string FullImagePath = "/Images/Admin/";
                string FullImagePath2 = "/Images/Employee/Documents/";
                string serverPath = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath);
                string serverPath2 = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath2);
                if (httpPostedFile != null)
                {




                    // Validate the uploaded image(optional) 

                    // Get the complete file path
                    ImageExtensions = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage"].FileName).ToString();
                    if (ImageExtensions.ToLower() == ".jpg" || ImageExtensions.ToLower() == ".png" || ImageExtensions.ToLower() == ".jpeg" || ImageExtensions.ToLower() == ".gif" || ImageExtensions.ToLower() == ".pdf")
                    {
                        ImageFile = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage"].FileName);
                        System.IO.FileInfo filename1 = new System.IO.FileInfo(serverPath + ImageFile);

                        ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

                        ImageFileName = ImageFile;
                        ImageFile = FullImagePath + ImageFile;
                        HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                        sqlHelper obj = new sqlHelper();
                        string[] cols = { "Signature" };
                        object[] vals = { ImageFile };
                        obj.updateValIntoTable("tblUser", cols, vals, "ID", EmployeeID);
                    }

                }





            }
            return "";

        }

        [System.Web.Http.Route("api/Default/getAdvertisingVedio")]
        [System.Web.Http.HttpPost]
        public VedioAdvertising getAdvertisingVedio()
        {
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable("select * from tblAdvertisingVedio");
            VedioAdvertising usr = new VedioAdvertising();
            if (dt.Rows.Count > 0)
            {

                usr.Url = dt.Rows[0]["VedioUrl"].ToString();
                usr.Time = dt.Rows[0]["VedioDuration"].ToString();

            }
            return usr;

        }

        [System.Web.Http.Route("api/Default/getSchoolClassGraph")]
        [System.Web.Http.HttpPost]
        public charts[] getSchoolClassGraph(List<string> val)
        {
            List<charts> list = new List<charts>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            var clss = db.tblCourses.Where(s => s.Status == true && s.IsDeleted == null).ToList();

            foreach (var a in clss)
            {
                var countclass = db.TBLStudents.Where(x => x.ClassID == a.Id && x.Status == 3).Count();

                charts usr = new charts();
                usr.label = a.Code;
                usr.y = countclass;
                list.Add(usr);
            }
            return list.ToArray();

        }
        [System.Web.Http.Route("api/Default/getSchoolClassGraphofSchooladmin")]
        [System.Web.Http.HttpPost]
        public charts[] getSchoolClassGraphofSchooladmin(List<string> val)
        {
            sqlHelper obj = new sqlHelper();
            int? schid = Convert.ToInt32(val[0]);
            if (schid == null)
            {
                schid = 0;
            }
            List<charts> list = new List<charts>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            var clss = db.tblCourses.Where(s => s.Status == true && s.IsDeleted == null && s.SchoolID == schid).ToList();

            foreach (var a in clss)
            {
                var countclass = db.TBLStudents.Where(x => x.ClassID == a.Id && x.Status == 3 && x.IsDeleted == null).Count();
                charts usr = new charts();
                usr.label = a.CourseName;
                usr.y = countclass;
                list.Add(usr);
            }
            return list.ToArray();

        }

        [System.Web.Http.Route("api/Default/getSchoolClassGraphofSchool")]
        [System.Web.Http.HttpPost]
        public charts[] getSchoolClassGraphofSchool(List<string> val)
        {
            sqlHelper obj = new sqlHelper();
            int? schid = Convert.ToInt32(obj.ExecuteScaler("select schoolid from tblemployee where isdeleted is null and userid='" + val[0] + "'"));
            if (schid == null)
            {
                schid = 0;
            }
            List<charts> list = new List<charts>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            var clss = db.tblCourses.Where(s => s.Status == true && s.IsDeleted == null && s.SchoolID == schid).ToList();

            foreach (var a in clss)
            {
                var countclass = db.TBLStudents.Where(x => x.ClassID == a.Id && x.Status == 3 && x.IsDeleted == null).Count();
                charts usr = new charts();
                usr.label = a.CourseName;
                usr.y = countclass;
                list.Add(usr);
            }
            return list.ToArray();

        }

        [System.Web.Http.Route("api/Default/getEmployeesStrength")]
        [System.Web.Http.HttpPost]
        public charts[] getEmployeesStrength(List<string> val)
        {
            List<charts> list = new List<charts>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            var clss = db.tblStaffCategories.Where(s => s.Status == true).ToList();

            foreach (var a in clss)
            {
                var countclass = db.tblEmployees.Where(x => x.StaffCategory == a.Id && x.Status == true).Count();

                charts usr = new charts();
                usr.label = a.Name;
                if (a.Name == "Teaching")
                {
                    usr.color = "#E7823A";

                }
                else
                {
                    usr.color = "#546BC1";
                }

                usr.y = countclass;
                list.Add(usr);
            }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/Default/getEmployeesStrengthforachooladmin")]
        [System.Web.Http.HttpPost]
        public charts[] getEmployeesStrengthforachooladmin(List<string> val)
        {
            List<charts> list = new List<charts>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            sqlHelper obj = new sqlHelper();
            int? schid = Convert.ToInt32(val[0]);
            if (schid == null)
            {
                schid = 0;
            }
            var clss = db.tblStaffCategories.Where(s => s.Status == true).ToList();

            foreach (var a in clss)
            {
                var countclass = db.tblEmployees.Where(x => x.StaffCategory == a.Id && x.Status == true && x.SchoolID == schid && x.IsDeleted == null).Count();

                charts usr = new charts();
                usr.label = a.Name;
                if (a.Name == "Teaching")
                {
                    usr.color = "#E7823A";
                }
                else
                {
                    usr.color = "#546BC1";
                }

                usr.y = countclass;
                list.Add(usr);
            }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/Default/getEmployeesStrengthforachool")]
        [System.Web.Http.HttpPost]
        public charts[] getEmployeesStrengthforachool(List<string> val)
        {
            List<charts> list = new List<charts>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            sqlHelper obj = new sqlHelper();
            int? schid = Convert.ToInt32(obj.ExecuteScaler("select schoolid from tblemployee where isdeleted is null and userid='" + val[0] + "'"));
            if (schid == null)
            {
                schid = 0;
            }
            var clss = db.tblStaffCategories.Where(s => s.Status == true).ToList();

            foreach (var a in clss)
            {
                var countclass = db.tblEmployees.Where(x => x.StaffCategory == a.Id && x.Status == true && x.SchoolID == schid && x.IsDeleted == null).Count();

                charts usr = new charts();
                usr.label = a.Name;
                if (a.Name == "Teaching")
                {
                    usr.color = "#E7823A";
                }
                else
                {
                    usr.color = "#546BC1";
                }

                usr.y = countclass;
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/Default/getTotalAbsentsStudents")]
        [System.Web.Http.HttpPost]
        public string getTotalAbsentsStudents(List<string> val)
       {
            DateTime attendencDate = Convert.ToDateTime(val[0]);
            int SchoolID = Convert.ToInt32(val[1]);
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            var result = db.tblStudentAttendences.Where(x => (x.AttendenceType == "Absent" || x.AttendenceType == "Leave") && x.AttendenceDate == attendencDate && x.SchoolID == SchoolID).Count();
            return result.ToString();
        }

        [System.Web.Http.Route("api/Default/getTotalAbsentsEmployees")]
        [System.Web.Http.HttpPost]
        public string getTotalAbsentsEmployees(List<string> val)
        {
            DateTime attendencDate = Convert.ToDateTime(val[0]);
            int SchoolID = Convert.ToInt32(val[1]);
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            var result = db.tblEmployeeAttendences.Where(x => (x.AttendenceType == "Absent" || x.AttendenceType == "Leave") && x.AttendenceDate == attendencDate && x.SchoolID== SchoolID).Count();
            return result.ToString();
        }

        [System.Web.Http.Route("api/Default/SaveThoughts")]
        [System.Web.Http.HttpPost]
        public string SaveThoughts(SendEmail se)
        {

            SCHOOLERPEntities cb = new SCHOOLERPEntities();
            //var result = cb.Database.ExecuteSqlCommand("truncate table tblThoughtsOfDay");
            var result = cb.Database.ExecuteSqlCommand("delete from tblThoughtsOfDay where SchoolID='" + se.SchoolID + "'");
            cb.SaveChanges();
            tblThoughtsOfDay usr = new tblThoughtsOfDay();
            usr.Thoughts = se.Messsage;
            usr.dateCreated = DateTime.Now;
            usr.SchoolID = se.SchoolID;
            cb.tblThoughtsOfDays.Add(usr);
            cb.SaveChanges();
            return "Thought Created Successfully";
        }

        [System.Web.Http.Route("api/Default/GetThoughtofday")]
        [System.Web.Http.HttpPost]
        public SendEmail GetThoughtofday(SendEmail se)
        {
            int SchoolID = Convert.ToInt32(se.SchoolID);
            SendEmail aa = new SendEmail();
              var check = db.tblThoughtsOfDays.Where(x => x.SchoolID == SchoolID).FirstOrDefault();
            if (check!=null)
            {

                aa.Thought = check.Thoughts;
                aa.Todaydate = DateTime.Now.ToString("dddd ,MMMM dd, yyyy");
            }
            else
            {
                aa.Thought = "Have a Good Day";
                aa.Todaydate = DateTime.Now.ToString("dddd ,MMMM dd, yyyy");
            }


            return aa;
        }

        [System.Web.Http.Route("api/Default/saveNoticeBoardDetails")]
        [System.Web.Http.HttpPost]
        public string saveNoticeBoardDetails([FromBody]NoticeboardDetails notice)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            tblNoticeBoard usr = new tblNoticeBoard();
            sqlHelper obj = new sqlHelper();
            if (string.IsNullOrEmpty(notice.Id))
            {

                usr.Title = notice.Title;
                usr.NoticeDate = notice.NoticeDate;
                usr.description = notice.Desc;
                usr.userType = notice.userType;
                usr.status = Convert.ToBoolean(notice.Status);
                usr.SchoolID = notice.SchoolID;
                usr.dateCreated = DateTime.Now;
                db.tblNoticeBoards.Add(usr);
                db.SaveChanges();
                //SendSms
                if (notice.SendSms == true)
                {
                    string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + notice.SchoolID + "' and  active=1");
                    if (Checksmsservice != null)
                    {
                        string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + notice.SchoolID + "' and  active=1");
                        string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + notice.SchoolID + "' and  active=1");
                        if (notice.userType == "S")
                        {
                            // FOR Student
                            var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == notice.SchoolID).ID;


                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + notice.SchoolID + "'");
                            //var GetStudents = db.TBLStudents.Where(s => s.AcademicYear == GetActiveAcademicyear.ToString() && s.SchoolID == notice.SchoolID && s.IsDeleted == null && s.Status == 3).ToList();

                            var smslist = string.Join(",", db.TBLStudents.Where(stu => stu.SchoolID == notice.SchoolID && stu.IsDeleted == null && stu.Status == 3 && stu.AcademicYear == GetActiveAcademicyear.ToString())
                                     .Select(stu => stu.SMSmobileNo.ToString()));                       
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = smslist;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Notice Title : " + notice.Title);
                            st.AppendLine("Notice description  :" + notice.Desc);
                            st.AppendLine("NoticeDate :" + notice.NoticeDate);
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(Schoolname);
                            string message = HttpUtility.UrlEncode(st.ToString());

                            //Prepare you post parameters
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("authkey={0}", authKey);
                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                            sbPostData.AppendFormat("&message={0}", message);
                            sbPostData.AppendFormat("&sender={0}", senderId);
                            sbPostData.AppendFormat("&route={0}", 4);
                            sbPostData.AppendFormat("&unicode={0}", 1);
                            
                            try
                            {
                                //Call Send SMS API
                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                //Create HTTPWebrequest
                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                //Prepare and Add URL Encoded data
                                UTF8Encoding encoding = new UTF8Encoding();
                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                //Specify post method
                                httpWReq.Method = "POST";
                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                httpWReq.ContentLength = data1.Length;
                                using (Stream stream = httpWReq.GetRequestStream())
                                {
                                    stream.Write(data1, 0, data1.Length);
                                }
                                //Get the response
                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                string responseString = reader.ReadToEnd();

                                //Close the response
                                reader.Close();
                                response1.Close();
                            }
                            catch (SystemException ex)
                            {
                                ex.Message.ToString();
                            }










                        }
                        else if (notice.userType == "E")
                        {




                           


                            var SMSmobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == notice.SchoolID && x.IsDeleted == null && x.Status == true).Select(x => x.Mobile.ToString()));

                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + notice.SchoolID + "'");                                                      
                                 string authKey = GetauthKey;
                                //Multiple mobiles numbers separated by comma
                                string mobileNumber = SMSmobileNo;
                                //Sender ID,While using route4 sender id should be 6 characters long.
                                string senderId = GetsenderId;
                                //Your message to send, Add URL encoding here.
                                StringBuilder st = new StringBuilder();
                                st.AppendLine("Hi Sir/mam,");
                                st.AppendLine("Notice Title : " + notice.Title);
                                st.AppendLine("Notice description  :" + notice.Desc);
                                st.AppendLine("NoticeDate :" + notice.NoticeDate);
                                st.AppendLine("");
                                st.AppendLine("Regards");
                                st.AppendLine(Schoolname);
                                string message = HttpUtility.UrlEncode(st.ToString());

                                //Prepare you post parameters
                                StringBuilder sbPostData = new StringBuilder();
                                sbPostData.AppendFormat("authkey={0}", authKey);
                                sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                                sbPostData.AppendFormat("&message={0}", message);
                                sbPostData.AppendFormat("&sender={0}", senderId);
                                sbPostData.AppendFormat("&route={0}", 4);
                               sbPostData.AppendFormat("&unicode={0}", 1);
                            try
                                {
                                    //Call Send SMS API
                                    string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                    //Create HTTPWebrequest
                                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                    //Prepare and Add URL Encoded data
                                    UTF8Encoding encoding = new UTF8Encoding();
                                    byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                    //Specify post method
                                    httpWReq.Method = "POST";
                                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                                    httpWReq.ContentLength = data1.Length;
                                    using (Stream stream = httpWReq.GetRequestStream())
                                    {
                                        stream.Write(data1, 0, data1.Length);
                                    }
                                    //Get the response
                                    HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                    StreamReader reader = new StreamReader(response1.GetResponseStream());
                                    string responseString = reader.ReadToEnd();

                                    //Close the response
                                    reader.Close();
                                    response1.Close();
                                }
                                catch (SystemException ex)
                                {
                                    ex.Message.ToString();
                                }




                            
                          





                        }
                        else if (notice.userType == "G")
                        {

                            var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == notice.SchoolID).ID;


                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + notice.SchoolID + "'");
                            //var GetStudents = db.TBLStudents.Where(s => s.AcademicYear == GetActiveAcademicyear.ToString() && s.SchoolID == notice.SchoolID && s.IsDeleted == null && s.Status == 3).ToList();

                            var smslist = string.Join(",", db.TBLStudents.Where(stu => stu.SchoolID == notice.SchoolID && stu.IsDeleted == null && stu.Status == 3 && stu.AcademicYear == GetActiveAcademicyear.ToString())
                                     .Select(stu => stu.SMSmobileNo.ToString()));
                            var SMSmobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == notice.SchoolID && x.IsDeleted == null && x.Status == true).Select(x => x.Mobile.ToString()));
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = smslist+","+ SMSmobileNo;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Notice Title : " + notice.Title);
                            st.AppendLine("Notice description  :" + notice.Desc);
                            st.AppendLine("NoticeDate :" + notice.NoticeDate);
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(Schoolname);
                            string message = HttpUtility.UrlEncode(st.ToString());

                            //Prepare you post parameters
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("authkey={0}", authKey);
                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                            sbPostData.AppendFormat("&message={0}", message);
                            sbPostData.AppendFormat("&sender={0}", senderId);
                            sbPostData.AppendFormat("&route={0}", 4);
                            sbPostData.AppendFormat("&unicode={0}", 1);
                            try
                            {
                                //Call Send SMS API
                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                //Create HTTPWebrequest
                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                //Prepare and Add URL Encoded data
                                UTF8Encoding encoding = new UTF8Encoding();
                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                //Specify post method
                                httpWReq.Method = "POST";
                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                httpWReq.ContentLength = data1.Length;
                                using (Stream stream = httpWReq.GetRequestStream())
                                {
                                    stream.Write(data1, 0, data1.Length);
                                }
                                //Get the response
                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                string responseString = reader.ReadToEnd();

                                //Close the response
                                reader.Close();
                                response1.Close();
                            }
                            catch (SystemException ex)
                            {
                                ex.Message.ToString();
                            }




                        }
                    }
                       
                }


                   
                
                return usr.Id.ToString();
            }
            else
            {
                int idd = Convert.ToInt32(notice.Id);
                var result = db.tblNoticeBoards.Where(x => x.Id == idd).SingleOrDefault();
                result.Title = notice.Title;
                result.NoticeDate = notice.NoticeDate;
                result.description = notice.Desc;
                result.userType = notice.userType;
                result.status = Convert.ToBoolean(notice.Status);
                result.dateCreated = DateTime.Now;
                result.SchoolID = notice.SchoolID;
                db.SaveChanges();
                if (notice.SendSms == true)
                {
                    string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + notice.SchoolID + "' and  active=1");
                    if (Checksmsservice != null)
                    {
                        string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + notice.SchoolID + "' and  active=1");
                        string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + notice.SchoolID + "' and  active=1");
                        if (notice.userType == "S")
                        {
                            // FOR Student
                            var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == notice.SchoolID).ID;


                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + notice.SchoolID + "'");
                            //var GetStudents = db.TBLStudents.Where(s => s.AcademicYear == GetActiveAcademicyear.ToString() && s.SchoolID == notice.SchoolID && s.IsDeleted == null && s.Status == 3).ToList();

                            var smslist = string.Join(",", db.TBLStudents.Where(stu => stu.SchoolID == notice.SchoolID && stu.IsDeleted == null && stu.Status == 3 && stu.AcademicYear == GetActiveAcademicyear.ToString())
                                     .Select(stu => stu.SMSmobileNo.ToString()));
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = smslist;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Edited Notice");
                            st.AppendLine("Notice Title : " + notice.Title);
                            st.AppendLine("Notice description  :" + notice.Desc);
                            st.AppendLine("NoticeDate :" + notice.NoticeDate);
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(Schoolname);
                            string message = HttpUtility.UrlEncode(st.ToString());

                            //Prepare you post parameters
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("authkey={0}", authKey);
                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                            sbPostData.AppendFormat("&message={0}", message);
                            sbPostData.AppendFormat("&sender={0}", senderId);
                            sbPostData.AppendFormat("&route={0}", 4);
                            sbPostData.AppendFormat("&unicode={0}", 1);
                            try
                            {
                                //Call Send SMS API
                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                //Create HTTPWebrequest
                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                //Prepare and Add URL Encoded data
                                UTF8Encoding encoding = new UTF8Encoding();
                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                //Specify post method
                                httpWReq.Method = "POST";
                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                httpWReq.ContentLength = data1.Length;
                                using (Stream stream = httpWReq.GetRequestStream())
                                {
                                    stream.Write(data1, 0, data1.Length);
                                }
                                //Get the response
                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                string responseString = reader.ReadToEnd();

                                //Close the response
                                reader.Close();
                                response1.Close();
                            }
                            catch (SystemException ex)
                            {
                                ex.Message.ToString();
                            }










                        }
                        else if (notice.userType == "E")
                        {







                            var SMSmobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == notice.SchoolID && x.IsDeleted == null && x.Status == true).Select(x => x.Mobile.ToString()));

                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + notice.SchoolID + "'");
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = SMSmobileNo;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Edited Notice");
                            st.AppendLine("Notice Title : " + notice.Title);
                            st.AppendLine("Notice description  :" + notice.Desc);
                            st.AppendLine("NoticeDate :" + notice.NoticeDate);
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(Schoolname);
                            string message = HttpUtility.UrlEncode(st.ToString());

                            //Prepare you post parameters
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("authkey={0}", authKey);
                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                            sbPostData.AppendFormat("&message={0}", message);
                            sbPostData.AppendFormat("&sender={0}", senderId);
                            sbPostData.AppendFormat("&route={0}", 4);
                            sbPostData.AppendFormat("&unicode={0}", 1);
                            try
                            {
                                //Call Send SMS API
                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                //Create HTTPWebrequest
                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                //Prepare and Add URL Encoded data
                                UTF8Encoding encoding = new UTF8Encoding();
                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                //Specify post method
                                httpWReq.Method = "POST";
                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                httpWReq.ContentLength = data1.Length;
                                using (Stream stream = httpWReq.GetRequestStream())
                                {
                                    stream.Write(data1, 0, data1.Length);
                                }
                                //Get the response
                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                string responseString = reader.ReadToEnd();

                                //Close the response
                                reader.Close();
                                response1.Close();
                            }
                            catch (SystemException ex)
                            {
                                ex.Message.ToString();
                            }











                        }
                        else if (notice.userType == "G")
                        {

                            var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == notice.SchoolID).ID;


                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + notice.SchoolID + "'");
                            //var GetStudents = db.TBLStudents.Where(s => s.AcademicYear == GetActiveAcademicyear.ToString() && s.SchoolID == notice.SchoolID && s.IsDeleted == null && s.Status == 3).ToList();

                            var smslist = string.Join(",", db.TBLStudents.Where(stu => stu.SchoolID == notice.SchoolID && stu.IsDeleted == null && stu.Status == 3 && stu.AcademicYear == GetActiveAcademicyear.ToString())
                                     .Select(stu => stu.SMSmobileNo.ToString()));
                            var SMSmobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == notice.SchoolID && x.IsDeleted == null && x.Status == true).Select(x => x.Mobile.ToString()));
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = smslist + "," + SMSmobileNo;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Edited Notice");
                            st.AppendLine("Notice Title : " + notice.Title);
                            st.AppendLine("Notice description  :" + notice.Desc);
                            st.AppendLine("NoticeDate :" + notice.NoticeDate);
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(Schoolname);
                            string message = HttpUtility.UrlEncode(st.ToString());

                            //Prepare you post parameters
                            StringBuilder sbPostData = new StringBuilder();
                            sbPostData.AppendFormat("authkey={0}", authKey);
                            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                            sbPostData.AppendFormat("&message={0}", message);
                            sbPostData.AppendFormat("&sender={0}", senderId);
                            sbPostData.AppendFormat("&route={0}", 4);
                            sbPostData.AppendFormat("&unicode={0}", 1);

                            try
                            {
                                //Call Send SMS API
                                string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                //Create HTTPWebrequest
                                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                //Prepare and Add URL Encoded data
                                UTF8Encoding encoding = new UTF8Encoding();
                                byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                //Specify post method
                                httpWReq.Method = "POST";
                                httpWReq.ContentType = "application/x-www-form-urlencoded";
                                httpWReq.ContentLength = data1.Length;
                                using (Stream stream = httpWReq.GetRequestStream())
                                {
                                    stream.Write(data1, 0, data1.Length);
                                }
                                //Get the response
                                HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                StreamReader reader = new StreamReader(response1.GetResponseStream());
                                string responseString = reader.ReadToEnd();

                                //Close the response
                                reader.Close();
                                response1.Close();
                            }
                            catch (SystemException ex)
                            {
                                ex.Message.ToString();
                            }




                        }
                    }

                }



                return idd.ToString();
            }
        }

        [System.Web.Http.Route("api/Default/NoticeBoardFileUpload")]
        [System.Web.Http.HttpPost]
        public string NoticeBoardFileUpload()
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {

                    string EmployeeID = HttpContext.Current.Request.Params["Id"];


                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    string ImageExtensions = "";
                    string ImageFileName = "";
                    string ImageFile = "";
                    string FullImagePath = "/Images/Noticeboard/";
                    string serverPath = System.Web.HttpContext.Current.Server.MapPath("~" + FullImagePath);

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional) 

                        // Get the complete file path 
                        ImageExtensions = Path.GetExtension(HttpContext.Current.Request.Files["UploadedImage"].FileName).ToString();
                        if (ImageExtensions.ToLower() == ".jpg" || ImageExtensions.ToLower() == ".png" || ImageExtensions.ToLower() == ".jpeg" /*|| ImageExtensions.ToLower() == ".gif" || ImageExtensions.ToLower() == ".pdf" || ImageExtensions.ToLower() == ".xlsx"*/)
                        {
                            ImageFile = Path.GetFileName(HttpContext.Current.Request.Files["UploadedImage"].FileName);
                            System.IO.FileInfo filename1 = new System.IO.FileInfo(serverPath + ImageFile);

                            ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

                            ImageFileName = ImageFile;
                            ImageFile = "http:/" + "/www.smartvidhya.com"+ FullImagePath + ImageFile;
                            HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
                            sqlHelper obj = new sqlHelper();
                            string[] cols = { "NoticeFile" };
                            object[] vals = { ImageFile };
                            obj.updateValIntoTable("tblNoticeBoard", cols, vals, "Id", EmployeeID);
                        }

                    }
                }



                return "Notice Board Added Successfully";
            }
            catch
            {
                return "File size limit exceeded";
            }

        }
        //[System.Web.Http.Route("api/Default/checkSubjectassigned")]
        //[System.Web.Http.HttpPost]
        //public string checkSubjectassigned(List<string> val)
        //{
        //    string msg = "";
        //    try
        //    {
        //        int SchoolID = Convert.ToInt32(val[0]);
              
        //        int empid = Convert.ToInt32(val[2]);
        //        int CourseID = Convert.ToInt32(val[3]);
        //        var arr = val[1].Split(',');
        //        foreach ( var s in arr)
        //        {
        //            int subjectid = Convert.ToInt32(s);
        //            var checksubject = db.tblSubjectTeacherAllocates.Where(x => x.TeacherID == empid && x.classid == CourseID && x.SubjectID == subjectid && x.SchoolID == SchoolID && x.IsDeleted == null).FirstOrDefault();
        //            if (checksubject != null)
        //            {
        //                int a = Convert.ToInt32(checksubject.SubjectID);
        //                var subname = db.tblSubjects.Where(x => x.ID == a && x.IsDeleted == null).FirstOrDefault();

        //                msg = subname.Subject + "already assigned to this Teacher";
        //            }
        //            else
        //            {
        //                msg = "0";
        //            }

        //        }
               
               
        //    }
        //    catch
        //    {
        //        msg = "1";
        //    }



        //    return msg;
        //}
        [System.Web.Http.Route("api/Default/getAllSchoolNameTimeTable")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllSchoolNameTimeTable(List<string> val)
        {
            try
            {
                List<CountyMaster> list = new List<CountyMaster>();
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                string i = val[0].ToString().Replace("#", "");

                var result = db.tblSchoolDetails.SqlQuery("select * from tblSchoolDetails scl left outer join tblTimeTableConfig Tbl on  scl.ID=Tbl.SchoolID where Tbl.ID=" + Convert.ToInt32(i) + "").ToList();
                //string query = "select * from tblSchoolDetails";
                //SqlCommand cmd = new SqlCommand(query, con);
                //SqlDataAdapter adap = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //adap.Fill(dt);
                foreach (var a in result)
                {
                    CountyMaster usr = new CountyMaster();
                    usr.Name = a.School;
                    usr.Id = Convert.ToString(a.ID);
                    //  usr.Status = bool.Parse(dr["Status"].ToString());
                    list.Add(usr);
                }
                con.Close();
                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [System.Web.Http.Route("api/Default/School")]
        [System.Web.Http.HttpGet]
        public string School(string schcode)
        {
            try
            {
                string i = "0";
                sqlHelper obj = new sqlHelper();
                int tc = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSchoolDetails where isdeleted is null and SchoolCode='" + schcode.Trim() + "'"));

                if (tc > 0)
                {
                    i = Convert.ToString(obj.ExecuteScaler("select logopic from tblSchoolDetails where isdeleted is null and SchoolCode='" + schcode.Trim() + "'"));
                }
                return i;
            }
            catch
            {
                return null;
            }
        }

        public class charts
        {
            public string label { get; set; }
            public string color { get; set; }
            public int y { get; set; }
        }
    }
}
