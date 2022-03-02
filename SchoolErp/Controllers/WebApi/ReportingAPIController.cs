using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using schoolERP_BLL;
using System.Globalization;
using System.Web;
using System.Data.SqlClient;
using SchoolErp.Models;

namespace SchoolErp.Controllers.WebApi
{
    public class ReportingAPIController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/ReportingAPI/getAllEmployeeDetailsReport")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getAllEmployeeDetailsReport(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            List<EmployeeEm> list = new List<EmployeeEm>();


            if (typeuser == 2)
            {
                DataTable dt = obj.sp_GetDataTableNoParam("sp_getAllEmployeeDetailsReports");
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Id = dr["Id"].ToString().Trim();
                    usr.Employeecode = dr["Empcode"].ToString().Trim();
                    usr.FName = dr["FirstName"].ToString().Trim();
                    usr.MName = dr["MiddleName"].ToString().Trim();
                    usr.LName = dr["LastName"].ToString().Trim();
                    usr.School = dr["School"].ToString().Trim();
                    usr.Department = dr["DepartmentName"].ToString().Trim();
                    usr.Designation = dr["Designation"].ToString().Trim();
                    usr.UserType = dr["staff"].ToString().Trim();
                    //usr.UserType = Convert.ToInt32(dr["staff"].ToString().Trim());
                    usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Trim();


                    usr.JoiningDate = ((DateTime)dr["JoiningDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Trim();
                    usr.AccountNumber = dr["AccountNumber"].ToString().Trim();

                    usr.BankName = dr["BankName"].ToString().Trim();
                    usr.TotalExperience = dr["Experience"].ToString().Trim();

                    usr.AdhaarNo = dr["AdharNo"].ToString().Trim();
                    usr.BiometricId = dr["BiometricID"].ToString().Trim();
                    usr.PfNo = dr["PFNo"].ToString().Trim();
                    usr.PANNo = dr["PANNO"].ToString().Trim();
                    usr.EsiNo = dr["ESINO"].ToString().Trim();
                    usr.IfscCode = dr["IFSCCode"].ToString().Trim();


                    usr.Gender = dr["GenderName"].ToString().Trim();

                    usr.Mobile = dr["Mobile"].ToString().Trim();
                    usr.Email = dr["Email"].ToString().Trim();

                    usr.Gender = dr["GenderName"].ToString().Trim();
                    usr.Pin = dr["Pincode"].ToString().Trim();
                    usr.PresentAddress = dr["PresentAddress"].ToString().Trim();
                    usr.PermanentAddress = dr["PermAddress"].ToString().Trim();
                    usr.AdhaarNo = dr["AdhaarNo"].ToString().Trim();
                    usr.MaritalStaus = dr["MaritalStaus"].ToString().Trim();


                    usr.FatherName = dr["FatherName"].ToString().Trim();
                    usr.MotherName = dr["MotherName"].ToString().Trim();
                    usr.Religon = dr["ReligionName"].ToString().Trim();
                    usr.Country = dr["CountryName"].ToString().Trim();
                    usr.State = dr["StateName"].ToString().Trim();
                    usr.city = dr["CityName"].ToString().Trim();
                    usr.UserId = dr["UserId"].ToString().Trim();

                    usr.Pwd = dr["Pwd"].ToString().Trim();

                    if (dr["Image"].ToString().Trim() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString().Trim();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString().Trim() == "True")
                    {

                        usr.Status = "Activate";
                        usr.Extra10 = "#00a65a";

                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra10 = "#dd4b39";

                    }
                    usr.qualfication = dr["QualificationName"].ToString().Trim();
                    list.Add(usr);
                }
            }
            else
            {
                var a = db.tblEmployees.SingleOrDefault(u => u.UserID == loginuser && u.IsDeleted == null);
                if (a != null)
                {
                    int schoolid = Convert.ToInt32(a.SchoolID);

                    //                    con.Open();
                    //                    string query = "";
                    //                    query = @"select em.Id,Empcode,scd.School,td.DepartmentName,desg.Designation,sc.Name staff,
                    //qc.QualificationName,FirstName,MiddleName,LastName,Cast(DOB AS DATE) DOB,
                    //	gen.GenderName,em.Image,em.Status,em.Mobile,em.Email,
                    //	Cast(em.JoiningDt AS DATE) JoiningDate,em.Experience,
                    //	em.AdharNo,em.BankName,em.BiometricID,em.ESINO,em.IFSCCode,
                    //	em.PANNO,em.PFNo,em.AccountNumber,em.BankName from tblEmployee em
                    //left outer join tblDepartmnet td on td.DepartmentId=em.DeptID 
                    //left outer join tblDesignation desg on desg.DesigID=em.DesigID 
                    //left outer join tblStaffCategory sc on sc.Id=em.StaffCategory 
                    //left outer join tblQualifications qc on qc.QualificationId=em.Qualification 
                    //left outer join tblGender gen on gen.gender_id=em.GenderID 
                    //left outer join tblSchoolDetails scd on scd.ID= em.SchoolID
                    //where em.SchoolID='" + schoolid + "' and em.Status=1 and em.Isdeleted is null order by em.id desc   ";
                    //                    SqlCommand cmd = new SqlCommand(query, con);
                    //                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //                    DataTable dt = new DataTable();
                    //                    adap.Fill(dt)
                    // ;
                    string[] cols = { "@School" };
                    object[] vals = { schoolid };

                    DataTable dt = obj.sp_GetDataTable("sp_getAllEmployeeDetailsReports", cols, vals);

                    foreach (DataRow dr in dt.Rows)
                    {
                        EmployeeEm usr = new EmployeeEm();
                        usr.Id = dr["Id"].ToString().Trim();
                        usr.Employeecode = dr["Empcode"].ToString().Trim();
                        usr.FName = dr["FirstName"].ToString().Trim();
                        usr.MName = dr["MiddleName"].ToString().Trim();
                        usr.LName = dr["LastName"].ToString().Trim();
                        usr.School = dr["School"].ToString().Trim();
                        usr.Department = dr["DepartmentName"].ToString().Trim();
                        usr.Designation = dr["Designation"].ToString().Trim();
                        usr.UserType = dr["staff"].ToString().Trim();
                        //usr.UserType = Convert.ToInt32(dr["staff"].ToString().Trim());
                        usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Trim();


                        usr.JoiningDate = ((DateTime)dr["JoiningDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Trim();
                        usr.AccountNumber = dr["AccountNumber"].ToString().Trim();

                        usr.BankName = dr["BankName"].ToString().Trim();
                        usr.TotalExperience = dr["Experience"].ToString().Trim();

                        usr.AdhaarNo = dr["AdharNo"].ToString().Trim();
                        usr.BiometricId = dr["BiometricID"].ToString().Trim();
                        usr.PfNo = dr["PFNo"].ToString().Trim();
                        usr.PANNo = dr["PANNO"].ToString().Trim();
                        usr.EsiNo = dr["ESINO"].ToString().Trim();
                        usr.IfscCode = dr["IFSCCode"].ToString().Trim();


                        usr.Gender = dr["GenderName"].ToString().Trim();

                        usr.Mobile = dr["Mobile"].ToString().Trim();
                        usr.Email = dr["Email"].ToString().Trim();

                        usr.Pin = dr["Pincode"].ToString().Trim();
                        usr.PresentAddress = dr["PresentAddress"].ToString().Trim();
                        usr.PermanentAddress = dr["PermAddress"].ToString().Trim();
                        usr.MaritalStaus = dr["MaritalSatus"].ToString().Trim();


                        usr.FatherName = dr["FatherName"].ToString().Trim();
                        usr.MotherName = dr["MotherName"].ToString().Trim();
                        usr.Religon = dr["ReligionName"].ToString().Trim();
                        usr.Country = dr["CountryName"].ToString().Trim();
                        usr.State = dr["StateName"].ToString().Trim();
                        usr.city = dr["CityName"].ToString().Trim();
                        usr.UserId = dr["UserId"].ToString().Trim();

                        usr.Pwd = dr["Pwd"].ToString().Trim();


                        if (dr["Image"].ToString().Trim() == "")
                        {
                            usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                        }
                        else
                        {
                            usr.ImageUpload = dr["Image"].ToString().Trim();
                        }
                        //  usr.Status = dr["Status"].ToString();
                        if (dr["Status"].ToString().Trim() == "True")
                        {

                            usr.Status = "Activate";
                            usr.Extra10 = "#00a65a";

                        }
                        else
                        {
                            usr.Status = "DeActivate";
                            usr.Extra10 = "#dd4b39";

                        }
                        usr.qualfication = dr["QualificationName"].ToString().Trim();
                        list.Add(usr);
                    }
                    con.Close();
                }
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/ReportingAPI/searchEmployeeReporting")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] searchEmployeeReporting(EmployeeEm employye)
        {
            try
            {
                sqlHelper obj = new sqlHelper();
                List<EmployeeEm> list = new List<EmployeeEm>();

                string[] cols = { "@empcode", "@empName", "@School", "@department", "@designation", "@staff", "@status" };
                object[] vals = { employye.Employeecode, employye.FName, employye.School, employye.Department, employye.Designation, employye.UserType, employye.Status };

                DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeReporting", cols, vals);
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Id = dr["Id"].ToString().Trim();
                    usr.Employeecode = dr["Empcode"].ToString().Trim();
                    usr.FName = dr["FirstName"].ToString().Trim();
                    usr.MName = dr["MiddleName"].ToString().Trim();
                    usr.LName = dr["LastName"].ToString().Trim();
                    usr.School = dr["School"].ToString().Trim();
                    usr.Department = dr["DepartmentName"].ToString().Trim();
                    usr.Designation = dr["Designation"].ToString().Trim();
                    usr.UserType = dr["staff"].ToString().Trim();
                    usr.DOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Trim();


                    usr.JoiningDate = ((DateTime)dr["JoiningDate"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Trim();
                    usr.AccountNumber = dr["AccountNumber"].ToString().Trim();

                    usr.BankName = dr["BankName"].ToString().Trim();
                    usr.TotalExperience = dr["Experience"].ToString().Trim();

                    usr.AdhaarNo = dr["AdharNo"].ToString().Trim();
                    usr.BiometricId = dr["BiometricID"].ToString().Trim();
                    usr.PfNo = dr["PFNo"].ToString().Trim();
                    usr.PANNo = dr["PANNO"].ToString().Trim();
                    usr.EsiNo = dr["ESINO"].ToString().Trim();
                    usr.IfscCode = dr["IFSCCode"].ToString().Trim();


                    usr.Gender = dr["GenderName"].ToString().Trim();

                    usr.Mobile = dr["Mobile"].ToString().Trim();
                    usr.Email = dr["Email"].ToString().Trim();

                    if (dr["Image"].ToString().Trim() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString().Trim();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString().Trim() == "True")
                    {

                        usr.Status = "Activate";
                        usr.Extra10 = "#00a65a";

                    }
                    else
                    {
                        usr.Status = "DeActivate";
                        usr.Extra10 = "#dd4b39";

                    }
                    usr.qualfication = dr["QualificationName"].ToString().Trim();
                    list.Add(usr);
                }
                return list.ToArray();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }

        [System.Web.Http.Route("api/ReportingAPI/getAllStudentsDetailsReports")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsDetailsReports(List<string> aa)
        {
            try
            {
                string loginuser = aa[0];
                int typeuser = Convert.ToInt32(aa[1]);
                sqlHelper obj = new sqlHelper();
                List<Student> list = new List<Student>();
                string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                string query = "";
                SqlConnection con = new SqlConnection(constr);

                if (typeuser == 2)
                {




                    DataTable dt = obj.sp_GetDataTableNoParam("sp_getStudentDetailsReports");
                    foreach (DataRow dr in dt.Rows)
                    {
                        Student usr = new Student();
                        usr.ID = int.Parse(dr["ID"].ToString());
                        usr.RegNo = dr["RegNo"].ToString();
                        usr.FirstName = dr["FirstName"].ToString();
                        usr.MiddleName = dr["MiddleName"].ToString();
                        usr.LastName = dr["LastName"].ToString();
                        usr.Section = dr["SectionName"].ToString();
                        usr.School = dr["School"].ToString();
                        usr.Class = dr["Class"].ToString();
                        usr.RollNo = dr["RollNo"].ToString();
                        usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                        usr.SStatus = dr["StatusName"].ToString();

                        usr.FatherName = dr["FatherName"].ToString();
                        usr.MotherName = dr["MotherName"].ToString();
                        usr.FMobile = dr["FMobile"].ToString();
                        usr.Mmobile = dr["Mmobile"].ToString();
                        // usr.DOB = Convert.ToDateTime(((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                        usr.SDOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        usr.FAdharNo = dr["FAdharNo"].ToString();
                        usr.MAdharNo = dr["MAdharNo"].ToString();

                        usr.Fmail = dr["Fmail"].ToString();
                        usr.Mmail = dr["Mmail"].ToString();
                        usr.Gender = dr["GenderName"].ToString();
                        usr.BloodGroup = dr["BloodGroup"].ToString();
                        usr.Caste = dr["CategoryName"].ToString();
                        usr.BirthPlace = dr["BirthPlace"].ToString();
                        usr.GuardianName = dr["GuardianName"].ToString();
                        usr.Gmobile = dr["Gmobile"].ToString();
                        usr.GAdharNo = dr["GAdharNo"].ToString();
                        usr.Gmail = dr["Gmail"].ToString();
                        usr.EmergencyNo = dr["EmergencyNo"].ToString();
                        usr.EmerContPerson = dr["EmerContPerson"].ToString();
                        usr.stream = dr["StreamName"].ToString();
                        usr.AcademicYear = dr["AcademicYear"].ToString();

                        usr.AadharNo = dr["AadharNo"].ToString();
                        usr.jDate = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        usr.batch = dr["BatchName"].ToString();
                        usr.Religion = dr["ReligionName"].ToString();
                        usr.Nationality = dr["Nationality"].ToString();
                        usr.MotherTongue = dr["MotherTongue"].ToString();
                        usr.Languages = dr["Language"].ToString();
                        usr.PermanentAddress = dr["PermanentAddress"].ToString();
                        usr.CorrespondAddress = dr["CorrespondAddress"].ToString();
                        usr.PPincode = dr["PermPincode"].ToString();
                        usr.CPincode = dr["CorsPincode"].ToString();

                        usr.PermcountrynameNm = dr["PermCountry"].ToString();
                        usr.PermState = dr["PermState"].ToString();
                        usr.PermCity = dr["PermCity"].ToString();
                        usr.country = dr["CorsCountry"].ToString();
                        usr.CorsState = dr["CorsState"].ToString();
                        usr.CorsCity = dr["CorsCity"].ToString();
                        usr.Relation = dr["Relation"].ToString();
                        usr.ContPersRelation = dr["ContPersRelation"].ToString();
                        usr.DestinationNm = dr["DestinationID"].ToString();

                        usr.SUserID = dr["SUserID"].ToString();
                        usr.PUserID = dr["PUserID"].ToString();
                        usr.BiometricID = dr["BiometricID"].ToString();
                        usr.FQualification = dr["FQualification"].ToString();
                        usr.MQualification = dr["MQualification"].ToString();
                        usr.Route = dr["RouteCode"].ToString();






                        list.Add(usr);
                    }

                }
                else
                {
                    //                    con.Open();
                    //                    query = @"select st.ID,RegNo,scd.School,c.CourseName Class,st.FirstName,st.MiddleName,st.LastName,PicPath,
                    //RollNo,s.SectionName,st.DOB,st.FatherName,st.SMSmobileNo,st.MotherName,
                    //st.FMobile,st.Mmobile,ss.Status StatusName,st.Status,
                    //st.AadharNo,st.FAdharNo,st.MAdharNo,st.Fmail,st.Mmail,
                    //g.GenderName,stre.StreamName,st.emailID,st.BloodGroup,
                    //cct.CategoryName,st.AadharNo,
                    //st.BirthPlace,st.GuardianName,st.Gmobile,st.GAdharNo,
                    //st.Gmail,st.EmergencyNo,st.EmerContPerson,st.AcademicYear,
                    //st.JoiningDate from TBLStudent st
                    //left join tblCourses c on c.Id=st.ClassID
                    //left join tblSections s on s.Id=st.SectionID
                    //left outer join tblStatus ss on ss.StatusID=st.Status 
                    //left outer join tblStream stre on stre.Id=st.StreamID 
                    //left outer join tblCastCategory cct on cct.CatId=st.CategoryID 
                    //left outer join tblGender g on g.gender_id=st.GenderID 
                    //left outer join tblSchoolDetails scd on scd.ID= st.SchoolID
                    //left outer join tblemployee em on st.SchoolID =em.SchoolID
                    //where em.UserID='" + loginuser + "' and em.IsDeleted is null and st.IsDeleted is null";
                    //                    SqlCommand cmd = new SqlCommand(query, con);
                    //                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //                    DataTable dt = new DataTable();
                    //                    adap.Fill(dt);
                    DataTable dt = obj.sp_GetDataTableNoParam("sp_getStudentDetailsReports");

                    foreach (DataRow dr in dt.Rows)
                    {
                        Student usr = new Student();
                        usr.ID = int.Parse(dr["ID"].ToString());
                        usr.RegNo = dr["RegNo"].ToString();
                        usr.FirstName = dr["FirstName"].ToString();
                        usr.MiddleName = dr["MiddleName"].ToString();
                        usr.LastName = dr["LastName"].ToString();
                        usr.Section = dr["SectionName"].ToString();
                        usr.School = dr["School"].ToString();
                        usr.Class = dr["Class"].ToString();
                        usr.RollNo = dr["RollNo"].ToString();
                        usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                        usr.SStatus = dr["StatusName"].ToString();

                        usr.FatherName = dr["FatherName"].ToString();
                        usr.MotherName = dr["MotherName"].ToString();
                        usr.FMobile = dr["FMobile"].ToString();
                        usr.Mmobile = dr["Mmobile"].ToString();
                        // usr.DOB = Convert.ToDateTime(((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                        usr.SDOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        usr.FAdharNo = dr["FAdharNo"].ToString();
                        usr.MAdharNo = dr["MAdharNo"].ToString();

                        usr.Fmail = dr["Fmail"].ToString();
                        usr.Mmail = dr["Mmail"].ToString();
                        usr.Gender = dr["GenderName"].ToString();
                        usr.BloodGroup = dr["BloodGroup"].ToString();
                        usr.Caste = dr["CategoryName"].ToString();
                        usr.BirthPlace = dr["BirthPlace"].ToString();
                        usr.GuardianName = dr["GuardianName"].ToString();
                        usr.Gmobile = dr["Gmobile"].ToString();
                        usr.GAdharNo = dr["GAdharNo"].ToString();
                        usr.Gmail = dr["Gmail"].ToString();
                        usr.EmergencyNo = dr["EmergencyNo"].ToString();
                        usr.EmerContPerson = dr["EmerContPerson"].ToString();
                        usr.stream = dr["StreamName"].ToString();
                        usr.AcademicYear = dr["AcademicYear"].ToString();

                        usr.batch = dr["BatchName"].ToString();
                        usr.Religion = dr["ReligionName"].ToString();
                        usr.Nationality = dr["Nationality"].ToString();
                        usr.MotherTongue = dr["MotherTongue"].ToString();
                        usr.Languages = dr["Language"].ToString();
                        usr.PermanentAddress = dr["PermanentAddress"].ToString();
                        usr.CorrespondAddress = dr["CorrespondAddress"].ToString();
                        usr.PPincode = dr["PermPincode"].ToString();
                        usr.CPincode = dr["CorsPincode"].ToString();

                        usr.PermcountrynameNm = dr["PermCountry"].ToString();
                        usr.PermState = dr["PermState"].ToString();
                        usr.PermCity = dr["PermCity"].ToString();
                        usr.country = dr["CorsCountry"].ToString();
                        usr.CorsState = dr["CorsState"].ToString();
                        usr.CorsCity = dr["CorsCity"].ToString();
                        usr.Relation = dr["Relation"].ToString();
                        usr.ContPersRelation = dr["ContPersRelation"].ToString();
                        usr.DestinationNm = dr["DestinationID"].ToString();

                        usr.SUserID = dr["SUserID"].ToString();
                        usr.PUserID = dr["PUserID"].ToString();
                        usr.BiometricID = dr["BiometricID"].ToString();
                        usr.FQualification = dr["FQualification"].ToString();
                        usr.MQualification = dr["MQualification"].ToString();
                        usr.Route = dr["RouteCode"].ToString();




                        int AcY = Convert.ToInt32(usr.AcademicYear);
                        if (AcY != 0 && AcY != -1)
                        {
                            var academicyear = db.tblAcademicYears.Where(x => x.ID == AcY && x.IsDeleted == null).FirstOrDefault();
                            if (academicyear != null)
                            {
                                usr.AcademicYear = academicyear.DateFrom.Year + "-" + academicyear.DateTo.ToString("yy");
                            }

                            else
                            {
                                usr.AcademicYear = "";
                            }
                        }
                        usr.AadharNo = dr["AadharNo"].ToString();
                        usr.jDate = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);



                        list.Add(usr);
                    }
                    con.Close();
                }
                return list.ToArray();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }


        [System.Web.Http.Route("api/ReportingAPI/getEmployeeColumnForGrids")]
        [System.Web.Http.HttpPost]
        public Student[] getEmployeeColumnForGrids()
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();


            string query = @"EmployeeCode,JoiningDate,School,Department,Designation,Qualification,Experience,Staff,FirstName,MiddleName,LastName,DOB, Mobile,Email, AdharNo,BiometricID, Status ,AccountNumber, IFSCCode, PFNo,  PANNO,ESINO, BankName,Gender,UserId,City,State,Country,PinCode,Present_Address,Permanent_Address,Marital_Status,Religion,FatherName,MotherName";
            string[] column = query.Split(',');
            for (int i = 0; i < column.Length; i++)
            {
                Student usr = new Student();
                usr.Section = column[i].Trim();
                list.Add(usr);
            }


            return list.ToArray();
        }

        [System.Web.Http.Route("api/ReportingAPI/getStudentColumnForGrids")]
        [System.Web.Http.HttpPost]
        public Student[] getStudentColumnForGrids()
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();


            string query = @"AcademicYear,RegistrationNo,JoiningDate,School,Class,Section,Stream,FirstName,MiddleName,LastName,DOB,Gender,BloodGroup,BirthPlace,Caste,AadharNo,Sms mobileNo,Email,Father Name,Father Mobile,Father Email,Father AdhaarNo,MotherName,Mother Mobile,Mother Email,Mother AdharNo,GuardianName,Guardian Mobile,Guardian AdharNo,Guardian Email,EmergencyNo,Emergency Contact Person,Status,RollNo,Batch,Religion,Nationality,MotherTongue,Language,PermanentAddress,CorrespondAddress,PermPincode,PermState,PermCity,PermCountry,CorsPincode,CorsState,CorsCity,CrosCountry,FatherQualification,MotherQualification,Relation,Contact Person Relation,Route,Destintion,Student UserId,Parent UserId,BiomatricId";
            string[] column = query.Split(',');
            for (int i = 0; i < column.Length; i++)
            {
                Student usr = new Student();
                usr.Section = column[i].Trim();
                list.Add(usr);
            }


            return list.ToArray();
        }





        [System.Web.Http.Route("api/ReportingAPI/searchStudentReporting")]
        [System.Web.Http.HttpPost]
        public Student[] searchStudentReporting(Student std)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();

            string[] cols = { "@studentcode", "@stdname", "@class", "@section", "@rollno", "@status", "@School" };
            object[] vals = { std.RegNo, std.FirstName, std.Class, std.Section, std.RollNo, std.SStatus, std.School };

            DataTable dt = obj.sp_GetDataTable("sp_searchStudentReporting", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.ID = int.Parse(dr["ID"].ToString());
                usr.RegNo = dr["RegNo"].ToString();
                usr.FirstName = dr["FirstName"].ToString();
                usr.MiddleName = dr["MiddleName"].ToString();
                usr.LastName = dr["LastName"].ToString();
                usr.Section = dr["SectionName"].ToString();
                usr.Class = dr["Class"].ToString();
                usr.RollNo = dr["RollNo"].ToString();
                usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                usr.SStatus = dr["StatusName"].ToString();

                usr.FatherName = dr["FatherName"].ToString();
                usr.MotherName = dr["MotherName"].ToString();
                usr.FMobile = dr["FMobile"].ToString();
                usr.Mmobile = dr["Mmobile"].ToString();
                usr.SDOB = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                usr.FAdharNo = dr["FAdharNo"].ToString();
                usr.MAdharNo = dr["MAdharNo"].ToString();

                usr.Fmail = dr["Fmail"].ToString();
                usr.Mmail = dr["Mmail"].ToString();
                usr.Gender = dr["GenderName"].ToString();
                usr.BloodGroup = dr["BloodGroup"].ToString();
                usr.Caste = dr["CategoryName"].ToString();
                usr.BirthPlace = dr["BirthPlace"].ToString();
                usr.GuardianName = dr["GuardianName"].ToString();
                usr.Gmobile = dr["Gmobile"].ToString();
                usr.GAdharNo = dr["GAdharNo"].ToString();
                usr.Gmail = dr["Gmail"].ToString();
                usr.EmergencyNo = dr["EmergencyNo"].ToString();
                usr.EmerContPerson = dr["EmerContPerson"].ToString();
                usr.stream = dr["StreamName"].ToString();
                usr.AcademicYear = dr["AcademicYear"].ToString();

                int AcY = Convert.ToInt32(usr.AcademicYear);
                if (AcY != 0 && AcY != -1)
                {
                    var academicyear = db.tblAcademicYears.Where(x => x.ID == AcY && x.IsDeleted == null).FirstOrDefault();
                    if (academicyear != null)
                    {
                        usr.AcademicYear = academicyear.DateFrom.Year + "-" + academicyear.DateTo.ToString("yy");
                    }
                    else
                    {
                        usr.AcademicYear = "";
                    }
                }

                usr.School = dr["School"].ToString();
                usr.AadharNo = dr["AadharNo"].ToString();
                usr.jDate = ((DateTime)dr["DOB"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                usr.batch = dr["BatchName"].ToString();
                usr.Religion = dr["ReligionName"].ToString();
                usr.Nationality = dr["Nationality"].ToString();
                usr.MotherTongue = dr["MotherTongue"].ToString();
                usr.Languages = dr["Language"].ToString();
                usr.PermanentAddress = dr["PermanentAddress"].ToString();
                usr.CorrespondAddress = dr["CorrespondAddress"].ToString();
                usr.PPincode = dr["PermPincode"].ToString();
                usr.CPincode = dr["CorsPincode"].ToString();

                usr.PermcountrynameNm = dr["PermCountry"].ToString();
                usr.PermState = dr["PermState"].ToString();
                usr.PermCity = dr["PermCity"].ToString();
                usr.country = dr["CorsCountry"].ToString();
                usr.CorsState = dr["CorsState"].ToString();
                usr.CorsCity = dr["CorsCity"].ToString();
                usr.Relation = dr["Relation"].ToString();
                usr.ContPersRelation = dr["ContPersRelation"].ToString();
                usr.DestinationNm = dr["DestinationID"].ToString();

                usr.SUserID = dr["SUserID"].ToString();
                usr.PUserID = dr["PUserID"].ToString();
                usr.BiometricID = dr["BiometricID"].ToString();
                usr.FQualification = dr["FQualification"].ToString();
                usr.MQualification = dr["MQualification"].ToString();
                usr.Route = dr["RouteCode"].ToString();






                list.Add(usr);
            }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/ReportingAPI/getAllAbsentStudentNameForThisclass")]
        [System.Web.Http.HttpPost]
        public Student[] getAllAbsentStudentNameForThisclass(EmployeeEm em)
        {

            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            string[] cols = { "@class", "@section", "@stuname", "@regcode", "@School" };
            object[] vals = { em.Extra1, em.Extra2, em.FName, em.Employeecode, em.School };
            DataTable dt = obj.sp_GetDataTable("sp_searchStudentattendenceReports", cols, vals);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Student usr = new Student();
                    usr.attendenceList = new List<StudentAttendenceDetails>();
                    usr.ID = int.Parse(dr["ID"].ToString());
                    usr.RegNo = dr["RegNo"].ToString();
                    usr.SchoolID = dr["SchoolID"].ToString();
                    int SchoolIDD = Convert.ToInt32(dr["SchoolID"]);
                    usr.ID = Convert.ToInt32(dr["ID"]);
                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,* from tblStudentAttendence where   cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + usr.ID + "  and  cast(year(AttendenceDate) as varchar(50))='" + em.Year + "' and SchoolID='" + usr.SchoolID + "'");
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                        ////usr.StudentId = dr["StudentId"].ToString();
                        ////usr.ClassID = dr["ClassId"].ToString();

                        //usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                        //usr1.AttendenceType = dr1["AttendenceType"].ToString();
                        //if (dr1["AttendenceType"].ToString() == "Present")
                        //{
                        //    usr1.AttendenceType = "P";
                        //}
                        //else if (dr1["AttendenceType"].ToString() == "Absent")
                        //{
                        //    usr1.AttendenceType = "A";
                        //}
                        //else
                        //{
                        //    usr1.AttendenceType = "L";
                        //}
                        //usr.attendenceList.Add(usr1);
                        usr1.Idd = Convert.ToInt32(dr1["StudentId"]);
                        usr1.StudentId = dr1["StudentId"].ToString();
                        usr1.ClassID = dr1["ClassId"].ToString();
                        usr1.SectionId = dr1["SectionId"].ToString();

                        usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                        usr1.AttendenceRealDate = Convert.ToDateTime(dr1["AttendenceDate"]);
                        usr1.AttendenceType = dr1["AttendenceType"].ToString();
                        if (dr1["AttendenceType"].ToString() == "Present")
                        {
                            usr1.AttendenceType = "P";
                        }

                        else if (dr1["AttendenceType"].ToString() == "Absent" /*&& Convert.ToBoolean(dr1["IsBiometric"]) == true*/)
                        {
                            //Leave 

                            var checkleave = db.tblStudentLeaveApplies.Where(lv => lv.StudentID == usr1.Idd && lv.datefrom <= usr1.AttendenceRealDate && lv.dateto >= usr1.AttendenceRealDate && lv.IsDeleted == null && lv.Status == 5 && lv.IsDeleted == null).FirstOrDefault();
                            if (checkleave != null)
                            {
                                usr1.AttendenceType = "L";

                            }
                            //Holiday
                            else
                            {
                                var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == SchoolIDD && Hl.IsDeleted == null).FirstOrDefault();
                                if (checkHoliday != null)
                                {
                                    usr1.AttendenceType = "H";

                                }
                                else
                                {
                                    usr1.AttendenceType = "A";
                                }
                            }


                        }
                        usr.attendenceList.Add(usr1);
                    }

                    list.Add(usr);


                }

            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/ReportingAPI/getTeacherAllocationReports")]
        [System.Web.Http.HttpPost]
        public Student[] getTeacherAllocationReports(List<string> aa)
        {

            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();

            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select c.CourseName,scd.School,s.SectionName,e.FirstName+' '+e.MiddleName+' '+e.LastName EmployeeName,
                                        d.DepartmentName, dg.Designation, ct.* from tblClassTeacherAllocation ct
                                        left outer join tblCourses c on c.Id = ct.ClassID
                                        left outer join tblSections s on s.Id = ct.SectionID
                                        left outer join tblemployee e on e.Id = ct.intEmpID
                                        left outer join tblDepartmnet d on d.DepartmentId = e.DeptID
                                        left outer join tblDesignation dg on dg.DesigID = e.DeptID
                                       left outer join tblSchoolDetails scd on scd.ID= ct.SchoolID
                                       where  ct.Status =1");
                //  string query = @"AcademicYear,RegistrationNo,JoiningDate,Class,Section,Stream,RollNo,FirstName,MiddleName,LastName,DOB,Gender,BloodGroup,BirthPlace,Caste,AadharNo,Sms mobileNo,Email,Father Name,Father Mobile,Father Email,Father AdhaarNo,MotherName,Mother Mobile,Mother Email,Mother AdharNo,GuardianName,Guardian Mobile,Guardian AdharNo,Guardian Email,EmergencyNo,Emergency Contact Person,Status";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Student usr = new Student();
                        usr.School = dr["School"].ToString();
                        usr.Class = dr["CourseName"].ToString();
                        usr.Section = dr["SectionName"].ToString();
                        usr.FirstName = dr["EmployeeName"].ToString();
                        usr.FDesig = dr["DepartmentName"].ToString();
                        list.Add(usr);
                    }
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select c.CourseName,scd.School,s.SectionName,e.FirstName+' '+e.MiddleName+' '+e.LastName EmployeeName,
                                        d.DepartmentName, dg.Designation, ct.* from tblClassTeacherAllocation ct
                                        left outer join tblCourses c on c.Id = ct.ClassID
                                        left outer join tblSections s on s.Id = ct.SectionID
                                        left outer join tblemployee e on e.Id = ct.intEmpID
                                        left outer join tblDepartmnet d on d.DepartmentId = e.DeptID
                                        left outer join tblDesignation dg on dg.DesigID = e.DeptID
                                       left outer join tblSchoolDetails scd on scd.ID= ct.SchoolID
									   left outer join tblEmployee em on em.SchoolID=ct.SchoolID
                                        where em.UserID='" + loginuser + "' and em.IsDeleted is null and  ct.Status =1");
                //  string query = @"AcademicYear,RegistrationNo,JoiningDate,Class,Section,Stream,RollNo,FirstName,MiddleName,LastName,DOB,Gender,BloodGroup,BirthPlace,Caste,AadharNo,Sms mobileNo,Email,Father Name,Father Mobile,Father Email,Father AdhaarNo,MotherName,Mother Mobile,Mother Email,Mother AdharNo,GuardianName,Guardian Mobile,Guardian AdharNo,Guardian Email,EmergencyNo,Emergency Contact Person,Status";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Student usr = new Student();
                        usr.School = dr["School"].ToString();
                        usr.Class = dr["CourseName"].ToString();
                        usr.Section = dr["SectionName"].ToString();
                        usr.FirstName = dr["EmployeeName"].ToString();
                        usr.FDesig = dr["DepartmentName"].ToString();
                        list.Add(usr);
                    }
                }
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/ReportingAPI/getAllClassTeacherbYSchool")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllClassTeacherbYSchool(List<string> aa)
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            int SchoolID = Convert.ToInt32(aa[0]);
            //DataTable dt = obj.getDataTable(@"select EmpID,e.FirstName+' '+e.MiddleName+' '+e.LastName EmployeeName from tblClassTeacherAllocation ct
            //                        inner join tblemployee e on e.Id=ct.EmpID
            //                        where ct.Status=3 and IsDeleted is null and ");

            DataTable dt = obj.getDataTable(@"select intEmpID,e.FirstName+' '+e.MiddleName+' '+e.LastName EmployeeName from tblClassTeacherAllocation ct
                                    inner join tblemployee e on e.Id=ct.intEmpID
                                    where ct.Status=1 and e.IsDeleted is null and ct.SchoolID='" + SchoolID + "' ");

            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["intEmpID"].ToString();
                usr.Name = dr["EmployeeName"].ToString();

                list.Add(usr);
            }
            return list.ToArray();


        }

        [System.Web.Http.Route("api/ReportingAPI/getAllClassTeacher")]
        [System.Web.Http.HttpPost]
        public DepartmentMaster[] getAllClassTeacher()
        {
            List<DepartmentMaster> list = new List<DepartmentMaster>();
            sqlHelper obj = new sqlHelper();
            DataTable dt = obj.getDataTable(@"select EmpID,e.FirstName+' '+e.MiddleName+' '+e.LastName EmployeeName from tblClassTeacherAllocation ct
                                    inner join tblemployee e on e.Id=ct.EmpID
                                    where ct.Status=3");
            foreach (DataRow dr in dt.Rows)
            {
                DepartmentMaster usr = new DepartmentMaster();
                usr.Id = dr["EmpID"].ToString();
                usr.Name = dr["EmployeeName"].ToString();

                list.Add(usr);
            }
            return list.ToArray();


        }


        [System.Web.Http.Route("api/ReportingAPI/searchClassTeacherReport")]
        [System.Web.Http.HttpPost]
        public Student[] searchClassTeacherReport(Student std)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();

            string[] cols = { "@class", "@section", "@teacherName", "@School" };
            object[] vals = { std.Class, std.Section, std.FirstName, std.School };

            DataTable dt = obj.sp_GetDataTable("sp_searchClassTeacherReport", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.School = dr["School"].ToString();
                usr.Class = dr["CourseName"].ToString();
                usr.Section = dr["SectionName"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();

                usr.FDesig = dr["DepartmentName"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/ReportingAPI/getAllEmployeeLeaveRequest")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] getAllEmployeeLeaveRequest(List<string> id)
        {

            string loginuser = id[0];
            int typeuser = Convert.ToInt32(id[1]);

            sqlHelper obj = new sqlHelper();
            List<EmployeeEm> list = new List<EmployeeEm>();
            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select sc.School,lr.SchoolID,e.FirstName,e.Empcode,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,lt.LeaveName,s.Status ,lr.* from tblemployeeLeaveRequest lr
                                        left outer join tblEmployee e on e.Id=lr.EmployeeID
                                        left outer join tblDepartmnet d on d.DepartmentId=lr.Department
                                        left outer join tblDesignation dd on dd.DesigID=lr.Designation
                                        left outer join tblLeaveType lt on lt.LeaveId=lr.LeaveType
                                        left outer join tblStatus s on s.StatusID=lr.LeavStatus  
                                        left outer join tblSchoolDetails sc on lr.SchoolID=sc.ID
                                        order by lr.DateCreated desc
                                                ");

                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Extra1 = dr["LeaveName"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.Department = dr["DepartmentName"].ToString();
                    usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }



                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString() == "Pending")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "#f39c12";
                    }
                    else if (dr["Status"].ToString() == "Approved")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "#00a65a";
                    }
                    else
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "#dd4b39";
                    }

                    usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.Extra3 = dr["TakeLeaveCount"].ToString();
                    usr.Extra4 = dr["LeaveName"].ToString();
                    usr.Month = dr["Reason"].ToString();
                    usr.Employeecode = dr["Empcode"].ToString();

                    usr.Id = dr["Id"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select sc.School,lr.SchoolID,e.FirstName,e.Empcode,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,lt.LeaveName,s.Status ,lr.* from tblemployeeLeaveRequest lr
                                        left outer join tblEmployee e on e.Id=lr.EmployeeID
                                        left outer join tblDepartmnet d on d.DepartmentId=lr.Department
                                        left outer join tblDesignation dd on dd.DesigID=lr.Designation
                                        left outer join tblLeaveType lt on lt.LeaveId=lr.LeaveType
                                        left outer join tblStatus s on s.StatusID=lr.LeavStatus  
                                        left outer join tblSchoolDetails sc on lr.SchoolID=sc.ID
										left outer join tblEmployee em on em.SchoolID = lr.SchoolID
										where em.UserID='" + loginuser + "' and em.IsDeleted is null order by lr.DateCreated desc ");

                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Extra1 = dr["LeaveName"].ToString();
                    usr.Designation = dr["Designation"].ToString();
                    usr.Department = dr["DepartmentName"].ToString();
                    usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }



                    //  usr.Status = dr["Status"].ToString();
                    if (dr["Status"].ToString() == "Pending")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "#f39c12";
                    }
                    else if (dr["Status"].ToString() == "Approved")
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "#00a65a";
                    }
                    else
                    {
                        usr.Status = dr["Status"].ToString();
                        usr.Extra10 = "#dd4b39";
                    }

