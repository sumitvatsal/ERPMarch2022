using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security;
using System.Security.Cryptography;
using System.Web;








namespace schoolERP_BLL
{

   


        public class EmployeeEm
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Pwd { get; set; }
        public string Employeecode { get; set; }
        public string JoiningDate { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string DesigId { get; set; }
        public string qualfication { get; set; }
        public string TotalExperience { get; set; }
        public string UserType { get; set; }

        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string city { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ImageUpload { get; set; }
        public string Status { get; set; }

        public string AdhaarNo { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string Religon { get; set; }
        public string MaritalStaus { get; set; }



        public string PaymentMode { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string BankName { get; set; }
        public string PfNo { get; set; }
        public string PANNo { get; set; }
        public string EsiNo { get; set; }



        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
        public string Extra4 { get; set; }
        public string Extra5 { get; set; }
        public string Extra6 { get; set; }
        public string Extra7 { get; set; }
        public string Extra8 { get; set; }
        public string Extra9 { get; set; }
        public string Extra10 { get; set; }
        public string departmentID { get; set; }
        public string BiometricId { get; set; }


        public string studentId { get; set; }
        public string day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public string School { get; set; }
        public int SchoolID { get; set; }
        public int Classid { get; set; }
        public int SectionID { get; set; }
        public Nullable<int> UserType1 { get; set; }
        public string TypeName { get; set; }
        public string staffCategory { get; set; }
    }

    public partial class SuperAdminDetails
    {
        public string userid { get; set; }

    }

    public partial class SchoolDETAILSAPP
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<SchoolDetails1> data { get; set; }
    }
    public partial class SchoolDetails1
    {
        public string ID { get; set; }
        public string School { get; set; }
        public string SchoolCode { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Board { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string LogoPic { get; set; }
        public string principleSign { get; set; }
        public Nullable<int> PrincipalID { get; set; }
        public string ImageUpload { get; set; }
        public string DateTimeCreated { get; set; }
       public string DateCreated { get; set; }

    }
   
        public partial class GalleryImage
    {
        public int ID { get; set; }
        public int Sno { get; set; }
        public string image { get; set; }
        public int SchoolID { get; set; }
        public string ImageTitle { get; set; }
        public string ImageDesc { get; set; }
        public Nullable<bool> status { get; set; }
        public string StatusShow { get; set; }
    }
        public partial class SchoolDetails
    {
        public string ID { get; set; }
        public string School { get; set; }
        public string SchoolCode { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Board { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string LogoPic { get; set; }
        public string principleSign { get; set; }
        public Nullable<int> PrincipalID { get; set; }
        public string ImageUpload { get; set; }
        public DateTime DateCreated { get; set; }
        public string Password { get; set; }
        //  public string type { get; set; }
        public int Feedueday { get; set; }

        public static string saveSchoolDetails(SchoolDetails Sch)
        {
            sqlHelper obj = new sqlHelper();
            

            if (string.IsNullOrEmpty(Sch.ID))
            {

                DateTime TODAYDATE = DateTime.Now;
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Sch.Password));

                //get hash result after compute it  
                byte[] result = md5.Hash;

                StringBuilder strBuilderPPWD = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilderPPWD.Append(result[i].ToString("x2"));
                }
                string SchoolPassword = strBuilderPPWD.ToString();
                int feeduedate = 10; //manual at the time SchoolEnter
                string[] cols = { "School", "SchoolCode", "Address", "State", "CityID", "District", "Board",
                              "Pincode","Phone","Fax","Mobile","Email","Website","CountryID","DateCreated","Password","FeeDueDay"};
                object[] vals = { Sch.School, Sch.SchoolCode,Sch.Address,Sch.State,Sch.City,Sch.District, Sch.Board,Sch.Pincode,
            Sch.Phone,Sch.Fax,Sch.Mobile,Sch.Email,Sch.Website,Sch.Country,TODAYDATE,SchoolPassword,feeduedate};

                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSchoolDetails where IsDeleted is null and School='" + Sch.School + "'"));
                if (chk == 0)
                {
                    long SchoolId = 0;
                    obj.insertValIntoTableGetId("tblSchoolDetails", cols, vals, ref SchoolId);
                    sqlHelper obj1 = new sqlHelper();

                    string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                    SqlConnection con1 = new SqlConnection(constr1);
                    return (SchoolId).ToString();
                }
                else
                {
                    return "-1";
                }
            }
            else
            {
                if (Sch.Password =="")
                {

                 
                    

                    string[] cols = { "School", "SchoolCode", "Address", "State", "CityID", "District", "Board",
                                "Pincode","Phone","Fax","Mobile","Email","Website","CountryID"};
                    object[] vals = { Sch.School, Sch.SchoolCode,Sch.Address,Sch.State,Sch.City,Sch.District, Sch.Board,Sch.Pincode,
                                Sch.Phone,Sch.Fax,Sch.Mobile,Sch.Email,Sch.Website,Sch.Country};

                    int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSchoolDetails where IsDeleted is null and School='" + Sch.School + "'  and id<>'" + Sch.ID + "'"));
                    if (chk == 0)
                    {
                        obj.updateValIntoTable("tblSchoolDetails", cols, vals, "Id", Sch.ID);

                        return (Sch.ID).ToString();
                    }
                    else
                    {
                        return "-1";
                    }
                }
               else
                {
                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Sch.Password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilderPPWD = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilderPPWD.Append(result[i].ToString("x2"));
                    }
                    string SchoolPassword = strBuilderPPWD.ToString();

                    string[] cols = { "School", "SchoolCode", "Address", "State", "CityID", "District", "Board",
                                "Pincode","Phone","Fax","Mobile","Email","Website","CountryID","Password"};
                    object[] vals = { Sch.School, Sch.SchoolCode,Sch.Address,Sch.State,Sch.City,Sch.District, Sch.Board,Sch.Pincode,
                                Sch.Phone,Sch.Fax,Sch.Mobile,Sch.Email,Sch.Website,Sch.Country,SchoolPassword};

                    int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from tblSchoolDetails where IsDeleted is null and School='" + Sch.School + "'  and id<>'" + Sch.ID + "'"));
                    if (chk == 0)
                    {
                        obj.updateValIntoTable("tblSchoolDetails", cols, vals, "Id", Sch.ID);

                        return (Sch.ID).ToString();
                    }
                    else
                    {
                        return "-1";
                    }
                }
             
            }
        }
    }

    public class ChangePwd
    {
        public string Id { get; set; }
        public string USerID { get; set; }
        public string Pwd { get; set; }
        public string OldPwd { get; set; }
        public string NewPWd { get; set; }
        public string ChagePwd { get; set; }
        public string SchoolID { get; set; }
    }

   
    public class Event
    {
        public Guid EventID { get { return new Guid(); } }
        public string EventName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ImageType { get; set; }
        public string Url { get; set; }
        public string Color { get; set; }
    }
    public class Changepassword
    {
        public string UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
    public class feecalculationdetails
    {
        //  public feecalculationdetails() { }
        public string Name { get; set; }
        public string Month { get; set; }
        public int MonthId { get; set; }
        public string amount { get; set; }
        public string duesAmount { get; set; }
        public int studentId { get; set; }
        public string monthlyamt { get; set; }
        public string discountPer { get; set; }
        public string discountAmnt { get; set; }
        public string ActualPayableAmnt { get; set; }
        public int SchoolID { get; set; }
        public int LateFeeFine { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public Nullable<int> feeStructureID { get; set; }

    }
    public class EmployeeDetails
    {

        public static string saveEmployeeDetails(EmployeeEm emp)
        {
            sqlHelper obj = new sqlHelper();

           
            if (string.IsNullOrEmpty(emp.Id))
            {

               
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(emp.Password));

                //get hash result after compute it  
                byte[] result = md5.Hash;

                StringBuilder empBuilderPPWD = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    empBuilderPPWD.Append(result[i].ToString("x2"));
                }
                string EmployeePassword = empBuilderPPWD.ToString();



                string[] cols = {"Empcode", "JoiningDt", "DeptID", "DesigID", "Qualification", "Experience", "StaffCategory",
                              "FirstName","MiddleName","LastName","DOB","GenderID","UserID","Pwd",
                                "City","State","Country","Pincode","Phone","Mobile","Email","PresentAddress",
                                      "PermAddress","MaritalSatus","AdharNo","Religion","FatherName","MotherName",
                                     "PaymentMod","AccountNumber","IFSCCode","PFNo","PANNO","BankName","ESINO","SchoolID","UserType","BiometricID"};
                object[] vals = { emp.Employeecode, emp.JoiningDate, emp.Department,emp.Designation,emp.qualfication,emp.TotalExperience,emp.UserType,
            emp.FName,emp.MName,emp.LName,emp.DOB,emp.Gender,emp.UserName,EmployeePassword,emp.city,emp.State,
            emp.Country,emp.Pin,emp.Phone,emp.Mobile,emp.Email,emp.PresentAddress,
                emp.PermanentAddress,emp.MaritalStaus,emp.AdhaarNo,emp.Religon,emp.FatherName,emp.MotherName,
            emp.PaymentMode,emp.AccountNumber,emp.IfscCode,emp.PfNo,emp.PANNo,emp.BankName,emp.EsiNo,emp.School,emp.UserType1,emp.BiometricId};

                long empId = 0;
                obj.insertValIntoTableGetId("tblEmployee", cols, vals, ref empId);
                sqlHelper obj1 = new sqlHelper();
                string lastval = obj1.ExecuteScaler("select LastSeries from tblDocumentNo where UserType='Employee' and Status=1 and SchoolID='" + emp.School + "' and IsDeleted is null ");
                string incemtno = obj1.ExecuteScaler("select DocumentNo from tblDocumentNo where UserType='Employee' and Status=1 and SchoolID='" + emp.School + "' and IsDeleted is null ");
                if (lastval == "")
                {
                    lastval = "0";
                }
                var lastupdate = Int64.Parse(lastval) + Int64.Parse(incemtno);
                string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                SqlConnection con1 = new SqlConnection(constr1);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("update tblDocumentNo set LastSeries='" + lastupdate + "' where UserType='Employee' and SchoolID='" + emp.School + "' and IsDeleted is null  ", con1);
                cmd1.ExecuteNonQuery();
                con1.Close();

                return (empId).ToString() + "***" + emp.Employeecode;
            }
            else
            {

                string[] cols = {"Empcode", "JoiningDt", "DeptID", "DesigID", "Qualification", "Experience", "StaffCategory",
                              "FirstName","MiddleName","LastName","DOB","GenderID","UserID",
                                "City","State","Country","Pincode","Phone","Mobile","Email","PresentAddress",
                                      "PermAddress","MaritalSatus","AdharNo","Religion","FatherName","MotherName",
                                     "PaymentMod","AccountNumber","IFSCCode","PFNo","PANNO","BankName","ESINO","SchoolID","UserType","BiometricID"};
                object[] vals = { emp.Employeecode, emp.JoiningDate, emp.Department,emp.Designation,emp.qualfication,emp.TotalExperience,emp.UserType,
            emp.FName,emp.MName,emp.LName,emp.DOB,emp.Gender,emp.UserName,emp.city,emp.State,
            emp.Country,emp.Pin,emp.Phone,emp.Mobile,emp.Email,emp.PresentAddress,
                emp.PermanentAddress,emp.MaritalStaus,emp.AdhaarNo,emp.Religon,emp.FatherName,emp.MotherName,
            emp.PaymentMode,emp.AccountNumber,emp.IfscCode,emp.PfNo,emp.PANNo,emp.BankName,emp.EsiNo,emp.School,emp.UserType1,emp.BiometricId};

                obj.updateValIntoTable("tblEmployee", cols, vals, "Id", emp.Id);

                return (emp.Id).ToString() + "***" + emp.Employeecode;
            }



        }

        public static string SchoolAdminChangePassword(Changepassword pass)
        {
            sqlHelper obj = new sqlHelper();
            string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            SqlConnection con1 = new SqlConnection(constr1);
            con1.Open();
            SchoolDetails sc = new SchoolDetails();
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass.OldPassword));

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


            MD5 mdd5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            mdd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass.NewPassword));

