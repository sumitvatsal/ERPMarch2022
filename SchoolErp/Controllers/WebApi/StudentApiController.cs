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
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web.Security;



namespace SchoolErp.Controllers.WebApi
{
    public class StudentApiController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();

        [System.Web.Http.Route("api/StudentApi/SchoolDashboardApi")]
        [System.Web.Http.HttpPost]
        public feestructureapp SchoolDashboardApi(feestructure id)
        {
            sqlHelper objsql = new sqlHelper();
            feestructureapp obj = new feestructureapp();
            //List<feestructure> list = new List<Models.feestructure>();

            feestructure ct = new Models.feestructure();
            // feestructure obj1 = new feestructure();

            try
            {
                if (id.AcademicYear.Equals(0) || id.AcademicYear.Equals(null) || "".Equals(id.AcademicYear))

                {
                    obj.status = false;
                    obj.message = "Please Enter AcademicYear";
                }
                else if (id.StudentID.Equals(0) || "".Equals(id.StudentID) || id.StudentID.Equals(null) || "".Equals(id.StudentID))
                {
                    obj.status = false;
                    obj.message = "Please Enter StudentID";
                }
                else if (id.SchoolID.Equals(0) || id.SchoolID.Equals(null) || "".Equals(id.SchoolID))
                {
                    obj.status = false;
                    obj.message = "Please Enter SchoolID";

                }
                else
                {
                    ct.SchoolID = id.SchoolID;
                    ct.AcademicYear = id.AcademicYear;
                    ct.StudentID = id.StudentID;
                    ct.CurrentMonth = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    DateTime.DaysInMonth(1980, 08);
                    int SchoolID = Convert.ToInt32(id.SchoolID);
                    int month = Convert.ToInt32(DateTime.Now.Month.ToString());
                    int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                    int monthdayss = DateTime.DaysInMonth(year, month);

                    int cmonth = Convert.ToInt32(DateTime.Now.Month.ToString("00"));
                    int cyear = Convert.ToInt32(DateTime.Now.Year.ToString("0000"));

                    var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);



                    ct.attendence = new attendencee();
                    ct.attendence.monthlyattendence = new List<StudentAttendenceDetailsAPP1>();





                    int p = 0;
                    int ab = 0;
                    int l = 0;
                    int hh = 0;

                    ct.attendence = new attendencee();
                    ct.attendence.monthlyattendence = new List<StudentAttendenceDetailsAPP1>();


                    //DataTable dt11 = objsql.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,AttendenceDate,* from tblStudentAttendence where SchoolID='" + id.SchoolID + "' and cast(Month(AttendenceDate) as varchar(50))=" + month + " and StudentId=" + id.StudentID + " and cast(year(AttendenceDate) as varchar(50))='" + year + "'");
                    DataTable dt11 = objsql.getDataTable(@"select CONVERT(int,Day(AttendenceDate)) as  CurrentDay   ,AttendenceDate,* from tblStudentAttendence where SchoolID='" + id.SchoolID + "' and cast(Month(AttendenceDate) as varchar(50))=" + month + " and StudentId = " + id.StudentID + " and cast(year(AttendenceDate) as varchar(50)) = '" + year + "' order by CurrentDay");

                    foreach (DataRow dr1 in dt11.Rows)
                    {

                        StudentAttendenceDetailsAPP1 usr1 = new StudentAttendenceDetailsAPP1();

                        usr1.AttendenceDay = Convert.ToInt32(dr1["CurrentDay"]);

                        usr1.AttendenceDate = Convert.ToDateTime(dr1["AttendenceDate"]).ToString("yyyy-MM-dd");
                        usr1.AttendenceRealDate = Convert.ToDateTime((dr1["AttendenceDate"]));
                        usr1.AttendenceType = dr1["AttendenceType"].ToString();

                        if (dr1["AttendenceType"].ToString() == "Present")
                        {
                            usr1.AttendenceType = "P";
                            p++;
                        }
                        else if (dr1["AttendenceType"].ToString() == "Absent")
                        {
                            //Leave 

                            var checkleave = db.tblStudentLeaveApplies.Where(lv => lv.StudentID == id.StudentID && lv.datefrom <= usr1.AttendenceRealDate && lv.dateto >= usr1.AttendenceRealDate && lv.IsDeleted == null && lv.Status == 5 && lv.IsDeleted == null).FirstOrDefault();
                            if (checkleave != null)
                            {
                                usr1.AttendenceType = "L";
                                l++;

                            }
                            //Holiday
                            else
                            {
                                var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == SchoolID && Hl.IsDeleted == null).FirstOrDefault();
                                if (checkHoliday != null)
                                {
                                    usr1.AttendenceType = "H";
                                    hh++;

                                }
                                else
                                {
                                    usr1.AttendenceType = "A";
                                    ab++;
                                }
                            }



                        }

                        ct.attendence.Present = p; //change for total leave 
                        ct.attendence.Absent = ab;
                        ct.attendence.leave = l;
                        ct.attendence.TotalDays = monthdayss;
                        ct.attendence.Holidays = hh;
                        ct.attendence.monthlyattendence.Add(usr1);
                    }



                    ct.FeeDetails = new List<feestructure1>();
                    feestructure1 fee = new feestructure1();

                    int currentmonth = Convert.ToInt32(DateTime.Now.Month.ToString("00")) - 1;
                    var feecalculation = db.tblFeeCalculates.Where(d => d.fldstudentID == id.StudentID && d.AcademicYear == id.AcademicYear && d.monthId == currentmonth && d.SchoolID == SchoolID).FirstOrDefault();
                    if (feecalculation != null)
                    {
                        ct.AcademicYear = Convert.ToInt32(feecalculation.AcademicYear);

                        ct.StudentID = Convert.ToInt32(feecalculation.fldstudentID);
                        fee.TotalFeeAmount = Convert.ToInt32(feecalculation.monthlyAmount);
                        fee.PaidFeeAmount = Convert.ToInt32(feecalculation.PaidAmount);
                        fee.DueAmount = Convert.ToInt32(feecalculation.duesAmount);
                        ct.FeeDetails.Add(fee);
                        obj.data = ct;
                        obj.message = "Sucess";
                        obj.status = true;
                    }
                    else
                    {
                        var feeassign = db.tblFeeStructureAssigns.Where(x => x.StudentID == id.StudentID && x.SchoolID == SchoolID).FirstOrDefault();
                        if (feeassign != null)
                        {
                            //feestructure obj = new feestructure();
                            int feestruid = Convert.ToInt32(feeassign.FeeStructureID);
                            var feestructure = db.tblFeeStructures.Where(g => g.AcademicYear == id.AcademicYear && g.ID == feestruid).FirstOrDefault();
                            if (feestructure != null)
                            {
                                var feestru = db.tblFeeStructureConfigs.Where(c => c.FeeStructureID == feestruid).ToList();
                                if (feestru != null)
                                {
                                    int TotalAmount = 0;
                                    foreach (var aa in feestru)
                                    {
                                        TotalAmount = TotalAmount + Convert.ToInt32(aa.Amount);
                                        fee.TotalFeeAmount = TotalAmount;
                                        fee.PaidFeeAmount = Convert.ToInt32(0);
                                        fee.DueAmount = Convert.ToInt32(TotalAmount);

                                        ct.StudentID = Convert.ToInt32(feeassign.StudentID);
                                        ct.AcademicYear = Convert.ToInt32(feestructure.AcademicYear);
                                    }
                                    ct.FeeDetails.Add(fee);
                                    obj.message = "Sucess";
                                    obj.data = ct;

                                    obj.status = true;

                                }
                            }
                            else
                            {
                                obj.message = "Sucess";
                                obj.data = ct;
                                obj.status = true;
                            }


                        }
                        else
                        {
                            obj.message = "Sucess";
                            obj.data = ct;
                            obj.status = true;
                        }
                    }
                }



            }
            catch
            {
                obj.message = "Something Went Wrong";
                obj.data = ct;
                obj.status = false;
            }


            return obj;
        }

        //[System.Web.Http.Route("api/StudentApi/getSubjectTeacher")]
        //[System.Web.Http.HttpPost]
        //public SubjectTeacherAllocatesAppp getclassteacherallocated (SubjectTeacherAllocates1 VAL)
        //{
        //    SubjectTeacherAllocatesAppp obj = new SubjectTeacherAllocatesAppp();
        //    SubjectTeacherAllocates1 ct = new Models.SubjectTeacherAllocates1();
        //    ct.SubTeacherDetails = new List<SubjectTeacherDetails>();
        //    try
        //    {
        //        if (VAL.SchoolID.Equals(0) || VAL.SchoolID.Equals(null) || "".Equals(VAL.SchoolID))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else if (VAL.ClassID.Equals(0) || VAL.ClassID.Equals(null) || "".Equals(VAL.ClassID))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else
        //        {

        //            var result = (from s in db.tblSubjectTeacherAllocates
        //                          join sch in db.tblSchoolDetails
        //                          on s.SchoolID equals sch.ID
        //                          where s.IsDeleted == null && s.classid == VAL.ClassID
        //                          select new
        //                          {
        //                              model = s

        //                          }
        //                        ).ToList();

        //            if (VAL.SchoolID != "" && VAL.SchoolID != "0" && VAL.SchoolID != "-1")
        //            {
        //                int SchoolID = Convert.ToInt32(VAL.SchoolID);
        //                result = result.Where(x => x.model.SchoolID == SchoolID).ToList();

        //            }
        //            result.Select(x => x.model.TeacherID).Distinct();
        //          //result =  result.GroupBy(x => x.model.TeacherID).Select(y => y.First());
        //            ct.SchoolID = VAL.SchoolID;
        //            ct.SubTeacherDetails = new List<SubjectTeacherDetails>();
        //            int avi = 0;
        //            foreach (var a in result)
        //            {

        //                SubjectTeacherDetails AA = new SubjectTeacherDetails();


        //                AA.TeacherID = a.model.TeacherID;
        //                var CTname = db.tblEmployees.Where(y => y.Id == a.model.TeacherID && y.IsDeleted == null).FirstOrDefault();
        //                if (CTname != null)
        //                {
        //                    AA.TeacherName = CTname.FirstName +" "+ CTname.MiddleName +" "+ CTname.LastName;
        //                    AA.PhoneNo = CTname.Mobile;
        //                }
        //                else
        //                {
        //                    AA.TeacherName = "";
        //                }
        //                //AA.SubjectID = Convert.ToInt32(a.model.SubjectID);

        //                var Subjectname = db.tblSubjects.Where(z => z.ID == a.model.SubjectID && z.IsDeleted == null).FirstOrDefault();
        //                if (Subjectname != null)
        //                {
        //                    AA.SubjectName = Subjectname.Subject;
        //                }
        //                else
        //                {
        //                    AA.SubjectName = "";
        //                }

        //                AA.ClassID = Convert.ToInt32(a.model.SubjectID);
        //                var Classname = db.tblCourses.Where(c => c.Id == a.model.classid && c.IsDeleted == null).FirstOrDefault();
        //                if (Classname != null)
        //                {
        //                    AA.ClassName = Classname.CourseName;
        //                }
        //                else
        //                {
        //                    AA.ClassName = "";

        //                }

        //                if (AA.TeacherName != "" && AA.SubjectName != "" && AA.ClassName != "")
        //                {
        //                    avi++;
        //                    ct.SubTeacherDetails.Add(AA);

        //                }



        //            }
        //            if (avi != 0)
        //            {
        //                obj.message = "Sucess";
        //                obj.data = ct;
        //                obj.status = true;
        //            }
        //            else
        //            {
        //                obj.message = "No Data Found";
        //                obj.data = ct;
        //                obj.status = false;

        //            }
        //        }
        //    }
        //    catch
        //    {
        //        obj.message = "Something Went Wrong";
        //        obj.data = ct;
        //        obj.status = false;

        //    }






        //    return (obj);

        //}



        [System.Web.Http.Route("api/StudentApi/getSubjectTeacher")]
        [System.Web.Http.HttpGet]
        public SubjectTeacherAllocatesAppp getclassteacherallocated(int SchoolID, int ClassID, int SectionID)
        {
            SubjectTeacherAllocatesAppp obj = new SubjectTeacherAllocatesAppp();
            SubjectTeacherAllocates1 ct = new Models.SubjectTeacherAllocates1();
            ct.SubTeacherDetails = new List<SubjectTeacherDetails>();
            ct.ClassTeacherDetail = new List<ClassTeacherDetails>();
            ct.PricipleDetail = new List<PricipleDetails>();
            ct.AccountsDepartment = new List<AccountsDepartment>();
            try
            {
                if (SchoolID.Equals(0) || SchoolID.Equals(null) || "".Equals(SchoolID))
                {
                    obj.status = false;
                    obj.message = "Please Enter SchoolID";

                }
                else if (ClassID.Equals(0) || ClassID.Equals(null) || "".Equals(ClassID))
                {
                    obj.status = false;
                    obj.message = "Please Enter ClassID";
                }
                else if (SectionID.Equals(0) || SectionID.Equals(null) || "".Equals(SectionID))
                {
                    obj.status = false;
                    obj.message = "Please Enter SectionID";
                }
                else
                {
                    int avi = 0;
                    int SchoolIDD = Convert.ToInt32(SchoolID);
                    //Accounts Department
                    string accname = "account";
                    var AccDepartment = db.tblDepartmnets.Where(d => d.SchoolID == SchoolIDD && d.IsDeleted == null && d.DepartmentName.Contains(accname)).ToList();
                    foreach (var acc in AccDepartment)
                    {
                        var Accountemp = db.tblEmployees.Where(e => e.SchoolID == acc.SchoolID && e.IsDeleted == null && e.DeptID == acc.DepartmentId).ToList();
                        foreach (var acc1 in Accountemp)
                        {
                            AccountsDepartment Ac = new AccountsDepartment();
                            Ac.EmployeeName = acc1.FirstName + " " + acc1.MiddleName + "" + acc1.LastName;
                            Ac.PhoneNo = acc1.Phone;
                            Ac.DepartmentName = acc.DepartmentName;
                            ct.AccountsDepartment.Add(Ac);
                            avi++;
                        }

                    }
                    //DESIGNATION PRINCIPLE
                    string PrinNAME = "Princi";
                    var designationid = db.tblDesignations.Where(z => z.SchoolID == SchoolIDD && z.IsDeleted == null && z.Designation.Contains(PrinNAME)).ToList();
                    foreach (var AB in designationid)
                    {
                        var Pname = db.tblEmployees.Where(em => em.DesigID == AB.DesigID && em.SchoolID == AB.SchoolID && em.IsDeleted == null).ToList();
                        foreach (var ABC in Pname)
                        {
                            PricipleDetails Pr = new PricipleDetails();
                            Pr.PrincipleID = ABC.Id;
                            Pr.PrincipleName = ABC.FirstName + " " + ABC.MiddleName + " " + ABC.LastName;
                            Pr.PhoneNo = ABC.Mobile;
                            Pr.DesignationName = AB.Designation;
                            ct.PricipleDetail.Add(Pr);
                            avi++;

                        }





                    }

                    //ClassTeacher
                    ClassTeacherDetails CT1 = new ClassTeacherDetails();

                    var CLASSTEACHER = db.tblClassTeacherAllocations.Where(t => t.ClassID == ClassID && t.SectionID == SectionID && t.SchoolID == SchoolIDD).FirstOrDefault();

                    if (CLASSTEACHER != null)
                    {
                        var Tname = db.tblEmployees.Where(em => em.Id == CLASSTEACHER.intEmpID && em.SchoolID == CLASSTEACHER.SchoolID && em.IsDeleted == null).FirstOrDefault();
                        if (Tname != null)
                        {
                            CT1.TeacherName = Tname.FirstName + " " + Tname.MiddleName + " " + Tname.LastName;
                            CT1.PhoneNo = Tname.Mobile;
                            CT1.TeacherID = Tname.Id;
                            ct.ClassTeacherDetail.Add(CT1);
                            avi++;
                        }


                    }




                    //Sub_teacher
                    var result = (from s in db.tblSubjectTeacherAllocates
                                  join sch in db.tblSchoolDetails
                                  on s.SchoolID equals sch.ID
                                  where s.IsDeleted == null && s.classid == ClassID
                                  select new
                                  {
                                      model = s

                                  }
                                ).ToList();

                    if (SchoolID != null)
                    {
                        result = result.Where(x => x.model.SchoolID == SchoolID).ToList();

                    }
                    //result.Select(x => x.model.TeacherID).Distinct();
                    var result1 = result.GroupBy(x => x.model.TeacherID).Select(y => y.First());
                    ct.SchoolID =Convert.ToString(SchoolID);
                    ct.ClassID = ClassID;
                    ct.SectionID = SectionID;
                    ct.SubTeacherDetails = new List<SubjectTeacherDetails>();

                    foreach (var a in result1)
                    {

                        SubjectTeacherDetails AA = new SubjectTeacherDetails();


                        AA.TeacherID = a.model.TeacherID;
                        var CTname = db.tblEmployees.Where(y => y.Id == a.model.TeacherID && y.IsDeleted == null).FirstOrDefault();
                        if (CTname != null)
                        {
                            AA.TeacherName = CTname.FirstName + " " + CTname.MiddleName + " " + CTname.LastName;
                            AA.PhoneNo = CTname.Mobile;
                        }
                        else
                        {
                            AA.TeacherName = "";
                        }
                        string[] arr = new string[] { };



                        var sub = db.tblSubjectTeacherAllocates.Where(x => x.TeacherID == a.model.TeacherID && x.classid == a.model.classid && x.IsDeleted == null).ToList();
                        foreach (var b in sub)
                        {



                            var Subjectname1 = db.tblSubjects.Where(z => z.ID == b.SubjectID && z.IsDeleted == null).FirstOrDefault();
                            if (Subjectname1 != null)
                            {
                                arr = arr.Concat(new string[] { Subjectname1.Subject }).ToArray();


                            }
                            AA.SubjectName = arr;








                        }


                        //AA.ClassID = Convert.ToInt32(a.model.SubjectID);
                        //var Classname = db.tblCourses.Where(c => c.Id == a.model.classid && c.IsDeleted == null).FirstOrDefault();
                        //if (Classname != null)
                        //{
                        //    AA.ClassName = Classname.CourseName;
                        //}
                        //else
                        //{
                        //    AA.ClassName = "";

                        //}

                        if (AA.TeacherName != ""/* && AA.ClassName != ""*/)
                        {
                            avi++;
                            ct.SubTeacherDetails.Add(AA);

                        }



                    }
                    if (avi != 0)
                    {
                        obj.message = "Sucess";
                        obj.data = ct;
                        obj.status = true;
                    }
                    else
                    {
                        obj.message = "No Data Found";
                        obj.data = ct;
                        obj.status = false;

                    }
                }
            }

            catch
            {
                obj.message = "Something Went Wrong";
                obj.data = ct;
                obj.status = false;

            }






            return (obj);

        }















        //[System.Web.Http.Route("api/TimeTableApi/getTimeTableConfigDetapp")]
        //[System.Web.Http.HttpPost]
        //public TimeTableapp getTimeTableConfigDetapp(TimeTable val)
        //{



        //    //List<TimeTable> list = new List<Models.TimeTable>();
        //    TimeTableapp obj = new TimeTableapp();
        //    TimeTable ct = new Models.TimeTable();
        //    try
        //    {
        //        if (val.Classid.Equals(0) || val.Classid.Equals(null) || "".Equals(val.Classid))

        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter ClassID";
        //        }
        //        else if ("".Equals(val.Section) || val.Section.Equals(null) || "".Equals(val.Section))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter Section";
        //        }
        //        else if (val.SchoolID.Equals(0) || val.SchoolID.Equals(null) || "".Equals(val.SchoolID))
        //        {
        //            obj.status = false;
        //            obj.message = "Please Enter SchoolID";

        //        }
        //        else
        //        {
        //            int classID = Convert.ToInt32(val.Classid);
        //            int SectionID = Convert.ToInt32(val.Section);
        //            int SchoolID = Convert.ToInt32(val.SchoolID);
        //            int timetableid = 0;

        //            var timetablecon = db.tblTimeTableConfigs.Where(x => x.ClassID == classID && x.SectionID == SectionID && x.SchoolID == SchoolID && x.IsDeleted == null).FirstOrDefault();
        //            if (timetablecon != null)
        //            {

        //                timetableid = Convert.ToInt32(timetablecon.ID);


        //                int count = 0;
        //                int id = Convert.ToInt32(timetableid);
        //                var m = (from c in db.tblTimeTableConfigs
        //                         join ac in db.tblAcademicYears on c.AcademicYear equals ac.ID
        //                         join cl in db.tblCourses on c.ClassID equals cl.Id
        //                         join s in db.tblSections on c.SectionID equals s.Id
        //                         where c.ID == id && c.IsDeleted == null
        //                         select new
        //                         {
        //                             model = c,
        //                             DtFrom = ac.DateFrom,
        //                             DtTo = ac.DateTo,
        //                             courseNm = cl.CourseName,
        //                             sectionNm = s.SectionName
        //                         }).SingleOrDefault();

        //                count++;
        //                tblTimeTableConfig cls = new tblTimeTableConfig();
        //                ct.CountID = count;
        //                ct.ID = m.model.ID;
        //                cls = m.model;
        //                ct.timingNm = m.model.Name;//for timing dropdown in TimeTableConfigCreate
        //                ct.AcYear = m.DtFrom.Year + "-" + m.DtTo.ToString("yy");
        //                ct.Course = m.courseNm + "-" + m.sectionNm;
        //                ct.fromDT = ((DateTime)m.model.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                ct.ToDt = ((DateTime)m.model.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                ct.con = cls;
        //                if (ct.con.Status == 0)
        //                {
        //                    ct.StatusNm = "Inactive";
        //                    ct.style = "btn btn-block btn-danger btn-sm";
        //                    ct.action = "Activate";
        //                }
        //                else
        //                {
        //                    ct.StatusNm = "Active";
        //                    ct.style = "btn btn-block btn-success btn-sm";
        //                    ct.action = "Inactivate";
        //                }
        //                if (ct.con.WeekDays != "")
        //                {
        //                    string days = "";
        //                    var w = ct.con.WeekDays.Split(',');
        //                    int i = 0;
        //                    foreach (string s in w)
        //                    {
        //                        i++;
        //                        int dayID = Convert.ToInt32(s);
        //                        var week = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();
        //                        if (i == 1)
        //                        {
        //                            days = week.WeekDay;
        //                        }
        //                        else
        //                        {
        //                            days = days + "," + week.WeekDay;
        //                        }
        //                    }
        //                    ct.WeekDaysNm = days;

        //                    var classTimingdet = db.tblClassTimingDets.Where(x => x.CT_ID == m.model.CT_ID && x.IsDeleted == null).ToList();


        //                    ct.TimeTableList = new List<TimeTable>();
        //                    foreach (var ctd in classTimingdet)
        //                    {
        //                        TimeTable a = new TimeTable();
        //                        a.ttList = new List<tblTimeTable>();
        //                        a.period = new tblClassTimingDet();
        //                        a.TimeTableCLS_list = new List<TimeTableCLS>();

        //                        var timetable = db.tblTimeTables.Where(x => x.CTDet_ID == ctd.ID && x.TimeTableID == m.model.ID && x.IsDeleted == null).ToList();
        //                        DateTime SsdtTime = (DateTime.Now.Date + ctd.STime).AddMinutes(-1);
        //                        DateTime EddtTime = (DateTime.Now.Date + ctd.ETime).AddMinutes(1);
        //                        a.timingNm = SsdtTime.ToString("hh:mm:ss tt ") + "-" + EddtTime.ToString("hh:mm:ss tt ");
        //                        a.period.IsBreak = ctd.IsBreak;
        //                        foreach (var t in timetable)
        //                        {
        //                            TimeTableCLS ttCLS = new TimeTableCLS();
        //                            ttCLS.ID = t.ID;
        //                            ttCLS.CTDet_ID = t.CTDet_ID;
        //                            ttCLS.CT_ID = t.CT_ID;
        //                            ttCLS.Status = t.Status;
        //                            ttCLS.SubjectID = t.SubjectID;
        //                            ttCLS.TeacherID = t.TeacherID;
        //                            ttCLS.TimeTableID = t.TimeTableID;
        //                            ttCLS.WeekDay = t.WeekDay;
        //                            if (t.SubjectID != null && t.TeacherID != -1)
        //                            {
        //                                var sub = db.tblSubjects.Where(s => s.ID == t.SubjectID).SingleOrDefault();
        //                                ttCLS.subjectNm = sub.Subject;
        //                            }
        //                            else { ttCLS.subjectNm = ""; }
        //                            if (t.TeacherID != null && t.TeacherID != -1)
        //                            {
        //                                var teacher = db.tblEmployees.Where(s => s.Id == t.TeacherID).SingleOrDefault();
        //                                ttCLS.teacherNm = teacher.FirstName + " " + teacher.LastName;
        //                            }
        //                            else { ttCLS.teacherNm = ""; }
        //                            a.TimeTableCLS_list.Add(ttCLS);
        //                            //a.ttList.Add(t);
        //                        }
        //                        ct.TimeTableList.Add(a);
        //                        obj.status = true;
        //                        obj.message = "Sucess";
        //                        obj.data = ct;
        //                    }

        //                }

        //            }
        //            else
        //            {
        //                obj.status = false;
        //                obj.message = "No Timetable Assigend";
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        obj.status = false;
        //        obj.message = "Something Went Wrong";
        //    }

        //    return obj;
        //}
        [System.Web.Http.Route("api/StudentApi/deleteStudentById")]
        [System.Web.Http.HttpGet]
        public string deleteStudentById(Int32 id)
        {
            int idd = Convert.ToInt32(id);
            var aa = db.TBLStudents.SingleOrDefault(s => s.ID == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            //var result = new tblExpenseCategory { Id = id };
            //db.Entry(result).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return "Student Deleted Successfully";

        }



        [System.Web.Http.Route("api/StudentApi/GetEnclosures")]
        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetEnclosures(string name, string utype)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            try
            {

                string userlogin = name;
                int usertype = Convert.ToInt32(utype);
                if (usertype == 1 || usertype == 3)
                {
                    var result = db.tblDocuments.SqlQuery("Select * from tblDocument doc left join tblEmployee em on doc.SchoolID = em.SchoolID  where  em.UserID = '" + userlogin + "' and em.IsDeleted is null and doc.IsDeleted is null and doc.Status = " + 1 + " and doc.userId = " + 4).ToList();
                    foreach (var a in result)
                    {
                        SelectListItem items = new SelectListItem();
                        items.Text = a.DocumentName;
                        items.Value = a.Id.ToString();
                        list.Add(items);
                    }
                }
                else if (usertype == 6)
                {
                    var result = db.tblDocuments.SqlQuery("Select * from tblDocument where doc left join tblSchoolDetails sc on sc.ID = doc.SchoolID where  sc.SchoolCode = '" + userlogin + "' and doc.IsDeleted is null and doc.Status = " + 1 + " and doc.userId = " + 4).ToList();
                    foreach (var a in result)
                    {
                        SelectListItem items = new SelectListItem();
                        items.Text = a.DocumentName;
                        items.Value = a.Id.ToString();
                        list.Add(items);
                    }
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        [System.Web.Http.Route("api/StudentApi/GetClassesNew")]
        [System.Web.Http.HttpPost]
        public tblCours[] GetClassesNew(List<string> val)
        {
            List<tblCours> list = new List<tblCours>();

            try
            {
                string i = val[0].ToString();
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblCourses.SqlQuery("Select * from tblCourses where Status= 1  and  SchoolID=" + Convert.ToInt32(i) + "").ToList();

                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblCours items = new tblCours();
                    items.CourseName = a.CourseName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/StudentApi/getAllStudentsDetailsNew")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsDetailsNew(Student std)
        {
            try
            {
                sqlHelper obj = new sqlHelper();
                List<Student> list = new List<Student>();
                string[] cols = { "@School", "@class", "@section" };
                object[] vals = { std.School, std.Class, std.Section };


                DataTable dt = obj.sp_GetDataTable("sp_getStudentDetailsNewData", cols, vals);
                foreach (DataRow dr in dt.Rows)
                {
                    Student usr = new Student();
                    usr.ID = int.Parse(dr["ID"].ToString());
                    usr.RegNo = dr["RegNo"].ToString();
                    usr.School = dr["School"].ToString();
                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    usr.Section = dr["SectionName"].ToString();
                    usr.Class = dr["Class"].ToString();
                    usr.RollNo = dr["RollNo"].ToString();
                    //  usr.PicPath = dr["PicPath"].ToString();
                    usr.SStatus = dr["StatusName"].ToString();
                    usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                    usr.Fmail = dr["Fmail"].ToString();
                    usr.Mmail = dr["Mmail"].ToString();
                    if (dr["PicPath"].ToString() == "")
                    {
                        usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.PicPath = dr["PicPath"].ToString();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    //if (dr["Status"].ToString() == "True")
                    //{

                    //    usr.SStatus = "Activate";
                    //    usr.Extra10 = "btn btn-block btn-success btn-sm";

                    //}
                    //else
                    //{
                    //    usr.SStatus = "DeActivate";
                    //    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                    //}


                    list.Add(usr);
                }
                return list.ToArray();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.Route("api/StudentApi/GetReligions")]
        [System.Web.Http.HttpPost]
        public tblReligion[] GetReligions()
        {
            List<tblReligion> list = new List<tblReligion>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblReligions.SqlQuery("Select * from tblReligion where Status=1").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblReligion items = new tblReligion();
                    items.ReligionName = a.ReligionName;
                    items.ReligionId = a.ReligionId;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/GetReligionsbyID")]
        [System.Web.Http.HttpPost]
        public tblReligion[] GetReligionsbyID(List<string> id)
        {
            List<tblReligion> list = new List<tblReligion>();

            try
            {

                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblReligions.SqlQuery("Select * from tblReligion where Status=1 and ReligionId='" + id[0] + "' ").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblReligion items = new tblReligion();
                    items.ReligionName = a.ReligionName;
                    items.ReligionId = a.ReligionId;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/GetCastes")]
        [System.Web.Http.HttpGet]
        public List<SelectListItem> GetCastes()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblCastes.SqlQuery("Select * from tblCaste where Status=" + 1 + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    SelectListItem items = new SelectListItem();
                    items.Text = a.CasteName;
                    items.Value = a.CasteId.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        //public JsonResult GetEnclosures()
        //{
        //    var result = db.TBLEnclosureMasters.SqlQuery("Select * from TBLEnclosureMaster where Status=" + 1 + "").ToList();

        //    //db.PageStats.Where(x => x.Country.Equals(country)).OrderBy(x => x.City).Select(x => x.City).Distinct().ToList();

        //    List<SelectListItem> cities = new List<SelectListItem>();

        //    foreach (var a in result)
        //    {
        //        SelectListItem city = new SelectListItem
        //        {
        //            Text = a.Enclosure,
        //        Value = a.ID.ToString()
        //        };
        //        cities.Add(city);
        //    }


        //    return Json(cities, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult CityDropDownList(string country)
        //{
        //    var results = (from c in db.PageStats
        //                   where c.Country.Equals(country)
        //                   orderby c.City
        //                   select c.City).ToList().Distinct();

        //    //db.PageStats.Where(x => x.Country.Equals(country)).OrderBy(x => x.City).Select(x => x.City).Distinct().ToList();

        //    List<SelectListItem> cities = new List<SelectListItem>();

        //    foreach (var item in results)
        //    {
        //        SelectListItem city = new SelectListItem
        //        {
        //            Text = item,
        //            Value = item
        //        };
        //        cities.Add(city);
        //    }
        //    return Json(cityList, JsonRequestBehavior.AllowGet);
        //}


        [System.Web.Http.Route("api/StudentApi/GetAcademicYear")]
        [System.Web.Http.HttpPost]
        public AcademicYrModel[] GetAcademicYear(List<string> val)
        {

            List<AcademicYrModel> list = new List<AcademicYrModel>();

            try
            {
                int i = Convert.ToInt32(val[0]);
                var result = db.tblAcademicYears.Where(a => a.SchoolID == i && a.IsDeleted == null && a.Status == true).OrderByDescending(a => a.DateFrom).ToList();
                foreach (var a in result)
                {
                    AcademicYrModel items = new AcademicYrModel();
                    items.StartYear = a.DateFrom.Year + "-" + a.DateTo.ToString("yy");
                    items.ID = a.ID;
                    list.Add(items);


                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();

        }


        [System.Web.Http.Route("api/StudentApi/GetStructure")]
        [System.Web.Http.HttpPost]
        public tblFeeCategory[] tblFeeCategory(List<string> val)
        {
            List<tblFeeCategory> list = new List<tblFeeCategory>();
            try
            {
                int i = int.Parse(val[0]);
                var result = db.tblFeeCategories.Where(a => a.SchoolID == i && a.IsDeleted == null).ToList();

                foreach (var a in result)
                {
                    tblFeeCategory items = new tblFeeCategory();
                    items.FeeCategory = a.FeeCategory;
                    items.ID = a.ID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }

            return list.ToArray();
        }
        [System.Web.Http.Route("api/StudentApi/GetCategories")]
        [System.Web.Http.HttpPost]
        public CategoryModel[] GetCategories(List<string> val)
        {
            List<CategoryModel> list = new List<CategoryModel>();
            // var SchoolId = Convert.ToInt32(val[0]);
            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblCastCategories.SqlQuery("Select * from tblCastCategory where isdeleted is null and Status=" + 1 + "  ").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    CategoryModel items = new CategoryModel();
                    items.Category = a.CategoryName;
                    items.CatID = a.CatId;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/GetClassesbySchoolID")]
        [System.Web.Http.HttpPost]
        public tblCours[] GetClassesbySchoolID(List<string> val)
        {
            List<tblCours> list = new List<tblCours>();

            try
            {
                string i = val[0].ToString();
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblCourses.SqlQuery("Select * from tblCourses where Status=1 and SchoolID=" + i + " and IsDeleted is null ").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblCours items = new tblCours();
                    items.CourseName = a.CourseName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/GetClassesbySchoolIDselect")]
        [System.Web.Http.HttpPost]
        public tblCours[] GetClassesbySchoolIDselect(List<string> val)
        {
            List<tblCours> list = new List<tblCours>();

            try
            {
                tblCours items1 = new tblCours();
                items1.CourseName = "----select----";
                items1.Id = -1;
                list.Add(items1);
                string i = val[0].ToString();
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblCourses.SqlQuery("Select * from tblCourses where Status=1 and SchoolID=" + i + " and IsDeleted is null ").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblCours items = new tblCours();
                    items.CourseName = a.CourseName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/GetClasses")]
        [System.Web.Http.HttpPost]
        public tblCours[] GetClasses()
        {
            List<tblCours> list = new List<tblCours>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblCourses.SqlQuery("Select * from tblCourses where Status=" + 1 + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblCours items = new tblCours();
                    items.CourseName = a.CourseName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/GetStreamsbySchool")]
        [System.Web.Http.HttpPost]
        public tblStream[] GetStreamsbySchool(List<string> val)
        {
            int SchoolID = Convert.ToInt32(val[1]);
            string i = val[0].ToString();
            List<tblStream> list = new List<tblStream>();

            try
            {

                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblStreams.SqlQuery("Select * from tblStream where Status=1 and Class=" + Convert.ToInt32(i) + "  and IsDeleted is null and SchoolID='" + SchoolID + "'").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblStream items = new tblStream();
                    items.StreamName = a.StreamName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/GetStreams")]
        [System.Web.Http.HttpPost]
        public tblStream[] GetStreams(List<string> val)
        {
            List<tblStream> list = new List<tblStream>();

            try
            {
                string i = val[0].ToString();
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblStreams.SqlQuery("Select * from tblStream where Status=" + 1 + " and Class=" + Convert.ToInt32(i) + " and IsDeleted is null").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblStream items = new tblStream();
                    items.StreamName = a.StreamName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/StudentApi/GetSectionsbyschool")]
        [System.Web.Http.HttpPost]
        public tblSection[] GetSectionsbyschool(List<string> val)
        {
            List<tblSection> list = new List<tblSection>();

            try
            {
                int SchoolID = Convert.ToInt32(val[1]);
                string i = val[0].ToString();
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblSections.SqlQuery("Select * from tblSections where Status= 1  and ClassId=" + Convert.ToInt32(i) + " and SchoolID='" + SchoolID + "' and IsDeleted is null order by LOWER(SectionName)").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSection items = new tblSection();
                    items.SectionName = a.SectionName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/StudentApi/GetSectionsbyschoolSelect")]
        [System.Web.Http.HttpPost]
        public tblSection[] GetSectionsbyschoolSelect(List<string> val)
        {
            List<tblSection> list = new List<tblSection>();

            try
            {
                int SchoolID = Convert.ToInt32(val[1]);
                string i = val[0].ToString();
                tblSection item1 = new tblSection();
                item1.SectionName = "----Select----";
                item1.Id = -1;
                list.Add(item1);
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblSections.SqlQuery("Select * from tblSections where Status= 1  and ClassId=" + Convert.ToInt32(i) + " and SchoolID='" + SchoolID + "' and IsDeleted is null order by LOWER(SectionName)").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSection items = new tblSection();
                    items.SectionName = a.SectionName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/StudentApi/GetSections")]
        [System.Web.Http.HttpPost]
        public tblSection[] GetSections(List<string> val)
        {
            List<tblSection> list = new List<tblSection>();

            try
            {
                string i = val[0].ToString();
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblSections.SqlQuery("Select * from tblSections where Status= 1  and ClassId=" + Convert.ToInt32(i) + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblSection items = new tblSection();
                    items.SectionName = a.SectionName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/GetBatches")]
        [System.Web.Http.HttpPost]
        public tblBatch[] GetBatches(List<string> val)
        {
            List<tblBatch> list = new List<tblBatch>();

            try
            {
                string i = val[0].ToString();
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblBatches.SqlQuery("Select * from tblBatch where status=" + 1 + " and ClassId=" + Convert.ToInt32(i) + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblBatch items = new tblBatch();
                    items.BatchName = a.BatchName;
                    items.Id = a.Id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/GetStates")]
        [System.Web.Http.HttpPost]
        public tblState[] GetStates()
        {
            List<tblState> list = new List<tblState>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblStates.SqlQuery("Select * from tblState where isdeleted is null and status=" + 1 + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblState items = new tblState();
                    items.StateName = a.StateName;
                    items.StateId = a.StateId;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/GetQualificationBySchool")]
        [System.Web.Http.HttpPost]
        public tblQualification[] GetQualificationBySchool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<tblQualification> list = new List<tblQualification>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblQualifications.SqlQuery("Select * from tblQualifications where Status=1 and IsDeleted is null and SchoolID='" + SchoolID + "'").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblQualification items = new tblQualification();
                    items.QualificationName = a.QualificationName;
                    items.QualificationId = a.QualificationId;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/StudentApi/GetQualification")]
        [System.Web.Http.HttpPost]
        public tblQualification[] GetQualification()
        {
            List<tblQualification> list = new List<tblQualification>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblQualifications.SqlQuery("Select * from tblQualifications where Status=" + 1 + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblQualification items = new tblQualification();
                    items.QualificationName = a.QualificationName;
                    items.QualificationId = a.QualificationId;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/GetYears")]
        [System.Web.Http.HttpPost]
        public years[] GetYears()
        {
            List<years> list = new List<years>();

            try
            {
                for (int i = 2000; i <= 2050; i++)
                {
                    years yr = new years();
                    yr.Idd = i;
                    yr.year = i.ToString();
                    list.Add(yr);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }
        ////Foredit
        [System.Web.Http.Route("api/StudentApi/GetYearsforedit")]
        [System.Web.Http.HttpPost]
        public years[] GetYearsforedit(List<string> yearreceive)
        {
            List<years> list = new List<years>();

            try
            {
                for (int i = 2000; i <= 2050; i++)
                {

                    years yr = new years();
                    yr.year = i.ToString();
                    if (i == Convert.ToInt32(yearreceive[0]))
                    {
                        list.Add(yr);
                    }

                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        //SaveOfficialDet

        //SaveOfficialDet

        // StudentloginDetailsEmail


        [System.Web.Http.Route("api/StudentApi/SaveOfficialDet")]
        [System.Web.Http.HttpPost]
        public string SaveOfficialDet(TBLStudent val)
        {
            try
            {
                sqlHelper obj = new sqlHelper();
                // int chk = For Roll no only  Convert.ToInt32(obj.ExecuteScaler("select count(*) from TBLStudent where IsDeleted is null and SchoolID='" + val.SchoolID + "' and ClassID='" + val.ClassID + "' and SectionID='" + val.SectionID + "' and AcademicYear='" + val.AcademicYear + "'  and RollNo='" + val.RollNo + "'  and RollNo!='' "));

                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from TBLStudent where IsDeleted is null and SchoolID='" + val.SchoolID + "' and BiometricID='" + val.BiometricID + "' and BiometricID!='' "));
                if (chk == 0)
                {
                    val.Status = 9;
                    db.TBLStudents.Add(val);
                    db.SaveChanges();

                    string lastval = obj.ExecuteScaler("select LastSeries from tblDocumentNo where UserType='Student' and Status=1 and SchoolID='" + val.SchoolID + "' and IsDeleted is null");
                    string incemtno = obj.ExecuteScaler("select DocumentNo from tblDocumentNo where UserType='Student' and Status=1 and SchoolID='" + val.SchoolID + "' and IsDeleted is null");
                    if (lastval == "")
                    {
                        lastval = "0";
                    }
                    var lastupdate = Int64.Parse(lastval) + Int64.Parse(incemtno);
                    string constr1 = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                    SqlConnection con1 = new SqlConnection(constr1);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("update tblDocumentNo set LastSeries='" + lastupdate + "' where UserType='Student' and SchoolID='" + val.SchoolID + "' and IsDeleted is null", con1);
                    cmd1.ExecuteNonQuery();
                    con1.Close();

                    var i = val.ID.ToString();
                    tblStudentFee1 lv = new tblStudentFee1();
                    lv.StudentId = Convert.ToInt32(val.ID.ToString());
                    lv.AcademicYear = val.AcademicYear;
                    lv.ClassId = val.ClassID;
                    lv.SectionId = val.SectionID;
                    lv.SchoolId = val.SchoolID;

                    db.tblStudentFee1.Add(lv);
                    db.SaveChanges();





                    return i;
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception ex) { return ex.Message; }
        }

        [System.Web.Http.Route("api/StudentApi/EditStudentDet")]
        [System.Web.Http.HttpPost]
        public string EditStudentDet(TBLStudent val)
        {
            try
            {
                sqlHelper obj = new sqlHelper();
                //  int chk = check Roll no only  Convert.ToInt32(obj.ExecuteScaler("select count(*) from TBLStudent where IsDeleted is null and SchoolID='" + val.SchoolID + "' and ClassID='" + val.ClassID + "' and SectionID='" + val.SectionID + "' and AcademicYear='" + val.AcademicYear + "'  and RollNo='" + val.RollNo + "' and ID<>'"+val.ID+ "' and RollNo!='' "));
                //val.Status = 3;
                int chk = Convert.ToInt32(obj.ExecuteScaler("select count(*) from TBLStudent where IsDeleted is null and SchoolID='" + val.SchoolID + "' and BiometricID='" + val.BiometricID + "' and ID<>'" + val.ID + "' and BiometricID !='' "));
                var i = "00";
                if (chk == 0)
                {

                    db.Entry(val).State = EntityState.Modified;
                    db.SaveChanges();
                    i = val.ID.ToString();
                }



                return i;
            }
            catch (Exception) { return "0"; }
        }



        [System.Web.Http.Route("api/StudentApi/GetGenders")]
        [System.Web.Http.HttpPost]
        public tblGender[] GetGenders()
        {
            List<tblGender> list = new List<tblGender>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblGenders.SqlQuery("Select * from tblGender where Status=" + 1 + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblGender items = new tblGender();
                    items.GenderName = a.GenderName;
                    items.gender_id = a.gender_id;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/GetLanguages")]
        [System.Web.Http.HttpPost]
        public TBLLanguage[] GetLanguages()
        {
            List<TBLLanguage> list = new List<TBLLanguage>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.TBLLanguages.SqlQuery("Select * from TBLLanguages where Status=1").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    TBLLanguage items = new TBLLanguage();
                    items.Language = a.Language;
                    items.LangID = a.LangID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }

        //[System.Web.Http.Route("api/StudentApi/UpdateParentsDet")]
        //[System.Web.Http.HttpPost]
        //public string UpdateParentsDet(TBLStudent student)
        //{
        //    try
        //    {
        //        student.Status = 9;
        //        db.Entry(student).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return "1";
        //    }
        //    catch (Exception e)
        //    {
        //        return "0";
        //    }
        //}

        [System.Web.Http.Route("api/StudentApi/SaveOtherDetails")]
        [System.Web.Http.HttpPost]
        public string SaveOtherDetails(Student val)
        {
            try
            {
                if (val.PSClass != null && val.PSClass != -1)
                {
                    TBLPrevSchoolDet ps = new SchoolErp.TBLPrevSchoolDet();
                    ps.PSYear = val.PSYear;
                    ps.School = val.School;
                    ps.Class = val.PSClass;
                    ps.Board = val.Board;
                    ps.Marks = val.Marks;
                    ps.Awards = val.Awards;
                    ps.StudentID = val.ID;
                    ps.TCNo = val.TCNo;
                    db.TBLPrevSchoolDets.Add(ps);

                    if (val.PSClass2 != null && val.PSClass2 != -1)
                    {
                        TBLPrevSchoolDet ps2 = new SchoolErp.TBLPrevSchoolDet();
                        ps2.PSYear = val.PSYear2;
                        ps2.School = val.School2;
                        ps2.Class = val.PSClass2;
                        ps2.Board = val.Board2;
                        ps2.Marks = val.Marks2;
                        ps2.Awards = val.Awards;
                        ps2.StudentID = val.ID;
                        db.TBLPrevSchoolDets.Add(ps2);

                        if (val.PSClass3 != null && val.PSClass2 != -1)
                        {
                            TBLPrevSchoolDet ps3 = new SchoolErp.TBLPrevSchoolDet();
                            ps3.PSYear = val.PSYear3;
                            ps3.School = val.School3;
                            ps3.Class = val.PSClass3;
                            ps3.Board = val.Board3;
                            ps3.Marks = val.Marks3;
                            ps3.Awards = val.Awards;
                            ps3.StudentID = val.ID;
                            db.TBLPrevSchoolDets.Add(ps3);
                        }
                    }
                }

                if (val.Height != null || val.Weight != null)
                {
                    TBLStudentHealthDet ht = new TBLStudentHealthDet();
                    ht.Height = val.Height;
                    ht.Weight = val.Weight;
                    ht.VisionLeft = val.VisionLeft;
                    ht.VisionRight = val.VisionRight;
                    ht.MedicationDetails = val.MedicationDetails;
                    ht.StudentID = val.ID;
                    db.TBLStudentHealthDets.Add(ht);
                }
                db.SaveChanges();

                var i = val.ID.ToString();
                return i;
            }
            catch (Exception) { return "No"; }

        }

        [System.Web.Http.Route("api/StudentApi/EditOtherDetails1")]
        [System.Web.Http.HttpPost]
        public string EditOtherDetails1(Student val)
        {
            try
            {
                //DateTime dt = new DateTime();
                if (val.ID != 0)
                {
                    TBLPrevSchoolDet ps = new SchoolErp.TBLPrevSchoolDet();
                    ps.ID = Convert.ToInt32(val.PSID);
                    ps.School = val.School;
                    ps.PSYear = val.PSYear;
                    ps.Class = val.PSClass;
                    ps.Board = val.Board;
                    ps.Marks = val.Marks;
                    ps.Awards = val.Awards;
                    ps.StudentID = val.ID;

                    ps.TCNo = val.TCNo;
                    db.Entry(ps).State = EntityState.Modified;
                    if (val.PSID == null)
                    {
                        db.TBLPrevSchoolDets.Add(ps);
                    }
                    else
                    {
                        //Update TBLPrevSchoolDets
                        ps.ID = Convert.ToInt32(val.PSID);
                        var update = db.TBLPrevSchoolDets.Where(u => u.ID == ps.ID).FirstOrDefault();


                        update.School = val.School;
                        update.PSYear = val.PSYear;
                        update.Class = val.PSClass;
                        update.Board = val.Board;
                        update.Marks = val.Marks;
                        update.Awards = val.Awards;
                        update.StudentID = val.ID;
                        update.TCNo = val.TCNo;

                        db.SaveChanges();
                        //db.Entry(ps).State = EntityState.Modified;
                    }
                    if (val.PSClass2 != null)
                    {
                        TBLPrevSchoolDet ps2 = new SchoolErp.TBLPrevSchoolDet();
                        ps2.PSYear = val.PSYear2;
                        ps2.School = val.School2;
                        ps2.Class = val.PSClass2;
                        ps2.Board = val.Board2;
                        ps2.Marks = val.Marks2;
                        ps2.Awards = val.Awards;
                        ps2.StudentID = val.ID;
                        if (val.PSID2 == null)
                        {
                            db.TBLPrevSchoolDets.Add(ps2);
                        }
                        else
                        {
                            //ps2.ID = Convert.ToInt32(val.PSID2);
                            //db.Entry(ps2).State = EntityState.Modified;

                            //Update TBLPrevSchoolDets
                            ps2.ID = Convert.ToInt32(val.PSID2);
                            var update = db.TBLPrevSchoolDets.Where(u => u.ID == ps2.ID).FirstOrDefault();


                            update.School = val.School2;
                            update.PSYear = val.PSYear2;
                            update.Class = val.PSClass2;
                            update.Board = val.Board2;
                            update.Marks = val.Marks2;
                            update.Awards = val.Awards;
                            update.StudentID = val.ID;
                            update.TCNo = val.TCNo;

                            db.SaveChanges();
                        }

                    }

                    if (val.PSClass3 != null)
                    {
                        TBLPrevSchoolDet ps3 = new SchoolErp.TBLPrevSchoolDet();
                        ps3.School = val.School3;
                        ps3.PSYear = val.PSYear3;
                        ps3.Class = val.PSClass3;
                        ps3.Board = val.Board3;
                        ps3.Marks = val.Marks3;
                        ps3.Awards = val.Awards;
                        ps3.StudentID = val.ID;

                        if (val.PSID3 == null)
                        {
                            db.TBLPrevSchoolDets.Add(ps3);
                        }
                        else
                        {
                            //ps3.ID = Convert.ToInt32(val.PSID3);
                            //db.Entry(ps3).State = EntityState.Modified;

                            //Update TBLPrevSchoolDets
                            ps3.ID = Convert.ToInt32(val.PSID3);
                            var update = db.TBLPrevSchoolDets.Where(u => u.ID == ps3.ID).FirstOrDefault();


                            update.School = val.School3;
                            update.PSYear = val.PSYear3;
                            update.Class = val.PSClass3;
                            update.Board = val.Board3;
                            update.Marks = val.Marks3;
                            update.Awards = val.Awards;
                            update.StudentID = val.ID;
                            update.TCNo = val.TCNo;

                            db.SaveChanges();





                        }
                    }
                }

                if (val.Height != null || val.Weight != null)
                {
                    TBLStudentHealthDet ht = new TBLStudentHealthDet();
                    ht.Height = val.Height;
                    ht.Weight = val.Weight;
                    ht.VisionLeft = val.VisionLeft;
                    ht.VisionRight = val.VisionRight;
                    ht.MedicationDetails = val.MedicationDetails;
                    ht.StudentID = val.ID;
                    if (val.HID != 0)
                    {
                        ht.ID = Convert.ToInt32(val.HID);
                        db.Entry(ht).State = EntityState.Modified;
                    }
                    else
                    {
                        db.TBLStudentHealthDets.Add(ht);
                    }
                }

                db.SaveChanges();

                var i = val.ID.ToString();
                return i;
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
                return "No";
            }
        }

        [System.Web.Http.Route("api/StudentApi/UploadDocs")]
        [System.Web.Http.HttpPost]
        public string UploadDocs()
        {
            try
            {
                int iUploadedCnt = 0;
                var type = "";
                var docIDs = "";
                var docTex = "";
                string[] d = { };
                string[] dT = { };
                int dcount = 0;
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                string relPath = "";

                NameValueCollection nvc = HttpContext.Current.Request.Form;
                var model = new Student();

                // iterate through and map to strongly typed model
                foreach (string kvp in nvc.AllKeys)
                {
                    //PropertyInfo pi = model.GetType().GetProperty(kvp, BindingFlags.Public | BindingFlags.Instance);
                    //if (pi != null)
                    //{
                    //    pi.SetValue(model, nvc[kvp], null);
                    //}
                    if (kvp == "ID")
                    {
                        model.ID = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "RollNo")
                    {
                        model.RollNo = nvc[kvp];
                    }
                    else if (kvp == "FMobile")
                    {
                        model.FMobile = nvc[kvp];
                    }
                    else if (kvp == "Mmobile")
                    {
                        model.Mmobile = nvc[kvp];
                    }
                    else if (kvp == "type")
                    {
                        type = nvc[kvp];
                        if (nvc[kvp] == "Student")
                        {

                            //relPath = "/Images/Student/Pics/";
                            relPath = "http://www.smartvidhya.com/Images/Student/Pics/";
                            sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Images/Student/Pics/");

                            //sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Student/Pics/");
                            //string imagenamestring = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + System.IO.Path.GetRandomFileName().Replace(".", string.Empty) + ImageExtensions;
                        }
                        else if (nvc[kvp] == "FParent" || nvc[kvp] == "MParent")
                        {
                            relPath = "http://www.smartvidhya.com/Images/Parent/";
                            sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Images/Parent/");
                        }
                        else if (nvc[kvp] == "Docs")
                        {
                            relPath = "http://www.smartvidhya.com/Images/Student/Docs/";
                            sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Images/Student/Docs/");
                        }
                    }

                    //fd.append('Susername', txtStUsername);
                    //fd.append('Spwd', txtStPassword);
                    //fd.append('PuserName', txtPUsername);
                    //fd.append('Ppwd', txtPPassword);

                    else if (kvp == "DocLength")
                    {
                        model.docLength = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "DocIDs")
                    {
                        docIDs = nvc[kvp];
                    }
                    else if (kvp == "Susername")
                    {
                        model.SUserID = nvc[kvp];
                    }
                    else if (kvp == "Spwd")
                    {
                        model.SPwd = nvc[kvp];


                        MD5 md5 = new MD5CryptoServiceProvider();

                        //compute hash from the bytes of text  
                        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(nvc[kvp]));

                        //get hash result after compute it  
                        byte[] result = md5.Hash;

                        StringBuilder strBuilderSPWD = new StringBuilder();
                        for (int i = 0; i < result.Length; i++)
                        {
                            //change it into 2 hexadecimal digits  
                            //for each byte  
                            strBuilderSPWD.Append(result[i].ToString("x2"));
                        }

                        model.SPwd = strBuilderSPWD.ToString();
                    }
                    else if (kvp == "PuserName")
                    {
                        model.PUserID = nvc[kvp];
                    }
                    else if (kvp == "Ppwd")
                    {
                        MD5 md5 = new MD5CryptoServiceProvider();

                        //compute hash from the bytes of text  
                        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(nvc[kvp]));

                        //get hash result after compute it  
                        byte[] result = md5.Hash;

                        StringBuilder strBuilderPPWD = new StringBuilder();
                        for (int i = 0; i < result.Length; i++)
                        {
                            //change it into 2 hexadecimal digits  
                            //for each byte  
                            strBuilderPPWD.Append(result[i].ToString("x2"));
                        }

                        model.PPwd = strBuilderPPWD.ToString();
                    }
                    else if (kvp == "docTexts")
                    {
                        docTex = nvc[kvp];
                    }
                }

                if (type == "Docs")
                {
                    var checkstu = db.TBLStudents.Where(x => x.ID == model.ID && x.Status != 3).FirstOrDefault();
                    if (checkstu != null)
                    {
                        d = docIDs.Split(',');
                        dT = docTex.Split(',');
                        int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update TBLStudent set SUserID = '" + model.SUserID + "',SPwd='" + model.SPwd + "',PUserID='" + model.PUserID + "',PPwd='" + model.PPwd + "' where ID = " + model.ID);
                    }
                    else if (checkstu == null)
                    {



                        d = docIDs.Split(',');
                        dT = docTex.Split(',');
                        int noOfRowUpdated1 = db.Database.ExecuteSqlCommand("Update TBLStudent set SUserID = '" + model.SUserID + "',PUserID='" + model.PUserID + "' where ID = " + model.ID);
                    }
                }

                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

                // CHECK THE FILE COUNT.
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        {
                            // SAVE THE FILES IN THE FOLDER.
                            if (type == "Student")
                            {

                                //PIC NAME NULL
                                int _min = 1000;
                                int _max = 9999;
                                Random _rdm = new Random();
                                int StupicNO = _rdm.Next(_min, _max);

                                string Stupic1 = model.RollNo.Replace(" ", string.Empty);
                                string Stupic2 = Stupic1.Replace(".", string.Empty);
                                string Stupic3 = Stupic2.Substring(0, 3).ToUpper();

                                string St_pic = sPath + Stupic3 + StupicNO + Path.GetExtension(hpf.FileName);
                                string picpath = relPath + Stupic3 + StupicNO + Path.GetExtension(hpf.FileName);
                                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update TBLStudent set PicPath = '" + picpath + "' where ID = " + model.ID);

                                hpf.SaveAs(St_pic);
                            }
                            else if (type == "FParent")
                            {

                                int _min = 1000;
                                int _max = 9999;
                                Random _rdm = new Random();
                                int FatherpicNO = _rdm.Next(_min, _max);
                                string Fatherpic1 = model.FMobile.Replace(" ", string.Empty);
                                string Fatherpic2 = Fatherpic1.Replace(".", string.Empty);
                                string Fatherpic3 = Fatherpic2.Substring(0, 3).ToUpper();

                                string F_pic = sPath + Fatherpic3 + FatherpicNO + Path.GetExtension(hpf.FileName);
                                string picpath = relPath + Fatherpic3 + FatherpicNO + Path.GetExtension(hpf.FileName);
                                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update TBLStudent set FPicPath = '" + picpath + "' where ID = " + model.ID);
                                hpf.SaveAs(F_pic);
                            }
                            else if (type == "MParent")
                            {

                                int _min = 1000;
                                int _max = 9999;
                                Random _rdm = new Random();
                                int MotherpicNO = _rdm.Next(_min, _max);

                                string Motherpic1 = model.Mmobile.Replace(" ", string.Empty);
                                string Motherpic2 = Motherpic1.Replace(".", string.Empty);
                                string Motherpic3 = Motherpic2.Substring(0, 3).ToUpper();

                                string M_pic = sPath + Motherpic3 + MotherpicNO + Path.GetExtension(hpf.FileName);
                                string picpath = relPath + Motherpic3 + MotherpicNO + Path.GetExtension(hpf.FileName);
                                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update TBLStudent set MPicPath = '" + picpath + "' where ID = " + model.ID);
                                hpf.SaveAs(M_pic);

                            }
                            else if (type == "Docs")
                            {
                                if (type == "Docs")
                                {
                                    string sitename = "http://www.smartvidhya.com";
                                    var docid = Convert.ToInt32(d[dcount]);
                                    var docText = dT[dcount];
                                    docText = docText.Replace(" ", String.Empty);
                                    string document = sPath + sitename + docText + model.ID + Path.GetExtension(hpf.FileName);
                                    string Docpath = relPath + docText + model.ID + Path.GetExtension(hpf.FileName);
                                    int noOfRowUpdated1 = db.Database.ExecuteSqlCommand("Insert into TBLStudentDocs (StudentID,DocID,DocPath,Status)" +
                                        " values (" + model.ID + "," + docid + ",'" + Docpath + "'," + 1 + ")");
                                    hpf.SaveAs(document);
                                    dcount = dcount + 1;
                                }
                            }
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }




                return "1";
            }
            catch (Exception e) { return "0"; throw e; }
        }

        [System.Web.Http.Route("api/StudentApi/FinalSubmit")]
        [System.Web.Http.HttpPost]
        public string FinalSubmit(List<string> val)
        {
            try
            {
                int id = Convert.ToInt32(val[0]);
                int status = Convert.ToInt32(val[1]);
                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update TBLStudent set Status = " + status + " where ID=" + id);

                var i = noOfRowUpdated.ToString();
                return i;
            }
            catch (Exception e) { return "0"; throw e; }
        }



        [System.Web.Http.Route("api/StudentApi/FindStudentByIDapp")]
        [System.Web.Http.HttpGet]
        public studentdetails FindStudentByIDapp(string val)
        {
            studentdetails obj = new studentdetails();
            Student st = new Models.Student();
            try
            {
                if (string.IsNullOrEmpty(val))
                {
                    obj.status = false;
                    obj.message = "Please Enter Student ID";
                }
                else
                {
                    string a = val.ToString();
                    TBLStudent s = db.TBLStudents.Find(Convert.ToInt32(a));
                    if (s != null)
                    {
                        st.ID = s.ID;
                        st.FirstName = s.FirstName;
                        st.MiddleName = s.MiddleName;
                        st.LastName = s.LastName;
                        st.RegNo = s.RegNo;
                        st.RollNo = s.RollNo;
                        st.streamID = s.StreamID;
                        if (s.StreamID != -1 && s.StreamID != 0)
                        {
                            tblStream strm = db.tblStreams.Find(Convert.ToInt32(s.StreamID));
                            st.stream = strm.StreamName;
                        }

                        if (s.Religion != -1 && s.Religion != null)
                        {
                            tblReligion r = db.tblReligions.Find(Convert.ToInt32(s.Religion));

                            st.Religion = r.ReligionName;
                        }

                        st.ReligionID = s.Religion;
                        st.AadharNo = s.AadharNo;
                        st.AcademicYear = s.AcademicYear;
                        st.BirthPlace = s.BirthPlace;
                        st.BloodGroup = s.BloodGroup;
                        st.CategoryID = s.CategoryID;

                        if (s.CategoryID != -1)
                        {
                            tblCastCategory c = db.tblCastCategories.Find(Convert.ToInt32(s.CategoryID));
                            st.Category = c.CategoryName;
                        }

                        tblCours cl = db.tblCourses.Find(Convert.ToInt32(s.ClassID));
                        st.batchid = s.BatchID;
                        if (s.BatchID != -1 && s.BatchID != 0)
                        {
                            tblBatch bt = db.tblBatches.Find(Convert.ToInt32(s.BatchID));
                            st.batch = bt.BatchName;
                        }

                        st.Class = cl == null ? null : cl.CourseName;
                        st.ClassID = s.ClassID;

                        st.CorrespondAddress = s.CorrespondAddress;
                        if (s.DOB != null)
                        {
                            st.DOB = s.DOB;
                        }
                        st.emailID = s.emailID;
                        st.FAdharNo = s.FAdharNo;
                        st.FatherName = s.FatherName;
                        st.FIncome = s.FIncome;
                        st.FJob = s.FJob;
                        if (s.FDOB != null)
                        {
                            st.FDOB = s.FDOB;
                        }
                        st.FDesig = s.FDesig;
                        st.FOfficeAddress = s.FOfficeAddress;
                        st.Fmail = s.Fmail;
                        st.FMobile = s.FMobile;
                        if (s.FQualification != -1 && s.FQualification != null)
                        {
                            tblQualification q = db.tblQualifications.Find(Convert.ToInt32(s.FQualification));
                            st.FQualification = q.QualificationName;
                        }
                        if (s.MQualification != -1 && s.MQualification != null)
                        {
                            tblQualification q1 = db.tblQualifications.Find(Convert.ToInt32(s.MQualification));
                            st.MQualification = q1.QualificationName;
                        }
                        if (s.GQualification != -1 && s.GQualification != null)
                        {
                            tblQualification q2 = db.tblQualifications.Find(Convert.ToInt32(s.GQualification));
                            st.GQualification = q2.QualificationName;
                        }
                        st.FQualificationID = s.FQualification;
                        st.FNationality = s.FNationality;

                        tblGender g = db.tblGenders.Find(Convert.ToInt32(s.GenderID));
                        st.Gender = g.GenderName;
                        st.GenderID = s.GenderID;
                        st.GAdharNo = s.GAdharNo;
                        st.GIncome = s.GIncome;
                        st.GJob = s.GJob;
                        st.GQualificationID = s.GQualification;
                        st.Gmail = s.Gmail;
                        st.GuardianName = s.GuardianName;
                        st.GNationality = s.GNationality;
                        st.GDesig = s.GDesig;
                        st.Gmobile = s.Gmobile;
                        st.GOfficeAddress = s.GOfficeAddress;
                        st.JoiningDate = s.JoiningDate;
                        st.Languages = s.Languages;
                        string languages = "";
                        if (st.Languages != "" && st.Languages != null)
                        {
                            var l = st.Languages.Split(',');
                            for (int i = 0; i < l.Length; i++)
                            {
                                TBLLanguage lg = db.TBLLanguages.Find(Convert.ToInt32(l[i]));
                                if (i == 0) { languages = lg.Language; }
                                else { languages = languages + "," + lg.Language; }
                            }
                            st.Lang_known = languages;
                        }
                        st.MAdharNo = s.MAdharNo;
                        st.MIncome = s.MIncome;
                        st.MotherName = s.MotherName;
                        st.MotherTongue = s.MotherTongue;
                        st.MQualificationID = s.MQualification;
                        st.Mmail = s.Mmail;
                        st.MJob = s.MJob;
                        st.Mmobile = s.Mmobile;
                        st.MNationality = s.MNationality;
                        if (s.MDOB != null)
                        {
                            st.MDOB = s.MDOB;
                        }

                        st.MDesig = s.MDesig;
                        st.MPicPath = s.MPicPath;
                        st.MOfficeAddress = s.MOfficeAddress;
                        st.FPicPath = s.FPicPath;
                        st.Nationality = s.Nationality;
                        st.PermanentAddress = s.PermanentAddress;
                        st.PermCity = s.PermCity;
                        st.PermPincode = s.PermPincode;
                        if (s.PermState != "" && s.PermState != null && s.PermState != "-1" && s.PermState != "0")
                        {
                            tblState state = db.tblStates.Find(Convert.ToInt32(s.PermState));
                            st.PermStateNm = state.StateName;
                        }
                        if (s.CorsState != "" && s.CorsState != null && s.CorsState != "-1" && s.CorsState != "0")
                        {
                            tblState state1 = db.tblStates.Find(Convert.ToInt32(s.CorsState));
                            st.CorsStateNm = state1.StateName;
                        }
                        if (s.CorsCountry != "" && s.CorsCountry != null && s.CorsCountry != "-1" && s.CorsCountry != "0")
                        {
                            tblCountry Country = db.tblCountries.Find(Convert.ToInt32(s.CorsCountry));
                            st.croscountrynameNm = Country.CountryName;
                        }

                        if (s.PermCountry != "" && s.PermCountry != null && s.PermCountry != "-1" && s.PermCountry != "0")
                        {
                            tblCountry Country = db.tblCountries.Find(Convert.ToInt32(s.PermCountry));
                            st.PermcountrynameNm = Country.CountryName;
                        }

                        //if (s.PermCity != "" && s.PermCity != null && s.PermCity != "-1")
                        //{
                        //    tblCity city = db.tblCities.Find(Convert.ToInt32(s.PermCity));
                        //    st.PermCityNm = city.CityName;
                        //}
                        //if (s.CorsCity != "" && s.CorsCity != null && s.CorsCity != "-1")
                        //{
                        //    tblCity city = db.tblCities.Find(Convert.ToInt32(s.CorsCity));
                        //    st.corrCityNm = city.CityName;
                        //}

                        st.PermState = s.PermState;


                        if (string.IsNullOrEmpty(s.PicPath))
                        {
                            st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                        }
                        else
                        {
                            st.PicPath = s.PicPath;
                        }



                        st.PPwd = s.PPwd;
                        st.Relation = s.Relation;
                        st.SectionID = Convert.ToInt32(s.SectionID);
                        if (s.SectionID != -1 && st.Languages != null)
                        {
                            tblSection sec = db.tblSections.Find(Convert.ToInt32(s.SectionID));
                            st.Section = sec.SectionName;
                        }
                        st.SMSmobileNo = s.SMSmobileNo;
                        st.SPwd = s.SPwd;
                        st.SUserID = s.SUserID;
                        st.PUserID = s.PUserID;
                        st.CorsPincode = s.CorsPincode;
                        st.CorsCity = s.CorsCity;
                        st.CorsState = s.CorsState;
                        st.SMSmobileNo = s.SMSmobileNo;
                        st.EmergencyNo = s.EmergencyNo;
                        st.EmerContPerson = s.EmerContPerson;
                        st.ContPersRelation = s.ContPersRelation;
                        st.SRNo = s.SRNo;
                        st.Status = s.Status;
                        tblStatu stat = db.tblStatus.Find(s.Status);
                        st.StatusName = stat == null ? null : stat.Status;

                        //TBLStudentDoc doc = new TBLStudentDoc();
                        List<TBLPrevSchoolDet> list = new List<TBLPrevSchoolDet>();

                        var result = db.TBLPrevSchoolDets.SqlQuery("Select * from TBLPrevSchoolDet where StudentID=" + s.ID + "").ToList();
                        //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                        foreach (var x in result)
                        {
                            TBLPrevSchoolDet p = new TBLPrevSchoolDet();
                            p.ID = x.ID;
                            p.Awards = x.Awards;
                            p.Board = x.Board;
                            p.Class = x.Class;
                            p.Marks = x.Marks;
                            p.PSYear = x.PSYear;
                            p.School = x.School;
                            p.TCNo = x.TCNo;
                            list.Add(p);
                        }
                        st.prevlist = list;

                        var result1 = db.TBLStudentHealthDets.SqlQuery("Select * from TBLStudentHealthDet where StudentID=" + s.ID + "");
                        //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                        foreach (var x in result1)
                        {
                            TBLStudentHealthDet p = new TBLStudentHealthDet();
                            p.ID = x.ID;
                            p.Height = x.Height;
                            p.MedicationDetails = x.MedicationDetails;
                            p.VisionLeft = x.VisionLeft;
                            p.VisionRight = x.VisionRight;
                            p.Weight = x.Weight;
                            st.ht = p;
                        }

                        //TBLStudentHealthDet ht = new TBLStudentHealthDet();
                        obj.data = st;
                        obj.status = true;
                        obj.message = "Sucess";

                    }
                    else
                    {

                        obj.status = false;
                        obj.message = "Student Details Not Found";

                    }
                }
            }
            catch
            {
                obj.status = false;
                obj.message = "Something Went Wrong";

            }


            return obj;
        }


        [System.Web.Http.Route("api/StudentApi/FindStudentByID")]
        [System.Web.Http.HttpPost]
        public Student FindStudentByID(Parameters param)
        {

            Student st = new Models.Student();
            string a = param.val.ToString();
            TBLStudent s = db.TBLStudents.Find(Convert.ToInt32(a));

            st.ID = s.ID;
            st.FirstName = s.FirstName;
            st.MiddleName = s.MiddleName;
            st.LastName = s.LastName;
            st.RegNo = s.RegNo;
            st.RollNo = s.RollNo;

            if (s.StreamID != -1 && s.StreamID != 0)
            {
                tblStream strm = db.tblStreams.Find(Convert.ToInt32(s.StreamID));
                st.stream = strm.StreamName;
            }

            if (s.Religion != -1 && s.Religion != null)
            {
                tblReligion r = db.tblReligions.Find(Convert.ToInt32(s.Religion));

                st.Religion = r.ReligionName;
            }

            st.ReligionID = s.Religion;
            st.AadharNo = s.AadharNo;
            st.AcademicYear = s.AcademicYear;
            st.BirthPlace = s.BirthPlace;
            st.BloodGroup = s.BloodGroup;
            st.CategoryID = s.CategoryID;
            st.streamID = s.StreamID;
            if (s.CategoryID != -1)
            {
                tblCastCategory c = db.tblCastCategories.Find(Convert.ToInt32(s.CategoryID));
                st.Category = c.CategoryName;
            }

            tblCours cl = db.tblCourses.Find(Convert.ToInt32(s.ClassID));
            //st.batchid = s.BatchID;
            //if (s.BatchID != -1)
            //{
            //    tblBatch bt = db.tblBatches.Find(Convert.ToInt32(s.BatchID));
            //    st.batch = bt.BatchName;
            //}





            st.Class = cl.CourseName;

            st.ClassID = s.ClassID;
            st.CorrespondAddress = s.CorrespondAddress;
            if (s.DOB != null)
            {
                st.DOB = s.DOB;
            }
            st.emailID = s.emailID;
            st.FAdharNo = s.FAdharNo;
            st.FatherName = s.FatherName;
            st.FIncome = s.FIncome;
            st.FJob = s.FJob;
            if (s.FDOB != null)
            {
                st.FDOB = s.FDOB;
            }
            st.FDesig = s.FDesig;
            st.FOfficeAddress = s.FOfficeAddress;
            st.Fmail = s.Fmail;
            st.FMobile = s.FMobile;
            if (s.FQualification != -1 && s.FQualification != null)
            {
                tblQualification q = db.tblQualifications.Find(Convert.ToInt32(s.FQualification));
                st.FQualification = q.QualificationName;
            }
            if (s.MQualification != -1 && s.MQualification != null)
            {
                tblQualification q1 = db.tblQualifications.Find(Convert.ToInt32(s.MQualification));
                st.MQualification = q1.QualificationName;
            }
            if (s.GQualification != -1 && s.GQualification != null)
            {
                tblQualification q2 = db.tblQualifications.Find(Convert.ToInt32(s.GQualification));
                st.GQualification = q2.QualificationName;
            }
            st.FQualificationID = s.FQualification;
            st.FNationality = s.FNationality;

            tblGender g = db.tblGenders.Find(Convert.ToInt32(s.GenderID));
            if (g != null)
            {
                st.Gender = g.GenderName;
                st.GenderID = s.GenderID;
            }

            st.GAdharNo = s.GAdharNo;
            st.GIncome = s.GIncome;
            st.GJob = s.GJob;
            st.GQualificationID = s.GQualification;
            st.Gmail = s.Gmail;
            st.GuardianName = s.GuardianName;
            st.GNationality = s.GNationality;
            st.GDesig = s.GDesig;
            st.Gmobile = s.Gmobile;
            st.GOfficeAddress = s.GOfficeAddress;
            st.JoiningDate = s.JoiningDate;
            st.Languages = s.Languages;
            st.BiometricID = s.BiometricID;
            string languages = "";
            if (st.Languages != "" && st.Languages != null)
            {
                var l = st.Languages.Split(',');
                for (int i = 0; i < l.Length; i++)
                {
                    TBLLanguage lg = db.TBLLanguages.Find(Convert.ToInt32(l[i]));
                    if (i == 0) { languages = lg.Language; }
                    else { languages = languages + "," + lg.Language; }
                }
                st.Lang_known = languages;
            }
            st.MAdharNo = s.MAdharNo;
            st.MIncome = s.MIncome;
            st.MotherName = s.MotherName;
            st.MotherTongue = s.MotherTongue;
            st.MQualificationID = s.MQualification;
            st.Mmail = s.Mmail;
            st.MJob = s.MJob;
            st.Mmobile = s.Mmobile;
            st.MNationality = s.MNationality;
            if (s.MDOB != null)
            {
                st.MDOB = s.MDOB;
            }

            st.MDesig = s.MDesig;
            st.MPicPath = s.MPicPath;
            st.MOfficeAddress = s.MOfficeAddress;
            st.FPicPath = s.FPicPath;
            st.Nationality = s.Nationality;
            st.PermanentAddress = s.PermanentAddress;
            st.PermCity = s.PermCity;

            st.PermPincode = s.PermPincode;
            if (s.PermState != "" && s.PermState != null && s.PermState != "-1")
            {
                tblState state = db.tblStates.Find(Convert.ToInt32(s.PermState));
                st.PermStateNm = state.StateName;
            }
            if (s.CorsState != "" && s.CorsState != null && s.CorsState != "-1")
            {
                tblState state1 = db.tblStates.Find(Convert.ToInt32(s.CorsState));
                st.CorsStateNm = state1.StateName;
            }
            if (s.CorsCountry != "" && s.CorsCountry != null && s.CorsCountry != "-1" && s.CorsCountry != "0")
            {
                tblCountry Country = db.tblCountries.Find(Convert.ToInt32(s.CorsCountry));
                st.croscountrynameNm = Country.CountryName;
            }

            if (s.PermCountry != "" && s.PermCountry != null && s.PermCountry != "-1" && s.PermCountry != "0")
            {
                tblCountry Country = db.tblCountries.Find(Convert.ToInt32(s.PermCountry));
                st.PermcountrynameNm = Country.CountryName;
            }
            st.PermcountrynameNm1 = s.PermCountry;
            st.croscountrynameNm1 = s.CorsCountry;
            st.PermState1 = s.PermState;
            st.CorsState1 = s.CorsState;
            //st.PermCity1 = s.PermCity;
            //st.CorsCity1 = s.CorsCity;
            //if (s.PermCity != "" && s.PermCity != null && s.PermCity != "-1")
            //{
            //    tblCity city = db.tblCities.Find(Convert.ToInt32(s.PermCity));
            //    st.PermCityNm = city.CityName;
            //}
            //if (s.CorsCity != "" && s.CorsCity != null && s.CorsCity != "-1")
            //{
            //    tblCity city = db.tblCities.Find(Convert.ToInt32(s.CorsCity));
            //    st.corrCityNm = city.CityName;
            //}

            st.PermState = s.PermState;


            if (string.IsNullOrEmpty(s.PicPath))
            {
                st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
            }
            else
            {
                st.PicPath = s.PicPath;
            }
            //get password Parents
            //if (s.PPwd != null)
            //{
            //    MD5 md5_p = new MD5CryptoServiceProvider();

            //    //compute hash from the bytes of text  
            //    md5_p.ComputeHash(ASCIIEncoding.ASCII.GetBytes(s.PPwd));

            //    //get hash result after compute it  
            //    byte[] resultp = md5_p.Hash;

            //    StringBuilder strBuilderpPWD = new StringBuilder();
            //    for (int i = 0; i < resultp.Length; i++)
            //    {
            //        //change it into 2 hexadecimal digits  
            //        //for each byte  
            //        strBuilderpPWD.Append(resultp[i].ToString("x2"));
            //    }

            //    st.PPwd = strBuilderpPWD.ToString();
            //}

            //  st.PPwd = s.PPwd;


            st.Relation = s.Relation;
            st.SectionID = Convert.ToInt32(s.SectionID);
            if (s.SectionID != -1)
            {
                tblSection sec = db.tblSections.Find(Convert.ToInt32(s.SectionID));
                st.Section = sec.SectionName;
            }
            st.SMSmobileNo = s.SMSmobileNo;
            //if (s.SPwd !=null)
            //{
            //    MD5 md5 = new MD5CryptoServiceProvider();

            //    //compute hash from the bytes of text  
            //    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(s.PPwd));

            //    //get hash result after compute it  
            //    byte[] resultS = md5.Hash;

            //    StringBuilder strBuilderSPWD = new StringBuilder();
            //    for (int i = 0; i < resultS.Length; i++)
            //    {
            //        //change it into 2 hexadecimal digits  
            //        //for each byte  
            //        strBuilderSPWD.Append(resultS[i].ToString("x2"));
            //    }
            //    st.SPwd = strBuilderSPWD.ToString();
            //}


            st.PPwd = s.PPwd;
            st.SPwd = s.SPwd;
            st.SUserID = s.SUserID;
            st.PUserID = s.PUserID;
            st.CorsPincode = s.CorsPincode;
            st.CorsCity = s.CorsCity;
            st.CorsState = s.CorsState;
            st.SMSmobileNo = s.SMSmobileNo;
            st.EmergencyNo = s.EmergencyNo;
            st.EmerContPerson = s.EmerContPerson;
            st.ContPersRelation = s.ContPersRelation;
            st.SRNo = s.RegNo;
            st.Status = s.Status;
            tblStatu stat = db.tblStatus.Find(s.Status);
            if (stat != null)
            {
                st.StatusName = stat.Status;
            }
            st.SchoolID = s.SchoolID.ToString();
            //TBLStudentDoc doc = new TBLStudentDoc();
            List<TBLPrevSchoolDet> list = new List<TBLPrevSchoolDet>();

            var result = db.TBLPrevSchoolDets.SqlQuery("Select * from TBLPrevSchoolDet where StudentID=" + s.ID + "").ToList();
            //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
            foreach (var x in result)
            {
                TBLPrevSchoolDet p = new TBLPrevSchoolDet();
                p.ID = x.ID;
                p.Awards = x.Awards;
                p.Board = x.Board;
                p.Class = x.Class;
                p.Marks = x.Marks;
                p.PSYear = x.PSYear;
                p.School = x.School;
                p.TCNo = x.TCNo;
                list.Add(p);
            }
            st.prevlist = list;

            var result1 = db.TBLStudentHealthDets.SqlQuery("Select * from TBLStudentHealthDet where StudentID=" + s.ID + "");
            //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
            TBLStudentHealthDet pa = new TBLStudentHealthDet();
            foreach (var x in result1)
            {

                pa.ID = x.ID;
                pa.Height = x.Height;
                pa.MedicationDetails = x.MedicationDetails;
                pa.VisionLeft = x.VisionLeft;
                pa.VisionRight = x.VisionRight;
                pa.Weight = x.Weight;

            }
            st.ht = pa;
            //TBLStudentHealthDet ht = new TBLStudentHealthDet();

            return st;
        }

        [System.Web.Http.Route("api/StudentApi/getRegCode")]
        [System.Web.Http.HttpPost]
        public CodeGenMaster getRegCode(List<string> SchoolID)
        {
            sqlHelper obj = new sqlHelper();
            SqlDataReader dr = obj.GetReader("select * from tblDocumentNo where Status=1 and UserType='Student'  and SchoolID='" + SchoolID[0] + "' and IsDeleted is null ");
            CodeGenMaster usr = new CodeGenMaster();
            if (dr.Read())
            {
                usr.Id = dr["id"].ToString();
                usr.DocType = dr["UserType"].ToString();
                usr.Prefix = dr["Perfix"].ToString();
                usr.Suffix = dr["Suffix"].ToString();
                usr.DocNo = dr["DocumentNo"].ToString();
                usr.StartSeries = dr["StartSeries"].ToString();
                usr.Seprator = dr["Serprator"].ToString();
                usr.LastSeries = dr["LastSeries"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
            }

            return usr;



        }

        [System.Web.Http.Route("api/StudentApi/getAllStudentsbyclassSection")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsbyclassSection(List<string> aa)
        {
            //string loginuser = aa[0];
            //int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();


            int rno = 0;
            DataTable dt = obj.getDataTable(@"select st.ID,st.AcademicYear,em.userid as PUserID ,st.SchoolID,st.fathername,st.ClassID,st.SectionID,SUserID,RegNo,c.CourseName Class,scd.School,st.FirstName,st.MiddleName,st.LastName,PicPath,RollNo, s.SectionName, st.SMSmobileNo, ss.Status StatusName, st.Status from TBLStudent
 st inner join tblCourses c on c.Id = st.ClassID inner
join tblSections s on s.Id = st.SectionID
left outer join tblStatus ss on ss.StatusID = st.Status
left outer join tblschooldetails scd on scd.ID = st.SchoolID
left outer join tblemployee em on em.ID = st.SchoolID
where st.IsDeleted is null and st.SchoolID='" + aa[0] + "' and st.ClassID='" + aa[1] + "' and st.SectionID='" + aa[2] + "' and st.IsDeleted is null order by st.FirstName");
            foreach (DataRow dr in dt.Rows)
            {
                rno++;
                Student usr = new Student();
                usr.RollNo = rno.ToString();
                usr.ID = int.Parse(dr["ID"].ToString());
                usr.AcademicYear = dr["AcademicYear"].ToString();
                usr.RegNo = dr["RegNo"].ToString();
                usr.FatherName = dr["fathername"].ToString();
                usr.SUserID = dr["SUserID"].ToString();
                usr.PUserID = dr["PUserID"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.Section = dr["SectionName"].ToString();
                usr.Class = dr["Class"].ToString();
                usr.ClassID = int.Parse(dr["ClassID"].ToString());
                usr.SectionID = int.Parse(dr["SectionID"].ToString());
                usr.SchoolID = dr["SchoolID"].ToString();
                usr.SStatus = dr["StatusName"].ToString();
                usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                usr.School = dr["School"].ToString();



                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                //if (dr["Status"].ToString() == "True")
                //{

                //    usr.SStatus = "Activate";
                //    usr.Extra10 = "btn btn-block btn-success btn-sm";

                //}
                //else
                //{
                //    usr.SStatus = "DeActivate";
                //    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                //}
                //  usr.School = dr["School"].ToString();

                list.Add(usr);
            }





            return list.ToArray();
        }




        [System.Web.Http.Route("api/StudentApi/getAllStudentsbyclassSection1")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsbyclassSection1(List<string> aa)
        {
            //string loginuser = aa[0];
            //int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();


            int rno = 0;
            DataTable dt = obj.getDataTable(@"select st.ID, std.voucherid,std.Amount,std.dated,std.DocNo, st.AcademicYear,em.userid as PUserID ,st.SchoolID,st.fathername,st.ClassID,st.SectionID,SUserID,RegNo,c.CourseName Class,scd.School,st.FirstName,st.MiddleName,st.LastName,PicPath,RollNo, s.SectionName, st.SMSmobileNo, ss.Status StatusName, st.Status from TBLStudent
 st 
left outer join tblStatus ss on ss.StatusID = st.Status
left outer join tblschooldetails scd on scd.ID = st.SchoolID
left outer join tblemployee em on em.ID = st.SchoolID
 inner join stdfee1 std on std.Studentid = st.ID
left join tblCourses c on c.Id=std.CourseID
left join tblSections s on s.Id=std.sem

where st.IsDeleted is null and st.SchoolID='" + aa[0] + "' and std.CourseID='" + aa[1] + "' and std.sem='" + aa[2] + "' and std.Sessionid='" + aa[3] + "' and st.IsDeleted is null order by st.FirstName");
            foreach (DataRow dr in dt.Rows)
            {
                rno++;
                Student usr = new Student();
                usr.RollNo = rno.ToString();
                usr.voucherid = int.Parse(dr["voucherid"].ToString());
                usr.ID = int.Parse(dr["ID"].ToString());
                usr.AcademicYear = dr["AcademicYear"].ToString();
                usr.RegNo = dr["RegNo"].ToString();
                usr.Amount = decimal.Parse(dr["Amount"].ToString());
                usr.dated = DateTime.Parse(dr["dated"].ToString());
                usr.DocNo = dr["DocNo"].ToString();
                usr.FatherName = dr["fathername"].ToString();
                usr.SUserID = dr["SUserID"].ToString();
                usr.PUserID = dr["PUserID"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.Section = dr["SectionName"].ToString();
                usr.Class = dr["Class"].ToString();
                usr.ClassID = int.Parse(dr["ClassID"].ToString());
                usr.SectionID = int.Parse(dr["SectionID"].ToString());
                usr.SchoolID = dr["SchoolID"].ToString();
                usr.SStatus = dr["StatusName"].ToString();
                usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                usr.School = dr["School"].ToString();



                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                //if (dr["Status"].ToString() == "True")
                //{

                //    usr.SStatus = "Activate";
                //    usr.Extra10 = "btn btn-block btn-success btn-sm";

                //}
                //else
                //{
                //    usr.SStatus = "DeActivate";
                //    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                //}
                //  usr.School = dr["School"].ToString();

                list.Add(usr);
            }





            return list.ToArray();
        }







        [System.Web.Http.Route("api/StudentApi/getAllStudentsDetailsBySchool")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsDetailsBySchool(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            if (typeuser == 2)
            {
                DataTable dt = obj.sp_GetDataTableNoParam("sp_getStudentDetails");
                foreach (DataRow dr in dt.Rows)
                {
                    Student usr = new Student();
                    usr.ID = int.Parse(dr["ID"].ToString());
                    usr.RegNo = dr["RegNo"].ToString();
                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    usr.Section = dr["SectionName"].ToString();
                    usr.Class = dr["Class"].ToString();
                    usr.RollNo = dr["RollNo"].ToString();
                    //  usr.PicPath = dr["PicPath"].ToString();
                    usr.SStatus = dr["StatusName"].ToString();
                    usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                    usr.School = dr["School"].ToString();



                    if (dr["PicPath"].ToString() == "")
                    {
                        usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.PicPath = dr["PicPath"].ToString();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    //if (dr["Status"].ToString() == "True")
                    //{

                    //    usr.SStatus = "Activate";
                    //    usr.Extra10 = "btn btn-block btn-success btn-sm";

                    //}
                    //else
                    //{
                    //    usr.SStatus = "DeActivate";
                    //    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                    //}

                    // usr.School = dr["School"].ToString();
                    list.Add(usr);
                }
            }
            else
            {
                int rno = 0;
                DataTable dt = obj.getDataTable(@"select st.ID,SUserID,RegNo,c.CourseName Class,scd.School,st.FirstName,st.MiddleName,st.LastName,PicPath,RollNo, s.SectionName, st.SMSmobileNo, ss.Status StatusName, st.Status from TBLStudent st inner join tblCourses c on c.Id = st.ClassID inner
                                                   join tblSections s on s.Id = st.SectionID
                                                     left outer join tblStatus ss on ss.StatusID = st.Status
                                                  left outer join tblschooldetails scd on scd.ID = st.SchoolID
                                              left outer join tblemployee em on em.SchoolID = st.SchoolID
                                      where st.IsDeleted is null and em.UserID = '" + loginuser + "' and em.IsDeleted is null and em.IsDeleted is null order by st.FirstName");
                foreach (DataRow dr in dt.Rows)
                {
                    rno++;
                    Student usr = new Student();
                    usr.RollNo = rno.ToString();
                    usr.ID = int.Parse(dr["ID"].ToString());
                    usr.RegNo = dr["RegNo"].ToString();
                    usr.SUserID = dr["SUserID"].ToString();
                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                    usr.Section = dr["SectionName"].ToString();
                    usr.Class = dr["Class"].ToString();
                    //usr.RollNo = dr["RollNo"].ToString();
                    //  usr.PicPath = dr["PicPath"].ToString();
                    usr.SStatus = dr["StatusName"].ToString();
                    usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                    usr.School = dr["School"].ToString();



                    if (dr["PicPath"].ToString() == "")
                    {
                        usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                    }
                    else
                    {
                        usr.PicPath = dr["PicPath"].ToString();
                    }
                    //  usr.Status = dr["Status"].ToString();
                    //if (dr["Status"].ToString() == "True")
                    //{

                    //    usr.SStatus = "Activate";
                    //    usr.Extra10 = "btn btn-block btn-success btn-sm";

                    //}
                    //else
                    //{
                    //    usr.SStatus = "DeActivate";
                    //    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                    //}
                    //  usr.School = dr["School"].ToString();

                    list.Add(usr);
                }
            }




            return list.ToArray();
        }




        [System.Web.Http.Route("api/StudentApi/getAllStudentsDetails")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsDetails()
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();


            DataTable dt = obj.sp_GetDataTableNoParam("sp_getStudentDetails");
            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.ID = int.Parse(dr["ID"].ToString());
                usr.RegNo = dr["RegNo"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.Section = dr["SectionName"].ToString();
                usr.Class = dr["Class"].ToString();
                usr.RollNo = dr["RollNo"].ToString();
                //  usr.PicPath = dr["PicPath"].ToString();
                usr.SStatus = dr["StatusName"].ToString();
                usr.SMSmobileNo = dr["SMSmobileNo"].ToString();
                usr.School = dr["School"].ToString();



                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }
                //  usr.Status = dr["Status"].ToString();
                //if (dr["Status"].ToString() == "True")
                //{

                //    usr.SStatus = "Activate";
                //    usr.Extra10 = "btn btn-block btn-success btn-sm";

                //}
                //else
                //{
                //    usr.SStatus = "DeActivate";
                //    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                //}


                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/GetStudentAdmitCard")]
        [System.Web.Http.HttpPost]
        public StudentAdmitCard GetStudentAdmitCard(List<string> value)
        {

            StudentAdmitCard stucard = new StudentAdmitCard();
            try
            {
                int SchoolID = Convert.ToInt32(value[1]);
                int StudentID = Convert.ToInt32(value[0]);

                var SchoolDetails = db.tblSchoolDetails.Where(x => x.ID == SchoolID && x.IsDeleted == null).FirstOrDefault();
                if (SchoolDetails != null)
                {
                    stucard.SchoolName = SchoolDetails.School;
                    stucard.SchoolLogo = SchoolDetails.LogoPic;
                    stucard.SchoolAddress = SchoolDetails.Address;
                    stucard.AffilatedFrom = SchoolDetails.Board;
                    stucard.SchoolPhoneNO = SchoolDetails.Phone/*+","+SchoolDetails.Mobile*/;
                    stucard.SchoolEmailID = SchoolDetails.Email;
                    stucard.District = SchoolDetails.District;




                }


            }
            catch (Exception ex)
            {


            }
            finally
            {
                try
                {




                    string stringClassname = "";
                    int SchoolID = Convert.ToInt32(value[1]);
                    int StudentID = Convert.ToInt32(value[0]);
                    var StudentDetails = db.TBLStudents.Where(s => s.ID == StudentID && s.SchoolID == SchoolID).FirstOrDefault();
                    if (StudentDetails != null)
                    {
                        //studentName
                        stucard.StudentName = StudentDetails.FirstName + " " + StudentDetails.MiddleName + " " + StudentDetails.LastName;

                        //dob
                        stucard.DOB = Convert.ToDateTime(StudentDetails.DOB);
                        //address
                        stucard.StudentAddress = StudentDetails.PermanentAddress;
                        //pincode
                        stucard.Pincode = StudentDetails.PermPincode.ToString();
                        //mobileno
                        stucard.Mobilenumber = StudentDetails.EmergencyNo;
                        //Student Image
                        stucard.StudentImage = StudentDetails.PicPath;
                        //classandSection
                        string Classnamee = db.tblCourses.SingleOrDefault(S => S.Id == StudentDetails.ClassID && S.IsDeleted == null).CourseName;
                        if (Classnamee != null)
                        {
                            stringClassname = Classnamee;

                        }
                        else
                        {
                            stringClassname = "--";

                        }
                        string SectionName = db.tblSections.SingleOrDefault(s => s.Id == StudentDetails.SectionID && s.IsDeleted == null).SectionName;

                        if (SectionName != null)
                        {
                            stringClassname = stringClassname + " " + SectionName;

                        }
                        else
                        {
                            stringClassname = stringClassname + "" + "-";

                        }
                        //className
                        stucard.ClassName = stringClassname;
                        //state
                        if (StudentDetails.PermState != "" && StudentDetails.PermState != null)
                        {
                            int StateID = Convert.ToInt32(StudentDetails.PermState);

                            string statee = db.tblStates.SingleOrDefault(x => x.StateId == StateID).StateName;
                            stucard.State = statee;

                        }
                        else
                        {
                            stucard.State = "";
                        }
                        //city
                        if (StudentDetails.PermCity != "" && StudentDetails.PermCity != null)
                        {
                            int CityID = Convert.ToInt32(StudentDetails.PermCity);
                            string CityNamee = db.tblCities.SingleOrDefault(x => x.Id == CityID).CityName;
                            stucard.City = CityNamee;
                        }
                        else
                        {
                            stucard.City = "";
                        }


                        //gender
                        if (StudentDetails.GenderID != null)
                        {
                            string GenderName = db.tblGenders.SingleOrDefault(x => x.gender_id == StudentDetails.GenderID).GenderName;
                            stucard.Gender = GenderName;
                        }
                        else
                        {
                            stucard.Gender = "--";
                        }


                    }

                }
                catch (Exception ex) { }



            }




            return stucard;

        }


        //[System.Web.Http.Route("api/StudentApi/()")]
        //[System.Web.Http.HttpPost]
        //public Student (List<string> val)
        //{
        //    int a = int.Parse(val[0]);//.ToString();
        //    int SchoolID = int.Parse(val[1]);
        //    Student st = new Models.Student();


        //    string conn = "";
        //    conn = ConfigurationManager.ConnectionStrings["default"].ToString();
        //    SqlConnection objsqlconn = new SqlConnection(conn);
        //    try
        //    {

        //        objsqlconn.Open();
        //        DataSet ds = new DataSet();
        //        SqlCommand objcmd = new SqlCommand("sp_GetStudentDet", objsqlconn);
        //        objcmd.CommandType = CommandType.StoredProcedure;
        //        objcmd.Parameters.AddWithValue("@id", a);
        //        SqlDataReader dr = objcmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            st.ID = int.Parse(dr["ID"].ToString());
        //            st.FirstName = dr["FirstName"].ToString();
        //            st.MiddleName = dr["MiddleName"].ToString();
        //            st.LastName = dr["LastName"].ToString();
        //            st.RegNo = dr["RegNo"].ToString();
        //            st.RollNo = dr["RollNo"].ToString();
        //            st.streamID = int.Parse(dr["StreamID"].ToString());
        //            //st.stream = dr["StreamName"].ToString();
        //            //st.Religion = dr["ReligionName"].ToString();
        //            string religion = dr["Religion"].ToString();
        //            if (religion !="")
        //            {
        //                st.ReligionID = int.Parse(dr["Religion"].ToString());
        //            }
        //            else
        //            {
        //                st.ReligionID = 0;
        //            }

        //            st.AadharNo = dr["AadharNo"].ToString();
        //            st.AcademicYear = dr["AcademicYear"].ToString();
        //            st.BirthPlace = dr["BirthPlace"].ToString();
        //            st.BloodGroup = dr["BloodGroup"].ToString();
        //            st.CategoryID = int.Parse(dr["CategoryID"].ToString());
        //            //st.batchid = int.Parse(dr["BatchID"].ToString());
        //            // st.batch = dr["BatchName"].ToString();
        //            st.Class = dr["CourseName"].ToString();
        //            //st.Category = dr["CategoryName"].ToString();
        //            st.ClassID = int.Parse(dr["ClassID"].ToString());
        //            st.CorrespondAddress = dr["CorrespondAddress"].ToString();
        //            st.DOB = DateTime.Parse(dr["DOB"].ToString());
        //            st.emailID = dr["emailID"].ToString();
        //            //st.FAdharNo = int.Parse(dr["FAdharNo"].ToString());
        //            st.FatherName = dr["FatherName"].ToString();

        //            st.FOfficeAddress = dr["FOfficeAddress"].ToString();
        //            st.Fmail = dr["Fmail"].ToString();
        //            st.FMobile = dr["FMobile"].ToString();

        //            //st.Gender = dr["GenderName"].ToString();
        //            st.GenderID = int.Parse(dr["GenderID"].ToString());

        //            st.Gmail = dr["Gmail"].ToString();
        //            st.JoiningDate = DateTime.Parse(dr["JoiningDate"].ToString());
        //            st.MotherName = dr["MotherName"].ToString();
        //            //st.Mmobile = int.Parse(dr["Mmobile"].ToString());
        //            st.PermanentAddress = dr["PermanentAddress"].ToString();
        //            st.PermCity = dr["PermCity"].ToString();
        //            st.PermPincode = int.Parse(dr["PermPincode"].ToString());
        //            //st.PermStateNm = dr["permst"].ToString();
        //            //st.CorsStateNm = dr["cospst"].ToString();
        //            st.PicPath = dr["PicPath"].ToString();
        //            st.PPwd = dr["PPwd"].ToString();
        //            st.SectionID = int.Parse(dr["SectionID"].ToString());
        //            //st.Section = dr["SectionName"].ToString();
        //            st.SMSmobileNo = dr["SMSmobileNo"].ToString();
        //            string Pincode = dr["CorsPincode"].ToString();
        //            if(Pincode!="")
        //            {
        //                st.CorsPincode = int.Parse(dr["CorsPincode"].ToString());

        //            }
        //            else
        //            {
        //                st.CorsPincode = 0;
        //            }

        //FindStudentforIDCardByID
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        objsqlconn.Close();
        //    }


        //    //var result1 = db.tblSchoolDetails.SqlQuery("Select * from tblSchoolDetails where SchoolID='"+SchoolID+"'");
        //    ////var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
        //    //foreach (var x in result1)
        //    //{
        //    //    tblSchoolDetail p = new tblSchoolDetail();
        //    //    p.ID = x.ID;
        //    //    p.Address = x.Address;
        //    //    p.Board = x.Board;
        //    //    p.CityID = x.CityID;
        //    //    p.CountryID = x.CountryID;
        //    //    p.District = x.District;
        //    //    p.Email = x.Email;
        //    //    p.LogoPic = x.LogoPic;
        //    //    p.Mobile = x.Mobile;
        //    //    p.Phone = x.Phone;
        //    //    p.Pincode = x.Pincode;
        //    //    p.School = x.School;
        //    //    p.State = x.State;
        //    //    st.sd = p;
        //    //    tblUser usr = db.tblUsers.Find(x.PrincipalID);
        //    //    st.principalSign = usr.Signature;
        //    //}
        //    return st;
        //}


        [System.Web.Http.Route("api/StudentApi/GetStatus")]
        [System.Web.Http.HttpPost]
        public tblStatu[] GetStatus()
        {
            List<tblStatu> list = new List<tblStatu>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblStatus.SqlQuery("Select * from tblStatus where stStatus=1 and StudentStatus=1 and isdeleted is null").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblStatu items = new tblStatu();
                    items.Status = a.Status;
                    items.StatusID = a.StatusID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/StudentApi/getAllStudentsforSMS")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentsforSMS(List<string> val)
        {
            int crs = Convert.ToInt32(val[0]);
            int sec = 0;
            if (val[1] != "")
            { sec = Convert.ToInt32(val[1]); }

            string name = val[2].ToString();
            string rollno = val[3].ToString();
            int SchoolID = Convert.ToInt32(val[4]);

            List<Student> list = new List<Student>();

            //var result = db.TBLStudents.SqlQuery("Select * from TBLStudent where ClassID=" + crs + " and SectionID=" + sec).ToList();
            // var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
            var result = (from stu in db.TBLStudents
                          join cls in db.tblCourses on stu.ClassID equals cls.Id
                          join sect in db.tblSections on stu.SectionID equals sect.Id
                          join sch in db.tblSchoolDetails on stu.SchoolID equals sch.ID
                          where stu.IsDeleted == null
                          // join scl in db.tblSchoolDetails on stu.SchoolID equals scl.ID

                          //where stu.ClassID == crs && stu.SectionID==sec
                          select new
                          {
                              Schoolname = sch.School,
                              model = stu,
                              crse = cls.CourseName,
                              section = sect.SectionName,
                              //school = scl.School
                          });
            if (SchoolID != 0)
            {
                result = result.Where(s => s.model.SchoolID == SchoolID);
            }

            if (crs != -1 && crs != 0)
            {
                result = result.Where(s => s.model.ClassID == crs);
            }
            if (sec != -1 && sec != 0)
            {
                result = result.Where(s => s.model.SectionID == sec);
            }

            if (name != "")
            {
                result = result.Where(s =>
                                   (s.model.FirstName.Contains(name) || (s.model.MiddleName.Contains(name)) || (s.model.LastName.Contains(name))));
            }
            if (rollno != "")
            {
                result = result.Where(s =>
                                   s.model.RollNo.Contains(rollno));
            }
            //if (crs != -1 && sec != -1 && sec != 0 && name == "" && rollno == "")
            //{
            //    result = result.Where(s => s.model.ClassID == crs
            //                       && s.model.SectionID == sec);
            //}
            //else if (crs != -1 && sec != -1 && sec != 0 && name != "")
            //{
            //    result = result.Where(s => s.model.ClassID == crs
            //                       && s.model.SectionID == sec && (
            //                       (s.model.FirstName.Contains(name) || (s.model.MiddleName.Contains(name)) || (s.model.LastName.Contains(name)))));
            //}
            //else if (crs != -1 && sec != -1 && sec != 0 && name != "" && rollno != "")
            //{
            //    result = result.Where(s => s.model.ClassID == crs
            //                       && s.model.SectionID == sec && (
            //                       (s.model.FirstName.Contains(name) || (s.model.MiddleName.Contains(name)) || (s.model.LastName.Contains(name)))) && s.model.RollNo == rollno);
            //}
            //else if (crs == -1 && sec == 0 && name != "" && rollno != "")
            //{
            //    result = result.Where(s => (
            //                       (s.model.FirstName.Contains(name) || (s.model.MiddleName.Contains(name)) || (s.model.LastName.Contains(name)))) && s.model.RollNo == rollno);
            //}
            //else if (crs == -1 && sec == 0 && name != "" && rollno == "")
            //{
            //    result = result.Where(s =>
            //                       s.model.FirstName.Contains(name) || s.model.MiddleName.Contains(name) || s.model.LastName.Contains(name));
            //}
            foreach (var x in result)
            {
                Student usr = new Student();
                usr.ID = x.model.ID;
                usr.RegNo = x.model.RegNo;
                //usr.School = x.school;
                usr.FirstName = x.model.FirstName + " " + x.model.MiddleName + " " + x.model.LastName;
                usr.Section = x.section;
                usr.Class = x.crse;
                usr.RollNo = x.model.RollNo;
                //  usr.PicPath = dr["PicPath"].ToString();
                usr.SStatus = x.model.Status.ToString();
                usr.SMSmobileNo = x.model.SMSmobileNo;


                if (x.model.PicPath == "" || x.model.PicPath == null)
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = x.model.PicPath;
                }
                //  usr.Status = dr["Status"].ToString();
                if (x.model.Status.ToString() == "True")
                {

                    usr.SStatus = "Activate";
                    usr.Extra10 = "btn btn-block btn-success btn-sm";

                }
                else
                {
                    usr.SStatus = "DeActivate";
                    usr.Extra10 = "btn btn-block btn-danger btn-sm";

                }
                usr.School = x.Schoolname;
                usr.Mmail = x.model.Mmail;
                usr.Fmail = x.model.Fmail;
                list.Add(usr);
            }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/StudentApi/checksmsService")]
        [System.Web.Http.HttpPost]
        public string checksmsService(List<string> avi)
        {
            sqlHelper obj = new sqlHelper();
            int SmsServiceCount = 0;
            string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + avi[0] + "' and  active=1");
            if (Checksmsservice != null)
            {
                SmsServiceCount++;

            }
            return SmsServiceCount.ToString();

        }



        [System.Web.Http.Route("api/StudentApi/sendSmstoParent")]
        [System.Web.Http.HttpPost]
        public string sendSmstoParent(Student[] employye)
        {

            int Count = 0;
            int TotalCount = employye.Count(); ;
            int switchnow = 0;
            foreach (var emp in employye)
            {
                if (emp.ID != 0)
                {
                    //Count = emp.StudentCount;

                    if (TotalCount == emp.StudentCount)
                    {
                        switchnow = 1;
                    }



                }
            }

            if (switchnow == 1)
            {
                sqlHelper obj = new sqlHelper();
                foreach (var emp in employye)
                {




                    if (emp.ID != 0)
                    {

                        int SchoolID = Convert.ToInt32(emp.SchoolID);
                        string phoneno = obj.ExecuteScaler("select SMSmobileNo from TBLStudent where ID='" + emp.ID + "' and  SchoolID='" + emp.SchoolID + "' and IsDeleted is null ");
                        string SchoolName = db.tblSchoolDetails.SingleOrDefault(sn => sn.ID == SchoolID).School;
                        string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");
                        string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");


                        string authKey = GetauthKey;
                        //Multiple mobiles numbers separated by comma
                        string mobileNumber = phoneno;
                        //Sender ID,While using route4 sender id should be 6 characters long.
                        string senderId = GetsenderId;
                        //Your message to send, Add URL encoding here.
                        StringBuilder st = new StringBuilder();
                        st.AppendLine("Hi Sir/mam,");
                        st.AppendLine(emp.Awards);
                        st.AppendLine("");
                        st.AppendLine("Regards");
                        st.AppendLine(SchoolName);
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




            return "Sms Send Successfully";
            //return  Count.ToString();
        }

        [System.Web.Http.Route("api/StudentApi/ChangeStPwd")]
        [System.Web.Http.HttpPost]
        public Student ChangeStPwd(List<string> val)
        {
            Student std = new Models.Student();
            int id = Convert.ToInt32(val[0]);
            string pwd = val[1];
            string oldpwd = val[2];
            std.SPwd = "";
            std.Extra10 = "0";
            try
            {
                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update TBLStudent set SPwd = '" + pwd + "' where ID=" + id);
                if (noOfRowUpdated > 0)
                {
                    std.Extra10 = noOfRowUpdated.ToString();
                    std.SPwd = pwd;
                    std.FirstName = "Password successfully updated";
                }
                return std;
            }
            catch (Exception e)
            {
                std.FirstName = "Error!!";
                return std;
                throw e;
            }
        }

        [System.Web.Http.Route("api/StudentApi/ApplyForLeaveAPP")]
        [System.Web.Http.HttpPost]
        public StudentAPP ApplyForLeaveAPP([FromBody] Student stud)
        {
            //sqlHelper obj = new sqlHelper();
            StudentAPP std = new Models.StudentAPP();
            //List<EmployeeEm> list = new List<EmployeeEm>();
            // var abc = stud.SingleOrDefault();




            try
            {
                if (string.IsNullOrEmpty(stud.SchoolID)      /*"".Equals(stud.MDOB)*/   )
                {

                    std.status = false;
                    std.message = "Please Enter SchoolID";
                    return std;
                }
                else if (string.IsNullOrEmpty(stud.ResonForLeave))
                {
                    std.status = false;
                    std.message = "Please Enter Reason for leave";
                    return std;
                }
                else if (stud.StudentID == 0 || stud.StudentID.Equals(null) || "".Equals(stud.StudentID))
                {
                    std.status = false;
                    std.message = "Please Enter Student ID ";
                    return std;
                }
                else if (stud.LeaveFrom.Equals(null) || "".Equals(stud.LeaveFrom))
                {
                    std.status = false;
                    std.message = "Please Enter Date from";
                    return std;
                }
                else if (stud.LeaveTo.Equals(null) || "".Equals(stud.LeaveTo))
                {
                    std.status = false;
                    std.message = "Please Enter Date to";
                    return std;
                }
                else
                {
                    int avi = 0;
                    int schoolid = Convert.ToInt32(stud.SchoolID);
                    //string FindSchoolID = obj.ExecuteScaler("select SchoolID from TBLStudent where ID='" + abc.ID + "' and IsDeleted is null");

                    var cteacher = (from ct in db.tblClassTeacherAllocations
                                    from s in db.TBLStudents
                                    where ct.ClassID == s.ClassID && ct.SectionID == s.SectionID && ct.SchoolID == s.SchoolID && s.ID == stud.StudentID && ct.SchoolID == schoolid && s.SchoolID == schoolid
                                    select new
                                    {
                                        ct.intEmpID,
                                        s.SchoolID

                                    }).SingleOrDefault();
                    if (cteacher != null)
                    {
                        avi++;
                        tblStudentLeaveApply lv = new tblStudentLeaveApply();
                        lv.StudentID = Convert.ToInt32(stud.StudentID);
                        lv.datefrom = Convert.ToDateTime(stud.LeaveFrom);
                        lv.dateto = Convert.ToDateTime(stud.LeaveTo);
                        lv.Reason = stud.ResonForLeave;
                        lv.RequestDt = DateTime.Now;
                        lv.Status = 4;
                        lv.ApprovedBy = cteacher.intEmpID;
                        lv.SchoolID = Convert.ToInt32(stud.SchoolID);
                        db.tblStudentLeaveApplies.Add(lv);
                        db.SaveChanges();

                    }
                    if (avi == 0)
                    {

                        std.status = false;
                        std.message = "No Record Found";
                    }
                    else if (avi > 0)
                    {
                        std.status = true;
                        std.message = "Sucess";
                    }
                    return std;

                }

            }
            catch
            {
                std.status = false;
                std.message = "Something Went Wrong";
                return std;

            }
        }




        [System.Web.Http.Route("api/StudentApi/ApplyForLeave")]
        [System.Web.Http.HttpPost]
        public Student ApplyForLeave([FromBody] Student[] stud)
        {
            sqlHelper obj = new sqlHelper();
            Student std = new Models.Student();
            List<EmployeeEm> list = new List<EmployeeEm>();
            var abc = stud.SingleOrDefault();
            int schoolid = Convert.ToInt32(abc.SchoolID);


            std.RegNo = "0";
            try
            {
                //string FindSchoolID = obj.ExecuteScaler("select SchoolID from TBLStudent where ID='" + abc.ID + "' and IsDeleted is null");

                var cteacher = (from ct in db.tblClassTeacherAllocations
                                from s in db.TBLStudents
                                where ct.ClassID == s.ClassID && ct.SectionID == s.SectionID && ct.SchoolID == s.SchoolID && s.ID == abc.ID && ct.SchoolID == schoolid && s.SchoolID == schoolid
                                select new
                                {
                                    ct.intEmpID,
                                    s.SchoolID

                                }).SingleOrDefault();

                tblStudentLeaveApply lv = new tblStudentLeaveApply();
                lv.StudentID = abc.ID;
                lv.datefrom = Convert.ToDateTime(abc.DOB);
                lv.dateto = Convert.ToDateTime(abc.MDOB);
                lv.Reason = abc.PermanentAddress;
                lv.RequestDt = DateTime.Now;
                lv.Status = 4;
                lv.ApprovedBy = cteacher.intEmpID;
                lv.SchoolID = Convert.ToInt32(abc.SchoolID);
                db.tblStudentLeaveApplies.Add(lv);
                db.SaveChanges();
                std.FirstName = "Leave Application submitted successfully";
                std.RegNo = Convert.ToString(lv.ID);
                return std;
            }
            catch (Exception e)
            {
                std.FirstName = "Error!!";
                return std;
                throw e;
            }
        }

        [System.Web.Http.Route("api/StudentApi/getStudentsLeaveDetbyIDAPP")]
        [System.Web.Http.HttpPost]
        public StudentLeaveApplyAPP getStudentsLeaveDetbyIDAPP(leaveDetails param)
        {
            //sqlHelper obj = new sqlHelper();
            int a = int.Parse(param.StudentID);//.ToString();
            int SchoolID = Convert.ToInt32(param.SchoolID);
            int count = 0;
            int avi = 0;
            StudentLeaveApplyAPP obj = new StudentLeaveApplyAPP();
            List<StudentLeaveApply1> list = new List<Models.StudentLeaveApply1>();
            try
            {

                //string FindSchoolID = obj.ExecuteScaler("select SchoolID from TBLStudent where ID='" + a + "' and IsDeleted is null");
                //int SchoolID = Convert.ToInt32(FindSchoolID);
                var result = (from lv in db.tblStudentLeaveApplies
                              join stats in db.tblStatus on lv.Status equals stats.StatusID
                              where lv.StudentID == a && lv.IsDeleted == null && lv.SchoolID == SchoolID
                              select new
                              {
                                  model = lv,
                                  statusNm = stats.Status
                              }).ToList();

                foreach (var m in result)
                {
                    avi++;
                    count++;
                    StudentLeaveApply1 st = new Models.StudentLeaveApply1();
                    st.leave = new tblStudentLeaveApply1();
                    st.leaveID = count;
                    st.leave.ID = m.model.ID;
                    st.leave.StudentID = m.model.StudentID;
                    //st.leave.datefrom = ((DateTime)m.model.datefrom).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    //st.leave.dateto = ((DateTime)m.model.dateto).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    //st.leave.RequestDt = ((DateTime)m.model.RequestDt).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    st.leave.datefrom = (m.model.datefrom).ToString("dd MMMM yyyy");
                    st.leave.dateto = (m.model.dateto).ToString("dd MMMM yyyy");
                    st.leave.RequestDt = Convert.ToDateTime(m.model.RequestDt).ToString("dd MMMM yyyy");

                    if (m.model.ApproveDt != null)
                    {
                        //st.leave.ApproveDt = ((DateTime)m.model.ApproveDt).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        st.leave.ApproveDt = Convert.ToDateTime(m.model.ApproveDt).ToString("dd MMMM yyyy");
                    }
                    else
                    {
                        st.leave.ApproveDt = "";

                    }

                    st.leave.ApprovedBy = m.model.ApprovedBy;
                    st.leave.Status = m.model.Status;

                    st.leave.Reason = m.model.Reason;
                    st.leave.SchoolID = m.model.SchoolID;
                    st.statusNm = m.statusNm;

                    st.datefrom = ((DateTime)m.model.datefrom).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    st.dateto = ((DateTime)m.model.dateto).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    st.RequestDt = ((DateTime)m.model.RequestDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (st.statusNm == "Pending")
                    {
                        st.color = "#f39c12";
                        st.leave.color = "#f39c12";
                    }
                    else if (st.statusNm == "Approved")
                    {
                        st.color = "#00a65a";
                        st.leave.color = "#00a65a";
                    }
                    else
                    {
                        st.color = "#dd4b39";
                        st.leave.color = "#dd4b39";
                    }
                    list.Add(st);

                }
                if (avi != 0)
                {
                    obj.status = "200";
                    obj.message = "Sucess";
                    obj.data = list;
                }
                else if (avi == 0)
                {
                    obj.status = "200";
                    obj.message = "No Record Found";
                    obj.data = list;
                }
            }
            catch
            {
                obj.status = "404";
                obj.message = "Something Went Wrong";
            }
            return obj;

        }



        [System.Web.Http.Route("api/StudentApi/getStudentsLeaveDetbyID")]
        [System.Web.Http.HttpPost]
        public StudentLeaveApply[] getStudentsLeaveDetbyID(Parameters param)
        {
            //sqlHelper obj = new sqlHelper();
            int a = int.Parse(param.val);//.ToString();
            int SchoolID = Convert.ToInt32(param.val1);
            int count = 0;

            List<StudentLeaveApply> list = new List<Models.StudentLeaveApply>();
            try
            {
                //string FindSchoolID = obj.ExecuteScaler("select SchoolID from TBLStudent where ID='" + a + "' and IsDeleted is null");
                //int SchoolID = Convert.ToInt32(FindSchoolID);
                var result = (from lv in db.tblStudentLeaveApplies
                              join stats in db.tblStatus on lv.Status equals stats.StatusID
                              where lv.StudentID == a && lv.IsDeleted == null && lv.SchoolID == SchoolID
                              select new
                              {
                                  model = lv,
                                  statusNm = stats.Status
                              }).ToList();

                foreach (var m in result)
                {
                    count++;


                    StudentLeaveApply st = new Models.StudentLeaveApply();
                    st.leaveID = count;
                    st.leave = m.model;
                    st.statusNm = m.statusNm;
                    st.datefrom = ((DateTime)m.model.datefrom).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    st.dateto = ((DateTime)m.model.dateto).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    st.RequestDt = ((DateTime)m.model.RequestDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (st.statusNm == "Pending")
                    {
                        st.color = "#f39c12";
                    }
                    else if (st.statusNm == "Approved")
                    {
                        st.color = "#00a65a";
                    }
                    else
                    {
                        st.color = "#dd4b39";
                    }
                    list.Add(st);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list.ToArray();

        }

        [System.Web.Http.Route("api/StudentApi/DeleteLeave")]
        [System.Web.Http.HttpPost]
        public StudentLeaveApply DeleteLeave(List<string> val)
        {
            int id = Convert.ToInt32(val[0]);
            StudentLeaveApply std = new Models.StudentLeaveApply();

            try
            {
                tblStudentLeaveApply lv = new tblStudentLeaveApply();
                //var employer = new tblStudentLeaveApply { ID = id };
                //db.Entry(employer).State = EntityState.Deleted;
                int idd = Convert.ToInt32(id);
                var aa = db.tblStudentLeaveApplies.SingleOrDefault(x => x.ID == idd);
                aa.IsDeleted = 1;
                aa.Deleted_on = DateTime.Now;
                db.SaveChanges();
                std.statusNm = "Record successfully deleted";
                return std;
            }
            catch (Exception e)
            {
                std.statusNm = "Error!!";
                return std;
                throw e;
            }
        }

        [System.Web.Http.Route("api/StudentApi/CheckClassTeacher")]
        [System.Web.Http.HttpPost]
        public bool CheckClassTeacher(List<string> val)
        {
            int a = Convert.ToInt32(val[0]);

            var result = (from lv in db.tblStudentLeaveApplies
                          join ct in db.tblClassTeacherAllocations on lv.ApprovedBy equals ct.intEmpID
                          where lv.ApprovedBy == a
                          select new
                          {
                              classTeacher = lv.ApprovedBy
                          }).ToList();

            if (result.Any(l => l.classTeacher == a))
            {
                return true;
            }
            //try
            //{
            //    if (db.tblStudentLeaveApplies.Any(l => l.ApprovedBy == a)) return true;

            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            return false;
        }

        [System.Web.Http.Route("api/StudentApi/getStudentsLeaveDet")]
        [System.Web.Http.HttpPost]
        public StudentLeaveApply[] getStudentsLeaveDet(List<string> val)
        {
            int count = 0;
            int empid = Convert.ToInt32(val[0]);
            string check = "";
            int status = 0;
            string f = "", mn = "", ln = "";
            int SchoolID = Convert.ToInt32(val[5]);
            //sqlHelper obj = new sqlHelper();
            if (val.Count > 1)
            {
                check = val[1];
                if (val[1] == "status")
                {
                    status = Convert.ToInt32(val[2]);
                }
                else if (val[1] == "date")
                {

                }
                else
                {
                    f = val[2];
                    mn = val[3];
                    ln = val[4];
                }
            }
            //string SchoolID = obj.ExecuteScaler("select SchoolID from tblEmployee where UserID='" + em.Extra5 + "' and IsDeleted is null");
            List<StudentLeaveApply> list = new List<Models.StudentLeaveApply>();
            try
            {
                //string FindSchoolID = obj.ExecuteScaler("select SchoolID from tblEmployee where Id='" + empid + "' and IsDeleted is null");
                //int SchoolID = Convert.ToInt32(FindSchoolID);

                var result = (from lv in db.tblStudentLeaveApplies
                              join stats in db.tblStatus on lv.Status equals stats.StatusID
                              join stud in db.TBLStudents on lv.StudentID equals stud.ID
                              where lv.ApprovedBy == empid && lv.IsDeleted == null && lv.SchoolID == SchoolID
                              select new
                              {
                                  model = lv,
                                  Fname = stud.FirstName,
                                  Mname = stud.MiddleName,
                                  Lname = stud.LastName,
                                  rollNo = stud.RollNo,
                                  statusNm = stats.Status,
                                  stuUserid = stud.SUserID
                              }).ToList();

                if (check != "")
                {
                    if (check == "status")
                    {
                        result = result.Where(s => s.model.Status == status).ToList();
                    }
                    else if (check == "rollno")
                    {
                        result = result.Where(s => s.rollNo == val[2]).ToList();
                    }
                    else if (check == "name")
                    {
                        f = f.ToUpper();
                        mn = mn.ToUpper();
                        ln = ln.ToUpper();
                        if (f != "")
                        {
                            result = result.Where(s => s.Fname.ToUpper().Contains(f)).ToList();
                        }
                        if (mn != "")
                        {
                            result = result.Where(s => s.Mname.ToUpper().Contains(mn)).ToList();
                        }
                        if (ln != "")
                        {
                            result = result.Where(s => s.Lname.ToUpper().Contains(ln)).ToList();
                        }
                    }
                    else
                    {
                        DateTime fromDate = DateTime.Parse(val[2]);
                        DateTime toDate = DateTime.Parse(val[3]);
                        result = result.Where(s => s.model.datefrom >= fromDate && s.model.dateto <= toDate).ToList();
                    }
                }

                foreach (var m in result)
                {
                    count++;
                    StudentLeaveApply st = new Models.StudentLeaveApply();
                    st.leaveID = count;
                    st.leave = m.model;
                    st.leave.Status = m.model.Status;
                    st.statusNm = m.statusNm;
                    st.FirstName = m.Fname + " " + m.Lname;
                    st.MidName = m.Mname;
                    st.LastName = m.Lname;
                    st.RollNo = m.rollNo;
                    st.StudentUserID = m.stuUserid;
                    st.datefrom = ((DateTime)m.model.datefrom).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    st.dateto = ((DateTime)m.model.dateto).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    st.RequestDt = ((DateTime)m.model.RequestDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (st.statusNm.ToLower() == "pending")
                    {
                        st.style = "btn btn-block btn-warning btn-sm";
                    }
                    else if (st.statusNm.ToLower() == "approved")
                    {
                        st.style = "btn btn-block btn-success btn-sm";
                    }
                    else
                    {
                        st.style = "btn btn-block btn-danger btn-sm";
                    }
                    list.Add(st);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list.ToArray();

        }
        //avi
        [System.Web.Http.Route("api/StudentApi/Approve_Reject_Leave")]
        [System.Web.Http.HttpPost]
        public StudentLeaveApply Approve_Reject_Leave(StudentLeaveApply[] val)
        {
            StudentLeaveApply std = new Models.StudentLeaveApply();
            sqlHelper obj = new sqlHelper();

            string type = "";
            try
            {
                foreach (var v in val)
                {
                    int SchoolID = Convert.ToInt32(v.SchoolID);
                    //string FindSchoolID = obj.ExecuteScaler("SELECT SchoolID  from tblEmployee where Id='" + v.MidName + "' and IsDeleted is null");
                    //int SchoolID = Convert.ToInt32(FindSchoolID);
                    tblStudentLeaveApply lv = new tblStudentLeaveApply();
                    type = v.statusNm.ToLower();
                    var result = db.tblStudentLeaveApplies.SingleOrDefault(b => b.ID == v.leaveID && b.SchoolID == SchoolID && b.IsDeleted == null);
                    result.ApproveDt = DateTime.Now;
                    if (result != null && v.statusNm.ToLower() == "approve")
                    {
                        result.ApprovedBy = Convert.ToInt32(v.MidName);
                        result.Status = 5;
                        db.SaveChanges();
                        //send sms to parents for leave approved 
                        string leavefrom = db.tblStudentLeaveApplies.SingleOrDefault(d => d.ID == v.leaveID).datefrom.ToString("dd/MM/yyyy");
                        string leaveTO = db.tblStudentLeaveApplies.SingleOrDefault(d => d.ID == v.leaveID).dateto.ToString("dd/MM/yyyy");

                        long studentid = db.tblStudentLeaveApplies.SingleOrDefault(l => l.ID == v.leaveID).StudentID;
                        int studentidd = Convert.ToInt32(studentid);
                        string schoolname = db.tblSchoolDetails.SingleOrDefault(s => s.ID == SchoolID).School;

                        string Mmobile = db.TBLStudents.SingleOrDefault(SMS => SMS.ID == studentidd).Mmobile;
                        string FMobile = db.TBLStudents.SingleOrDefault(Fm => Fm.ID == studentidd).FMobile;

                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");
                        if (Checksmsservice != null)
                        {
                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");
                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = Mmobile + "," + FMobile;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Applied leave from" + " " + leavefrom + " To " + leaveTO + " " + "has been approved");
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(schoolname);
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
                    else if (result != null && v.statusNm.ToLower() == "reject")
                    {
                        result.ApprovedBy = Convert.ToInt32(v.MidName);
                        result.Status = 6;
                        db.SaveChanges();
                        //send sms to parents for leave Rejected

                        string leavefrom = db.tblStudentLeaveApplies.SingleOrDefault(d => d.ID == v.leaveID).datefrom.ToString("dd/MM/yyyy");
                        string leaveTO = db.tblStudentLeaveApplies.SingleOrDefault(d => d.ID == v.leaveID).dateto.ToString("dd/MM/yyyy");

                        long studentid = db.tblStudentLeaveApplies.SingleOrDefault(l => l.ID == v.leaveID).StudentID;
                        int studentidd = Convert.ToInt32(studentid);
                        string schoolname = db.tblSchoolDetails.SingleOrDefault(s => s.ID == SchoolID).School;

                        string Mmobile = db.TBLStudents.SingleOrDefault(SMS => SMS.ID == studentidd).Mmobile;
                        string FMobile = db.TBLStudents.SingleOrDefault(Fm => Fm.ID == studentidd).FMobile;

                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");
                        if (Checksmsservice != null)
                        {
                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");
                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolID + "' and  active=1");
                            //string authKey = "12920AvJSHCkt5d4cfe2c";
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = Mmobile + "," + FMobile;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Applied leave from" + " " + leavefrom + "To" + leaveTO + " " + "has been rejected");
                            st.AppendLine("");
                            st.AppendLine("Regards");
                            st.AppendLine(schoolname);
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
                if (type == "approve")
                {
                    std.statusNm = "Leaves approved successfully";
                }
                else
                {
                    std.statusNm = "Leaves rejected successfully";
                }
                return std;
            }
            catch (Exception e)
            {
                std.statusNm = "Error!!";
                return std;
                throw e;
            }
        }
        [System.Web.Http.Route("api/StudentApi/GetLeaveStatus")]
        [System.Web.Http.HttpPost]
        public tblStatu[] GetLeaveStatus()
        {
            List<tblStatu> list = new List<tblStatu>();

            try
            {
                //string query1 = @"select  Cat_id,subcat_id,Name from  Sub_Category   where Cat_id={0} and  status={1} order by Name ";
                //var data1 = db.Database.SqlQuery<MultipleSubcategory>(query1, catid, "1").ToList();
                var result = db.tblStatus.SqlQuery("Select * from tblStatus where LeaveStatus=" + 1 + "").ToList();
                //var result = db.TBLEnclosureMasters.Where(i => i.Status == 0);
                foreach (var a in result)
                {
                    tblStatu items = new tblStatu();
                    items.Status = a.Status;
                    items.StatusID = a.StatusID;
                    list.Add(items);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getStudentForAttendenceClassWise")]
        [System.Web.Http.HttpPost]
        public Student[] getStudentForAttendenceClassWise(EmployeeEm em)
        {
            int rno = 0;
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();


            DataTable dt = obj.getDataTable(@"select * from TBLStudent s
                                               left outer join tblAcademicYear ac on ac.ID =s.AcademicYear
                                               left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
                                                where s.Status = 3 and ac.CurrActive=1 and s.IsDeleted is null and ac.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and ca.intEmpID ='" + em.Employeecode + "' order by FirstName ");

            // DataTable dt = obj.getDataTable(@"select * from TBLStudent s
            //                        left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
            //                         where s.Status = 3 and s.IsDeleted is null and s.SchoolID='"+em.SchoolID+"' and ca.intEmpID =" + em.Employeecode);
            foreach (DataRow dr in dt.Rows)
            {
                rno++;
                Student usr = new Student();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["RegNo"].ToString();
                usr.SUserID = dr["SUserID"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                //usr.RollNo = dr["RollNo"].ToString();
                usr.RollNo = rno.ToString();
                usr.emailID = em.Employeecode;
                usr.Class = dr["ClassID"].ToString();
                usr.Section = dr["SectionID"].ToString();
                //  usr.Gender = dr["GenderName"].ToString();

                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }

                list.Add(usr);
            }
            return list.ToArray();
        }



        [System.Web.Http.Route("api/StudentApi/getStudentLeavesByDate")]
        [System.Web.Http.HttpPost]
        public leaveDetails[] getStudentLeavesByDate(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<leaveDetails> list = new List<leaveDetails>();



            DataTable dt = obj.getDataTable(@"select la.* from tblStudentLeaveApply la
                               left outer join tblStatus s on s.StatusID = la.Status
                             where la.SchoolID='" + employye.SchoolID + "' and la.IsDeleted is null and la.datefrom <= convert(date, '" + employye.Employeecode + "', 101) and la.dateto >= convert(date, '" + employye.Employeecode + "', 101)  and s.Status = 'Approved'");
            foreach (DataRow dr in dt.Rows)
            {
                leaveDetails usr = new leaveDetails();
                usr.Id = dr["StudentID"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/TakeStudenceAttendence")]
        [System.Web.Http.HttpPost]
        public string TakeStudenceAttendence(StudentAttendenceDetails1[] attendence)
        {

            string save = StudnetAttendenceDetailss.saveStudentAttendence1(attendence);
            return save;
        }
        public class StudnetAttendenceDetailss
        {

            public static string saveStudentAttendence1(StudentAttendenceDetails1[] attendence)
            {
                string AttendenceTime = DateTime.Now.ToString("HH: mm:ss tt");
                sqlHelper obj = new sqlHelper();
                foreach (var emp in attendence)
                {
                    string exits = obj.ExecuteScaler("select StudentId from tblStudentAttendence where StudentId=" + emp.StudentId + " and  AttendenceDate='" + emp.AttendenceDate + "' and SchoolID ='" + emp.SchoolID + "' ");

                    if (exits == emp.StudentId)
                    {
                        string exits2 = obj.ExecuteScaler("select StudentId from tblStudentAttendence where StudentId='" + emp.StudentId + "' and  AttendenceDate='" + emp.AttendenceDate + "' and AttendenceType!='" + emp.AttendenceType + "' ");
                        if (exits2 == emp.StudentId)
                        {
                            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                            SqlConnection con = new SqlConnection(constr);
                            con.Open();
                            string query = @"update tblStudentAttendence set AttendenceType='" + emp.AttendenceType + "',AttendenceDate='"
                                + emp.AttendenceDate + "',TeacherId='" + emp.EmployeeId + "',ClassId='" + emp.ClassID + "',SectionId='" + emp.SectionId
                                + "',IsBiometric='" + false + "'  where StudentId=" + emp.StudentId + " and AttendenceDate='" + emp.AttendenceDate + "' and SchoolID='" + emp.SchoolID + "'";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //send sms to parents for informing attendence
                            //string FMmob = obj.ExecuteScaler("select FMobile from TBLStudent where ID='" + emp.StudentId + "'");
                            //string MMmob = obj.ExecuteScaler("select Mmobile from TBLStudent where ID='" + emp.StudentId + "'");
                            string SMSmobileNo = obj.ExecuteScaler("select SMSmobileNo from TBLStudent where ID='" + emp.StudentId + "'");
                            string Studentname = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from TBLStudent where ID='" + emp.StudentId + "'");
                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + emp.SchoolID + "'");

                            string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                            if (Checksmsservice != null)
                            {
                                string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                                string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                                //string authKey = "12920AvJSHCkt5d4cfe2c";
                                string authKey = GetauthKey;
                                //Multiple mobiles numbers separated by comma
                                string mobileNumber = SMSmobileNo;
                                //Sender ID,While using route4 sender id should be 6 characters long.
                                //string senderId = "ZEUSAD";
                                string senderId = GetsenderId;
                                //Your message to send, Add URL encoding here.
                                StringBuilder st = new StringBuilder();
                                st.AppendLine("Hi Sir/mam,");
                                st.AppendLine("Student Name :" + Studentname);
                                st.AppendLine("Student Attendence Status :" + emp.AttendenceType);
                                st.AppendLine("Attendence Date :" + emp.AttendenceDate);
                                st.AppendLine("Attendence Time :" + AttendenceTime);
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
                            else
                            {

                            }


                        }
                        //else
                        //{
                        //    //string FMmob = obj.ExecuteScaler("select FMobile from TBLStudent where ID='" + emp.StudentId + "'");
                        //    //string MMmob = obj.ExecuteScaler("select Mmobile from TBLStudent where ID='" + emp.StudentId + "'");
                        //    string SMSmobileNo = obj.ExecuteScaler("select SMSmobileNo from TBLStudent where ID='" + emp.StudentId + "'");
                        //    string Studentname = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from TBLStudent where ID='" + emp.StudentId + "'");
                        //    string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + emp.SchoolID + "'");
                        //    string authKey = "12920AvJSHCkt5d4cfe2c";
                        //    //Multiple mobiles numbers separated by comma
                        //    string mobileNumber = SMSmobileNo;
                        //    //Sender ID,While using route4 sender id should be 6 characters long.
                        //    string senderId = "ZEUSAD";
                        //    //Your message to send, Add URL encoding here.
                        //    StringBuilder st = new StringBuilder();
                        //    st.AppendLine("Hi Sir/mam,");
                        //    st.AppendLine("Student Name :" + Studentname);
                        //    st.AppendLine("Student Attendence Status :" + emp.AttendenceType);
                        //    st.AppendLine("Attendence Date :" + emp.AttendenceDate);
                        //    st.AppendLine("Attendence Time :" + AttendenceTime);
                        //    st.AppendLine("");
                        //    st.AppendLine("Regards");
                        //    st.AppendLine(Schoolname);
                        //    string message = HttpUtility.UrlEncode(st.ToString());

                        //    //Prepare you post parameters
                        //    StringBuilder sbPostData = new StringBuilder();
                        //    sbPostData.AppendFormat("authkey={0}", authKey);
                        //    sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
                        //    sbPostData.AppendFormat("&message={0}", message);
                        //    sbPostData.AppendFormat("&sender={0}", senderId);
                        //    sbPostData.AppendFormat("&route={0}", 4);

                        //    try
                        //    {
                        //        //Call Send SMS API
                        //        string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                        //        //Create HTTPWebrequest
                        //        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                        //        //Prepare and Add URL Encoded data
                        //        UTF8Encoding encoding = new UTF8Encoding();
                        //        byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                        //        //Specify post method
                        //        httpWReq.Method = "POST";
                        //        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        //        httpWReq.ContentLength = data1.Length;
                        //        using (Stream stream = httpWReq.GetRequestStream())
                        //        {
                        //            stream.Write(data1, 0, data1.Length);
                        //        }
                        //        //Get the response
                        //        HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                        //        StreamReader reader = new StreamReader(response1.GetResponseStream());
                        //        string responseString = reader.ReadToEnd();

                        //        //Close the response
                        //        reader.Close();
                        //        response1.Close();
                        //    }
                        //    catch (SystemException ex)
                        //    {
                        //        ex.Message.ToString();
                        //    }
                        //}





                    }
                    //string[] cols1 = { "AttendenceType", "AttendenceDate", "TeacherId", "ClassId", "SectionId" };
                    //object[] vals1 = { emp.AttendenceType, emp.AttendenceDate, emp.EmployeeId, emp.ClassID, emp.SectionId };
                    //obj.updateValIntoTable("tblStudentAttendence", cols1, vals1, "StudentId", emp.StudentId);

                    else
                    {
                        string[] cols = { "StudentId", "AttendenceType", "AttendenceDate", "TeacherId", "ClassId", "SectionId", "SchoolID", "IsBiometric" };
                        object[] vals = { emp.StudentId, emp.AttendenceType, emp.AttendenceDate, emp.EmployeeId, emp.ClassID, emp.SectionId, emp.SchoolID, false };
                        obj.insertValIntoTable("tblStudentAttendence", cols, vals);

                        //send sms to parents for informing attendence
                        //string FMmob = obj.ExecuteScaler("select FMobile from TBLStudent where ID='" + emp.StudentId + "'");
                        //string MMmob = obj.ExecuteScaler("select Mmobile from TBLStudent where ID='" + emp.StudentId + "'");
                        string SMSmobileNo = obj.ExecuteScaler("select SMSmobileNo from TBLStudent where ID='" + emp.StudentId + "'");

                        string Studentname = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from TBLStudent where ID='" + emp.StudentId + "'");
                        string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + emp.SchoolID + "'");

                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                        if (Checksmsservice != null)
                        {
                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");
                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + emp.SchoolID + "' and  active=1");

                            //string authKey = "12920AvJSHCkt5d4cfe2c";
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = SMSmobileNo;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            //string senderId = "ZEUSAD";
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Student Name :" + Studentname);
                            st.AppendLine("Student attendence status :" + emp.AttendenceType);
                            st.AppendLine("Attendence Date :" + emp.AttendenceDate);
                            st.AppendLine("Attendence Time :" + AttendenceTime);
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
                return "Attendence has been Taken Successfully Thank u!";


            }

        }
        public class StudentAttendenceDetails1
        {
            public string Id { get; set; }
            public string ClassID { get; set; }
            public string SectionId { get; set; }
            public string StudentId { get; set; }

            public string AttendenceDate { get; set; }
            public string AttendenceType { get; set; }
            public string EmployeeId { get; set; }
            public string LeaveType { get; set; }
            public string SchoolID { get; set; }
            public string School { get; set; }
            public string InTime { get; set; }
            public string OutTime { get; set; }
        }
        [System.Web.Http.Route("api/StudentApi/getStudentAttendenceCurrentDate1")]
        [System.Web.Http.HttpPost]
        public StudentAttendenceDetails[] getEmployyeAttendenceCurrentDate1(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<StudentAttendenceDetails> list = new List<StudentAttendenceDetails>();
            DataTable dt = obj.getDataTable(@"select * from tblStudentAttendence where SchoolID='" + employye.SchoolID + "' and  AttendenceDate = convert(date, '" + employye.Employeecode + "', 101)  and TeacherId=" + employye.DesigId);
            foreach (DataRow dr in dt.Rows)
            {
                StudentAttendenceDetails usr = new StudentAttendenceDetails();
                usr.Idd = Convert.ToInt32(dr["StudentId"]);
                usr.StudentId = dr["StudentId"].ToString();
                usr.ClassID = dr["ClassId"].ToString();
                usr.SectionId = dr["SectionId"].ToString();


                usr.AttendenceRealDate = Convert.ToDateTime(dr["AttendenceDate"]);
                usr.AttendenceType = dr["AttendenceType"].ToString();


                if (dr["AttendenceType"].ToString() == "Present")
                {
                    usr.AttendenceType = "Present";
                }

                else if (dr["AttendenceType"].ToString() == "Absent" /*&& Convert.ToBoolean(dr1["IsBiometric"]) == true*/)
                {
                    //Leave 

                    var checkleave = db.tblStudentLeaveApplies.Where(lv => lv.StudentID == usr.Idd && lv.datefrom <= usr.AttendenceRealDate && lv.dateto >= usr.AttendenceRealDate && lv.IsDeleted == null && lv.Status == 5 && lv.IsDeleted == null).FirstOrDefault();
                    if (checkleave != null)
                    {
                        usr.AttendenceType = "Leave";

                    }
                    //Holiday
                    else
                    {
                        var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr.AttendenceRealDate && Hl.DateTo >= usr.AttendenceRealDate && Hl.SchoolID == employye.SchoolID && Hl.IsDeleted == null).FirstOrDefault();
                        if (checkHoliday != null)
                        {
                            usr.AttendenceType = "Holiday";

                        }
                        else
                        {
                            usr.AttendenceType = "Absent";
                        }
                    }


                }

                //  usr.LeaveType = dr["LeaveType"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getStudentAttendenceCurrentDate")]
        [System.Web.Http.HttpPost]
        public StudentAttendenceDetails[] getEmployyeAttendenceCurrentDate(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<StudentAttendenceDetails> list = new List<StudentAttendenceDetails>();
            DataTable dt = obj.getDataTable(@"select * from tblStudentAttendence where SchoolID='" + employye.SchoolID + "' and  AttendenceDate = convert(date, '" + employye.Employeecode + "', 101)  and TeacherId=" + employye.DesigId);
            foreach (DataRow dr in dt.Rows)
            {
                StudentAttendenceDetails usr = new StudentAttendenceDetails();
                usr.StudentId = dr["StudentId"].ToString();
                usr.ClassID = dr["ClassId"].ToString();
                usr.SectionId = dr["SectionId"].ToString();
                usr.AttendenceType = dr["AttendenceType"].ToString();
                //  usr.LeaveType = dr["LeaveType"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/searchStudentForAttendence")]
        [System.Web.Http.HttpPost]
        public Student[] searchStudentForAttendence(EmployeeEm employye)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();

            string[] cols = { "@regNo", "@studentName", "@attendenceDate", "@empId", "@SchoolID" };
            object[] vals = { employye.Employeecode, employye.FName, employye.JoiningDate, employye.Id, employye.SchoolID };

            DataTable dt = obj.sp_GetDataTable("sp_searchStudentForAttendence", cols, vals);
            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.ID = int.Parse(dr["StudentId"].ToString());
                usr.SUserID = dr["SUserID"].ToString();
                usr.RegNo = dr["RegNo"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.RollNo = dr["RollNo"].ToString();
                //usr.emailID = em.Employeecode;
                usr.Class = dr["ClassID"].ToString();
                usr.Section = dr["SectionID"].ToString();
                //  usr.Gender = dr["GenderName"].ToString();

                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }
                usr.SchoolID = dr["SchoolID"].ToString();

                list.Add(usr);
            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/getAllStudentNameForThisclass1")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentNameForThisclass1(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {

                dt = obj.getDataTable(@" select * from TBLStudent s
                                       left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
                                       left outer join tblAcademicYear ac on ac.ID=s.AcademicYear 
                                       where s.Status = 3 and ac.CurrActive=1 and s.IsDeleted is null and ac.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and ca.SchoolID='" + em.SchoolID + "' and ca.intEmpID ='" + em.DesigId + "' order by s.FirstName ");

                //dt = obj.getDataTable(@"select * from TBLStudent s
                //                        left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
                //                         where s.Status = 3 and ca.intEmpID =" + em.DesigId);
            }
            else
            {

                dt = obj.getDataTable(@"select * from TBLStudent s
                                        left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
                                        left outer join tblAcademicYear ac on ac.ID=s.AcademicYear 
                                         where s.Status = 3 and ac.CurrActive=1 and s.IsDeleted is null and ac.IsDeleted is null and  s.SchoolID='" + em.SchoolID + "' and ca.SchoolID='" + em.SchoolID + "' and ca.EmpID =" + em.DesigId + " and s.FirstName+s.MiddleName+s.LastName like '%" + em.FName + "%' order by s.FirstName");
            }
            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["RegNo"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.RollNo = dr["RollNo"].ToString();
                usr.emailID = em.Employeecode;
                usr.Class = dr["ClassID"].ToString();
                usr.Section = dr["SectionID"].ToString();
                //  usr.Gender = dr["GenderName"].ToString();

                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }

                DataTable dt1 = obj.getDataTable(@"select AttendenceDate,cast(Day(AttendenceDate) as varchar(50)) CurrentDay,cast (MONTH(AttendenceDate) as varchar(50))CurrentMonth,
                                                   cast (YEAR(AttendenceDate) as varchar (50))CurrentYear ,* from tblStudentAttendence where SchoolID='" + em.SchoolID + "' and cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + dr["Id"].ToString() + " and TeacherId=" + em.DesigId + " and cast(Year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                    //usr.StudentId = dr["StudentId"].ToString();
                    //usr.ClassID = dr["ClassId"].ToString();


                    usr1.AttendenceRealDate = Convert.ToDateTime(dr1["AttendenceDate"]);
                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceMonth = dr1["CurrentMonth"].ToString();
                    usr1.AttendenceYear = dr1["CurrentYear"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();


                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }


                    else if (dr1["AttendenceType"].ToString() == "Absent")
                    {
                        //Leave 

                        var checkleave = db.tblStudentLeaveApplies.Where(lv => lv.StudentID == usr.ID && lv.datefrom <= usr1.AttendenceRealDate && lv.dateto >= usr1.AttendenceRealDate && lv.IsDeleted == null && lv.Status == 5 && lv.IsDeleted == null).FirstOrDefault();
                        if (checkleave != null)
                        {
                            usr1.AttendenceType = "L";

                        }
                        //Holiday
                        else
                        {
                            var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == em.SchoolID && Hl.IsDeleted == null).FirstOrDefault();
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
            return list.ToArray();
        }
        [System.Web.Http.Route("api/StudentApi/getAllStudentNameForThisclass")]
        [System.Web.Http.HttpPost]
        public Student[] getAllStudentNameForThisclass(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {

                dt = obj.getDataTable(@" select * from TBLStudent s
                                       left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
                                       left outer join tblAcademicYear ac on ac.ID=s.AcademicYear 
                                       where s.Status = 3 and ac.CurrActive=1 and s.IsDeleted is null and ac.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and ca.SchoolID='" + em.SchoolID + "' and ca.intEmpID ='" + em.DesigId + "' order by s.FirstName ");

                //dt = obj.getDataTable(@"select * from TBLStudent s
                //                        left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
                //                         where s.Status = 3 and ca.intEmpID =" + em.DesigId);
            }
            else
            {

                dt = obj.getDataTable(@"select * from TBLStudent s
                                        left outer join tblClassTeacherAllocation ca on ca.ClassID = s.ClassID and ca.SectionID = s.SectionID
                                        left outer join tblAcademicYear ac on ac.ID=s.AcademicYear 
                                         where s.Status = 3 and ac.CurrActive=1 and s.IsDeleted is null and ac.IsDeleted is null and  s.SchoolID='" + em.SchoolID + "' and ca.SchoolID='" + em.SchoolID + "' and ca.EmpID =" + em.DesigId + " and s.FirstName+s.MiddleName+s.LastName like '%" + em.FName + "%' order by s.FirstName");
            }
            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["RegNo"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.RollNo = dr["RollNo"].ToString();
                usr.emailID = em.Employeecode;
                usr.Class = dr["ClassID"].ToString();
                usr.Section = dr["SectionID"].ToString();
                //  usr.Gender = dr["GenderName"].ToString();

                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }

                DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,* from tblStudentAttendence where SchoolID='" + em.SchoolID + "' and  cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + dr["Id"].ToString() + " and TeacherId=" + em.DesigId + " and cast(Year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                    //usr.StudentId = dr["StudentId"].ToString();
                    //usr.ClassID = dr["ClassId"].ToString();

                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();
                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }
                    else if (dr1["AttendenceType"].ToString() == "Absent")
                    {
                        usr1.AttendenceType = "A";
                    }
                    else
                    {
                        usr1.AttendenceType = "L";
                    }
                    usr.attendenceList.Add(usr1);
                }

                list.Add(usr);


            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getStudentAttendenceMontlyByDate")]
        [System.Web.Http.HttpPost]
        public StudentAttendenceDetails[] getStudentAttendenceMontlyByDate(attendenceReporst employye)
        {
            sqlHelper obj = new sqlHelper();
            List<StudentAttendenceDetails> list = new List<StudentAttendenceDetails>();
            DataTable dt = obj.getDataTable(@"select * from tblStudentAttendence where   cast(Month(AttendenceDate) as varchar(50))='" + employye.Month + "' and StudentId=" + employye.studentId + " and TeacherId=" + employye.empID);
            foreach (DataRow dr in dt.Rows)
            {
                StudentAttendenceDetails usr = new StudentAttendenceDetails();
                //usr.StudentId = dr["StudentId"].ToString();
                //usr.ClassID = dr["ClassId"].ToString();
                usr.AttendenceDate = dr["AttendenceDate"].ToString();
                usr.AttendenceType = dr["AttendenceType"].ToString();
                if (dr["AttendenceType"].ToString() == "Present")
                {
                    usr.AttendenceType = "P";
                }
                else if (dr["AttendenceType"].ToString() == "Absent")
                {
                    usr.AttendenceType = "A";
                }
                else
                {
                    usr.AttendenceType = "L";
                }

                //  usr.LeaveType = dr["LeaveType"].ToString();
                list.Add(usr);
            }
            return list.ToArray();
        }
        [System.Web.Http.Route("api/StudentApi/getStudentAttendenceReportByStudentIdAPP1")]
        [System.Web.Http.HttpPost]
        public StudentAPP getStudentAttendenceReportByStudentIdAPP1(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            StudentAPP obj1 = new StudentAPP();
            List<Student> list = new List<Student>();

            StudentAttendenceDetailsAPP AA = new StudentAttendenceDetailsAPP();


            int avi = 0;

            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(em.studentId))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter Student ID";
                }
                else if (em.SchoolID.Equals(null) || "".Equals(em.SchoolID) || em.SchoolID == 0)
                {
                    obj1.status = false;
                    obj1.message = "Please Enter SchoolID";
                }
                else if (string.IsNullOrEmpty(em.Month))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter Month";
                }
                else if (string.IsNullOrEmpty(em.Year))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter Year";
                }
                else
                {
                    int avi1 = 0;
                    if (string.IsNullOrEmpty(em.FName))
                    {
                        AA.Attendence = new List<STUAttendence>();
                        dt = obj.getDataTable(@"select * from TBLStudent s
                                        where s.Status = 3 and s.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and s.ID =" + em.studentId);
                        foreach (DataRow dr in dt.Rows)
                        {
                            avi1++;

                            Student usr = new Student();
                            usr.attendenceList = new List<StudentAttendenceDetails>();
                            usr.ID = int.Parse(dr["Id"].ToString());
                            usr.RegNo = dr["RegNo"].ToString();
                            usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                            usr.RollNo = dr["RollNo"].ToString();
                            usr.emailID = em.Employeecode;
                            usr.Class = dr["ClassID"].ToString();
                            usr.Section = dr["SectionID"].ToString();
                            //  usr.Gender = dr["GenderName"].ToString();

                            if (dr["PicPath"].ToString() == "")
                            {
                                usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                            }
                            else
                            {
                                usr.PicPath = dr["PicPath"].ToString();
                            }

                            //   DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,AttendenceDate,* from tblStudentAttendence where SchoolID='" + em.SchoolID + "' and cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + dr["Id"].ToString() + " and cast(year(AttendenceDate) as varchar(50))='" + em.Year + "' order by CurrentDay");
                            DataTable dt1 = obj.getDataTable(@"select CONVERT(int,Day(AttendenceDate)) as  CurrentDay   ,AttendenceDate,* from tblStudentAttendence where SchoolID='" + em.SchoolID + "' and cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId = " + dr["Id"].ToString() + " and cast(year(AttendenceDate) as varchar(50)) = '" + em.Year + "' order by CurrentDay");
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                avi++;
                                STUAttendence usr1 = new STUAttendence();
                                //usr.StudentId = dr["StudentId"].ToString();
                                //usr.ClassID = dr["ClassId"].ToString();



                                usr1.AttendenceDate = Convert.ToDateTime(dr1["AttendenceDate"]).ToString("yyyy-MM-dd");
                                usr1.AttendenceType = dr1["AttendenceType"].ToString();


                                //  app
                                int StudentID = Convert.ToInt32(dr1["StudentId"]);
                                usr1.AttendenceDay = Convert.ToInt32(dr1["CurrentDay"]);
                                usr1.AttendenceType = dr1["AttendenceType"].ToString();
                                DateTime AttendenceRealDate = Convert.ToDateTime(dr1["AttendenceDate"]);
                                if (dr1["AttendenceType"].ToString() == "Present")
                                {
                                    usr1.AttendenceType = "P";
                                }
                                else if (dr1["AttendenceType"].ToString() == "Absent")
                                {
                                    //Leave 

                                    var checkleave = db.tblStudentLeaveApplies.Where(lv => lv.StudentID == StudentID && lv.datefrom <= AttendenceRealDate && lv.dateto >= AttendenceRealDate && lv.IsDeleted == null && lv.Status == 5 && lv.IsDeleted == null).FirstOrDefault();
                                    if (checkleave != null)
                                    {
                                        usr1.AttendenceType = "L";

                                    }
                                    //Holiday
                                    else
                                    {
                                        var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= AttendenceRealDate && Hl.DateTo >= AttendenceRealDate && Hl.SchoolID == em.SchoolID && Hl.IsDeleted == null).FirstOrDefault();
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

                                //app

                                obj1.SchoolID = dr1["SchoolID"].ToString();
                                obj1.ClassID = dr1["ClassID"].ToString();
                                obj1.SectionId = dr1["SectionId"].ToString();
                                obj1.StudentId = dr1["StudentId"].ToString();
                                obj1.ClassTeacherID = dr1["TeacherId"].ToString();


                                AA.Attendence.Add(usr1);
                            }



                            if (avi != 0)
                            {
                                obj1.status = true;
                                obj1.message = "Sucess";
                                obj1.data = AA;



                            }
                            else if (avi == 0)
                            {
                                obj1.status = false;
                                obj1.message = "Record Not Found";
                                //  obj1.data = list.Add(us)
                            }
                        }
                    }


                    if (avi1 == 0)
                    {
                        obj1.status = false;
                        obj1.message = "Record Not Found ";
                    }



                }

            }
            catch
            {
                obj1.status = false;
                obj1.message = "Something Went Wrong";
            }


            return obj1;
        }

        [System.Web.Http.Route("api/StudentApi/getStudentAttendenceReportByStudentIdAPP")]
        [System.Web.Http.HttpPost]
        public StudentAPP getStudentAttendenceReportByStudentIdAPP(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            StudentAPP obj1 = new StudentAPP();
            List<Student> list = new List<Student>();

            StudentAttendenceDetailsAPP AA = new StudentAttendenceDetailsAPP();


            int avi = 0;

            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(em.DesigId))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter Student ID";
                }
                else if (em.SchoolID.Equals(null) || "".Equals(em.SchoolID) || em.SchoolID == 0)
                {
                    obj1.status = false;
                    obj1.message = "Please Enter SchoolID";
                }
                else if (string.IsNullOrEmpty(em.Month))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter Month";
                }
                else if (string.IsNullOrEmpty(em.Year))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter Year";
                }
                else
                {
                    int avi1 = 0;
                    if (string.IsNullOrEmpty(em.FName))
                    {
                        AA.Attendence = new List<STUAttendence>();
                        dt = obj.getDataTable(@"select * from TBLStudent s
                                        where s.Status = 3 and s.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and s.ID =" + em.DesigId);
                        foreach (DataRow dr in dt.Rows)
                        {
                            avi1++;

                            Student usr = new Student();
                            usr.attendenceList = new List<StudentAttendenceDetails>();
                            usr.ID = int.Parse(dr["Id"].ToString());
                            usr.RegNo = dr["RegNo"].ToString();
                            usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                            usr.RollNo = dr["RollNo"].ToString();
                            usr.emailID = em.Employeecode;
                            usr.Class = dr["ClassID"].ToString();
                            usr.Section = dr["SectionID"].ToString();
                            //  usr.Gender = dr["GenderName"].ToString();

                            if (dr["PicPath"].ToString() == "")
                            {
                                usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                            }
                            else
                            {
                                usr.PicPath = dr["PicPath"].ToString();
                            }

                            DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,AttendenceDate,* from tblStudentAttendence where SchoolID='" + em.SchoolID + "' and cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + dr["Id"].ToString() + " and cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                avi++;
                                STUAttendence usr1 = new STUAttendence();
                                //usr.StudentId = dr["StudentId"].ToString();
                                //usr.ClassID = dr["ClassId"].ToString();

                                usr1.AttendenceDay = Convert.ToInt32(dr1["CurrentDay"]);

                                usr1.AttendenceDate = Convert.ToDateTime(dr1["AttendenceDate"]).ToString("yyyy-MM-dd");
                                usr1.AttendenceType = dr1["AttendenceType"].ToString();

                                if (dr1["AttendenceType"].ToString() == "Present")
                                {
                                    usr1.AttendenceType = "P";
                                }
                                else if (dr1["AttendenceType"].ToString() == "Absent")
                                {
                                    usr1.AttendenceType = "A";
                                }
                                else
                                {
                                    usr1.AttendenceType = "L";
                                }
                                obj1.SchoolID = dr1["SchoolID"].ToString();
                                obj1.ClassID = dr1["ClassID"].ToString();
                                obj1.SectionId = dr1["SectionId"].ToString();
                                obj1.StudentId = dr1["StudentId"].ToString();
                                obj1.ClassTeacherID = dr1["TeacherId"].ToString();


                                AA.Attendence.Add(usr1);
                            }
                            List<DateTime> Totaldate = new List<DateTime>();
                            datess a = new datess();
                            DataTable dt3 = obj.getDataTable(@"SELECT DateFrom,DateTo,CAST(MONTH(DateFrom) as varchar(50) )as StartMonth,CAST(MONTH(DateTo) as varchar(50) )as Endmonth,CAST(YEAR(DateFrom) as nvarchar(50)) as StartYear ,CAST(YEAR(DateTo) as nvarchar(50)) as EndYear ,* FROM tblHolidays where SchoolID='" + em.SchoolID + "' and IsDeleted is null");
                            foreach (DataRow dr3 in dt3.Rows)
                            {
                                DateTime startdate = Convert.ToDateTime(dr3["DateFrom"]);
                                DateTime EndDate = Convert.ToDateTime(dr3["DateTo"]);
                                int startmonth = Convert.ToInt32(dr3["StartMonth"]);
                                int Endmonth = Convert.ToInt32(dr3["Endmonth"]);
                                int StartYear = Convert.ToInt32(dr3["StartYear"]);
                                int EndYear = Convert.ToInt32(dr3["EndYear"]);
                                int monthno = Convert.ToInt32(em.Month);
                                int yearno = Convert.ToInt32(em.Year);
                                // int length = Convert.ToInt32(dr3["leaveCount"]);
                                double length = (EndDate - startdate).TotalDays;
                                //if (startmonth == Endmonth /*&& StartYear == EndYear*/)
                                //{

                                //if (startmonth == monthno)
                                //{
                                datess ab = new datess();
                                for (var i = 0; i <= length; i++)
                                {

                                    if (i == 0)
                                    {

                                        ab.betdates = startdate;
                                    }
                                    else if (i > 0)
                                    {
                                        ab.betdates = ab.betdates.AddDays(1);
                                    }


                                    Totaldate.Add(ab.betdates);
                                }
                            }
                            List<DateTime> Totaldate1 = new List<DateTime>();
                            int avi2 = Totaldate.Count;
                            datess ab1 = new datess();
                            for (var j = 0; j < avi2; j++)
                            {
                                DateTime comparedate = Totaldate[j].Date;
                                int monthDb = comparedate.Month;
                                int yearDb = comparedate.Year;
                                int SearchMonth = Convert.ToInt32(em.Month);
                                int SearchYear = Convert.ToInt32(em.Year);
                                if (monthDb == SearchMonth && yearDb == SearchYear)
                                {
                                    Totaldate1.Add(Totaldate[j].Date);
                                }

                            }

                            int avifinal = Totaldate1.Count;
                            for (var k = 0; k < avifinal; k++)
                            {

                                STUAttendence usr1 = new STUAttendence();
                                DateTime FinalDate = Totaldate1[k].Date;
                                int CurrentDay = FinalDate.Day;
                                usr1.AttendenceDay = Convert.ToInt32(CurrentDay);
                                usr1.AttendenceType = "H";
                                usr1.AttendenceDate = Convert.ToDateTime(FinalDate).ToString("yyyy-MM-dd");

                                AA.Attendence.Add(usr1);

                            }
                            //IDictionary<string, string> dict11 = new Dictionary<string, string>();
                            //for (var i = 0; i < AA.Attendence.Count; i++)
                            //{
                            //    STUAttendence usr1 = new STUAttendence();

                            //    dict11.Add(AA.Attendence[i].AttendenceDate, AA.Attendence[i].AttendenceType);
                            //}

                            //DEMOATTENDENCE AA1 = new DEMOATTENDENCE();

                            //IDictionary<string, DEMOATTENDENCE> dict11 = new Dictionary<string, DEMOATTENDENCE>();


                            if (avi != 0)
                            {
                                obj1.status = true;
                                obj1.message = "Sucess";
                                obj1.data = AA;



                            }
                            else if (avi == 0)
                            {
                                obj1.status = false;
                                obj1.message = "Record Not Found";
                                //  obj1.data = list.Add(us)
                            }
                        }
                    }


                    if (avi1 == 0)
                    {
                        obj1.status = false;
                        obj1.message = "Record Not Found ";
                    }



                }

            }
            catch
            {
                obj1.status = false;
                obj1.message = "Something Went Wrong";
            }


            return obj1;
        }


        //[System.Web.Http.Route("api/StudentApi/getStudentAttendenceReportByStudentIdAPP")]
        //[System.Web.Http.HttpPost]
        //public StudentAPP getStudentAttendenceReportByStudentIdAPP1(EmployeeEm em)
        //{
        //    sqlHelper obj = new sqlHelper();
        //    StudentAPP obj1 = new StudentAPP();
        //    List<Student> list = new List<Student>();

        //    DEMOATTENDENCE AA = new DEMOATTENDENCE();

        //    IDictionary<string, DEMOATTENDENCE> dict = new Dictionary<string, DEMOATTENDENCE>();


        //    int avi = 0;

        //    DataTable dt = null;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(em.DesigId))
        //        {
        //            obj1.status = false;
        //            obj1.message = "Please Enter Student ID";
        //        }
        //        else if (em.SchoolID.Equals(null) || "".Equals(em.SchoolID) || em.SchoolID == 0)
        //        {
        //            obj1.status = false;
        //            obj1.message = "Please Enter SchoolID";
        //        }
        //        else if (string.IsNullOrEmpty(em.Month))
        //        {
        //            obj1.status = false;
        //            obj1.message = "Please Enter Month";
        //        }
        //        else if (string.IsNullOrEmpty(em.Year))
        //        {
        //            obj1.status = false;
        //            obj1.message = "Please Enter Year";
        //        }
        //        else
        //        {
        //            int avi1 = 0;
        //            if (string.IsNullOrEmpty(em.FName))
        //            {
        //                AA.Attendence = new List<STUAttendence>();
        //                dt = obj.getDataTable(@"select * from TBLStudent s
        //                                where s.Status = 3 and s.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and s.ID =" + em.DesigId);
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    avi1++;

        //                    Student usr = new Student();
        //                    usr.attendenceList = new List<StudentAttendenceDetails>();
        //                    usr.ID = int.Parse(dr["Id"].ToString());
        //                    usr.RegNo = dr["RegNo"].ToString();
        //                    usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
        //                    usr.RollNo = dr["RollNo"].ToString();
        //                    usr.emailID = em.Employeecode;
        //                    usr.Class = dr["ClassID"].ToString();
        //                    usr.Section = dr["SectionID"].ToString();
        //                    //  usr.Gender = dr["GenderName"].ToString();

        //                    if (dr["PicPath"].ToString() == "")
        //                    {
        //                        usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
        //                    }
        //                    else
        //                    {
        //                        usr.PicPath = dr["PicPath"].ToString();
        //                    }

        //                    DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,AttendenceDate,* from tblStudentAttendence where SchoolID='" + em.SchoolID + "' and cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + dr["Id"].ToString() + " and cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
        //                    foreach (DataRow dr1 in dt1.Rows)
        //                    {
        //                        avi++;
        //                        STUAttendence usr1 = new STUAttendence();
        //                        //usr.StudentId = dr["StudentId"].ToString();
        //                        //usr.ClassID = dr["ClassId"].ToString();

        //                        usr1.AttendenceDay = Convert.ToInt32(dr1["CurrentDay"]);

        //                        usr1.AttendenceDate = Convert.ToDateTime(dr1["AttendenceDate"]).ToString("yyyy-MM-dd");
        //                        usr1.AttendenceType = dr1["AttendenceType"].ToString();

        //                        if (dr1["AttendenceType"].ToString() == "Present")
        //                        {
        //                            usr1.AttendenceType = "P";
        //                        }
        //                        else if (dr1["AttendenceType"].ToString() == "Absent")
        //                        {
        //                            usr1.AttendenceType = "A";
        //                        }
        //                        else
        //                        {
        //                            usr1.AttendenceType = "L";
        //                        }
        //                        obj1.SchoolID = dr1["SchoolID"].ToString();
        //                        obj1.ClassID = dr1["ClassID"].ToString();
        //                        obj1.SectionId = dr1["SectionId"].ToString();
        //                        obj1.StudentId = dr1["StudentId"].ToString();
        //                        obj1.ClassTeacherID = dr1["TeacherId"].ToString();


        //                        AA.Attendence.Add(usr1);
        //                    }
        //                    List<DateTime> Totaldate = new List<DateTime>();
        //                    datess a = new datess();
        //                    DataTable dt3 = obj.getDataTable(@"SELECT DateFrom,DateTo,CAST(MONTH(DateFrom) as varchar(50) )as StartMonth,CAST(MONTH(DateTo) as varchar(50) )as Endmonth,CAST(YEAR(DateFrom) as nvarchar(50)) as StartYear ,CAST(YEAR(DateTo) as nvarchar(50)) as EndYear ,* FROM tblHolidays where SchoolID='" + em.SchoolID + "' and IsDeleted is null");
        //                    foreach (DataRow dr3 in dt3.Rows)
        //                    {
        //                        DateTime startdate = Convert.ToDateTime(dr3["DateFrom"]);
        //                        DateTime EndDate = Convert.ToDateTime(dr3["DateTo"]);
        //                        int startmonth = Convert.ToInt32(dr3["StartMonth"]);
        //                        int Endmonth = Convert.ToInt32(dr3["Endmonth"]);
        //                        int StartYear = Convert.ToInt32(dr3["StartYear"]);
        //                        int EndYear = Convert.ToInt32(dr3["EndYear"]);
        //                        int monthno = Convert.ToInt32(em.Month);
        //                        int yearno = Convert.ToInt32(em.Year);
        //                        // int length = Convert.ToInt32(dr3["leaveCount"]);
        //                        double length = (EndDate - startdate).TotalDays;
        //                        //if (startmonth == Endmonth /*&& StartYear == EndYear*/)
        //                        //{

        //                        //if (startmonth == monthno)
        //                        //{
        //                        datess ab = new datess();
        //                        for (var i = 0; i <= length; i++)
        //                        {

        //                            if (i == 0)
        //                            {

        //                                ab.betdates = startdate;
        //                            }
        //                            else if (i > 0)
        //                            {
        //                                ab.betdates = ab.betdates.AddDays(1);
        //                            }


        //                            Totaldate.Add(ab.betdates);
        //                        }
        //                    }
        //                    List<DateTime> Totaldate1 = new List<DateTime>();
        //                    int avi2 = Totaldate.Count;
        //                    datess ab1 = new datess();
        //                    for (var j = 0; j < avi2; j++)
        //                    {
        //                        DateTime comparedate = Totaldate[j].Date;
        //                        int monthDb = comparedate.Month;
        //                        int yearDb = comparedate.Year;
        //                        int SearchMonth = Convert.ToInt32(em.Month);
        //                        int SearchYear = Convert.ToInt32(em.Year);
        //                        if (monthDb == SearchMonth && yearDb == SearchYear)
        //                        {
        //                            Totaldate1.Add(Totaldate[j].Date);
        //                        }

        //                    }

        //                    int avifinal = Totaldate1.Count;
        //                    for (var k = 0; k < avifinal; k++)
        //                    {

        //                        STUAttendence usr1 = new STUAttendence();
        //                        DateTime FinalDate = Totaldate1[k].Date;
        //                        int CurrentDay = FinalDate.Day;
        //                        usr1.AttendenceDay = Convert.ToInt32(CurrentDay);
        //                        usr1.AttendenceType = "H";
        //                        usr1.AttendenceDate = Convert.ToDateTime(FinalDate).ToString("yyyy-MM-dd");

        //                        AA.Attendence.Add(usr1);
        //                    }

        //                    DEMOATTENDENCE AA1 = new DEMOATTENDENCE();
        //                    IDictionary<string, DEMOATTENDENCE> dict11 = new Dictionary<string, DEMOATTENDENCE>();
        //                    for (var i = 0; i < AA.Attendence.Count; i++)
        //                    {
        //                        STUAttendence usr1 = new STUAttendence();
        //                        usr1.AttendenceDate = AA.Attendence[i].AttendenceDate;
        //                        dict11.Add(usr1.AttendenceDate, AA1);
        //                    }
        //                    //IDictionary<string, DEMOATTENDENCE> dict11 = new Dictionary<string, DEMOATTENDENCE>();
        //                    //for (var i=0;i< AA.Attendence.Count();i++)
        //                    //{

        //                    //string DATE =   AA.Attendence[i].AttendenceDate.ToString();
        //                    //    AA1.Attendence..AttendenceType = AA.Attendence[i].AttendenceType;
        //                    //    dict11.Add(DATE, dict11)


        //                    //}

        //                    //student obj = new student();
        //                    //obj.id = 1;

        //                    //IDictionary<string, student> dict = new Dictionary<string, student>();
        //                    //dict.Add("test", obj);

        //                    //AA.Attendence.OrderBy(X => X.AttendenceDay).Distinct().ToList();
        //                    //for (var i=0;i<AA.Attendence.Count;i++)
        //                    //{
        //                    //    STUAttendence usr1 = new STUAttendence();
        //                    //    usr1.AttendenceDate = AA.Attendence[i].AttendenceDate;
        //                    //}

        //                    //student obj = new student();




        //                    if (avi != 0)
        //                    {
        //                        obj1.status = true;
        //                        obj1.message = "Sucess";
        //                        obj1.data = AA;



        //                    }
        //                    else if (avi == 0)
        //                    {
        //                        obj1.status = false;
        //                        obj1.message = "Record Not Found";
        //                        //  obj1.data = list.Add(us)
        //                    }
        //                }
        //            }


        //            if (avi1 == 0)
        //            {
        //                obj1.status = false;
        //                obj1.message = "Record Not Found ";
        //            }



        //        }

        //    }
        //    catch
        //    {
        //        obj1.status = false;
        //        obj1.message = "Something Went Wrong";
        //    }


        //    return obj1;
        //}
        [System.Web.Http.Route("api/StudentApi/getStudentAttendenceReportByStudentId1")]
        [System.Web.Http.HttpPost]
        public Student[] getStudentAttendenceReportByStudentId1(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {
                dt = obj.getDataTable(@"select * from TBLStudent s
                                        where s.Status = 3 and s.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and s.ID =" + em.DesigId);
            }

            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["RegNo"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.RollNo = dr["RollNo"].ToString();
                usr.emailID = em.Employeecode;
                usr.Class = dr["ClassID"].ToString();
                usr.Section = dr["SectionID"].ToString();
                //  usr.Gender = dr["GenderName"].ToString();

                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }

                DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,* from tblStudentAttendence where SchoolID=" + em.SchoolID + " and cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + dr["Id"].ToString() + " and cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                    //usr.StudentId = dr["StudentId"].ToString();
                    //usr.ClassID = dr["ClassId"].ToString();
                    usr1.Idd = Convert.ToInt32(dr1["StudentId"]);
                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();
                    usr1.AttendenceRealDate = Convert.ToDateTime(dr1["AttendenceDate"]);
                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }
                    else if (dr1["AttendenceType"].ToString() == "Absent")
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
                            var checkHoliday = db.tblHolidays.Where(Hl => Hl.DateFrom <= usr1.AttendenceRealDate && Hl.DateTo >= usr1.AttendenceRealDate && Hl.SchoolID == em.SchoolID && Hl.IsDeleted == null).FirstOrDefault();
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

                    usr1.SchoolID = dr1["SchoolID"].ToString();
                    usr.attendenceList.Add(usr1);
                }



                list.Add(usr);


            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getStudentAttendenceReportByStudentId")]
        [System.Web.Http.HttpPost]
        public Student[] getStudentAttendenceReportByStudentId(EmployeeEm em)
        {
            sqlHelper obj = new sqlHelper();
            List<Student> list = new List<Student>();
            DataTable dt = null;
            if (string.IsNullOrEmpty(em.FName))
            {
                dt = obj.getDataTable(@"select * from TBLStudent s
                                        where s.Status = 3 and s.IsDeleted is null and s.SchoolID='" + em.SchoolID + "' and s.ID =" + em.DesigId);
            }

            foreach (DataRow dr in dt.Rows)
            {
                Student usr = new Student();
                usr.attendenceList = new List<StudentAttendenceDetails>();
                usr.ID = int.Parse(dr["Id"].ToString());
                usr.RegNo = dr["RegNo"].ToString();
                usr.FirstName = dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["LastName"].ToString();
                usr.RollNo = dr["RollNo"].ToString();
                usr.emailID = em.Employeecode;
                usr.Class = dr["ClassID"].ToString();
                usr.Section = dr["SectionID"].ToString();
                //  usr.Gender = dr["GenderName"].ToString();

                if (dr["PicPath"].ToString() == "")
                {
                    usr.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
                }
                else
                {
                    usr.PicPath = dr["PicPath"].ToString();
                }

                DataTable dt1 = obj.getDataTable(@"select cast(Day(AttendenceDate) as varchar(50)) CurrentDay,* from tblStudentAttendence where SchoolID=" + em.SchoolID + " and cast(Month(AttendenceDate) as varchar(50))=" + em.Month + " and StudentId=" + dr["Id"].ToString() + " and cast(year(AttendenceDate) as varchar(50))='" + em.Year + "'");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                    //usr.StudentId = dr["StudentId"].ToString();
                    //usr.ClassID = dr["ClassId"].ToString();

                    usr1.AttendenceDate = dr1["CurrentDay"].ToString();
                    usr1.AttendenceType = dr1["AttendenceType"].ToString();
                    if (dr1["AttendenceType"].ToString() == "Present")
                    {
                        usr1.AttendenceType = "P";
                    }
                    else if (dr1["AttendenceType"].ToString() == "Absent")
                    {


                        usr1.AttendenceType = "A";
                    }
                    else
                    {
                        usr1.AttendenceType = "L";
                    }
                    usr1.SchoolID = dr1["SchoolID"].ToString();
                    usr.attendenceList.Add(usr1);
                }
                List<DateTime> Totaldate = new List<DateTime>();
                datess a = new datess();
                DataTable dt3 = obj.getDataTable(@"SELECT DateFrom,DateTo,CAST(MONTH(DateFrom) as varchar(50) )as StartMonth,CAST(MONTH(DateTo) as varchar(50) )as Endmonth,CAST(YEAR(DateFrom) as nvarchar(50)) as StartYear ,CAST(YEAR(DateTo) as nvarchar(50)) as EndYear ,* FROM tblHolidays where SchoolID='" + em.SchoolID + "' and IsDeleted is null");
                foreach (DataRow dr3 in dt3.Rows)
                {
                    DateTime startdate = Convert.ToDateTime(dr3["DateFrom"]);
                    DateTime EndDate = Convert.ToDateTime(dr3["DateTo"]);
                    int startmonth = Convert.ToInt32(dr3["StartMonth"]);
                    int Endmonth = Convert.ToInt32(dr3["Endmonth"]);
                    int StartYear = Convert.ToInt32(dr3["StartYear"]);
                    int EndYear = Convert.ToInt32(dr3["EndYear"]);
                    int monthno = Convert.ToInt32(em.Month);
                    int yearno = Convert.ToInt32(em.Year);
                    // int length = Convert.ToInt32(dr3["leaveCount"]);
                    double length = (EndDate - startdate).TotalDays;
                    //if (startmonth == Endmonth /*&& StartYear == EndYear*/)
                    //{

                    //if (startmonth == monthno)
                    //{
                    datess ab = new datess();
                    for (var i = 0; i <= length; i++)
                    {

                        if (i == 0)
                        {

                            ab.betdates = startdate;
                        }
                        else if (i > 0)
                        {
                            ab.betdates = ab.betdates.AddDays(1);
                        }


                        Totaldate.Add(ab.betdates);
                    }
                }
                List<DateTime> Totaldate1 = new List<DateTime>();
                int avi = Totaldate.Count;
                datess ab1 = new datess();
                for (var j = 0; j < avi; j++)
                {
                    DateTime comparedate = Totaldate[j].Date;
                    int monthDb = comparedate.Month;
                    int yearDb = comparedate.Year;
                    int SearchMonth = Convert.ToInt32(em.Month);
                    int SearchYear = Convert.ToInt32(em.Year);
                    if (monthDb == SearchMonth && yearDb == SearchYear)
                    {
                        Totaldate1.Add(Totaldate[j].Date);
                    }

                }

                int avifinal = Totaldate1.Count;
                for (var k = 0; k < avifinal; k++)
                {
                    StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                    DateTime FinalDate = Totaldate1[k].Date;
                    int CurrentDay = FinalDate.Day;
                    usr1.AttendenceDate = CurrentDay.ToString();
                    usr1.AttendenceType = "H";

                    usr.attendenceList.Add(usr1);
                }


                foreach (var av in Totaldate)
                {
                    //Convert.ToInt32( av.Date.Month)== em.Month

                }

                //DataTable dt2 = obj.getDataTable(@"select cast(Day(DateFrom) as varchar(50)) CurrentDay,* from tblholidays where SchoolID='" + em.SchoolID + "' and cast(Month(DateFrom) as varchar(50))=" + em.Month + " and cast(year(DateFrom) as varchar(50))='" + em.Year + "'");
                //   foreach (DataRow dr2 in dt2.Rows)
                //   {
                //       StudentAttendenceDetails usr1 = new StudentAttendenceDetails();
                //       usr1.AttendenceDate = dr2["CurrentDay"].ToString();
                //       usr1.AttendenceType = "H";

                //       usr.attendenceList.Add(usr1);
                //   }

                list.Add(usr);


            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getAllStudentsEventsBySchoolAPP")]
        [System.Web.Http.HttpPost]
        public EventsDetailsAPP eventapp(EventsDetails1 events)
        {
            sqlHelper obj = new sqlHelper();
            EventsDetailsAPP obje = new EventsDetailsAPP();
            List<EventsDetails1> list = new List<EventsDetails1>();
            int Count = 0;
            try
            {

                if (events.SchoolID.Equals(0) || "".Equals(events.SchoolID) || events.SchoolID.Equals(null))
                {
                    obje.status = false;
                    obje.message = "Please Enter SchoolID";
                    obje.data = list;
                }
                else if (string.IsNullOrEmpty(events.ClassId))
                {
                    obje.status = false;
                    obje.message = "Please Enter Class ID";
                    obje.data = list;
                }
                else if (string.IsNullOrEmpty(events.Section))
                {
                    obje.status = false;
                    obje.message = "Please Enter Section";
                    obje.data = list;
                }
                else
                {

                    //DataTable dt = obj.getDataTable(@" select et.EventName Type,* from tblEventDetails e 
                    //                          left outer join tblEventType et on et.EventId=e.EventType where e.EventFor=1   and e.Status=1  and e.SchoolID=" + events.SchoolID + " and e.IsDeleted is null  union select et.EventName Type,* from tblEventDetails e left outer join tblEventType et on et.EventId=e.EventType   where EventFor=2 and courseId=" + events.ClassId + " and  SectionId=" + events.Section + " and e.Status=1 and e.SchoolID='" + events.SchoolID + "' and e.IsDeleted is null order by StartdateTime desc");

                    DataTable dt = obj.getDataTable(@"select et.EventName Type,* from tblEventDetails e left outer join tblEventType et on et.EventId=e.EventType   where EventFor=2 and courseId=" + events.ClassId + " and  SectionId=" + events.Section + " and e.Status=1 and e.SchoolID='" + events.SchoolID + "' and e.IsDeleted is null order by StartdateTime desc");

                    foreach (DataRow dr in dt.Rows)
                    {
                        EventsDetails1 usr = new EventsDetails1();
                        Count++;

                        usr.Description = dr["Description"].ToString();
                        usr.EventName = dr["EventName"].ToString();
                        usr.EventType = dr["Type"].ToString();
                        //usr.StartEndDate = dr["StartdateTime"].ToString().Split(' ')[0] + "-" + dr["EnddateTime"].ToString().Split(' ')[0];
                        usr.StartEndDate = Convert.ToDateTime(dr["StartdateTime"]).ToString("dd MMMM yyyy") + " - " + Convert.ToDateTime(dr["EnddateTime"]).ToString("dd MMMM yyyy");
                        usr.StartEndTime = dr["StartdateTime"].ToString().Split(' ')[1] + "-" + dr["EnddateTime"].ToString().Split(' ')[1];
                        usr.ClassId = dr["courseId"].ToString();
                        usr.Section = dr["SectionId"].ToString();
                        usr.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                        list.Add(usr);

                    }
                    DataTable dt1 = obj.getDataTable(@"select et.EventName Type,* from tblEventDetails e left outer join tblEventType et on et.EventId=e.EventType where EventFor=1  and e.Status=1 and e.SchoolID='" + events.SchoolID + "' and e.IsDeleted is null order by StartdateTime desc");
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        EventsDetails1 usr1 = new EventsDetails1();
                        Count++;
                        usr1.Description = dr1["Description"].ToString();
                        usr1.EventName = dr1["EventName"].ToString();
                        usr1.EventType = dr1["Type"].ToString();
                        //usr1.StartEndDate = dr1["StartdateTime"].ToString().Split(' ')[0] + "-" + dr1["EnddateTime"].ToString().Split(' ')[0];
                        usr1.StartEndDate = Convert.ToDateTime(dr1["StartdateTime"]).ToString("dd MMMM yyyy") + " - " + Convert.ToDateTime(dr1["EnddateTime"]).ToString("dd MMMM yyyy");
                        usr1.StartEndTime = dr1["StartdateTime"].ToString().Split(' ')[1] + "-" + dr1["EnddateTime"].ToString().Split(' ')[1];
                        usr1.ClassId = dr1["courseId"].ToString();
                        usr1.Section = dr1["SectionId"].ToString();
                        usr1.SchoolID = Convert.ToInt32(dr1["SchoolID"]);
                        list.Add(usr1);

                    }

                    if (Count != 0)
                    {
                        obje.status = true;
                        obje.message = "Sucess";
                        obje.data = list;
                    }
                    else
                    {
                        obje.status = false;
                        obje.message = "No Data Found";
                        obje.data = list;
                    }
                }

            }
            catch
            {
                obje.status = false;
                obje.message = "Something Went Wrong";

            }
            return obje;

        }
        //new student events only
        [System.Web.Http.Route("api/StudentApi/getOnlyAllStudentsEventsBySchool")]
        [System.Web.Http.HttpPost]
        public EventsDetails[] Stuevents(EventsDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<EventsDetails> list = new List<EventsDetails>();


            DataTable dt = obj.getDataTable(@"select et.EventName Type,* from tblEventDetails e left outer join tblEventType et on et.EventId=e.EventType where EventFor=2 and courseId=" + events.ClassId + " and  SectionId=" + events.Section + " and e.Status=1 and e.SchoolID=" + events.SchoolID + " and e.IsDeleted is null order by StartdateTime desc");

            foreach (DataRow dr in dt.Rows)
            {
                EventsDetails usr = new EventsDetails();
                usr.EventName = dr["EventName"].ToString();
                usr.EventType = dr["Type"].ToString();
                usr.StartDate = dr["StartdateTime"].ToString().Split(' ')[0] + "-" + dr["EnddateTime"].ToString().Split(' ')[0];
                usr.EndDate = dr["StartdateTime"].ToString().Split(' ')[1] + "-" + dr["EnddateTime"].ToString().Split(' ')[1];


                list.Add(usr);

            }
            return list.ToArray();
        }




        [System.Web.Http.Route("api/StudentApi/getAllStudentsEventsBySchool")]
        [System.Web.Http.HttpPost]
        public EventsDetails[] events(EventsDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<EventsDetails> list = new List<EventsDetails>();


            DataTable dt = obj.getDataTable(@"select et.EventName Type,* from tblEventDetails e 
                                               left outer join tblEventType et on et.EventId=e.EventType where e.EventFor=1   and e.Status=1  and e.SchoolID='" + events.SchoolID + "' and e.IsDeleted is null  union select et.EventName Type,* from tblEventDetails e left outer join tblEventType et on et.EventId=e.EventType where EventFor=2 and courseId=" + events.ClassId + " and  SectionId=" + events.Section + " and e.Status=1 and e.SchoolID='" + events.SchoolID + "' and e.IsDeleted is null order by StartdateTime desc");





            foreach (DataRow dr in dt.Rows)
            {
                EventsDetails usr = new EventsDetails();
                usr.EventName = dr["EventName"].ToString();
                usr.EventType = dr["Type"].ToString();
                usr.StartDate = dr["StartdateTime"].ToString().Split(' ')[0] + "-" + dr["EnddateTime"].ToString().Split(' ')[0];
                usr.EndDate = dr["StartdateTime"].ToString().Split(' ')[1] + "-" + dr["EnddateTime"].ToString().Split(' ')[1];


                list.Add(usr);

            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getAllStudentsEvents")]
        [System.Web.Http.HttpPost]
        public EventsDetails[] getAllStudentsEvents(EventsDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<EventsDetails> list = new List<EventsDetails>();


            DataTable dt = obj.getDataTable(@"select et.EventName Type,* from tblEventDetails e 
                                                left outer join tblEventType et on et.EventId=e.EventType where EventFor=1   and e.Status=1 
                                                union
                                                select et.EventName Type,* from tblEventDetails e
                                                left outer join tblEventType et on et.EventId=e.EventType  
                                                 where EventFor=2 and courseId=" + events.ClassId + " and  SectionId=" + events.Section + " and e.Status=1 order by StartdateTime desc");
            foreach (DataRow dr in dt.Rows)
            {
                EventsDetails usr = new EventsDetails();
                usr.EventName = dr["EventName"].ToString();
                usr.EventType = dr["Type"].ToString();
                usr.StartDate = dr["StartdateTime"].ToString().Split(' ')[0] + "-" + dr["EnddateTime"].ToString().Split(' ')[0];
                usr.EndDate = dr["StartdateTime"].ToString().Split(' ')[1] + "-" + dr["EnddateTime"].ToString().Split(' ')[1];


                list.Add(usr);

            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/sendEmailtoParent")]
        [System.Web.Http.HttpPost]
        public string sendEmailtoParent(Student[] employye)
        {
            sqlHelper obj = new sqlHelper();
            foreach (var emp in employye)
            {
                if (emp.ID != 0)
                {
                    string motherMail = obj.ExecuteScaler("select Mmail from TBLStudent where SchoolID='" + emp.SchoolID + "' and ID=" + emp.ID);
                    string fatherMail = obj.ExecuteScaler("select fmail from TBLStudent where SchoolID='" + emp.SchoolID + "' and ID=" + emp.ID);
                    //int Sid = Convert.ToInt32(emp.SchoolID);
                    //var Schoolname = db.tblSchoolDetails.Single(x => x.ID == Sid).School;
                    if (motherMail.ToString().Trim() != "" || fatherMail.ToString().Trim() != "")
                    {
                        try
                        {
                            SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                            using (MailMessage mm = new MailMessage())
                            {
                                StringBuilder st = new StringBuilder();
                                st.AppendLine(emp.Awards);
                                //st.AppendLine("Regards");
                                //st.AppendLine(Schoolname);


                                mm.From = new MailAddress(secObj.From); //--- Email address of the sender

                                mm.To.Add(fatherMail); //---- Email address of the recipient.
                                mm.CC.Add(motherMail);
                                mm.Subject = emp.stream; //---- Subject of email.
                                mm.Body = (st.ToString()); //---- Content of email.

                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                                smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                                NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                                //---Your Email and password
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                //smtp.Port = 25;
                                smtp.Port = 587; //---- SMTP Server port number. This varies from host to host. 
                                smtp.Send(mm);

                                //WebMail.SmtpServer = "webmail.smartvidhya.com";
                                ////gmail port to send emails
                                //WebMail.SmtpPort = 25;
                                //WebMail.SmtpUseDefaultCredentials = false;
                                ////sending emails with secure protocol
                                //WebMail.EnableSsl = false;
                                ////EmailId used to send emails from application
                                //WebMail.UserName = "Info@smartvidhya.com";
                                //WebMail.Password = "smartvidhya123@";

                                ////Sender email address.
                                //WebMail.From = "Info@smartvidhya.com";

                                ////Send email
                                //WebMail.Send(to: fatherMail.ToString().Trim(), subject: emp.stream, body: emp.Awards, cc: motherMail.ToString().Trim(), bcc: "", isBodyHtml: true);
                            }
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }



                    }
                }
            }
            return "Email Sent Successfully.";
        }

        [System.Web.Http.Route("api/StudentApi/getStudentDocuments")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getStudentDocuments(List<string> id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string query = @"select DocPath,d.DocumentName from TBLStudentDocs ed
                                    inner join tblDocument d on d.Id = ed.DocID
                                     where d.userId = 4
                                    and ed.StudentID =" + id[0];

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["DocumentName"].ToString();
                usr.Id = dr["DocPath"].ToString();
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getStudentsBySectionClassIdbyschool")]
        [System.Web.Http.HttpPost]
        public TaskDetails[] getStudentsBySectionClassIdbyschool(TaskDetails events)
        {
            int SchoolID = Convert.ToInt32(events.SchoolID);
            sqlHelper obj = new sqlHelper();
            List<TaskDetails> list = new List<TaskDetails>();


            DataTable dt = obj.getDataTable(@"select ID,FirstName+' '+MiddleName+' '+LastName Name from  TBLStudent where ClassID=" + events.Class + " and SchoolID='" + SchoolID + "' and SectionID='" + events.Section + "' and IsDeleted is null");
            foreach (DataRow dr in dt.Rows)
            {
                TaskDetails usr = new TaskDetails();
                usr.Id = dr["ID"].ToString();
                usr.TaskName = dr["Name"].ToString();
                list.Add(usr);

            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getStudentsBySectionClassId")]
        [System.Web.Http.HttpPost]
        public TaskDetails[] getStudentsBySectionClassId(TaskDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<TaskDetails> list = new List<TaskDetails>();


            DataTable dt = obj.getDataTable(@"select ID,FirstName+' '+MiddleName+' '+LastName Name from  TBLStudent where ClassID=" + events.Class + " and SectionID=" + events.Section);
            foreach (DataRow dr in dt.Rows)
            {
                TaskDetails usr = new TaskDetails();
                usr.Id = dr["ID"].ToString();
                usr.TaskName = dr["Name"].ToString();
                list.Add(usr);

            }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/getEmployeeNameFromDepartmentBYSchool")]
        [System.Web.Http.HttpPost]
        public TaskDetails[] getEmployeeNameFromDepartmentBYSchool(TaskDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<TaskDetails> list = new List<TaskDetails>();


            DataTable dt = obj.getDataTable(@"select  Id,FirstName+' '+MiddleName+' '+LastName Name from tblemployee where DeptID=" + events.Department + " and Status=1 and SchoolID='" + events.SchoolID + "' and IsDeleted is null");

            foreach (DataRow dr in dt.Rows)
            {
                TaskDetails usr = new TaskDetails();
                usr.Id = dr["Id"].ToString();
                usr.TaskName = dr["Name"].ToString();
                list.Add(usr);

            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getEmployeeNameFromDepartment")]
        [System.Web.Http.HttpPost]
        public TaskDetails[] getEmployeeNameFromDepartment(TaskDetails events)
        {
            sqlHelper obj = new sqlHelper();
            List<TaskDetails> list = new List<TaskDetails>();


            DataTable dt = obj.getDataTable(@"select  Id,FirstName+' '+MiddleName+' '+LastName Name from tblemployee where DeptID=" + events.Department + " and Status=1");

            foreach (DataRow dr in dt.Rows)
            {
                TaskDetails usr = new TaskDetails();
                usr.Id = dr["Id"].ToString();
                usr.TaskName = dr["Name"].ToString();
                list.Add(usr);

            }
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getStudentTaskByStdIdAPP")]
        [System.Web.Http.HttpPost]
        public TaskDetailsAPP getStudentTaskByStdIdAPP(Parameters param)
        {
            int avi = 0;
            TaskDetailsAPP obj1 = new TaskDetailsAPP();
            List<TaskDetails1> list = new List<TaskDetails1>();
            sqlHelper obj = new sqlHelper();

            try
            {
                if (string.IsNullOrEmpty(param.val1))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter SchoolID";
                    obj1.data = list;
                }
                else if (string.IsNullOrEmpty(param.val))
                {
                    obj1.status = false;
                    obj1.message = "Please Enter Student ID";
                    obj1.data = list;
                }
                else
                {
                    int SchoolID = Convert.ToInt32(param.val1);
                    DataTable dt = obj.getDataTable(@"select ROW_NUMBER() over(order by TaskDate desc) sno, ss.FirstName+' '+ss.MiddleName+' '+ss.LastName FullName,cc.CourseName,s.SectionName,CONVERT(varchar(100),td.taskDate, 107) TaskDatets,td.* from tblTaskDetails td
                                                left outer join TBLStudent ss on ss.ID = td.Student
                                                left outer join tblCourses cc on cc.Id = td.classId
                                                left outer join tblSections s on s.Id = td.SectionId and td.classId = cc.Id
                                                where UserType = 1 and td.IsDeleted is null and td.SchoolID='" + SchoolID + "' and  Student='" + param.val + "'  order by TaskDate desc");

                    foreach (DataRow dr in dt.Rows)
                    {
                        avi++;
                        TaskDetails1 items = new TaskDetails1();
                        items.sno = dr["sno"].ToString();
                        items.TaskName = dr["TaskName"].ToString();
                        items.Section = dr["SectionName"].ToString();

                        items.Class = dr["CourseName"].ToString();
                        items.StudentName = dr["FullName"].ToString();

                        items.TaskDate = dr["TaskDatets"].ToString();

                        items.Status = dr["Status"].ToString();
                        if (items.Status == "On hold")
                        {
                            items.color = "#ff5722";
                        }
                        else if (items.Status == "Open")
                        {
                            items.color = "#00bcd4";
                        }
                        else if (items.Status == "Resolved")
                        {
                            items.color = "#4caf50";
                        }
                        else
                        {
                            items.color = "#999";
                        }



                        items.Priority = dr["TaskPriority"].ToString();
                        if (items.Priority == "Highest Priority")
                        {
                            items.priorityColor = "#f44336";
                        }
                        else if (items.Priority == "High Priority")
                        {
                            items.priorityColor = "#00bcd4";
                        }
                        else if (items.Priority == "Normal Priority")
                        {
                            items.priorityColor = "#2196f3";
                        }
                        else
                        {
                            items.priorityColor = "#4caf50";
                        }

                        items.Description = dr["Description"].ToString();
                        items.Id = dr["Id"].ToString();
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                        list.Add(items);
                    }
                    if (avi != 0)
                    {
                        obj1.status = true;
                        obj1.message = "Sucess";
                        obj1.data = list;
                    }
                    else if (avi == 0)
                    {
                        obj1.status = false;
                        obj1.message = "No Data Found";


                    }
                }


            }
            catch
            {
                obj1.status = false;
                obj1.message = "Something Went Wrong ";
            }
            return obj1;
        }



        [System.Web.Http.Route("api/StudentApi/getStudentTaskByStdId")]
        [System.Web.Http.HttpPost]
        public List<TaskDetails> getStudentTaskByStdId(Parameters param)
        {


            List<TaskDetails> list = new List<TaskDetails>();
            sqlHelper obj = new sqlHelper();
            int SchoolID = Convert.ToInt32(param.val1);
            try
            {
                DataTable dt = obj.getDataTable(@"select ROW_NUMBER() over(order by TaskDate desc) sno, ss.FirstName+' '+ss.MiddleName+' '+ss.LastName FullName,cc.CourseName,s.SectionName,CONVERT(varchar(100),td.taskDate, 107) TaskDatets,td.* from tblTaskDetails td
                                                left outer join TBLStudent ss on ss.ID = td.Student
                                                left outer join tblCourses cc on cc.Id = td.classId
                                                left outer join tblSections s on s.Id = td.SectionId and td.classId = cc.Id
                                                where UserType = 1 and td.IsDeleted is null and td.SchoolID='" + SchoolID + "' and  Student='" + param.val + "'  order by TaskDate desc");

                foreach (DataRow dr in dt.Rows)
                {
                    TaskDetails items = new TaskDetails();
                    items.sno = dr["sno"].ToString();
                    items.TaskName = dr["TaskName"].ToString();
                    items.Section = dr["SectionName"].ToString();

                    items.Class = dr["CourseName"].ToString();
                    items.StudentName = dr["FullName"].ToString();

                    items.TaskDate = dr["TaskDatets"].ToString();

                    items.Status = dr["Status"].ToString();
                    if (items.Status == "On hold")
                    {
                        items.color = "#ff5722";
                    }
                    else if (items.Status == "Open")
                    {
                        items.color = "#00bcd4";
                    }
                    else if (items.Status == "Resolved")
                    {
                        items.color = "#4caf50";
                    }
                    else
                    {
                        items.color = "#999";
                    }



                    items.Priority = dr["TaskPriority"].ToString();
                    if (items.Priority == "Highest Priority")
                    {
                        items.priorityColor = "#f44336";
                    }
                    else if (items.Priority == "High Priority")
                    {
                        items.priorityColor = "#00bcd4";
                    }
                    else if (items.Priority == "Normal Priority")
                    {
                        items.priorityColor = "#2196f3";
                    }
                    else
                    {
                        items.priorityColor = "#4caf50";
                    }

                    items.Description = dr["Description"].ToString();
                    items.Id = dr["Id"].ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        [System.Web.Http.Route("api/StudentApi/changeStudentTaskStatusAPP")]
        [System.Web.Http.HttpPost]
        public TaskDetailsAPP changeEmployeeTaskStatusAPP(Parameters param)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            TaskDetailsAPP obj = new TaskDetailsAPP();
            try
            {
                if (string.IsNullOrEmpty(param.val))
                {
                    obj.status = false;
                    obj.message = "Please Enter Task ID";
                }
                else if (string.IsNullOrEmpty(param.val1))
                {
                    obj.status = false;
                    obj.message = "Please Enter SchoolID";
                }
                else
                {
                    int idd = Convert.ToInt16(param.val);
                    int SchoolID = Convert.ToInt32(param.val1);
                    var result = db.tblTaskDetails.SingleOrDefault(s => s.Id == idd && s.SchoolID == SchoolID);
                    result.Status = "Resolved";
                    db.SaveChanges();
                    obj.status = true;
                    obj.message = "Sucess";
                }

            }
            catch
            {
                obj.status = false;
                obj.message = "Something Went Wrong";
            }
            return obj;
        }



        [System.Web.Http.Route("api/StudentApi/changeStudentTaskStatus")]
        [System.Web.Http.HttpPost]
        public string changeEmployeeTaskStatus(Parameters param)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();

            int idd = Convert.ToInt16(param.val);
            int SchoolID = Convert.ToInt32(param.val1);
            var result = db.tblTaskDetails.SingleOrDefault(s => s.Id == idd && s.SchoolID == SchoolID);
            result.Status = "Resolved";
            db.SaveChanges();
            return "";
        }

        [System.Web.Http.Route("api/StudentApi/viewTaskDetasilForStudentId")]
        [System.Web.Http.HttpPost]
        public TaskDetails viewTaskDetasilForStudentId(List<string> id)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            int iddd = Convert.ToInt32(id[0]);
            var result = db.tblTaskDetails.SingleOrDefault(s => s.Id == iddd);
            TaskDetails usr = new TaskDetails();
            usr.TaskName = result.TaskName;
            usr.Description = result.Description;
            usr.Priority = result.TaskPriority;
            usr.TaskDate = Convert.ToDateTime(result.TaskDate).ToString("MMM dd, yyyy");
            usr.Status = result.Status;
            if (usr.Status == "On hold")
            {
                usr.color = "#ff5722";
            }
            else if (usr.Status == "Open")
            {
                usr.color = "#00bcd4";
            }
            else if (usr.Status == "Resolved")
            {
                usr.color = "#4caf50";
            }
            else
            {
                usr.color = "#999";
            }

            if (usr.Priority == "Highest Priority")
            {
                usr.priorityColor = "#f44336";
            }
            else if (usr.Priority == "High Priority")
            {
                usr.priorityColor = "#00bcd4";
            }
            else if (usr.Priority == "Normal Priority")
            {
                usr.priorityColor = "#2196f3";
            }
            else
            {
                usr.priorityColor = "#4caf50";
            }


            return usr;

        }


        [System.Web.Http.Route("api/StudentApi/getAllClassWithSection")]
        [System.Web.Http.HttpPost]
        public List<CourseMaster> getAllClassWithSection()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                string Schoolid = (string)HttpContext.Current.Session["schoolid"];
                int? schid = Convert.ToInt32(Schoolid);

                SCHOOLERPEntities db = new SCHOOLERPEntities();
                List<CourseMaster> list = new List<CourseMaster>();
                var classMaster = db.tblCourses.Where(s => s.Status == true && s.SchoolID == schid && s.IsDeleted == null).ToList();

                foreach (var c in classMaster)
                {
                    CourseMaster usr = new CourseMaster();
                    usr.Name = c.CourseName;

                    var sectionMaster = db.tblSections.Where(s => s.Status == true && s.ClassId == c.Id && s.IsDeleted == null).ToList();
                    usr.MasterSection = new List<SectionMaster>();
                    foreach (var s in sectionMaster)
                    {
                        SectionMaster usr1 = new SectionMaster();
                        usr1.Name = s.SectionName;


                        usr.MasterSection.Add(usr1);

                    }
                    list.Add(usr);

                }



                return list;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        [System.Web.Http.Route("api/StudentApi/getAllClassAndStream")]
        [System.Web.Http.HttpPost]
        public List<CourseMaster> getAllClassAndStream()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string Schoolid = (string)HttpContext.Current.Session["schoolid"];
            int? schid = Convert.ToInt32(Schoolid);
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            List<CourseMaster> list = new List<CourseMaster>();
            var classMaster = db.tblCourses.Where(s => s.Status == true && s.SchoolID == schid && s.IsDeleted == null).ToList();

            foreach (var c in classMaster)
            {
                CourseMaster usr = new CourseMaster();
                usr.Name = c.CourseName;

                var streamMaster = db.tblStreams.Where(s => s.Status == true && s.Class == c.Id && s.SchoolID == schid && s.IsDeleted == null).ToList();
                usr.MasterStream = new List<StreamMaster>();
                foreach (var s in streamMaster)
                {
                    StreamMaster usr1 = new StreamMaster();
                    usr1.Name = s.StreamName;


                    usr.MasterStream.Add(usr1);

                }
                list.Add(usr);

            }



            return list;


        }

        [System.Web.Http.Route("api/StudentApi/getList")]
        [System.Web.Http.HttpPost]
        public ScholarRegister[] getList(ScholarRegister val)
        {
            List<ScholarRegister> list = new List<Models.ScholarRegister>();

            try
            {
                if (val.type == "SRList")
                {
                    list = fillSRListAll(val);
                }
                if (val.type == "AssignSRno")
                {
                    list = fillSRListToAssignSRno(val);
                }
                if (val.type == "StudentPromotion")
                {
                    list = fillSRListToPromoteStudents(val);
                }
            }

            catch (Exception ex)
            {

            }
            return list.ToArray();

        }

        public List<TBLStudent> GetFilteredStudentsSRList(ScholarRegister val)
        {
            //List<TBLStudent> stList = new List<TBLStudent>();
            var predicate = PredicateBuilder.True<TBLStudent>();
            if (!String.IsNullOrEmpty(val.std.FirstName))
            {
                if (!val.std.FirstName.Trim().Contains(" "))
                {
                    predicate = predicate.And(x => x.FirstName.ToUpper().Contains(val.std.FirstName.ToUpper()) || x.MiddleName.ToUpper().Contains(val.std.FirstName.ToUpper()) || x.LastName.ToUpper().Contains(val.std.FirstName.ToUpper()));
                }
                else
                {
                    var name = val.std.FirstName.Trim().Split(' ');
                    predicate = predicate.And(x => x.FirstName.ToUpper().Contains(name[0].ToUpper()) && x.LastName.ToUpper().Contains(name[1].ToUpper()));
                }
            }
            if (!String.IsNullOrEmpty(val.std.FatherName))
            {
                predicate = predicate.And(x => x.FatherName.ToUpper().Contains(val.std.FatherName.ToUpper()));
            }
            if (!String.IsNullOrEmpty(val.std.RegNo))
            {
                predicate = predicate.And(x => x.RegNo == val.std.RegNo);
            }
            if (!String.IsNullOrEmpty(val.std.SRNo))
            {
                var sr = db.tblScholarRegisters.FirstOrDefault(x => x.SRno.ToLower().Contains(val.std.SRNo.ToLower()));
                if (sr != null)
                {
                    predicate = predicate.And(x => x.ID == sr.StudentID);
                }
            }
            if (!String.IsNullOrEmpty(val.SDOB))
            {
                DateTime dtDOB = Convert.ToDateTime(val.SDOB);
                predicate = predicate.And(x => x.DOB == dtDOB);
            }
            if (!String.IsNullOrEmpty(val.jDate))
            {
                DateTime dtAdmitDt = Convert.ToDateTime(val.jDate);
                predicate = predicate.And(x => x.JoiningDate == dtAdmitDt);
            }
            if (val.StatusName != "-1" && !String.IsNullOrEmpty(val.StatusName))
            {
                var status = db.tblStatus.Where(x => x.Status.ToLower().Contains(val.StatusName)).FirstOrDefault().StatusID;
                predicate = predicate.And(x => x.Status == status);
            }
            if (val.std.ClassID > 0)
            {
                predicate = predicate.And(x => x.ClassID == val.std.ClassID);
                if (val.std.SectionID > 0)
                {
                    predicate = predicate.And(x => x.SectionID == val.std.SectionID);
                }
            }

            var Student_result = db.TBLStudents.AsExpandable().Where(predicate).ToList();
            return Student_result;
        }

        public List<ScholarRegister> fillSRListAll(ScholarRegister val)
        {
            int count = 0;
            List<ScholarRegister> list = new List<Models.ScholarRegister>();

            var Student_result = GetFilteredStudentsSRList(val);

            var result = (from st in Student_result
                          join sr in db.tblScholarRegisters on st.ID equals sr.StudentID
                          join stats in db.tblStatus on st.Status equals stats.StatusID
                          select new
                          {
                              sr,
                              st,
                              stats
                          }).ToList();

            foreach (var m in result)
            {
                count++;
                ScholarRegister ct = new Models.ScholarRegister();
                ct.tsr = m.sr;
                ct.std = m.st;

                ct.count = count;
                ct.fullStName = m.st.FirstName + " " + m.st.LastName;
                ct.SDOB = Convert.ToDateTime(m.st.DOB).ToString("MMMM dd,yyyy");
                ct.jDate = Convert.ToDateTime(m.st.JoiningDate).ToString("MMMM dd,yyyy");
                if (m.stats.Status.ToLower() == "active")
                {
                    ct.StatusName = "Active";
                    ct.style = "badge bg-green";
                }
                else
                {
                    ct.StatusName = "InActive";
                    ct.style = "badge bg-orange";
                }

                list.Add(ct);
            }
            return list;
        }

        public List<ScholarRegister> fillSRListToAssignSRno(ScholarRegister val)
        {
            int count = 0;
            List<ScholarRegister> list = new List<Models.ScholarRegister>();

            var Student_result = GetFilteredStudentsSRList(val);

            var result = (from st in Student_result
                          join sr in db.tblScholarRegisters on st.ID equals sr.StudentID
                          join stats in db.tblStatus on st.Status equals stats.StatusID
                          join cl in db.tblCourses on st.ClassID equals cl.Id
                          where string.IsNullOrEmpty(sr.SRno)
                          select new
                          {
                              sr,
                              st,
                              stats,
                              cl
                          }).ToList();

            foreach (var m in result)
            {
                count++;
                ScholarRegister ct = new Models.ScholarRegister();
                ct.tsr = m.sr;
                ct.std = m.st;
                ct.count = count;
                ct.fullStName = m.st.FirstName + " " + m.st.LastName;
                ct.SDOB = Convert.ToDateTime(m.st.DOB).ToString("MMMM dd,yyyy");
                ct.jDate = Convert.ToDateTime(m.st.JoiningDate).ToString("MMMM dd,yyyy");
                ct.ClassName = m.cl.CourseName;
                list.Add(ct);
            }
            return list;
        }

        public List<ScholarRegister> fillSRListToPromoteStudents(ScholarRegister val)
        {
            int count = 0;
            List<ScholarRegister> list = new List<Models.ScholarRegister>();

            var Student_result = GetFilteredStudentsSRList(val);

            //if((val.std.ClassID>0 && val.std.SectionID>0 && !String.IsNullOrEmpty(val.std.AcademicYear) && val.std.AcademicYear!="-1") || !string.IsNullOrEmpty(val.std.SRNo)
            //    || !String.IsNullOrEmpty(val.std.FirstName)|| !String.IsNullOrEmpty(val.std.RegNo))
            //{
            var result = (from st in Student_result
                          join sr in db.tblScholarRegisters on st.ID equals sr.StudentID
                          join stats in db.tblStatus on st.Status equals stats.StatusID
                          //join srd in db.tblScholarRegisterDetails on st.ID equals srd.ID
                          join cl in db.tblCourses on st.ClassID equals cl.Id
                          join sec in db.tblSections on st.SectionID equals sec.Id
                          where !string.IsNullOrEmpty(sr.SRno)
                          select new
                          {
                              sr,
                              st,
                              stats,
                              cl,
                              sec
                          }).ToList();

            foreach (var m in result)
            {
                count++;
                ScholarRegister ct = new Models.ScholarRegister();
                ct.tsr = m.sr;
                ct.std = m.st;
                ct.count = count;
                ct.fullStName = m.st.FirstName + " " + m.st.LastName;
                ct.SDOB = Convert.ToDateTime(m.st.DOB).ToString("MMMM dd,yyyy");
                ct.jDate = Convert.ToDateTime(m.st.JoiningDate).ToString("MMMM dd,yyyy");
                ct.ClassName = m.cl.CourseName;
                list.Add(ct);
            }
            //}


            return list;
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
        //            string ImageFileSTRING = "";
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

        //                    //Guid gB = Guid.NewGuid();
        //                    //string imagenamestring = Convert.ToBase64String(gB.ToByteArray());
        //                    //imagenamestring = imagenamestring.Replace("=", "");
        //                    //imagenamestring = imagenamestring.Replace("+", "");

        //                    string imagenamestring = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + System.IO.Path.GetRandomFileName().Replace(".", string.Empty) + ImageExtensions;
        //                    //  ImageFileSTRING = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString()+  imagenamestring + ImageExtensions;

        //                    ImageFileName = imagenamestring;
        //                    ImageFile = FullImagePath + imagenamestring;
        //                    HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
        //                    sqlHelper obj = new sqlHelper();

        //                    //  ImageFile = DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ImageFile;

        //                    //ImageFileName = ImageFile;
        //                    //ImageFile = FullImagePath + ImageFile;
        //                    //HttpContext.Current.Request.Files["UploadedImage"].SaveAs(Path.Combine(serverPath, ImageFileName));
        //                    //sqlHelper obj = new sqlHelper();
        //                    string fileimage = "http:/" + "/www.smartvidhya.com"+ ImageFile;
        //                    string[] cols = { "LogoPic" };
        //                    object[] vals = { fileimage };
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
        [System.Web.Http.Route("api/StudentApi/SaveDetails")]
        [System.Web.Http.HttpPost]
        public ScholarRegister SaveDetails(ScholarRegister sr)
        {
            try
            {
                if (sr.type == "AssignSRno")
                {
                    sr = SaveSRno(sr);
                }
                else if (sr.type == "StudentPromotion")
                {
                    sr = PromoteDemoteStudents(sr);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                sr.Msg = "Some error!! process unsuccessful";
                sr.ID = -1;
                return sr;
            }

            return sr;
        }

        public ScholarRegister SaveSRno(ScholarRegister sr)
        {
            var tsr = db.tblScholarRegisters.FirstOrDefault(x => x.ID == sr.tsr.ID);
            tsr.SRno = sr.tsr.SRno;
            tsr.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            sr.Msg = "SR No. added for " + sr.fullStName;
            sr.ID = 1;
            return sr;
        }

        public ScholarRegister PromoteDemoteStudents(ScholarRegister sr)
        {
            foreach (var srd in sr.SRdetailsList)
            {
                srd.ActionDate = DateTime.Now;
                srd.Status = true;
                if (srd.AcademicYear > 0 && srd.ClassID > 0 && srd.SectionID > 0 && srd.Student_ID > 0)
                {
                    var exist = db.tblScholarRegisterDetails.Any(x => x.Student_ID == srd.Student_ID && x.ClassID == srd.ClassID
                    && x.SectionID == srd.SectionID && x.AcademicYear == srd.AcademicYear &&
                    x.ActionTaken.ToLower() == srd.ActionTaken.ToLower());
                    if (!exist)
                    {
                        db.tblScholarRegisterDetails.Add(srd);
                    }
                }
            }
            db.SaveChanges();
            sr.Msg = "Students status saved";
            sr.ID = 1;
            return sr;
        }

        [System.Web.Http.Route("api/StudentApi/CheckSRNos")]
        [System.Web.Http.HttpGet]
        public bool CheckSRNos(string srNo)
        {
            var exist = false;
            exist = db.tblScholarRegisters.Any(x => x.SRno.ToLower().Contains(srNo.ToLower()));
            return exist;
        }

        [System.Web.Http.Route("api/StudentApi/getAllCountrybyID")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllCountrybyID(List<string> aa)
        {
            int id = Convert.ToInt32(aa[0]);
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "select * from tblCountry where Status=1 and CountryID='" + id + "' and isdeleted is null";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CountryName"].ToString();
                usr.Id = dr["CountryID"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getAllCountry")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllCountry()
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "select * from tblCountry where Status=1 and isdeleted is null";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CountryName"].ToString();
                usr.Id = dr["CountryID"].ToString();
                usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }



        [System.Web.Http.Route("api/StudentApi/getAllstatebyCountryId")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getAllstatebyCountryId(List<string> id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "select* from  tblState where countryId = " + id[0] + " and Status = 1 and IsDeleted is null order by LOWER(StateName)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["StateName"].ToString();
                usr.Id = dr["StateId"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }


        [System.Web.Http.Route("api/StudentApi/getCityByStateId")]
        [System.Web.Http.HttpPost]
        public CountyMaster[] getCityByStateId(List<string> id)
        {
            List<CountyMaster> list = new List<CountyMaster>();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "select Id,CityName from tblCity where Status=1 and StateId=" + id[0] + "";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CountyMaster usr = new CountyMaster();
                usr.Name = dr["CityName"].ToString();
                usr.Id = dr["Id"].ToString();
                //  usr.Status = bool.Parse(dr["Status"].ToString());
                list.Add(usr);
            }
            con.Close();
            return list.ToArray();
        }

        [System.Web.Http.Route("api/StudentApi/checkOldPwdOfStudent")]
        [System.Web.Http.HttpPost]
        public string checkOldPwdOfStudent(ChangePwd check)
        {
            sqlHelper obj = new sqlHelper();

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(check.OldPwd));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilderPPWD = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilderPPWD.Append(result[i].ToString("x2"));
            }

            string Stuoldpassword = strBuilderPPWD.ToString();


            string Pwd = obj.ExecuteScaler("select * from TBLStudent where SUserID='" + check.USerID + "' and SPwd='" + Stuoldpassword + "' and SchoolID='" + check.SchoolID + "' and ID='" + check.Id + "' ");
            if (!string.IsNullOrEmpty(Pwd))
            {
                return "";
            }
            else
            {
                return "Please Enter Correct Pwd";
            }

        }

        [System.Web.Http.Route("api/StudentApi/changeStudentPWd")]
        [System.Web.Http.HttpPost]
        public string changeStudentPWd(ChangePwd check)
        {
            sqlHelper obj = new sqlHelper();

            string Pwd = EmployeeDetails.changeStudentPWd(check);
            if (!string.IsNullOrEmpty(Pwd))
            {

                return Pwd;
            }
            else
            {
                return "Error Occured";
            }

        }

        [System.Web.Http.Route("api/StudentApi/checkOldPwdOfParents")]
        [System.Web.Http.HttpPost]
        public string checkOldPwdOfParents(ChangePwd check)
        {
            sqlHelper obj = new sqlHelper();

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(check.OldPwd));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilderPPWD = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilderPPWD.Append(result[i].ToString("x2"));
            }

            string Stuoldpassword = strBuilderPPWD.ToString();


            string Pwd = obj.ExecuteScaler("select * from TBLStudent where PUserID='" + check.USerID + "' and PPwd='" + Stuoldpassword + "' and SchoolID='" + check.SchoolID + "' and ID='" + check.Id + "' ");
            if (!string.IsNullOrEmpty(Pwd))
            {
                return "";
            }
            else
            {
                return "Please Enter Correct Pwd";
            }

        }

        //
        [System.Web.Http.Route("api/StudentApi/LoadStudentAttendence")]
        [System.Web.Http.HttpPost]
        public string LoadStudentAttendence(StudentATT ATT)
        {
            int SchoolID = Convert.ToInt32(ATT.SchoolID);
            string StartDate1 = Convert.ToDateTime(ATT.StartDate).ToString("yyyy/MM/dd");
            string EndDate1 = Convert.ToDateTime(ATT.EndDate).ToString("yyyy/MM/dd");
            int ClassTeacherID = Convert.ToInt32(ATT.EmpId);

            DateTime StartDate = Convert.ToDateTime(EndDate1);
            DateTime EndDate = Convert.ToDateTime(StartDate1);


            string AttendenceTime = DateTime.Now.ToString("HH: mm:ss tt");
            string TodayDate1 = DateTime.Now.ToString("yyyy/MM/dd");
            DateTime todatdate = Convert.ToDateTime(TodayDate1);


            string AA = "";
            int count = 0;
            var getmachines = db.tblAttendenceMachineMasters.Where(ma => ma.SchoolID == SchoolID && ma.IsActice == true && ma.machinetype == "Student").Select(y => y.MachineNo).ToList();
            if (getmachines != null)
            {
                sqlHelper obj = new sqlHelper();
                List<DateTime> allDates = new List<DateTime>();
                for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
                {
                    getstudents details = new getstudents();
                    string pdate = date.ToString("yyyy/MM/dd");

                    DateTime punchdate = Convert.ToDateTime(pdate);
                    var getdetails = db.tblClassTeacherAllocations.Where(cl => cl.intEmpID == ClassTeacherID && cl.Status == 1).FirstOrDefault();
                    details.sectionID = getdetails.SectionID;
                    details.classId = getdetails.ClassID;
                    var Cacademicyear = db.tblAcademicYears.Where(ac => ac.CurrActive == true && ac.SchoolID == SchoolID).FirstOrDefault();
                    details.CacademicyearID = Cacademicyear.ID.ToString();
                    var GetStudents = db.TBLStudents.Where(s => s.ClassID == details.classId && s.SectionID == details.sectionID && s.AcademicYear == details.CacademicyearID && s.IsDeleted == null && s.Status == 3 && s.SchoolID == SchoolID).ToList();
                    foreach (var StudentsDetails in GetStudents)
                    {
                        TBLStudent stu = new TBLStudent();
                        stu.ID = StudentsDetails.ID;
                        stu.BiometricID = StudentsDetails.BiometricID;
                        var MachineRawPunch = db.Tran_MachineRawPunch.Where(mp => mp.CardNo == stu.BiometricID && mp.PunchDatetime.Year == punchdate.Year && mp.PunchDatetime.Month == punchdate.Month && mp.PunchDatetime.Day == punchdate.Day && getmachines.Contains(mp.MachineNo)).FirstOrDefault();
                        if (MachineRawPunch != null)
                        {
                            var checkattendence = db.tblStudentAttendences.Where(sa => sa.StudentId == stu.ID && sa.AttendenceDate.Value.Year == punchdate.Year && sa.AttendenceDate.Value.Month == punchdate.Month && sa.AttendenceDate.Value.Day == punchdate.Day).FirstOrDefault();
                            if (checkattendence == null)
                            {
                                //present
                                count++;
                                tblStudentAttendence satt = new tblStudentAttendence();
                                satt.StudentId = stu.ID;
                                satt.AttendenceType = "Present";
                                satt.AttendenceDate = punchdate;
                                satt.TeacherId = ATT.EmpId;
                                satt.ClassId = details.classId;
                                satt.SectionId = details.sectionID;
                                satt.DateCreated = DateTime.Now;
                                satt.SchoolID = Convert.ToInt32(ATT.SchoolID);
                                satt.IsBiometric = true;
                                db.tblStudentAttendences.Add(satt);
                                db.SaveChanges();
                                string attendencedate1 = Convert.ToDateTime(satt.AttendenceDate).ToString("yyyy/MM/dd");
                                DateTime attendencedatee = Convert.ToDateTime(attendencedate1);
                                if (todatdate == attendencedatee)
                                {



                                    string SMSmobileNo = obj.ExecuteScaler("select SMSmobileNo from TBLStudent where ID='" + StudentsDetails.ID + "'");
                                    string Studentname = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from TBLStudent where ID='" + StudentsDetails.ID + "'");
                                    string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + StudentsDetails.SchoolID + "'");
                                    string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");
                                    if (Checksmsservice != null)
                                    {
                                        string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");
                                        string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");

                                        //string authKey = "12920AvJSHCkt5d4cfe2c";
                                        string authKey = GetauthKey;
                                        //Multiple mobiles numbers separated by comma
                                        string mobileNumber = SMSmobileNo;
                                        //Sender ID,While using route4 sender id should be 6 characters long.
                                        string senderId = GetsenderId;
                                        //Your message to send, Add URL encoding here.
                                        StringBuilder st = new StringBuilder();
                                        st.AppendLine("Hi Sir/mam,");
                                        st.AppendLine("Student Name :" + Studentname);
                                        st.AppendLine("Student Attendence Status :" + satt.AttendenceType);
                                        st.AppendLine("Attendence Date :" + satt.AttendenceDate);
                                        st.AppendLine("Attendence Time :" + AttendenceTime);
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
                            else
                            {
                                if (checkattendence.IsBiometric != false)
                                {
                                    // update
                                    count++;
                                    tblStudentAttendence satt = new tblStudentAttendence();
                                    checkattendence.StudentId = stu.ID;
                                    checkattendence.AttendenceType = "Present";
                                    checkattendence.AttendenceDate = punchdate;
                                    checkattendence.TeacherId = ATT.EmpId;
                                    checkattendence.ClassId = details.classId;
                                    checkattendence.SectionId = details.sectionID;
                                    checkattendence.DateCreated = DateTime.Now;
                                    checkattendence.IsBiometric = true;
                                    checkattendence.SchoolID = Convert.ToInt32(ATT.SchoolID);
                                    db.SaveChanges();
                                    string attendencedate1 = Convert.ToDateTime(satt.AttendenceDate).ToString("yyyy/MM/dd");
                                    DateTime attendencedatee = Convert.ToDateTime(attendencedate1);
                                    if (todatdate == attendencedatee)
                                    {


                                        string SMSmobileNo = obj.ExecuteScaler("select SMSmobileNo from TBLStudent where ID='" + StudentsDetails.ID + "'");
                                        string Studentname = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from TBLStudent where ID='" + StudentsDetails.ID + "'");
                                        string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + StudentsDetails.SchoolID + "'");
                                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");
                                        if (Checksmsservice != null)
                                        {
                                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");
                                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");

                                            string authKey = GetauthKey;
                                            //Multiple mobiles numbers separated by comma
                                            string mobileNumber = SMSmobileNo;
                                            //Sender ID,While using route4 sender id should be 6 characters long.
                                            string senderId = GetsenderId;
                                            //Your message to send, Add URL encoding here.
                                            StringBuilder st = new StringBuilder();
                                            st.AppendLine("Hi Sir/mam,");
                                            st.AppendLine("Student Name :" + Studentname);
                                            st.AppendLine("Student Attendence Status :" + checkattendence.AttendenceType);
                                            st.AppendLine("Attendence Date :" + checkattendence.AttendenceDate);
                                            st.AppendLine("Attendence Time :" + AttendenceTime);
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
                            }
                        }
                        else
                        {
                            var checkattendence = db.tblStudentAttendences.Where(sa => sa.StudentId == stu.ID && sa.AttendenceDate.Value.Year == punchdate.Year && sa.AttendenceDate.Value.Month == punchdate.Month && sa.AttendenceDate.Value.Day == punchdate.Day).FirstOrDefault();
                            if (checkattendence == null)
                            {
                                //absent
                                count++;
                                tblStudentAttendence satt = new tblStudentAttendence();
                                satt.StudentId = stu.ID;
                                satt.AttendenceType = "Absent";
                                satt.AttendenceDate = punchdate;
                                satt.TeacherId = ATT.EmpId;
                                satt.ClassId = details.classId;
                                satt.SectionId = details.sectionID;
                                satt.DateCreated = DateTime.Now;
                                satt.SchoolID = Convert.ToInt32(ATT.SchoolID);
                                satt.IsBiometric = true;
                                db.tblStudentAttendences.Add(satt);
                                db.SaveChanges();




                                string attendencedate1 = Convert.ToDateTime(satt.AttendenceDate).ToString("yyyy/MM/dd");
                                DateTime attendencedatee = Convert.ToDateTime(attendencedate1);
                                if (todatdate == attendencedatee)
                                {


                                    string SMSmobileNo = obj.ExecuteScaler("select SMSmobileNo from TBLStudent where ID='" + StudentsDetails.ID + "'");
                                    string Studentname = obj.ExecuteScaler("select FirstName+' '+MiddleName+' '+lastName from TBLStudent where ID='" + StudentsDetails.ID + "'");
                                    string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + StudentsDetails.SchoolID + "'");

                                    string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");
                                    if (Checksmsservice != null)
                                    {
                                        string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");
                                        string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + StudentsDetails.SchoolID + "' and  active=1");

                                        string authKey = GetauthKey;
                                        //Multiple mobiles numbers separated by comma
                                        string mobileNumber = SMSmobileNo;
                                        //Sender ID,While using route4 sender id should be 6 characters long.
                                        string senderId = GetsenderId;
                                        //Your message to send, Add URL encoding here.
                                        StringBuilder st = new StringBuilder();
                                        st.AppendLine("Hi Sir/mam,");
                                        st.AppendLine("Student Name :" + Studentname);
                                        st.AppendLine("Student Attendence Status :" + satt.AttendenceType);
                                        st.AppendLine("Attendence Date :" + satt.AttendenceDate);
                                        st.AppendLine("Attendence Time :" + AttendenceTime);
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
                            //else
                            //{
                            //manual attendence
                            //}
                        }


                    }


                }
                if (count > 0)
                {
                    return AA = "Attendence data Loaded";
                }
                else
                {
                    return AA = "No attendence data found";
                }
            }
            else
            {
                return AA = "No machine assinged to School";
            }



        }

        [System.Web.Http.Route("api/StudentApi/GetClassName")]
        [System.Web.Http.HttpPost]

        public getclassSectionDetails GetClassName(getclassSection values)
        {
            getclassSectionDetails getvalues = new getclassSectionDetails();
            try
            {

                int SchoolIDD = (values.SchoolID);
                int EmployeeIDD = (values.EmployeeID);

                var check = db.tblClassTeacherAllocations.Where(x => x.SchoolID == SchoolIDD && x.intEmpID == EmployeeIDD).FirstOrDefault();
                if (check != null)
                {
                    getvalues.ClassID = check.ClassID;
                    getvalues.ClassName = db.tblCourses.FirstOrDefault(x => x.Id == getvalues.ClassID && x.SchoolID == SchoolIDD && x.IsDeleted == null).CourseName;
                    getvalues.SectionID = check.SectionID;
                    getvalues.SectionName = db.tblSections.FirstOrDefault(x => x.Id == getvalues.SectionID && x.SchoolID == SchoolIDD && x.IsDeleted == null).SectionName;
                    getvalues.message = "1";


                }

            }

            catch (Exception ex)
            {
                getvalues.message = "0";
            }





            return getvalues;


        }
        [System.Web.Http.Route("api/StudentApi/getacademicyearr")]
        [System.Web.Http.HttpPost]
        public getAcademicYear[] getacademicyearr(List<string> values)
        {
            List<getAcademicYear> list = new List<getAcademicYear>();

            //{
            try
            {
                int SchoolIDD = Convert.ToInt32(values[0]);
                //{

                getAcademicYear manual = new getAcademicYear();
                manual.AcademicYear = "-----Select-----";
                manual.ID = -1;
                list.Add(manual);

                //}
                var getacademic = db.tblAcademicYears.Where(x => x.SchoolID == SchoolIDD && x.IsDeleted == null && x.Status == true).OrderBy(x => x.ID).ToList();
                if (getacademic != null)
                {

                    foreach (var years in getacademic)
                    {
                        getAcademicYear AY = new getAcademicYear();
                        AY.AcademicYear = years.DateFrom.Year.ToString() + "-" + years.DateTo.ToString("yy");
                        AY.ID = years.ID;

                        list.Add(AY);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }




            //}


            return list.ToArray();
        }
        public partial class getstudentlistofclass
        {
            public int Sno { get; set; }
            public int ID { get; set; }
            public int SchoolID { get; set; }
            public int ClassID { get; set; }
            public int SectionID { get; set; }
            public string AcademicYear { get; set; }
            public string StudentFirstLastName { get; set; }
            public string StudentRegistrationNo { get; set; }
            public string ClassName { get; set; }
            public string SectionName { get; set; }
            public string AcademicYearName { get; set; }
            public string StudentImage { get; set; }
            public int Stream { get; set; }
        }

        [System.Web.Http.Route("api/StudentApi/getStudentListofClass")]
        [System.Web.Http.HttpPost]
        public getstudentlistofclass[] getStudentListofClass(getstudentlistofclass val)
        {
            List<getstudentlistofclass> list = new List<getstudentlistofclass>();
            //{
            try
            {
                var StudentsList = (from stu in db.TBLStudents
                                    where stu.SchoolID == val.SchoolID &&
                                    stu.ClassID == val.ClassID &&
                                    stu.SectionID == val.SectionID && stu.Status == 3 && stu.IsDeleted == null
                                    select new
                                    {
                                        model = stu

                                    }

                                    ).ToList();

                if (val.AcademicYear != "0")
                {

                    StudentsList = StudentsList.Where(x => x.model.AcademicYear == val.AcademicYear).ToList();
                }
                if (val.StudentFirstLastName != "")
                {
                    StudentsList = StudentsList.Where(x => (x.model.FirstName + "" + x.model.MiddleName + "" + x.model.LastName).Contains(val.StudentFirstLastName)).ToList();
                }
                if (val.StudentRegistrationNo != "")
                {
                    StudentsList = StudentsList.Where(x => (x.model.RegNo).Contains(val.StudentRegistrationNo)).ToList();
                }
                int sno = 0;
                foreach (var values in StudentsList)
                {
                    sno++;
                    getstudentlistofclass avn = new getstudentlistofclass();
                    avn.Sno = sno;
                    avn.ID = values.model.ID;
                    avn.ClassName = val.ClassName;
                    avn.SectionName = val.SectionName;
                    avn.AcademicYearName = val.AcademicYearName;
                    avn.ClassID = values.model.ClassID;
                    avn.SectionID = values.model.SectionID;
                    avn.AcademicYear = values.model.AcademicYear;
                    avn.StudentFirstLastName = values.model.FirstName + "" + values.model.MiddleName + "" + values.model.LastName;
                    avn.StudentRegistrationNo = values.model.RegNo;
                    avn.StudentImage = values.model.PicPath;
                    if (avn.StudentImage == "" || avn.StudentImage == null) { avn.StudentImage = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg"; } else { avn.StudentImage = values.model.PicPath; }
                    avn.Stream = values.model.StreamID;
                    list.Add(avn);
                }
                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;

            }

            //}






        }
        public partial class StudentclassHistory
        {
            public int ID { get; set; }
            public Nullable<int> StudentID { get; set; }
            public string StudentName { get; set; }
            public string ClassID { get; set; }
            public string SectionID { get; set; }
            public Nullable<int> SchoolID { get; set; }
            public string ClassName { get; set; }
            public string SectionName { get; set; }
            public string Academicyear { get; set; }
            public string Result { get; set; }
            public Nullable<System.DateTime> DateOfAdmission { get; set; }
            public Nullable<System.DateTime> PromotedDate { get; set; }
        }
        [System.Web.Http.Route("api/StudentApi/PromoteStudent")]
        [System.Web.Http.HttpPost]
        public string PromoteStudent(GETPromoteStudentParameter[] values)
        {
            int count = 0;
            string msg = "";
            foreach (var a in values)
            {
                count++;
                Student stu = new Student();
                var updatestu = db.TBLStudents.Where(s => s.ID == a.StudentID && s.SchoolID == a.SchoolID).FirstOrDefault();
                if (updatestu != null)
                {
                    updatestu.ClassID = Convert.ToInt32(a.ClassID);
                    updatestu.SectionID = Convert.ToInt32(a.SectionID);
                    updatestu.AcademicYear = a.Academicyear;
                    if (a.StreamID != "-2")
                    {
                        updatestu.StreamID = Convert.ToInt32(a.StreamID);
                    }
                    var StudentHistoryDetails = db.tblStudentdetailsClassToClasses.Where(s => s.SchoolID == a.SchoolID && s.StudentID == a.StudentID).ToList();
                    if (StudentHistoryDetails.Count != 0)
                    {

                        tblStudentdetailsClassToClass His = new tblStudentdetailsClassToClass();
                        His.StudentID = a.StudentID;
                        His.ClassID = a.CurrentClassID;
                        His.SectionID = a.CurrentSectionID;
                        His.Academicyear = a.CurrentAcademicYear;
                        His.StudentName = a.StudentName;
                        His.ClassName = a.ClassName;
                        His.SectionName = a.SectionNmae;
                        His.SchoolID = a.SchoolID;
                        var aaval = (from val in db.tblStudentdetailsClassToClasses
                                     where val.SchoolID == a.SchoolID
                                      && val.StudentID == a.StudentID
                                     select new
                                     {
                                         valuee = val
                                     }
                                  ).Max(c => c.valuee.ID);
                        string DateOfAd = db.tblStudentdetailsClassToClasses.FirstOrDefault(s => s.ID == aaval).PromotedDate.ToString();
                        His.DateOfAdmission = Convert.ToDateTime(DateOfAd);
                        His.PromotedDate = DateTime.Now;
                        His.Result = a.Result;
                        His.Stream = a.StreamID;
                        db.tblStudentdetailsClassToClasses.Add(His);
                    }
                    else if (StudentHistoryDetails.Count == 0)
                    {
                        tblStudentdetailsClassToClass His = new tblStudentdetailsClassToClass();
                        His.StudentID = a.StudentID;
                        His.ClassID = a.CurrentClassID;
                        His.SectionID = a.CurrentSectionID;
                        His.Academicyear = a.CurrentAcademicYear;
                        His.StudentName = a.StudentName;
                        His.ClassName = a.ClassName;
                        His.SectionName = a.SectionNmae;
                        His.SchoolID = a.SchoolID;
                        His.DateOfAdmission = updatestu.JoiningDate;
                        His.PromotedDate = DateTime.Now;
                        His.Result = a.Result;
                        His.Stream = a.StreamID;
                        db.tblStudentdetailsClassToClasses.Add(His);
                    }
                    db.SaveChanges();

                    tblStudentFee1 std = new tblStudentFee1();
                    std.StudentId = a.StudentID;
                    std.AcademicYear = a.Academicyear;
                    std.ClassId = Convert.ToInt32(a.ClassID);
                    std.SectionId = Convert.ToInt32(a.SectionID);
                    std.SchoolId = a.SchoolID;
                    db.tblStudentFee1.Add(std);
                    db.SaveChanges();





                }





            }
            if (count > 0)
            {
                msg = "Student Promoted Successfully";
            }
            else
            {
                msg = "No Student Found to Promote";

            }

            return msg;

        }

        [System.Web.Http.Route("api/StudentApi/getstreambyclass")]
        [System.Web.Http.HttpPost]
        public togetstream[] getstreambyclass(List<string> values)
        {
            List<togetstream> list = new List<togetstream>();
            try
            {
                int SchoolID = Convert.ToInt32(values[0]);
                int ClassID = Convert.ToInt32(values[1]);

                var Checkstream = db.tblStreams.Where(a => a.SchoolID == SchoolID && a.Class == ClassID && a.IsDeleted == null && a.Status == true).ToList();
                if (Checkstream.Count != 0)
                {
                    togetstream item1 = new togetstream();
                    item1.ID = -1;
                    item1.Stream = "----Select----";
                    list.Add(item1);
                    foreach (var str in Checkstream)
                    {
                        togetstream item = new togetstream();
                        item.ID = str.Id;
                        item.Stream = str.StreamName;
                        list.Add(item);
                    }
                }
                else
                {
                    togetstream item1 = new togetstream();
                    item1.ID = -2;
                    item1.Stream = "----Empty----";
                    list.Add(item1);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list.ToArray();

        }



    }
}
