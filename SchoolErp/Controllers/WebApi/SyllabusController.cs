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
    public class SyllabusController : ApiController
    {

        SCHOOLERPEntities db = new SCHOOLERPEntities();

        [System.Web.Http.Route("api/Syllabus1/CreateSyllabus")]
        [System.Web.Http.HttpPost]
        public syllabous123 CreateSyllabus1()
        {
            var model = new syllabous123();
            try
            {
                int iUploadedCnt = 0;
                string[] d = { };
                string[] dT = { };
                int dcount = 0;
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                string relPath = "";
                string doc_path = "";
                NameValueCollection nvc = HttpContext.Current.Request.Form;
                var hw = new tblSyllabu();
                // iterate through and map to strongly typed model
                foreach (string kvp in nvc.AllKeys)
                {
                    if (kvp == "ID")
                    {
                        hw.ID = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "empID")
                    {
                        hw.empID = Convert.ToInt32(nvc[kvp]);
                    }

                    else if (kvp == "SchoolID")
                    {
                        hw.SchoolID = Convert.ToInt32(nvc[kvp]);
                    }

                    else if (kvp == "academicYr")
                    {
                        hw.AcademicYear = Convert.ToInt32(nvc[kvp]);
                    }

                    else if (kvp == "class")
                    {
                        hw.Class = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "section")
                    {
                        hw.section = nvc[kvp];
                    }
                    else if (kvp == "file")
                    {
                        //hw.FilePath = "www.smartvidhya.com";
                        hw.FilePath = nvc[kvp];

                    }
                    else if (kvp == "subject")
                    {
                        hw.subject = nvc[kvp];
                    }
                    else if (kvp == "teacher")
                    {
                        hw.teacher = nvc[kvp];
                    }
                    else if (kvp == "N_book")
                    {
                        hw.N_book = nvc[kvp];
                    }
                    else if (kvp == "pub")
                    {
                        hw.pub = nvc[kvp];
                    }
                    else if (kvp == "isbnnum")
                    {
                        hw.isbnnum = nvc[kvp];
                    }
                    else if (kvp == "desc")
                    {
                        hw.Description = nvc[kvp];
                    }
                    else if (kvp == "btnAboutSyllabus")
                    {
                        hw.btnAboutSyllabus = nvc[kvp];
                    }


                }
                var check = db.tblSyllabus.Where(x => x.AcademicYear == hw.AcademicYear && x.Class == hw.Class && x.ID != hw.ID && x.IsDeleted == null).FirstOrDefault();
                if (check != null)
                {
                    model.ID = -1;
                }
                else
                {
                    var a = db.tblAcademicYears.FirstOrDefault(x => x.ID == hw.AcademicYear && x.SchoolID == hw.SchoolID && x.IsDeleted == null);
                    model.avademicyear = a.DateFrom.Year + "-" + a.DateTo.ToString("yy");
                    var course = db.tblCourses.FirstOrDefault(x => x.Id == hw.Class && x.SchoolID == hw.SchoolID);
                    model.Classname = course.CourseName;
                    relPath = "/Images/Syllabus/" + model.avademicyear + "/" + model.Classname + "/";
                    sPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + relPath);
                    Directory.CreateDirectory(sPath);
                    //}
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
                            string document = sPath + code + hpf.FileName;
                            doc_path = relPath + code + hpf.FileName;
                            //hw.FilePath = doc_path;
                            hw.FilePath = "http:/" + "/www.smartvidhya.com" + doc_path;
                            if (File.Exists(document))
                            {
                                File.Delete(document);
                            }
                            else
                            { }
                            hpf.SaveAs(document);
                            dcount = dcount + 1;
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                    if (hw.ID == 0)
                    {
                        hw.DateCreated = DateTime.Now;
                        db.tblSyllabus.Add(hw);
                    }
                    else
                    {
                        var hw_edit = db.tblSyllabus.SingleOrDefault(x => x.ID == hw.ID);
                        hw_edit.FilePath = "http:/" + "/www.smartvidhya.com" + doc_path;
                        hw_edit.Description = hw.Description;
                        hw_edit.AcademicYear = hw.AcademicYear;
                        hw_edit.Class = hw.Class;
                        hw_edit.DateCreated = DateTime.Now;
                    }
                    db.SaveChanges();
                    model.ID = hw.ID;
                    model.hw = hw;
                }
            }
            catch (Exception e)
            { model.ID = -1; throw e; }
            return model;
        }

        [System.Web.Http.Route("api/Syllabus1/Elearning")]
        [System.Web.Http.HttpPost]
        public Elearning123 Elearning()
        {
            var model = new Elearning123();
            try
            {
                int iUploadedCnt = 0;
                string[] d = { };
                string[] dT = { };
                int dcount = 0;
                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                string sPath = "";
                string relPath = "";
                string doc_path = "";
                NameValueCollection nvc = HttpContext.Current.Request.Form;
                var hw = new tblElearning();
                // iterate through and map to strongly typed model
                foreach (string kvp in nvc.AllKeys)
                {
                    if (kvp == "ID")
                    {
                        hw.ID = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "empID")
                    {
                        hw.empID = Convert.ToInt32(nvc[kvp]);
                    }

                    else if (kvp == "SchoolID")
                    {
                        hw.SchoolID = Convert.ToInt32(nvc[kvp]);
                    }

                    else if (kvp == "academicYr")
                    {
                        hw.AcademicYear = Convert.ToInt32(nvc[kvp]);
                    }

                    else if (kvp == "class")
                    {
                        hw.Class = Convert.ToInt32(nvc[kvp]);
                    }
                    //else if (kvp == "section")
                    //{
                    //    hw.section = nvc[kvp];
                    //}
                    else if (kvp == "file")
                    {
                        //hw.FilePath = "www.smartvidhya.com";
                        hw.FilePath = nvc[kvp];

                    }
                    else if (kvp == "subject")
                    {
                        hw.subject = nvc[kvp];
                    }
                    else if (kvp == "teacher")
                    {
                        hw.teacher = nvc[kvp];
                    }
                    else if (kvp == "N_book")
                    {
                        hw.ChapterNo = nvc[kvp];
                    }
                    else if (kvp == "pub")
                    {
                        hw.ChapterName = nvc[kvp];
                    }
                    //else if (kvp == "isbnnum")
                    //{
                    //    hw.isbnnum = nvc[kvp];
                    //}
                    //else if (kvp == "desc")
                    //{
                    //    hw.Description = nvc[kvp];
                    //}
                    //else if (kvp == "btnAboutSyllabus")
                    //{
                    //    hw.btnAboutSyllabus = nvc[kvp];
                    //}


                }
                //var check = db.tblElearning.Where(x => x.AcademicYear == hw.AcademicYear && x.Class == hw.Class && x.ID != hw.ID && x.IsDeleted == null).FirstOrDefault();
                //if (check != null)
                //{
                //    model.ID = -1;
                //}
                //else
                //{
                var a = db.tblAcademicYears.FirstOrDefault(x => x.ID == hw.AcademicYear && x.SchoolID == hw.SchoolID && x.IsDeleted == null);
                model.avademicyear = a.DateFrom.Year + "-" + a.DateTo.ToString("yy");
                var course = db.tblCourses.FirstOrDefault(x => x.Id == hw.Class && x.SchoolID == hw.SchoolID);
                model.Classname = course.CourseName;
                relPath = "/Images/Elearning/" + model.avademicyear + "/" + model.Classname + "/";
                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + relPath);
                Directory.CreateDirectory(sPath);
                //}
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
                        string document = sPath + code + hpf.FileName;
                        doc_path = relPath + code + hpf.FileName;
                        //hw.FilePath = doc_path;
                        hw.FilePath = "http:/" + "/www.smartvidhya.com" + doc_path;
                        if (File.Exists(document))
                        {
                            File.Delete(document);
                        }
                        else
                        { }
                        hpf.SaveAs(document);
                        dcount = dcount + 1;
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                    // }
                    if (hw.ID == 0)
                    {
                        hw.DateCreated = DateTime.Now;
                        db.tblElearning.Add(hw);
                    }
                    else
                    {
                        var hw_edit = db.tblElearning.SingleOrDefault(x => x.ID == hw.ID);
                        hw_edit.FilePath = "http:/" + "/www.smartvidhya.com" + doc_path;
                        //hw_edit.Description = hw.Description;
                        hw_edit.AcademicYear = hw.AcademicYear;
                        hw_edit.Class = hw.Class;
                        hw_edit.DateCreated = DateTime.Now;
                    }
                    db.SaveChanges();
                    model.ID = hw.ID;
                    model.hw = hw;
                }
            }
            catch (Exception e)
            { model.ID = -1; throw e; }
            return model;
        }

        [System.Web.Http.Route("api/Syllabus/DeleteSyllabus")]
        [System.Web.Http.HttpPost]

        public syllabus DeleteSyllabus(List<string> aa)
        {
            int idd = Convert.ToInt32(aa[0]);
            syllabus obj = new syllabus();
            var del = db.tblSyllabus.Where(x => x.ID == idd).FirstOrDefault();
            del.IsDeleted = 1;
            del.Deleted_on = DateTime.Now;
            db.SaveChanges();
            obj.msg = "Syllabus Deleted Sucessfully";
            return obj;

        }
        [System.Web.Http.Route("api/Syllabus/getSyllabusListAPP")]
        [System.Web.Http.HttpPost]
        public syllabusappp getSyllabusListapp(Parameters param)
        {
            int count = 0;
            int a = 0;//.ToString();

            syllabusappp obj = new syllabusappp();
            List<syllabus1> list = new List<Models.syllabus1>();


            try
            {
                if (string.IsNullOrEmpty(param.val1) || param.val1 == "-1")
                {
                    obj.status = false;
                    obj.message = "Please Enter AcademicYear";
                }
                else if (string.IsNullOrEmpty(param.val2) || param.val2 == "-1")
                {
                    obj.status = false;
                    obj.message = "Please Enter ClassId";
                }
                else if (string.IsNullOrEmpty(param.val9) || param.val9 == "" || param.val9 == "0" || param.val9 == "-1")
                {
                    obj.status = false;
                    obj.message = "Please Enter SchoolID";
                }
                else
                {
                    string type = param.val;
                    string name = param.val4;
                    if (param.val8 != "")
                    { a = Convert.ToInt32(param.val8); }
                    var predicate = PredicateBuilder.True<tblSyllabu>();
                    //if (param.val8 !="")
                    //{
                    //    var aa=tbl
                    //}
                    if (param.val9 != "" && param.val9 != "0" && param.val9 != "-1")
                    {
                        int SchoolID = Convert.ToInt32(param.val9);
                        predicate = predicate.And(x => x.SchoolID == SchoolID);
                    }
                    if (param.val1 != "-1" && param.val1 != "" && param.val1 != null)
                    {

                        int year = Convert.ToInt32(param.val1);
                        predicate = predicate.And(x => x.AcademicYear == year);

                    }
                    if (param.val2 != "-1" && param.val2 != "" && param.val2 != null)
                    {
                        int classID = Convert.ToInt32(param.val2);
                        predicate = predicate.And(x => x.Class == classID);
                    }

                    predicate = predicate.And(x => x.IsDeleted == null);

                    var result = db.tblSyllabus.AsExpandable().Where(predicate).ToList();

                    //if (type == "Student" && a != 0)
                    //{
                    //    var std = db.TBLStudents.SingleOrDefault(x => x.ID == a);

                    //    result = result.Where(x => x.Class == std.ClassID).ToList();


                    //}
                    //else if (type == "Teacher" && a != 0)
                    //{
                    //    result = result.ToList();
                    //}

                    foreach (var m in result)
                    {
                        count++;
                        syllabus1 ct = new Models.syllabus1();
                        ct.Syllabus = new syllabusAPISHOW();
                        ct.Syllabus.ID = m.ID;
                        ct.Syllabus.AcademicYear = m.AcademicYear;
                        ct.Syllabus.Class = m.Class;
                        ct.Syllabus.FilePath = m.FilePath;
                        ct.Syllabus.Description = m.Description;
                        ct.Syllabus.SchoolID = m.SchoolID;
                        ct.Syllabus.DateCreated = Convert.ToDateTime(m.DateCreated).ToString("dd MMMM yyyy");


                        var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear && b.IsDeleted == null);
                        ct.academicyearname = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

                        var rslt = (
                                    from s in db.tblCourses
                                    where s.Id == m.Class
                                    select new
                                    {
                                        crs = s

                                    }).SingleOrDefault();
                        ct.Classname = rslt.crs.CourseName;

                        if (m.FilePath == null || m.FilePath == "")
                        {
                            ct.style = "";
                        }
                        else
                        {
                            ct.style = "fa fa-download fa-lg text-green";
                        }
                        list.Add(ct);




                    }

                    if (count != 0)
                    {
                        obj.status = true;
                        obj.message = "Sucess";
                        obj.data = list;
                    }
                    else if (count == 0)
                    {
                        obj.status = false;
                        obj.message = "Data not found";
                    }
                }

            }

            catch
            {
                obj.status = false;
                obj.message = "Something went wrong";
            }
            return obj;

        }


        [System.Web.Http.Route("api/Syllabus/getSyllabusList")]
        [System.Web.Http.HttpPost]
        public syllabus[] getSyllabusList(Parameters param)
        {
            int count = 0;
            int a = 0;//.ToString();
            string type = param.val;
            List<syllabus> list = new List<Models.syllabus>();

            string name = param.val4;
            try
            {


                if (param.val8 != "")
                { a = Convert.ToInt32(param.val8); }
                var predicate = PredicateBuilder.True<tblSyllabu>();
                //if (param.val8 !="")
                //{
                //    var aa=tbl
                //}
                if (param.val9 != "" && param.val9 != "0" && param.val9 != "-1")
                {
                    int SchoolID = Convert.ToInt32(param.val9);
                    predicate = predicate.And(x => x.SchoolID == SchoolID);
                }
                if (param.val1 != "-1" && param.val1 != "" && param.val1 != null)
                {

                    int year = Convert.ToInt32(param.val1);
                    predicate = predicate.And(x => x.AcademicYear == year);

                }
                if (param.val2 != "-1" && param.val2 != "" && param.val2 != null)
                {
                    int classID = Convert.ToInt32(param.val2);
                    predicate = predicate.And(x => x.Class == classID);
                }

                predicate = predicate.And(x => x.IsDeleted == null);

                var result = db.tblSyllabus.AsExpandable().Where(predicate).ToList();

                if (type == "Student" && a != 0)
                {
                    var std = db.TBLStudents.SingleOrDefault(x => x.ID == a);

                    result = result.Where(x => x.Class == std.ClassID).ToList();


                }
                else if (type == "Teacher" && a != 0)
                {
                    result = result.ToList();
                }



                foreach (var m in result)
                {
                    count++;
                    syllabus ct = new Models.syllabus();
                    ct.hw = new tblSyllabu();
                    ct.ID = count;
                    ct.hw = m;

                    var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear && m.IsDeleted == null);
                    ct.academicyearname = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

                    var rslt = (
                                from s in db.tblCourses
                                where s.Id == m.Class
                                select new
                                {
                                    crs = s

                                }).SingleOrDefault();
                    ct.Classname = rslt.crs.CourseName;
                    //var sec = Convert.ToInt32(m.section);
                    //var rslt1 = (
                    //          from s in db.tblSections
                    //          where s.Id == sec
                    //          select new
                    //          {
                    //              crs1 = s

                    //          }).SingleOrDefault();
                    //ct.SectionName = rslt1.crs1.SectionName;

                    if (m.FilePath == null || m.FilePath == "")
                    {
                        ct.style = "";
                    }
                    else
                    {
                        ct.style = "fa fa-download fa-lg text-green";
                    }




                    list.Add(ct);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return list.ToArray();

        }


        [System.Web.Http.Route("api/Syllabus/getElearningList")]
        [System.Web.Http.HttpPost]
        public Elearning[] getElearningList(Parameters param)
        {
            int count = 0;
            int a = 0;//.ToString();
            string type = param.val;
            int type1 = Convert.ToInt32(param.val10);
            List<Elearning> list = new List<Models.Elearning>();

            string name = param.val4;
            try
            {


                if (param.val8 != "")
                { a = Convert.ToInt32(param.val8); }
                var predicate = PredicateBuilder.True<tblElearning>();
                //if (param.val8 !="")
                //{
                //    var aa=tbl
                //}
                if (param.val9 != "" && param.val9 != "0" && param.val9 != "-1")
                {
                    int SchoolID = Convert.ToInt32(param.val9);
                    predicate = predicate.And(x => x.SchoolID == SchoolID);
                }
                if (param.val1 != "-1" && param.val1 != "" && param.val1 != null)
                {

                    int year = Convert.ToInt32(param.val1);
                    predicate = predicate.And(x => x.AcademicYear == year);

                }
                if (param.val2 != "-1" && param.val2 != "" && param.val2 != null)
                {
                    int classID = Convert.ToInt32(param.val2);
                    predicate = predicate.And(x => x.Class == classID);
                }

                predicate = predicate.And(x => x.IsDeleted == null);

                var result = db.tblElearning.AsExpandable().Where(predicate).ToList();

                if (type1 == 4 && a != 0)
                {
                    var std = db.TBLStudents.SingleOrDefault(x => x.ID == a);

                    result = result.Where(x => x.Class == std.ClassID).ToList();


                }
                else if (a != 0)
                {
                    result = result.ToList();
                }



                foreach (var m in result)
                {
                    count++;
                    Elearning ct = new Models.Elearning();
                    ct.hw = new tblElearning();
                    ct.ID = count;
                    ct.hw = m;
                    int sub = Convert.ToInt32(m.subject);
                    var Subject = db.tblSubjects.SingleOrDefault(x => x.ID == sub);
                    ct.course = Subject.Subject;
                    var d = db.tblAcademicYears.SingleOrDefault(b => b.ID == m.AcademicYear && m.IsDeleted == null);
                    ct.academicyearname = d.DateFrom.Year + "-" + d.DateTo.ToString("yy");

                    var rslt = (
                                from s in db.tblCourses
                                where s.Id == m.Class
                                select new
                                {
                                    crs = s

                                }).SingleOrDefault();
                    ct.Classname = rslt.crs.CourseName;
                    //var sec = Convert.ToInt32(m.section);
                    //var rslt1 = (
                    //          from s in db.tblSections
                    //          where s.Id == sec
                    //          select new
                    //          {
                    //              crs1 = s

                    //          }).SingleOrDefault();
                    //ct.SectionName = rslt1.crs1.SectionName;

                    if (m.FilePath == null || m.FilePath == "")
                    {
                        ct.style = "";
                    }
                    else
                    {
                        ct.style = "fa fa-download fa-lg text-green";
                    }




                    list.Add(ct);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return list.ToArray();

        }


        //    public string CreateSyllabus(syllabus aa)
        //    {
        //        int iUploadedCnt = 0;
        //        string relPath = "";
        //        string sPath = "";
        //        int dcount = 0;
        //        string doc_path = "";
        //        syllabusapp OBJ = new syllabusapp();
        //        try
        //        {

        //            if (string.IsNullOrEmpty(aa.ID))
        //            {
        //                tblSyllabu sl = new tblSyllabu();
        //                sl.AcademicYear = aa.AcademicYear;
        //                sl.Class = aa.Class;
        //                sl.Description = aa.Description;
        //                sl.SchoolID = aa.SchoolID;

        //                var a = db.tblAcademicYears.SingleOrDefault(x => x.ID == aa.AcademicYear && x.SchoolID == aa.SchoolID);
        //                aa.academicyearname = a.DateFrom.Year + "-" + a.DateTo.ToString("yy");
        //                var course = db.tblCourses.SingleOrDefault(x => x.Id == aa.Class && x.SchoolID == aa.SchoolID);
        //                aa.Classname = course.CourseName;

        //                relPath = "/Images/Assignments/" + aa.academicyearname + "/" + aa.academicyearname + "/";
        //                sPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + relPath);
        //                Directory.CreateDirectory(sPath);
        //                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
        //                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
        //                {
        //                    System.Web.HttpPostedFile hpf = hfc[iCnt];

        //                    if (hpf.ContentLength > 0)
        //                    {
        //                        CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
        //                        if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
        //                {
        //                    SAVE THE FILES IN THE FOLDER.

        //                        string document = sPath + hpf.FileName;
        //                    doc_path = relPath + hpf.FileName;
        //                    sl.FilePath = doc_path;


        //                    if (File.Exists(document))
        //                    {
        //                        File.Delete(document);
        //                    }
        //                    else
        //                    {
        //                    }
        //                    hpf.SaveAs(document);
        //                    dcount = dcount + 1;


        //                    iUploadedCnt = iUploadedCnt + 1;

        //                }
        //            }
        //            db.tblSyllabus.Add(sl);
        //            db.SaveChanges();
        //            return "";


        //        }
        //            else
        //            {
        //            int idd = Convert.ToInt32(aa.ID);
        //            var result = db.tblSyllabus.Where(x => x.ID == idd).FirstOrDefault();
        //            result.Class = aa.Class;
        //            result.AcademicYear = aa.AcademicYear;
        //            result.Description = aa.Description;
        //            result.SchoolID = aa.SchoolID;
        //            return "";
        //        }

        //    }
        //        catch
        //        {
        //            return "";
        //        }
        //          syllabusapp obj = new syllabusapp();



        //}





        //public class syllabus
        //{
        //    public string ID { get; set; }
        //    public string empID { get; set; }
        //    public Nullable<int> AcademicYear { get; set; }
        //    public Nullable<int> Class { get; set; }
        //    public string FilePath { get; set; }
        //    public string Description { get; set; }
        //    public Nullable<int> SchoolID { get; set; }
        //    public string Classname { get; set; }
        //    public string academicyearname { get; set; }
        //    public string file { get; set; }
        //    public string path { get; set; }
        //    public List<tblSyllabu> hw {get;set;}
        //}


        public class syllabous123
        {
            public long ID { get; set; }
            public tblSyllabu hw { get; set; }
            public string course { get; set; }
            public string section { get; set; }
            public string year { get; set; }
            public string subject { get; set; }
            public string hwDt { get; set; }
            public string subDt { get; set; }
            public string style { get; set; }
            public string teacher { get; set; }
            public string avademicyear { get; set; }
            public string Classname { get; set; }
            //public string marks { get; set; }
        }

        public class Elearning123
        {
            public long ID { get; set; }
            public tblElearning hw { get; set; }
            public string course { get; set; }
            public string section { get; set; }
            public string year { get; set; }
            public string subject { get; set; }
            public string hwDt { get; set; }
            public string subDt { get; set; }
            public string style { get; set; }
            public string teacher { get; set; }
            public string avademicyear { get; set; }
            public string Classname { get; set; }
            //public string marks { get; set; }
        }


    }



}