            //get hash result after compute it  
            byte[] result1 = mdd5.Hash;

            StringBuilder strBuilderPPWDnew = new StringBuilder();
            for (int i = 0; i < result1.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilderPPWDnew.Append(result1[i].ToString("x2"));
            }
            string newSchoolAdminPassword = strBuilderPPWDnew.ToString();

            SqlCommand cmd = new SqlCommand("select * from tblschooldetails where Password='" + SchoolAdminPassword + "' and SchoolCode='" + pass.UserID + "'", con1);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            string message = "";
            if (dt.Rows.Count > 0)
            {

                string[] cols = { "Password" };
                object[] vals = { newSchoolAdminPassword };
                if (string.IsNullOrEmpty(pass.UserID))
                {
                    //long empId = 0;
                    return "";
                }
                else
                {
                    obj.updateValIntoTable("tblschooldetails", cols, vals, "SchoolCode", pass.UserID);
                    //obj.insertValIntoTableGetId("tblschooldetails", cols, vals, ref empId);
                    sqlHelper obj1 = new sqlHelper();


                    return (message).ToString() + "Your Password Updated Sucessfully";

                }
            }
            else
            {

                return (message).ToString() + "Old password do not match";

            }
        }
        public static string EmployeStatusUpdate(EmployeeEm emp)
        {
            sqlHelper obj = new sqlHelper();
            string[] cols = { "Status" };
            object[] vals = { emp.Status };
            obj.updateValIntoTable("tblEmployee", cols, vals, "Id", emp.Id);
            return "Employee Status Updated Thank u!";
        }


        public static string changePwdOfEmployee(ChangePwd change)
        {
            sqlHelper obj = new sqlHelper();
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(change.NewPWd));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilderPPWD = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilderPPWD.Append(result[i].ToString("x2"));
            }

            string newpassword = strBuilderPPWD.ToString();

            string[] cols = { "Pwd" };
            object[] vals = { newpassword /*change.NewPWd*/ };
            obj.updateValIntoTable("tblEmployee", cols, vals, "Id", change.Id);
            return "Your Password has been changed successfully Thank u!";
        }

        public static string changeStudentPWd(ChangePwd change)
        {
            sqlHelper obj = new sqlHelper();
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(change.NewPWd));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilderPPWD = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilderPPWD.Append(result[i].ToString("x2"));
            }

            string newpassword = strBuilderPPWD.ToString();

            string[] cols = { "SPwd" };
            object[] vals = { newpassword /*change.NewPWd*/ };
            obj.updateValIntoTable("TBLStudent", cols, vals, "ID", change.Id);
            return "Your Password has been changed successfully Thank u!";
        }


    }


    //public class SaveSchoolDetails
    //{
    //    public static string saveSchoolDetails(SchoolDetails Sch)
    //    {
    //        sqlHelper obj = new sqlHelper();

    //        string[] cols = { "School", "SchoolCode", "Address", "State", "CityID", "District", "Board",
    //                          "Pincode","Phone","Fax","Mobile","Email","Website","CountryID",
    //                           };
    //        object[] vals = { Sch.School, Sch.SchoolCode,Sch.Address,Sch.State,Sch.State,Sch.City,Sch.District, Sch.Board,Sch.Pincode,
    //        Sch.Phone,Sch.Fax,Sch.Mobile,Sch.Email,Sch.Website,Sch.Country};
    //        if (string.IsNullOrEmpty(Sch.ID))
    //        {
    //            long SchoolId = 0;
    //            obj.insertValIntoTableGetId("tblSchoolDetails", cols, vals, ref empId);
    //            sqlHelper obj1 = new sqlHelper();


    //            string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
    //            SqlConnection con1 = new SqlConnection(constr1);
    //            con1.Open();
    //          //  SqlCommand cmd1 = new SqlCommand("update tblDocumentNo set LastSeries='" + lastupdate + "' where UserType='Employee'", con1);
    //           // cmd1.ExecuteNonQuery();
    //            con1.Close();

    //            return (SchoolId).ToString();
    //        }
    //        else
    //        {
    //            obj.updateValIntoTable("tblSchoolDetails", cols, vals, "Id", Sch.ID);

    //            return (Sch.ID).ToString();
    //        }



    //    }



    //}



}
