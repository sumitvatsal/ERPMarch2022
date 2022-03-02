using LinqKit;
using SchoolErp.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using schoolERP_BLL;
namespace SchoolErp.Controllers.WebApi
{
    public class HWapiController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/HWapi/AssignHW")]
        [System.Web.Http.HttpPost]
        public Assignment AssignHW()
        {
            var model = new Assignment();
            try
            {
                int iUploadedCnt = 0;
                //var type = "";
              //  var docIDs = "";
                //var docTex = "";
                string[] d = { };
                string[] dT = { };
                int dcount = 0;
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                string relPath = "";
                string doc_path = "";

                NameValueCollection nvc = HttpContext.Current.Request.Form;

                var hw = new tblHomeAssignment();
                hw.Status = true;
                // iterate through and map to strongly typed model
                foreach (string kvp in nvc.AllKeys)
                {
                    if (kvp == "ID")
                    {
                        hw.ID = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "class")
                    {
                        hw.Class = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "section")
                    {
                        hw.Section = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "academicYr")
                    {
                        hw.AcademicYear = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "name")
                    {
                        hw.AssignmentNm = nvc[kvp];
                    }
                    else if (kvp == "desc")
                    {
                        hw.AssignmentDesc = nvc[kvp];
                    }
                    else if (kvp == "sub_Dt")
                    {
                        DateTime dtime = DateTime.ParseExact(nvc[kvp], "dd/MM/yyyy", null);
                        hw.SubmitDt = dtime;
                    }
                    else if (kvp == "marks")
                    {
                        hw.Marks = nvc[kvp];
                    }
                    else if (kvp == "subject")
                    {
                        hw.Subject = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "empID")
                    {
                        hw.HW_givenBy = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "SchoolID")
                    {
                        hw.SchoolID = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "path")
                    {
                        doc_path = nvc[kvp];
                    }
                }

                if(hw.Class!=-1 && hw.Section!=-1)
                 {
                    var cls = (from c in db.tblCourses
                               where c.Id == hw.Class
                               from s in db.tblSections
                               where s.ClassId == hw.Class && s.Id == hw.Section && s.IsDeleted==null && c.IsDeleted==null
                               select new { c, s }).SingleOrDefault();
                    if(cls.c !=null && cls.s!=null)
                    {
                        var sub = db.tblSubjects.SingleOrDefault(x => x.ID == hw.Subject);
                        model.course = cls.c.CourseName + "_" + cls.s.SectionName;
                        model.subject = sub.Subject;
                        relPath = "/Images/Assignments/" + model.course + "/"+sub.Subject+"/";
                        sPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + relPath);
                        Directory.CreateDirectory(sPath);
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
                        //if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                        //{
                        // SAVE THE FILES IN THE FOLDER.
                        int _min = 1000;
                        int _max = 9999;
                        Random _rdm = new Random();
                        int code = _rdm.Next(_min, _max);
                        string document = sPath + code+ hpf.FileName ;
                            doc_path = relPath + code+ hpf.FileName;
                            hw.FilePath = "http:/"+"/www.smartvidhya.com"+ doc_path;
                            
                                                    
                            if (File.Exists(document))
                            {
                                File.Delete(document);
                            }
                            else
                            {}
                            hpf.SaveAs(document);
                            dcount = dcount + 1;

                        
                        iUploadedCnt = iUploadedCnt + 1;

                    }
                }
                if (hw.ID == 0)
                {
                    hw.HW_Dt = DateTime.Now;
                    db.tblHomeAssignments.Add(hw);
                }
                else
                {
                    var hw_edit = db.tblHomeAssignments.SingleOrDefault(x => x.ID == hw.ID);
                    hw_edit.FilePath ="http:/"+"/www.smartvidhya.com" +  doc_path;
                    hw_edit.AssignmentDesc = hw.AssignmentDesc;
                    hw_edit.Marks = hw.Marks;
                    hw_edit.SubmitDt = hw.SubmitDt;
                }
                hw.HW_Dt = DateTime.Now;
                db.SaveChanges();
                model.ID = hw.ID;
                model.hw = hw;
                sendAssignmentSMStoParent(model);
            }
            catch (Exception e) { model.ID = -1; throw e; }
            return model;
        }

        public string sendAssignmentSMStoParent(Assignment a)
        {
            try
            {
                var result = db.TBLStudents.Where(x => x.ClassID == a.hw.Class && x.SectionID == a.hw.Section).ToList();

                var emp = db.tblEmployees.SingleOrDefault(x=>x.Id==a.hw.HW_givenBy);
                foreach(var r in result)
                {
                    if (r.SMSmobileNo != null)
                    {
                        string hwdt= ((DateTime)a.hw.HW_Dt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string subDt= ((DateTime)a.hw.SubmitDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string msg = "Dear Parent,Your ward has got assignment in "+a.subject+". Title:'" +a.hw.AssignmentNm+"' on "+ hwdt + ". Submition Date:"+ subDt + ". Assigned by "+emp.FirstName+"("+emp.Empcode+")";
                        
                        //string strUrl = "http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=78978f2548b73f5edb1db725fcf65127&message=" + msg + "&senderId=DEMOOS&routeId=3&mobileNos=" + r.SMSmobileNo + "&smsContentType=english";
                        //// Create a request object  
                        //WebRequest request = HttpWebRequest.Create(strUrl);
                        //// Get the response back  
                        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        //Stream s = (Stream)response.GetResponseStream();
                        //StreamReader readStream = new StreamReader(s);
                        //string dataString = readStream.ReadToEnd();
                        //response.Close();
                        //s.Close();
                        //readStream.Close();
                    }
                }
            }
            catch(Exception ex)
            { throw ex; }
          
            return "Sms Send Successfully";
        }

        [System.Web.Http.Route("api/HWapi/getAssignmentListapp")]
        [System.Web.Http.HttpPost]
        public Assignmentapp getAssignmentListapp(Parameters param)
        {
            int count = 0;
            int avn = 0;
            int a = 0;//.ToString();
            string type = param.val;
            Assignmentapp obj = new Assignmentapp();
            List<Assignment2> list = new List<Models.Assignment2>();

            string name = param.val4;
            try
            {

                var predicate = PredicateBuilder.True<tblHomeAssignment>();

                if (param.val1 == null || param.val1 == "-1" || param.val1 == "")
                {
                    obj.status = false;
                    obj.message = " Please Enter Academic Year";
                    obj.data = list;
                }
                else if (param.val2 == "-1" || param.val2 == "" || param.val2 == null)
                {
                    obj.status = false;
                    obj.message = "Please Enter Class";
                    obj.data = list;
                }
                else if (param.val3 == "-1" || param.val3 == "" || param.val2 == null)
                {
                    obj.status = false;
                    obj.message = "Please Enter Section";
                    obj.data = list;
                }
                else if (param.val9 == "" || param.val9 == "0" || param.val9 == "-1" || param.val9 == null)
                {
                    obj.status = false;
                    obj.message = "Please Enter SchoolID";
                    obj.data = list;
                }
                else
                {

                    if (param.val9 != "" && param.val9 != "0" && param.val9 != "-1")
                    {
                        int SchoolID = Convert.ToInt32(param.val9);
                        predicate = predicate.And(x => x.SchoolID == SchoolID);
                    }
                    if (param.val1 != "-1" && param.val1 != "")
                    {
                        int year = Convert.ToInt32(param.val1);
                        predicate = predicate.And(x => x.AcademicYear == year);
                    }
                    if (param.val2 != "-1" && param.val2 != "")
                    {
                        int classID = Convert.ToInt32(param.val2);
                        predicate = predicate.And(x => x.Class == classID);
                    }
                    if (param.val3 != "-1" && param.val3 != "")
                    {
                        int SecID = Convert.ToInt32(param.val3);
                        predicate = predicate.And(x => x.Section == SecID);
                    }
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        predicate = predicate.And(x => x.AssignmentNm.ToUpper().Contains(name.ToUpper()));
                    }
                    if (param.val6 != "-1" && param.val6 != "" && param.val6 != null)
                    {
                        int subID = Convert.ToInt32(param.val6);
                        predicate = predicate.And(x => x.Subject == subID);
                    }
                    if (!string.IsNullOrWhiteSpace(param.val5))
                    {
                        DateTime fromDt = Convert.ToDateTime(param.val5);
                        predicate = predicate.And(x => x.SubmitDt == fromDt);
                    }
                    if (!string.IsNullOrWhiteSpace(param.val7))
                    {
                        DateTime hwDt = Convert.ToDateTime(param.val7);
                        predicate = predicate.And(x => x.HW_Dt == hwDt);
                    }

                    predicate = predicate.And(x => x.Status == true);
                    var result = db.tblHomeAssignments.AsExpandable().Where(predicate).ToList();

                    //else  if (type == "Student" && a != 0)
                    //  {
                    //      var std = db.TBLStudents.SingleOrDefault(x => x.ID == a);

                    //      result = result.Where(x => x.Class == std.ClassID && x.Section == std.SectionID).ToList();
                    //  }
                    //  else if (type == "Teacher" && a != 0)
                    //  {
                    //      result = result.Where(x => x.HW_givenBy == a).ToList();
                    //  }

                    foreach (var m in result)
                    {
                        count++;
                        Assignment2 ct = new Models.Assignment2();
                        ct.hw = new tblHomeAssignment();
                        ct.ID = count;
                        ct.hw = m;

                        var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear);
                        ct.year = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

                        var rslt = (from c in db.tblSections
                                    join s in db.tblCourses on c.ClassId equals s.Id
                                    where s.Id == m.Class && c.Id == m.Section
                                    select new
                                    {
                                        crs = s,
                                        sec = c
                                    }).SingleOrDefault();
                        ct.course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
                        ct.subDt = ((DateTime)m.SubmitDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.DateTimeCreated = ((DateTime)m.HW_Dt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ct.DateCreated = Convert.ToDateTime(m.HW_Dt).ToString("dd MMMM yyyy ");
                        if (m.FilePath == null || m.FilePath == "")
                        {
                            ct.style = "";
                        }
                        else
                        {
                            ct.style = "fa fa-download fa-lg text-green";
                        }

                        var sub = db.tblSubjects.SingleOrDefault(x => x.ID == m.Subject);
                        ct.subject = sub.Subject;

                        var emp = db.tblEmployees.SingleOrDefault(x => x.Id == m.HW_givenBy);
                        ct.teacher = emp.FirstName + " " + emp.LastName;

                        list.Add(ct);

                        avn++;

                    }
                    if (avn == 0)
                    {
                        obj.status = false;
                        obj.message = "Record Not Found";
                        obj.data = list;
                    }
                    else if (avn != 0)
                    {
                        obj.status = true;
                        obj.message = "Sucess";
                        obj.data = list;
                    }
                }





            }

            catch
            {
                obj.status = false;
                obj.message = "Something Went Wrong";
                obj.data = list;
            }

            return obj;

        }




        [System.Web.Http.Route("api/HWapi/getAssignmentList")]
        [System.Web.Http.HttpPost]
        public Assignment[] getAssignmentList(Parameters param)
        {
            int count = 0;
            int a = 0;//.ToString();
            string type = param.val;
            List<Assignment> list = new List<Models.Assignment>();   
            
            string name = param.val4;
            try
            {

               
                if(param.val8 != "")
                { a = Convert.ToInt32(param.val8); }
                var predicate = PredicateBuilder.True<tblHomeAssignment>();

                if (param.val9 != "" && param.val9 != "0" && param.val9 != "-1")
                {
                    int SchoolID = Convert.ToInt32(param.val9);
                    predicate = predicate.And(x => x.SchoolID == SchoolID);
                }
                if (param.val1 != "-1" && param.val1 != "")
                {
                    int year = Convert.ToInt32(param.val1);
                    predicate = predicate.And(x => x.AcademicYear == year);
                }
                if (param.val2 != "-1" && param.val2 != "")
                {
                    int classID = Convert.ToInt32(param.val2);
                    predicate = predicate.And(x => x.Class == classID);
                }
                if (param.val3 != "-1" && param.val3 != "")
                {
                    int SecID = Convert.ToInt32(param.val3);
                    predicate = predicate.And(x => x.Section == SecID);
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    predicate = predicate.And(x => x.AssignmentNm.ToUpper().Contains(name.ToUpper()));
                }
                if (param.val6 != "-1" && param.val6 != "" )
                {
                    int subID = Convert.ToInt32(param.val6);
                    predicate = predicate.And(x => x.Subject == subID);
                }
                if (!string.IsNullOrWhiteSpace(param.val5))
                {
                    DateTime fromDt = Convert.ToDateTime(param.val5);
                    predicate = predicate.And(x => x.SubmitDt == fromDt);
                }
                if (!string.IsNullOrWhiteSpace(param.val7))
                {
                    DateTime hwDt = Convert.ToDateTime(param.val7);
                    predicate = predicate.And(x => x.HW_Dt == hwDt);
                }
                predicate = predicate.And(x => x.Status == true );

                var result = db.tblHomeAssignments.AsExpandable().Where(predicate).ToList();

                if (type == "Student" && a!=0)
                {
                    var std = db.TBLStudents.SingleOrDefault(x => x.ID == a);

                    result = result.Where(x => x.Class == std.ClassID && x.Section == std.SectionID).ToList();
                }
                else if (type == "Teacher" && a != 0)
                {
                    result = result.Where(x => x.HW_givenBy == a ).ToList();
                }

                foreach (var m in result)
                {
                    count++;
                    Assignment ct = new Models.Assignment();
                    ct.hw = new tblHomeAssignment();
                    ct.ID = count;
                    ct.hw = m;

                    var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear );
                    ct.year = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");
                    
                    var rslt = (from c in db.tblSections
                                join s in db.tblCourses on c.ClassId equals s.Id
                                where s.Id == m.Class && c.Id == m.Section
                                select new
                                {
                                    crs = s,
                                    sec = c
                                }).SingleOrDefault();
                    ct.course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
                    ct.subDt = ((DateTime)m.SubmitDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ct.hwDt = ((DateTime)m.HW_Dt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if(m.FilePath==null || m.FilePath =="")
                    {
                        ct.style = "";
                    }
                    else
                    {
                        ct.style = "fa fa-download fa-lg text-green";
                    }

                    var sub = db.tblSubjects.SingleOrDefault(x=>x.ID==m.Subject);
                    ct.subject = sub.Subject;

                    var emp = db.tblEmployees.SingleOrDefault(x=>x.Id==m.HW_givenBy);
                    ct.teacher = emp.FirstName + " " + emp.LastName;
                    

                    list.Add(ct);
                }
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
            return list.ToArray();

        }

        [System.Web.Http.Route("api/Syllabus/GetSyllabus")]
        [System.Web.Http.HttpPost]
        public syllabus GetSyllabus(List<string> val)
        {
            syllabus ct = new Models.syllabus();
            try
            {
                long id = Convert.ToInt32(val[0]);
                var result = db.tblSyllabus.SingleOrDefault(x => x.ID == id);
                ct.hw = new tblSyllabu();
                ct.hw = result;
                ct.ID = result.ID;
              
            }
            catch (Exception ex) { ct.ID = -1; throw ex; }
            return ct;
        }


        [System.Web.Http.Route("api/HWapi/GetAssignment")]
        [System.Web.Http.HttpPost]
        public Assignment GetAssignment(List<string> val)
        {
            Assignment ct = new Models.Assignment();
            try
            {
                long id = Convert.ToInt32(val[0]);
                var result = db.tblHomeAssignments.SingleOrDefault(x=>x.ID==id);
                ct.hw = new tblHomeAssignment();
                ct.hw = result;
                ct.ID = result.ID;
                ct.subDt = ((DateTime)result.SubmitDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex) { ct.ID = -1; throw ex; }
            return ct;
        }



        [System.Web.Http.Route("api/HWapi/getAssignmentListDetailsDashboard")]
        [System.Web.Http.HttpPost]
       
        public Assignment[] getAssignmentListDetailsDashboard(List<string> val)
        {
            int count = 0;
            //int a = 0;//.ToString();
            int SchoolID = Convert.ToInt32(val[0]);
            List<Assignment> list = new List<Models.Assignment>();

            try
            {



                DateTime curentdate = DateTime.Now;
                var result = db.tblHomeAssignments.Where(x => x.HW_Dt <= curentdate && x.SubmitDt >= curentdate && x.SchoolID == SchoolID).ToList();

                if (val[1] != null && val[1] != "0")
                {
                    int sectionid = Convert.ToInt32(val[1]);
                    result = result.Where(x => x.Section == sectionid).ToList();
                }
                if (val[2] != null && val[2] != "0")
                {
                    int classid = Convert.ToInt32(val[2]);

                    result = result.Where(x => x.Class == classid).ToList();

                }
                if (val[3] != null && val[3] != "0")
                {
                    int year = Convert.ToInt32(val[3]);
                    result = result.Where(x => x.AcademicYear == year).ToList();
                }
                foreach (var m in result)
                {
                    count++;
                    Assignment ct = new Models.Assignment();
                    ct.hw = new tblHomeAssignment();
                    ct.ID = count;
                    ct.hw = m;

                    var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear);
                    ct.year = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

                    var rslt = (from c in db.tblSections
                                join s in db.tblCourses on c.ClassId equals s.Id
                                where s.Id == m.Class && c.Id == m.Section
                                select new
                                {
                                    crs = s,
                                    sec = c
                                }).SingleOrDefault();
                    ct.course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
                    ct.subDt = ((DateTime)m.SubmitDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ct.hwDt = ((DateTime)m.HW_Dt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (m.FilePath == null || m.FilePath == "")
                    {
                        ct.style = "";
                    }
                    else
                    {
                        ct.style = "fa fa-download fa-lg text-green";
                    }

                    var sub = db.tblSubjects.SingleOrDefault(x => x.ID == m.Subject);
                    ct.subject = sub.Subject;

                    var emp = db.tblEmployees.SingleOrDefault(x => x.Id == m.HW_givenBy);
                    ct.teacher = emp.FirstName + " " + emp.LastName;

                    list.Add(ct);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return list.ToArray();

        }
        //public Assignment[] getAssignmentListDetailsDashboard(List<string> val)
        //{
        //    int count = 0;
        //    //int a = 0;//.ToString();
        //    int SchoolID = Convert.ToInt32(val[0]);
        //    List<Assignment> list = new List<Models.Assignment>();

        //    try
        //    {


        //        DateTime curentdate = DateTime.Now;
        //       var result = db.tblHomeAssignments.Where(x => x.HW_Dt <= curentdate && x.SubmitDt >= curentdate && x.SchoolID==SchoolID).ToList();



        //        foreach (var m in result)
        //        {
        //            count++;
        //            Assignment ct = new Models.Assignment();
        //            ct.hw = new tblHomeAssignment();
        //            ct.ID = count;
        //            ct.hw = m;

        //            var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear);
        //            ct.year = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

        //            var rslt = (from c in db.tblSections
        //                        join s in db.tblCourses on c.ClassId equals s.Id
        //                        where s.Id == m.Class && c.Id == m.Section
        //                        select new
        //                        {
        //                            crs = s,
        //                            sec = c
        //                        }).SingleOrDefault();
        //            ct.course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
        //            ct.subDt = ((DateTime)m.SubmitDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //            ct.hwDt = ((DateTime)m.HW_Dt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //            if (m.FilePath == null || m.FilePath == "")
        //            {
        //                ct.style = "";
        //            }
        //            else
        //            {
        //                ct.style = "fa fa-download fa-lg text-green";
        //            }

        //            var sub = db.tblSubjects.SingleOrDefault(x => x.ID == m.Subject);
        //            ct.subject = sub.Subject;

        //            var emp = db.tblEmployees.SingleOrDefault(x => x.Id == m.HW_givenBy);
        //            ct.teacher = emp.FirstName + " " + emp.LastName;

        //            list.Add(ct);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return list.ToArray();

        //}



        //[System.Web.Http.Route("api/HWapi/getAssignmentList")]
        //[System.Web.Http.HttpPost]
        //public List<Assignment> getAssignmentList(List<string> val)
        //{

        //    try
        //    {
        //        DateTime curentdate = DateTime.Now;
        //        var result = db.tblHomeAssignments.Where(x => x.HW_Dt <= curentdate && x.SubmitDt >= curentdate).ToList();
        //        // long id = Convert.ToInt32(val[0]);
        //        foreach (var r in result)
        //        {
        //            Assignment ct = new Models.Assignment();
        //            ct.subject = r.Subject;
        //        }
        //        //ct.hw = new tblHomeAssignment();
        //        //ct.hw = result;
        //        //ct.ID = result.ID;
        //        //ct.subDt = ((DateTime)result.SubmitDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    }
        //    catch (Exception ex) { ct.ID = -1; }
        //    return ct;
        //}



    }
}