                    usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.Extra3 = dr["TakeLeaveCount"].ToString();
                    usr.Extra4 = dr["LeaveName"].ToString();
                    usr.Month = dr["Reason"].ToString();
                    usr.Employeecode = dr["Empcode"].ToString();

                    usr.Id = dr["Id"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    list.Add(usr);
                }
            }

            return list.ToArray();
        }


        [System.Web.Http.Route("api/ReportingAPI/searchEmployeeForLeaveRequest")]
        [System.Web.Http.HttpPost]
        public EmployeeEm[] searchEmployeeForLeaveRequest(EmployeeEm employye)
        {

            try
            {
                DateTime startDate = DateTime.ParseExact(employye.Extra5, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(employye.Extra6, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //sqlHelper obj = new sqlHelper();
                List<EmployeeEm> list = new List<EmployeeEm>();

                var result = (from lr in db.tblemployeeLeaveRequests
                              join e in db.tblEmployees on lr.EmployeeID equals e.Id
                              join d in db.tblDepartmnets on lr.Department equals d.DepartmentId
                              join dd in db.tblDesignations on lr.Designation equals dd.DesigID
                              join lt in db.tblLeaveTypes on lr.LeaveType equals lt.LeaveId
                              join s in db.tblStatus on lr.LeavStatus equals s.StatusID
                              join scd in db.tblSchoolDetails on lr.SchoolID equals scd.ID
                              where lr.IsDeleted == null
                              select new
                              {
                                  model = lr,
                                  Schoolnamee = scd.School,
                                  firstnamee = e.FirstName,
                                  EmployeeCodee = e.Empcode,
                                  middlenamee = e.MiddleName,
                                  lastnamee = e.LastName,
                                  departmentnamee = d.DepartmentName,
                                  designationn = dd.Designation,
                                  imagee = e.Image,
                                  StaffCategoryy = e.StaffCategory,
                                  LeaveNamee = lt.LeaveName,
                                  Statuss = s.Status


                              }
                             ).ToList();

                if (startDate != null && endDate != null)
                {
                    result = result.Where(x => x.model.StatDate > startDate && x.model.EndDate < endDate).ToList();

                }
                if (employye.Employeecode != "")
                {
                    result = result.Where(x => x.EmployeeCodee == employye.Employeecode).ToList();

                }
                if (employye.FName != "")
                {
                    result = result.Where(x => (x.firstnamee + "" + x.middlenamee + "" + x.lastnamee).Contains(employye.FName)).ToList();

                }

                if (employye.UserType != "0")
                {
                    int leavestatus = Convert.ToInt32(employye.UserType);
                    result = result.Where(x => x.model.LeavStatus == leavestatus).ToList();

                }
                if (employye.Department != "0")
                {
                    int departm = Convert.ToInt32(employye.Department);
                    result = result.Where(x => x.model.Department == departm).ToList();

                }
                if (employye.Designation != "0")
                {
                    int desig = Convert.ToInt32(employye.Designation);
                    result = result.Where(x => x.model.Designation == desig).ToList();

                }
                if (employye.qualfication != "0")
                {
                    int staff = Convert.ToInt32(employye.qualfication);

                    result = result.Where(x => x.StaffCategoryy == staff).ToList();

                }

                if (employye.State != "0")
                {
                    int status = Convert.ToInt32(employye.State);

                    result = result.Where(x => x.model.LeaveType == status).ToList();

                }
                if (employye.School != "0")
                {
                    int SchoolID = Convert.ToInt32(employye.School);

                    result = result.Where(x => x.model.SchoolID == SchoolID).ToList();

                }
                foreach (var m in result)
                {
                    EmployeeEm usr = new EmployeeEm();
                    usr.Extra1 = m.LeaveNamee.ToString();
                    usr.School = m.Schoolnamee.ToString();
                    usr.Designation = m.designationn.ToString();
                    usr.Department = m.departmentnamee.ToString();



                    usr.FName = m.firstnamee.ToString() + " " + m.middlenamee.ToString() + " " + m.lastnamee.ToString();


                    if (m.imagee == null)
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = m.imagee.ToString();
                    }



                    //  usr.Status = dr["Status"].ToString();
                    if (m.Statuss.ToString() == "Pending")
                    {
                        usr.Status = m.Statuss.ToString();
                        usr.Extra10 = "#f39c12";
                    }
                    else if (m.Statuss.ToString() == "Approved")
                    {
                        usr.Status = m.Statuss.ToString();
                        usr.Extra10 = "#00a65a";
                    }
                    else
                    {
                        usr.Status = m.Statuss.ToString();
                        usr.Extra10 = "#dd4b39";
                    }

                    usr.Extra2 = ((DateTime)m.model.StatDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)m.model.EndDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.Extra3 = m.model.TakeLeaveCount.ToString();
                    usr.Extra4 = m.LeaveNamee.ToString();

                    usr.Employeecode = m.EmployeeCodee.ToString();

                    usr.Id = m.model.Id.ToString();

                    list.Add(usr);

                }
                //string[] cols = { "@empcode", "@empName", "@startdate", "@enddate", "@status",
                //                           "@dept","@desg","@staff","@leaveType","@School" };
                //object[] vals = { employye.Employeecode, employye.FName, startDate, endDate, employye.UserType,
                //employye.Department,employye.Designation,employye.qualfication,employye.State,employye.School };

                //DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeForLeaveRequestReport", cols, vals);
                //    foreach (DataRow dr in dt.Rows)
                //{
                //    EmployeeEm usr = new EmployeeEm();
                //    usr.Extra1 = dr["LeaveName"].ToString();
                //    usr.School = dr["School"].ToString();
                //    usr.Designation = dr["Designation"].ToString();
                //    usr.Department = dr["DepartmentName"].ToString();
                //    usr.FName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();


                //    if (dr["Image"].ToString() == "")
                //    {
                //        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                //    }
                //    else
                //    {
                //        usr.ImageUpload = dr["Image"].ToString();
                //    }



                //    //  usr.Status = dr["Status"].ToString();
                //    if (dr["Status"].ToString() == "Pending")
                //    {
                //        usr.Status = dr["Status"].ToString();
                //        usr.Extra10 = "#f39c12";
                //    }
                //    else if (dr["Status"].ToString() == "Approved")
                //    {
                //        usr.Status = dr["Status"].ToString();
                //        usr.Extra10 = "#00a65a";
                //    }
                //    else
                //    {
                //        usr.Status = dr["Status"].ToString();
                //        usr.Extra10 = "#dd4b39";
                //    }

                //    usr.Extra2 = ((DateTime)dr["StatDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "-" + ((DateTime)dr["EndDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //    usr.Extra3 = dr["TakeLeaveCount"].ToString();
                //    usr.Extra4 = dr["LeaveName"].ToString();

                //    usr.Employeecode = dr["Empcode"].ToString();

                //    usr.Id = dr["Id"].ToString();

                //    list.Add(usr);
                //}
                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [System.Web.Http.Route("api/ReportingAPI/getSalaryReports")]
        [System.Web.Http.HttpPost]
        public EmployeeSalaryDerails[] getSalaryReports(EmployeeSalaryDerails employye)
        {
            string loginuser = employye.loginuser;
            int typeuser = employye.typeuser;
            DateTime startDate = DateTime.ParseExact(employye.FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(employye.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            sqlHelper obj = new sqlHelper();
            List<EmployeeSalaryDerails> list = new List<EmployeeSalaryDerails>();

            if (typeuser == 2)
            {
                DataTable dt = obj.getDataTable(@"select sc.School,s.SchoolID ,e.FirstName,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,s.* from tblEmployeeSalaryDetails s
                                            left outer join tblEmployee e on e.Id = s.EmployeeId
                                            left outer join tblDesignation dd on dd.DesigID = s.DesignationId
                                            left outer join tblDepartmnet d on d.DepartmentId = e.DeptID
                                            left outer join tblSchoolDetails sc on s.SchoolID=sc.ID
                                            where  s.FromDate >= '" + startDate + "' AND s.toDate <= '" + endDate + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeSalaryDerails usr = new EmployeeSalaryDerails();
                    usr.FromDate = ((DateTime)dr["FromDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.ToDate = ((DateTime)dr["toDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.Designation = dr["Designation"].ToString();
                    usr.NoOfLeave = dr["NoOfLeave"].ToString();
                    usr.NetSalary = dr["NetSalary"].ToString();
                    usr.EmployeeName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();

                    usr.MonthlyGross = dr["MonthlyGross"].ToString();

                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }

                    usr.Id = dr["Id"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToString(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            else
            {
                DataTable dt = obj.getDataTable(@"select sc.School,s.SchoolID ,e.FirstName,e.MiddleName,e.LastName,d.DepartmentName,dd.Designation,e.Image,s.* from tblEmployeeSalaryDetails s
                                            left outer join tblEmployee e on e.Id = s.EmployeeId
                                            left outer join tblDesignation dd on dd.DesigID = s.DesignationId
                                            left outer join tblDepartmnet d on d.DepartmentId = e.DeptID
                                            left outer join tblSchoolDetails sc on s.SchoolID=sc.ID
											left outer join tblEmployee em on em.SchoolID =s.SchoolID
                                            where em.UserID='" + loginuser + "' and em.IsDeleted is null and s.FromDate >= '" + startDate + "' AND s.toDate <= '" + endDate + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeSalaryDerails usr = new EmployeeSalaryDerails();
                    usr.FromDate = ((DateTime)dr["FromDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.ToDate = ((DateTime)dr["toDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    usr.Designation = dr["Designation"].ToString();
                    usr.NoOfLeave = dr["NoOfLeave"].ToString();
                    usr.NetSalary = dr["NetSalary"].ToString();
                    usr.EmployeeName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();

                    usr.MonthlyGross = dr["MonthlyGross"].ToString();

                    if (dr["Image"].ToString() == "")
                    {
                        usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.ImageUpload = dr["Image"].ToString();
                    }

                    usr.Id = dr["Id"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.SchoolID = Convert.ToString(dr["SchoolID"]);
                    list.Add(usr);
                }
            }
            return list.ToArray();
        }




        [System.Web.Http.Route("api/ReportingAPI/searchsalryDetails")]
        [System.Web.Http.HttpPost]
        public EmployeeSalaryDerails[] searchsalryDetails(EmployeeSalaryDerails employye)
        {

            DateTime startDate = DateTime.ParseExact(employye.FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(employye.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            sqlHelper obj = new sqlHelper();
            List<EmployeeSalaryDerails> list = new List<EmployeeSalaryDerails>();

            string[] cols = { "@empcode", "@empName", "@startdate", "@enddate", "@designation", "@School" };
            object[] vals = { employye.Id, employye.EmployeeName, startDate, endDate, employye.Designation, employye.School };

            DataTable dt = obj.sp_GetDataTable("sp_searchEmployeeSalryDetails", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeSalaryDerails usr = new EmployeeSalaryDerails();
                usr.FromDate = ((DateTime)dr["FromDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.ToDate = ((DateTime)dr["toDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                usr.School = dr["School"].ToString();
                usr.Designation = dr["Designation"].ToString();
                usr.NoOfLeave = dr["NoOfLeave"].ToString();
                usr.NetSalary = dr["NetSalary"].ToString();
                usr.EmployeeName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();

                usr.MonthlyGross = dr["MonthlyGross"].ToString();

                if (dr["Image"].ToString() == "")
                {
                    usr.ImageUpload = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.ImageUpload = dr["Image"].ToString();
                }



                usr.Id = dr["Id"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/ReportingAPI/getUnpaidFeesStudentReports")]
        [System.Web.Http.HttpPost]
        public Student[] getUnpaidFeesStudentReports(Student std)
        {
            try
            {
                sqlHelper obj = new sqlHelper();
                List<Student> list = new List<Student>();
               // var cond="";
                var cond1= "";

                //if (std.Class != null && Convert.ToInt32(std.Class) > 0)
                //{
                //    cond = cond + "and s.ClassID ='" + std.Class + "' ";
                //}
                //if (std.Section != null && std.Section !="")
                //{
                //    cond = cond + "and s.SectionID='" + std.Section + "' ";
                //}
                if ((std.DF.ToString() != "dd-mm-yyyy" || std.DF.ToString() != "")&&(std.DT.ToString() != "dd-mm-yyyy" || std.DT.ToString() != ""))
                {
                    cond1 = cond1 + "and convert(datetime,s.Dated,103) >= convert(datetime,'" + std.DF + "',103) and convert(datetime,s.Dated,103) <= convert(datetime,'" + std.DT + "',103)";
                }



                    DataTable dt = obj.getDataTable(@" select scd.School,c.CourseName,ss.SectionName ,s.* from TBLStudent s
                                            left outer join tblCourses c on c.Id = s.ClassID
                                            left outer join tblSections ss on ss.Id = s.SectionID and ss.ClassId = s.ClassID
                                            left outer join tblSchooldetails scd on scd.ID=s.SchoolID 
                                            where s.Status =3 and s.IsDeleted is null and s.SchoolID=" + std.School + " ");

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows) /*(CHANGES IN fldID)*/
                    {
                            DataTable dtFeecheck = obj.getDataTable("select Amount,CONVERT(varchar, s.Dated, 107) as Date,c.CourseName,se.SectionName from stdfee1 s left join tblCourses c on c.id=s.CourseID left join tblSections se on se.id = s.sem where s.InstituteID='" + std.School + "' and s.StudentID='" + dr["ID"].ToString() + "'"+ cond1 +" ");

                         if (dtFeecheck.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dtFeecheck.Rows) /*(CHANGES IN fldID)*/
                            {
                                Student usr = new Student();

                                usr.Paidamount = dr1["Amount"].ToString();
                                 usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                                usr.School = dr["School"].ToString();
                                usr.Class = dr1["CourseName"].ToString();
                                usr.Section = dr1["SectionName"].ToString();
                                usr.DT = dr1["Date"].ToString();

                                usr.RegNo = dr["RegNo"].ToString();
                                usr.FQualification = std.FQualification;

                                list.Add(usr);
                            }
                           

                        }
                        else
                        {
                           
                        }
                    }
                }




                return list.ToArray();
            }
            catch (Exception ex)

            {
                throw ex;
            }
        }



        [System.Web.Http.Route("api/ReportingAPI/getUnpaidFeesStudentReports1")]
        [System.Web.Http.HttpPost]
        public Student[] getUnpaidFeesStudentReports1(Student std)
        {
            try
            {
                sqlHelper obj = new sqlHelper();
                List<Student> list = new List<Student>();
                var cond = "";
                var cond1 = "";

                if (std.Class != null && Convert.ToInt32(std.Class) > 0)
                {
                    cond = cond + "and s.ClassID ='" + std.Class + "' ";
                }
                if (std.Section != null && std.Section != "")
                {
                    cond = cond + "and s.SectionID='" + std.Section + "' ";
                }
                //if ((std.DF.ToString() != "dd-mm-yyyy" || std.DF.ToString() != "") && (std.DT.ToString() != "dd-mm-yyyy" || std.DT.ToString() != ""))
                //{
                //    cond1 = cond1 + "and s.Dated between convert(datetime,'" + std.DF + "',103) and convert(datetime,'" + std.DT + "',103)";
                //}



                DataTable dt = obj.getDataTable(@" select scd.School,c.CourseName,ss.SectionName ,s.*,st.* from TBLStudentFee1 s
                                            left outer join tblCourses c on c.Id = s.ClassID
                                            left outer join tblSections ss on ss.Id = s.SectionID and ss.ClassId = s.ClassID
                                            left outer join tblSchooldetails scd on scd.ID=s.SchoolID
                                            left join tblstudent st on st.id=s.studentid
                                            where  s.SchoolID=" + std.School + " and s.AcademicYear="+ std.AcademicYear +" " + cond + "");

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows) /*(CHANGES IN fldID)*/
                    {

                        DataTable fee = obj.getDataTable("select sum(Total) Total,sum(concession) concession,sum(nettotal) nettotal,sum(paid) paid,sum(Balance) Balance from [udf_getsfeePaid]('" + dr["StudentId"].ToString() + "','"+ std.AcademicYear +"')");
                       // DataTable dtFeecheck = obj.getDataTable("select Amount,CONVERT(varchar, s.Dated, 107) as Date from stdfee1 s where s.InstituteID='" + std.School + "' and s.StudentID='" + dr["ID"].ToString() + "'" + cond1 + " ");

                        if (fee.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in fee.Rows) /*(CHANGES IN fldID)*/
                            {
                                Student usr = new Student();

                                usr.Feeamount = dr1["Total"].ToString();
                                usr.Consession = dr1["concession"].ToString();
                               
                                usr.Paidamount = dr1["paid"].ToString();
                                usr.Dueamount= dr1["Balance"].ToString();
                                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                                usr.School = dr["School"].ToString();
                                usr.Class = dr["CourseName"].ToString();
                                usr.Section = dr["SectionName"].ToString();
                               // usr.DT = dr1["Date"].ToString();

                               usr.RegNo = dr["RegNo"].ToString();
                                usr.FQualification = std.FQualification;

                                list.Add(usr);
                            }


                        }
                        else
                        {

                        }
                    }
                }




                return list.ToArray();
            }
            catch (Exception ex)

            {
                throw ex;
            }
        }





    }
}
