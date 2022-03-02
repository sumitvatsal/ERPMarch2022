using LinqKit;
using SchoolErp.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SchoolErp.Controllers.WebApi
{
    public class NewRegApiController : ApiController
    {
        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/NewRegApi/SaveNewRegDet")]
        [System.Web.Http.HttpPost]
        public Student SaveNewRegDet(tblStRegistration val)
        {
            Student st = new Models.Student();
            try
            {
                val.Status = 17;
                db.tblStRegistrations.Add(val);
                db.SaveChanges();

                st.ID = val.ID;
                
            }
            catch (Exception) { st.ID=-1; }
            return st;
        }

        [System.Web.Http.Route("api/NewRegApi/EditStudentDet")]
        [System.Web.Http.HttpPost]
        public Student EditStudentDet(tblStRegistration val)
        {
            Student st = new Student();
            try
            {
                val.Status = 17;
                //var result = db.tblClassTimings.SingleOrDefault(b => b.ID == val.ID);
                //result.Name = a.ct.Name;
                //result.Status = a.ct.Status;
                //result.Description = a.ct.Description;
                //db.SaveChanges();

                db.Entry(val).State = EntityState.Modified;
                db.SaveChanges();

                st.ID = val.ID;
            }
            catch (Exception) { st.ID = -1; }
            return st;
        }

        [System.Web.Http.Route("api/NewRegApi/SaveOtherDetails")]
        [System.Web.Http.HttpPost]
        public int SaveOtherDetails(Student val)
        {
            try
            {
                if(val.prevlist_reg != null)
                {
                    foreach(var p in val.prevlist_reg)
                    {
                        if(p.PSYear!="-1" && p.Class!=-1 && p.Board!="" && p.School!="")
                        {
                            db.tblStRegPrevSchoolDets.Add(p);
                        }
                    }
                }

                if (val.Height != null || val.Weight != null)
                {
                    tblStRegHealthDet ht = new tblStRegHealthDet();
                    ht.Height = val.Height;
                    ht.Weight = val.Weight;
                    ht.VisionLeft = val.VisionLeft;
                    ht.VisionRight = val.VisionRight;
                    ht.MedicationDetails = val.MedicationDetails;
                    ht.StudentID =val.ID;
                    db.tblStRegHealthDets.Add(ht);
                }
                db.SaveChanges();

                var i = val.ID;
                return i;
            }
            catch (Exception ex) { return -1; throw ex; }
        }

        [System.Web.Http.Route("api/NewRegApi/UploadDocs")]
        [System.Web.Http.HttpPost]
        public Student UploadDocs()
        {
            var model = new Student();
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
                string dir_path = "";

                NameValueCollection nvc = HttpContext.Current.Request.Form;
                
                model.rgDoc = new tblStRegDoc();
                model.rgDoclist = new List<Student>();

                // iterate through and map to strongly typed model
                foreach (string kvp in nvc.AllKeys)
                {
                    if (kvp == "ID")
                    {
                        model.ID = Convert.ToInt32(nvc[kvp]);
                        model.rgDoc.StudentID = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "doc")
                    {
                        model.rgDoc.DocID =Convert.ToInt32(nvc[kvp]);
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
                            dir_path = HttpContext.Current.Server.MapPath("~/Images/StRegistration/" + model.ID + "_" + model.FirstName);
                            Directory.CreateDirectory(dir_path);
                            relPath = "/Images/StRegistration/" + model.ID + "_" + model.FirstName + "/Student/";
                            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~"+ relPath);
                            Directory.CreateDirectory(sPath);
                        }
                        else if (nvc[kvp] == "FParent")
                        {
                            relPath = "/Images/StRegistration/" + model.ID + "_" + model.FirstName +"/Father/";
                            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + relPath);
                            Directory.CreateDirectory(sPath);
                        }
                        else if (nvc[kvp] == "MParent")
                        {
                            relPath = "/Images/StRegistration/" + model.ID + "_" + model.FirstName + "/Mother/";
                            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + relPath);
                            Directory.CreateDirectory(sPath);
                        }
                        else if (nvc[kvp] == "Docs")
                        {
                            relPath = "/Images/StRegistration/" + model.ID + "_" + model.FirstName + "/Docs/";
                            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + relPath);
                            Directory.CreateDirectory(sPath);
                        }
                    }
                    else if (kvp == "DocLength")
                    {
                        model.docLength = Convert.ToInt32(nvc[kvp]);
                    }
                    else if (kvp == "DocIDs")
                    {
                        docIDs = nvc[kvp];
                    }
                    else if (kvp == "StName")
                    {
                        model.FirstName = nvc[kvp];
                    }
                    else if (kvp == "Mname")
                    {
                        model.MotherName = nvc[kvp];
                    }
                    else if (kvp == "Fname")
                    {
                        model.FatherName = nvc[kvp];
                    }
                    else if (kvp == "Ppwd")
                    {
                        model.PPwd = nvc[kvp];
                    }
                    else if (kvp == "docText")
                    {
                        docTex = nvc[kvp];
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
                            if (type == "Student")
                            {
                                string St_pic = sPath + model.FirstName + model.ID + Path.GetExtension(hpf.FileName);
                                string picpath = relPath + model.FirstName + model.ID + Path.GetExtension(hpf.FileName);
                                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update tblStRegistration set PicPath = '" + picpath + "' where ID = " + model.ID);
                                if (noOfRowUpdated > 0)
                                {
                                if (File.Exists(St_pic))
                                {
                                    File.Delete(St_pic);
                                }
                                hpf.SaveAs(St_pic);
                                }
                            }
                            else if (type == "FParent")
                            {
                                string F_pic = sPath + model.FatherName + model.ID + Path.GetExtension(hpf.FileName);
                                string picpath = relPath + model.FatherName + model.ID + Path.GetExtension(hpf.FileName);
                                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update tblStRegistration set FPicPath = '" + picpath + "' where ID = " + model.ID);
                                if (noOfRowUpdated > 0)
                                {
                                if (File.Exists(F_pic))
                                {
                                    File.Delete(F_pic);
                                }
                                hpf.SaveAs(F_pic);
                                }
                            }
                            else if (type == "MParent")
                            {
                                string M_pic = sPath + model.MotherName+model.ID + Path.GetExtension(hpf.FileName);
                                string picpath = relPath + model.MotherName + model.ID + Path.GetExtension(hpf.FileName);
                                int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update tblStRegistration set MPicPath = '" + picpath + "' where ID = " + model.ID);
                                if (noOfRowUpdated > 0)
                                {
                                if (File.Exists(M_pic))
                                {
                                    File.Delete(M_pic);
                                }
                                hpf.SaveAs(M_pic);
                            }
                            }
                            else if (type == "Docs")
                            {
                                //var docid = Convert.ToInt32(d[dcount]);
                                //var docText = dT[dcount];
                                //docText = docText.Replace(" ", String.Empty);
                                
                                string document = sPath + docTex + model.ID + Path.GetExtension(hpf.FileName);
                                string Docpath = relPath + docTex + model.ID + Path.GetExtension(hpf.FileName);
                            //int noOfRowUpdated1 = db.Database.ExecuteSqlCommand("Insert into TBLStudentDocs (StudentID,DocID,DocPath,Status)" +
                            //    " values (" + model.ID + "," + model.rgDoc.DocID + ",'" + Docpath + "'," + 1 + ")");

                            model.rgDoc.DocPath = Docpath;
                            model.rgDoc.Status = 1;

                            //var rec = (from dc in db.tblStRegDocs
                            //           join en in db.tblDocuments on dc.DocID equals en.Id
                            //           where dc.StudentID == model.rgDoc.StudentID
                            //           select new
                            //           { dc, en }).ToList();
                            //foreach(var r in rec)
                            //{
                            //    Student std = new Student();
                            //    std.rgDoc = new SchoolErp.tblStRegDoc();
                            //    std.docName = new SchoolErp.tblDocument();
                            //    std.rgDoc = r.dc;
                            //    std.docName = r.en;
                            //    model.rgDoclist.Add(std);
                            //}
                            if (File.Exists(document))
                            {
                                File.Delete(document);
                            }
                            else
                            {
                                db.tblStRegDocs.Add(model.rgDoc);
                                db.SaveChanges();
                            }
                            hpf.SaveAs(document);
                                dcount = dcount + 1;
                                
                            }
                            iUploadedCnt = iUploadedCnt + 1;
                        
                    }
                }
                
            }
            catch (Exception e) { model.ID=-1; throw e; }
            return model;
        }

        [System.Web.Http.Route("api/NewRegApi/StDocList")]
        [System.Web.Http.HttpPost]
        public Student StDocList(List<string> val)
        {
            long id = Convert.ToInt32(val[0]);
            var st = new Student();
            st.rgDoclist = new List<Student>();
            try
            {
                var rec = (from dc in db.tblStRegDocs
                           join en in db.tblDocuments on dc.DocID equals en.Id
                           where dc.StudentID == id
                           select new
                           { dc, en }).ToList();
                foreach (var r in rec)
                {
                    Student std = new Student();
                    std.rgDoc = new SchoolErp.tblStRegDoc();
                    std.docName = new SchoolErp.tblDocument();
                    std.rgDoc = r.dc;
                    std.docName = r.en;
                    st.rgDoclist.Add(std);
                }
                st.ID = 1;
            }
            catch(Exception ex) { st.ID = -1; throw ex; }
            
            return st;
        }

        [System.Web.Http.Route("api/NewRegApi/DeleteRecord")]
        [System.Web.Http.HttpPost]
        public Student DeleteRecord(List<string> val)
        {
            int id = Convert.ToInt32(val[0]);
            Student std = new Models.Student();
            string docPath = "";
            try
            {
                //var employer = new tblStRegDoc { ID = id };
                //db.Entry(employer).State = EntityState.Deleted;

                //db.SaveChanges();
                var itemToRemove = db.tblStRegDocs.SingleOrDefault(x => x.ID == id); //returns a single item.
                docPath = System.Web.Hosting.HostingEnvironment.MapPath("~" + itemToRemove.DocPath); 
                if (itemToRemove != null)
                {
                    db.tblStRegDocs.Remove(itemToRemove);
                    db.SaveChanges();
                }
                std.StatusName = "Record successfully deleted";
                if (File.Exists(docPath))
                {
                    File.Delete(docPath);
                }

                //foreach (string file in Directory.GetFiles(docPath))
                //{
                //    File.Delete(file);
                //}
                return std;
            }
            catch (Exception e)
            {
                std.StatusName = "Error!!";
                return std;
                throw e;
            }
        }

        [System.Web.Http.Route("api/NewRegApi/FinalSubmit")]
        [System.Web.Http.HttpPost]
        public int FinalSubmit(List<string> val)
        {
            try
            {
                int id = Convert.ToInt32(val[0]);
                int status= Convert.ToInt32(val[1]);
                DateTime dt = DateTime.Now;
                var i = 0;
                if(val[2]== "NewStudentReg")
                {
                    i = db.Database.ExecuteSqlCommand("Update tblStRegistration set Status = " + status + ",RequestDt="+dt+" where ID=" + id);
                }
                else if(val[2]== "ViewRegApplication")
                {
                    i = db.Database.ExecuteSqlCommand("Update tblStRegistration set Status = " + status + ",ActionDt=" + dt + " where ID=" + id);
                    if(i>0)
                    {
                        fromRegistrationToTBLStudent(id);
                    }
                }
                                
                return i;
            }
            catch (Exception e) { return -1; throw e; }
        }

        [System.Web.Http.Route("api/NewRegApi/GetNewRegList")]
        [System.Web.Http.HttpPost]
        public Student[] GetNewRegList(List<string> val)
        {
            string ttname = val[1];

            List<Student> list = new List<Models.Student>();
            try
            {
                var predicate = PredicateBuilder.True<tblStRegistration>();

                if (!string.IsNullOrWhiteSpace(val[0]))
                {
                    int classID = Convert.ToInt32(val[0]);
                    if (classID != -1)
                        predicate = predicate.And(x => x.ClassID == classID);
                }
                if (!string.IsNullOrWhiteSpace(ttname))
                {
                    predicate = predicate.And(x => x.FirstName.ToUpper().Contains(ttname.ToUpper()));
                }
                //if (!string.IsNullOrWhiteSpace(val[2]))
                //{
                //    DateTime fromDt = Convert.ToDateTime(val[2]);
                //    predicate = predicate.And(x => x.StartDT == fromDt);
                //}
                //if (!string.IsNullOrWhiteSpace(val[3]))
                //{
                //    DateTime toDt = Convert.ToDateTime(val[3]);
                //    predicate = predicate.And(x => x.EndDt == toDt);
                //}
                if (!string.IsNullOrWhiteSpace(val[4]))
                {
                    int status = Convert.ToInt32(val[4]);
                    if (status != -1)
                        predicate = predicate.And(x => x.Status == status && x.Status != 17);
                    else
                    {
                        predicate = predicate.And(x => x.Status != 17);
                    }
                }
                int count = 0;

                var result = db.tblStRegistrations.AsExpandable().Where(predicate).ToList();

                foreach (var m in result)
                {
                    count++;
                    Student ct = new Models.Student();
                    ct.stReg = new tblStRegistration();
                    ct.ID = count;
                    ct.stReg = m;
                    var d = db.tblCourses.SingleOrDefault(b => b.Id == m.ClassID);
                    ct.Class = d.CourseName;

                    var s = db.tblStatus.SingleOrDefault(b => b.StatusID == m.Status);
                    ct.SStatus = s.Status;

                    if (m.DOB != null)
                        ct.PSYear = ((DateTime)m.DOB).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //long ctID = Convert.ToInt32(m.CT_ID);
                    //var t = db.tblClassTimings.Where(x => x.ID == ctID).SingleOrDefault();
                    //ct.timingNm = t.Name;

                    //var rslt = (from c in db.tblSections
                    //            join s in db.tblCourses on c.ClassId equals s.Id
                    //            where s.Id == m.ClassID && c.Id == m.SectionID
                    //            select new
                    //            {
                    //                crs = s,
                    //                sec = c
                    //            }).SingleOrDefault();
                    //ct.Course = rslt.crs.CourseName + "-" + rslt.sec.SectionName;
                    //ct.fromDT = ((DateTime)m.StartDT).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //ct.ToDt = ((DateTime)m.EndDt).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //ct.con = cls;

                    //var ar = m.WeekDays.Split(',');
                    //ct.WeekDaysNm = ""; int n = 0;
                    //foreach (var day in ar)
                    //{
                    //    int dayID = Convert.ToInt32(day);
                    //    var w = db.tblWeekDays.Where(x => x.DayID == dayID).SingleOrDefault();

                    //    if (n < 1)
                    //    {
                    //        ct.WeekDaysNm = w.WeekDay;
                    //    }
                    //    else
                    //    {
                    //        ct.WeekDaysNm = ct.WeekDaysNm + "," + w.WeekDay;
                    //    }
                    //    n++;
                    //}

                    if (m.Status == 4)
                    {
                        ct.StatusName = "badge bg-orange";
                    }
                    else if (m.Status == 5)
                    {
                        ct.StatusName = "badge bg-green";
                    }
                    else
                    {
                        ct.StatusName = "badge bg-red";
                    }
                    list.Add(ct);
                }
            }
            catch (Exception ex) { throw ex; }
            return list.ToArray();
        }

        [System.Web.Http.Route("api/NewRegApi/FindStRegByID")]
        [System.Web.Http.HttpPost]
        public Student FindStRegByID(List<string> val)
        {
            Student st = new Models.Student();
            string a = val[0].ToString();
            var s = db.tblStRegistrations.Find(Convert.ToInt32(a));

            st.ID = s.ID;
            st.FirstName = s.FirstName;
            st.MiddleName = s.MiddleName;
            st.LastName = s.LastName;
            st.streamID = s.StreamID;
            if (s.StreamID != -1)
            {
                tblStream strm = db.tblStreams.Find(Convert.ToInt32(s.StreamID));
                st.stream = strm.StreamName;
            }

            if (s.Religion != null && s.Religion != -1)
            {
                tblReligion r = db.tblReligions.Find(Convert.ToInt32(s.Religion));
                st.Religion = r.ReligionName;
            }
            st.ReligionID = s.Religion;
            
            st.AadharNo = s.AadharNo;
            st.BirthPlace = s.BirthPlace;
            st.BloodGroup = s.BloodGroup;
            st.CategoryID = s.CategoryID;

            if(s.CategoryID!=null && s.CategoryID!=-1 && s.CategoryID!=0)
            {
                tblCastCategory c = db.tblCastCategories.Find(Convert.ToInt32(s.CategoryID));
                st.Category = c.CategoryName;
            }
            tblCours cl = db.tblCourses.Find(Convert.ToInt32(s.ClassID));
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
            st.Languages = s.Languages;
            string languages = "";
            if (st.Languages != "")
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
            if (s.PermState != "" && s.PermState != null)
            {
                tblState state = db.tblStates.Find(Convert.ToInt32(s.PermState));
                st.PermStateNm = state.StateName;
            }
            if (s.CorsState != "" && s.CorsState != null)
            {
                tblState state1 = db.tblStates.Find(Convert.ToInt32(s.CorsState));
                st.CorsStateNm = state1.StateName;
            }

            st.PermState = s.PermState;


            if (string.IsNullOrEmpty(s.PicPath))
            {
                st.PicPath = "/Images/Employee/EmployyeImage/No_Photo_Available.jpg";
            }
            else
            {
                st.PicPath = s.PicPath;
            }

            st.Relation = s.Relation;
            st.SMSmobileNo = s.SMSmobileNo;
            st.SPwd = s.SPwd;
            st.CorsPincode = s.CorsPincode;
            st.CorsCity = s.CorsCity;
            st.CorsState = s.CorsState;
            st.SMSmobileNo = s.SMSmobileNo;
            st.EmergencyNo = s.EmergencyNo;
            st.EmerContPerson = s.EmerContPerson;
            st.ContPersRelation = s.ContPersRelation;
            st.Status = s.Status;
            tblStatu stat = db.tblStatus.Find(s.Status);
            st.StatusName = stat.Status;

            //TBLStudentDoc doc = new TBLStudentDoc();
            List<tblStRegPrevSchoolDet> list = new List<tblStRegPrevSchoolDet>();
            st.prevlist_reg = new List<tblStRegPrevSchoolDet>();
            st.rgDoclist = new List<Student>();

            var result = db.tblStRegPrevSchoolDets.Where(x => x.StudentID == s.ID).ToList();
            foreach (var x in result)
            {
                list.Add(x);
            }
            st.prevlist_reg = list;
            st.stregHt = new SchoolErp.tblStRegHealthDet();
            st.stregHt = db.tblStRegHealthDets.Where(x => x.StudentID == s.ID).SingleOrDefault();
            
            var docs = (from dc in db.tblStRegDocs
                       join en in db.tblDocuments on dc.DocID equals en.Id
                       where dc.StudentID == s.ID
                       select new
                       { dc, en }).ToList();
            foreach (var r in docs)
            {
                Student std = new Student();
                std.rgDoc = new SchoolErp.tblStRegDoc();
                std.docName = new SchoolErp.tblDocument();
                std.rgDoc = r.dc;
                std.docName = r.en;
                st.rgDoclist.Add(std);
            }

            return st;
        }

        public Student fromRegistrationToTBLStudent(int id)
        {
            var st = new Student();
            var reg = db.tblStRegistrations.SingleOrDefault(x => x.ID == id);

            return st;
        }
    }
}
