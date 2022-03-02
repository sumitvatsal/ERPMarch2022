using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schoolERP_BLL;
using System.Data.Entity;
using System.Data;
using System.Globalization;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using System.Web.Security;

using SchoolErp.Models;

using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

using System.Web.Helpers;

using System.IO;
using System.Threading.Tasks;

namespace SchoolErp.Controllers.WebApi
{
    public class EventsAPIController : ApiController
    {

        SCHOOLERPEntities db = new SCHOOLERPEntities();
        [System.Web.Http.Route("api/EventsAPI/saveEventType")]
        [System.Web.Http.HttpPost]
        public string saveEventType(EventsType events)
        {
            string msg = "";
            if (string.IsNullOrEmpty(events.Id))
            {
                tblEventType cta = new SchoolErp.tblEventType();
                cta.EventName = events.Name;
                if (events.Status == "True")
                {
                    cta.Status = true;
                }
                else
                {
                    cta.Status = false;
                }
                cta.SchoolID = events.SchoolID;
                db.tblEventTypes.Add(cta);
                db.SaveChanges();
                msg = "Events Added succesfully";
            }
            else
            {
                int eventId = Int32.Parse(events.Id);
                var result = db.tblEventTypes.SingleOrDefault(b => b.EventId == eventId);
                result.EventName = events.Name;
                if (events.Status == "True")
                {
                    result.Status = true;
                }
                else
                {
                    result.Status = false;
                }
                result.SchoolID = events.SchoolID;
                db.SaveChanges();
                msg = "Events Updated Successfully";
            }

            return msg;
        }


        [System.Web.Http.Route("api/EventsAPI/getAllEventsTypes")]
        [System.Web.Http.HttpPost]
        public List<EventsType> getAllEventsTypes(List<string> aa)
        {
            List<EventsType> list = new List<EventsType>();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);

            try
            {

                if (typeuser == 2)
                {

                    var result = (from b in db.tblEventTypes
                                  join s in db.tblSchoolDetails on b.SchoolID equals s.ID
                                  where b.IsDeleted == null
                                  select new
                                  {
                                      model = b,
                                      SchoolName = s.School
                                  }).ToList();
                    //var result = db.tblEventTypes.SqlQuery("select * from tblEventType").ToList();
                    foreach (var a in result)
                    {
                        EventsType items = new EventsType();
                        items.Name = a.model.EventName;
                        items.Id = a.model.EventId.ToString();


                        if (a.model.Status.ToString() == "True")
                        {
                            items.Status = "Activate";
                            items.Extra10 = "btn btn-block btn-success btn-sm";
                        }
                        else
                        {
                            items.Status = "DeActivate";
                            items.Extra10 = "btn btn-block btn-danger btn-sm";
                        }
                        //  items.Status = a.Status.ToString();
                        items.School = a.SchoolName;
                        items.SchoolID = Convert.ToInt32(a.model.SchoolID);
                        list.Add(items);
                    }

                }
                else
                {
                    var result = (from b in db.tblEventTypes
                                  join s in db.tblSchoolDetails on b.SchoolID equals s.ID
                                  join em in db.tblEmployees on b.SchoolID equals em.SchoolID
                                  where em.UserID == loginuser && b.IsDeleted == null && em.IsDeleted==null
                                  select new
                                  {
                                      model = b,
                                      SchoolName = s.School
                                  }).ToList();
                    //var result = db.tblEventTypes.SqlQuery("select * from tblEventType").ToList();
                    foreach (var a in result)
                    {
                        EventsType items = new EventsType();
                        items.Name = a.model.EventName;
                        items.Id = a.model.EventId.ToString();


                        if (a.model.Status.ToString() == "True")
                        {
                            items.Status = "Activate";
                            items.Extra10 = "btn btn-block btn-success btn-sm";
                        }
                        else
                        {
                            items.Status = "DeActivate";
                            items.Extra10 = "btn btn-block btn-danger btn-sm";
                        }
                        //  items.Status = a.Status.ToString();
                        items.School = a.SchoolName;
                        items.SchoolID = Convert.ToInt32(a.model.SchoolID);
                        list.Add(items);
                    }
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/deleteEventTypeById")]
        [System.Web.Http.HttpPost]
        public string deleteEventTypeById(Int32 id)
        {
            Int64 idd = Convert.ToInt64(id);
            //var employer = new tblEventType { EventId = id };
            //db.Entry(employer).State = EntityState.Deleted;
            var aa = db.tblEventTypes.SingleOrDefault(s => s.EventId == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;

            db.SaveChanges();

            return "Record successfully deleted"; ;

        }
        [System.Web.Http.Route("api/EventsAPI/getAllActiveEventsBySchool")]
        [System.Web.Http.HttpPost]
        public List<EventsType> getAllActiveEventsBySchool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<EventsType> list = new List<EventsType>();

            try
            {
                var result = db.tblEventTypes.SqlQuery("select * from tblEventType where Status=1 and SchoolID='" + SchoolID + "' and IsDeleted is null").ToList();
                foreach (var a in result)
                {
                    EventsType items = new EventsType();
                    items.Name = a.EventName;
                    items.Id = a.EventId.ToString();

                    //  items.Status = a.Status.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        [System.Web.Http.Route("api/EventsAPI/getAllActiveEvents")]
        [System.Web.Http.HttpPost]
        public List<EventsType> getAllActiveEvents()
        {
            List<EventsType> list = new List<EventsType>();

            try
            {
                var result = db.tblEventTypes.SqlQuery("select * from tblEventType where Status=1 and IsDeleted is null ").ToList();
                foreach (var a in result)
                {
                    EventsType items = new EventsType();
                    items.Name = a.EventName;
                    items.Id = a.EventId.ToString();

                    //  items.Status = a.Status.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        [System.Web.Http.Route("api/EventsAPI/getAllClassAndSectionBySchool")]
        [System.Web.Http.HttpPost]
        public List<EventsType> getAllClassAndSectionBySchool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<EventsType> list = new List<EventsType>();

            try
            {
                var result = (from c in db.tblCourses
                              join s in db.tblSections on c.Id equals s.ClassId
                              where s.Status == true && c.Status == true && c.IsDeleted == null && s.IsDeleted == null && c.SchoolID == SchoolID && s.SchoolID == SchoolID
                              select new
                              {
                                  c,
                                  s

                              }).ToList();


                foreach (var a in result)
                {
                    EventsType items = new EventsType();
                    items.Name = a.c.CourseName.ToString() + '-' + a.s.SectionName.ToString();
                    items.Id = a.c.Id.ToString() + '-' + a.s.Id.ToString();

                    //  items.Status = a.Status.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/getAllClassAndSection")]
        [System.Web.Http.HttpPost]
        public List<EventsType> getAllClassAndSection()
        {
            List<EventsType> list = new List<EventsType>();

            try
            {
                var result = (from c in db.tblCourses
                              join s in db.tblSections on c.Id equals s.ClassId
                              where s.Status == true && c.Status == true && c.IsDeleted == null && s.IsDeleted == null
                              select new
                              {
                                  c,
                                  s

                              }).ToList();


                foreach (var a in result)
                {
                    EventsType items = new EventsType();
                    items.Name = a.c.CourseName.ToString() + '-' + a.s.SectionName.ToString();
                    items.Id = a.c.Id.ToString() + '-' + a.s.Id.ToString();

                    //  items.Status = a.Status.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/getAllDepartmentBySchool")]
        [System.Web.Http.HttpPost]
        public List<EventsType> getAllDepartmentBySchool(List<string> aa)
        {
            int SchoolID = Convert.ToInt32(aa[0]);
            List<EventsType> list = new List<EventsType>();

            try
            {
                var result = db.tblDepartmnets.Where(x => x.Status == true && x.SchoolID == SchoolID && x.IsDeleted == null).ToList();

                foreach (var a in result)
                {
                    EventsType items = new EventsType();
                    items.Name = a.DepartmentName;
                    items.Id = a.DepartmentId.ToString();

                    //  items.Status = a.Status.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/getAllDepartment")]
        [System.Web.Http.HttpPost]
        public List<EventsType> getAllDepartment()
        {
            List<EventsType> list = new List<EventsType>();

            try
            {
                var result = db.tblDepartmnets.Where(x => x.Status == true).ToList();

                foreach (var a in result)
                {
                    EventsType items = new EventsType();
                    items.Name = a.DepartmentName;
                    items.Id = a.DepartmentId.ToString();

                    //  items.Status = a.Status.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/saveEventDetails")]
        [System.Web.Http.HttpPost]
        public string saveEventDetails(EventsDetails events)
        {
            sqlHelper obj = new sqlHelper();
            string msg = "";
            if (string.IsNullOrEmpty(events.Id))
            {
                if (events.EventFor == "2")
                {

                    string[] evensts = events.ClassId.Split(',');
                   
                    if (evensts[0]=="")
                    {
                        msg = "please select Class and Section";

                    }
                    else
                    {
                        for (int i = 0; i < evensts.Length; i++)
                        {
                            tblEventDetail cta = new SchoolErp.tblEventDetail();
                            cta.EventName = events.EventName;
                            cta.EventType = Convert.ToInt16(events.EventType);
                            cta.Description = events.Description;
                            cta.StartdateTime = Convert.ToDateTime(events.StartDatechange);
                            cta.EnddateTime = Convert.ToDateTime(events.EndDatechange);
                            cta.EventFor = events.EventFor;
                            cta.InchargeName = events.InchargName;
                            cta.courseId = evensts[i].ToString().Split('-')[0];
                            cta.SectionId = evensts[i].ToString().Split('-')[1];
                            cta.DepartmentId = 0;
                            cta.Status = true;
                            cta.SchoolID = Convert.ToInt32(events.SchoolID);
                            db.tblEventDetails.Add(cta);
                            db.SaveChanges();
                         
                            int SchoolIDD = Convert.ToInt32(events.SchoolID);
                            int courseIdd = Convert.ToInt32(evensts[i].ToString().Split('-')[0]);
                            int SectionIdd = Convert.ToInt32(evensts[i].ToString().Split('-')[1]);

                            if (events.SendSms == true)
                            {
                                string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                if (Checksmsservice != null)
                                {
                                    string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                    string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                    var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == SchoolIDD).ID.ToString();
                                    string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + SchoolIDD + "'");
                                    string studentsmsno = string.Join(",", db.TBLStudents.Where(x => x.SchoolID == SchoolIDD && x.IsDeleted == null && x.Status == 3 && x.ClassID == courseIdd && x.SectionID == SectionIdd && x.AcademicYear == GetActiveAcademicyear).Select(x => x.SMSmobileNo.ToString()));

                                    string authKey = GetauthKey;
                                    //Multiple mobiles numbers separated by comma
                                    string mobileNumber = studentsmsno;
                                    //Sender ID,While using route4 sender id should be 6 characters long.
                                    string senderId = GetsenderId;
                                    //Your message to send, Add URL encoding here.
                                    StringBuilder st = new StringBuilder();
                                    st.AppendLine("Hi Sir/mam,");
                                    st.AppendLine("Event Name : " + events.EventName);
                                    st.AppendLine("Event Start Date Time :" + Convert.ToDateTime(events.StartDatechange));
                                    st.AppendLine("Event End Date Time :" + Convert.ToDateTime(events.EndDatechange));
                                    st.AppendLine("Event description  :" + events.Description);

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
                                        //string sendSMSUri = "http://login.yourbulksms.com/api/sendhttp.php";
                                        ////Create HTTPWebrequest
                                        //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                                        ////Prepare and Add URL Encoded data
                                        //UTF8Encoding encoding = new UTF8Encoding();
                                        //byte[] data1 = encoding.GetBytes(sbPostData.ToString());
                                        ////Specify post method
                                        //httpWReq.Method = "POST";
                                        //httpWReq.ContentType = "application/x-www-form-urlencoded";
                                        //httpWReq.ContentLength = data1.Length;
                                        //using (Stream stream = httpWReq.GetRequestStream())
                                        //{
                                        //    stream.Write(data1, 0, data1.Length);
                                        //}
                                        ////Get the response
                                        //HttpWebResponse response1 = (HttpWebResponse)httpWReq.GetResponse();
                                        //StreamReader reader = new StreamReader(response1.GetResponseStream());
                                        //string responseString = reader.ReadToEnd();

                                        ////Close the response
                                        //reader.Close();
                                        //response1.Close();
                                    }
                                    catch (SystemException ex)
                                    {
                                        ex.Message.ToString();
                                    }



                                }




                            }
                        }
                        msg = "Events Added succesfully";
                    }
                   
                }
                else if (events.EventFor == "3")
                {
                    string[] evensts = events.Department.Split(',');

                    if (evensts[0]=="0")
                    {
                        msg = "please select Department";
                    }
                    else
                    {
                        for (int i = 0; i < evensts.Length; i++)
                        {
                            tblEventDetail cta = new SchoolErp.tblEventDetail();
                            cta.EventName = events.EventName;
                            cta.EventType = Convert.ToInt32(events.EventType);
                            cta.Description = events.Description;
                            cta.StartdateTime = Convert.ToDateTime(events.StartDatechange);
                            cta.EnddateTime = Convert.ToDateTime(events.EndDatechange);
                            cta.InchargeName = events.InchargName;
                            cta.EventFor = events.EventFor;
                            cta.courseId = "";
                            cta.SectionId = "";
                            cta.Status = true;
                            cta.DepartmentId = Convert.ToInt64(evensts[i]);
                            cta.SchoolID = Convert.ToInt32(events.SchoolID);
                            db.tblEventDetails.Add(cta);
                            db.SaveChanges();

                            //send sms

                            int SchoolIDD = Convert.ToInt32(events.SchoolID);
                            long DepartmentId = Convert.ToInt64(evensts[i]);


                            if (events.SendSms == true)
                            {
                                string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                if (Checksmsservice != null)
                                {
                                    string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                    string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");

                                    string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + SchoolIDD + "'");


                                    string EmpMobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == SchoolIDD && x.DeptID == DepartmentId && x.Status == true && x.IsDeleted == null).Select(x => x.Mobile.ToString()));

                                    string authKey = GetauthKey;
                                    //Multiple mobiles numbers separated by comma
                                    string mobileNumber = EmpMobileNo;
                                    //Sender ID,While using route4 sender id should be 6 characters long.
                                    string senderId = GetsenderId;
                                    //Your message to send, Add URL encoding here.
                                    StringBuilder st = new StringBuilder();
                                    st.AppendLine("Hi Sir/mam,");
                                    st.AppendLine("Event Name : " + events.EventName);
                                    st.AppendLine("Event Start Date Time :" + Convert.ToDateTime(events.StartDatechange));
                                    st.AppendLine("Event End Date Time :" + Convert.ToDateTime(events.EndDatechange));
                                    st.AppendLine("Event description  :" + events.Description);
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
                        msg = "Events Added succesfully";
                    }

                  
                }
                else if (events.EventFor=="1")
                {
                    tblEventDetail cta = new SchoolErp.tblEventDetail();
                    cta.EventName = events.EventName;
                    cta.EventType = Convert.ToInt32(events.EventType);
                    cta.Description = events.Description;
                    cta.StartdateTime = Convert.ToDateTime(events.StartDatechange);
                    cta.EnddateTime = Convert.ToDateTime(events.EndDatechange);
                    cta.InchargeName = events.InchargName;
                    cta.EventFor = events.EventFor;
                    cta.courseId = "";
                    cta.SectionId = "";
                    cta.DepartmentId = 0;
                    cta.Status = true;
                    cta.SchoolID = Convert.ToInt32(events.SchoolID);
                    db.tblEventDetails.Add(cta);
                    db.SaveChanges();
                   msg = "Events Added succesfully";
                    //SMS Common to all


                    int SchoolIDD = Convert.ToInt32(events.SchoolID);
                
                    if (events.SendSms == true)
                    {
                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                        if (Checksmsservice != null)
                        {
                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                            var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == SchoolIDD).ID.ToString();
                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + SchoolIDD + "'");
                            string studentsmsno = string.Join(",", db.TBLStudents.Where(x => x.SchoolID == SchoolIDD && x.IsDeleted == null && x.Status == 3 && x.AcademicYear == GetActiveAcademicyear).Select(x => x.SMSmobileNo.ToString()));
                            string EmpMobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == SchoolIDD && x.Status==true && x.IsDeleted == null).Select(x => x.Mobile.ToString()));
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = studentsmsno+","+ EmpMobileNo;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Event Name : " + events.EventName);
                            st.AppendLine("Event Start Date Time :" + Convert.ToDateTime(events.StartDatechange));
                            st.AppendLine("Event End Date Time :" + Convert.ToDateTime(events.EndDatechange));
                            st.AppendLine("Event description  :" + events.Description);
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

                    //




                }
                else
                {
                    msg = "please select Event For";
                }
                //msg = "Events Added succesfully";
            }
                else
                {
                    int iddd = Convert.ToInt32(events.Id);
                    var resultl = new tblEventDetail { EventId = iddd };
                    db.Entry(resultl).State = EntityState.Deleted;
                    db.SaveChanges();

                    if (events.EventFor == "2")
                    {
                    string[] evensts = events.ClassId.Split(',');

                    if (evensts[0] == "")
                    {
                        msg = "please select Class and Section";

                    }
                    else
                    {
                       
                        for (int i = 0; i < evensts.Length; i++)
                        {
                            tblEventDetail cta = new SchoolErp.tblEventDetail();
                            cta.EventName = events.EventName;
                            cta.EventType = Convert.ToInt32(events.EventType);
                            cta.Description = events.Description;
                            cta.StartdateTime = Convert.ToDateTime(events.StartDatechange);
                            cta.EnddateTime = Convert.ToDateTime(events.EndDatechange);
                            cta.EventFor = events.EventFor;
                            cta.InchargeName = events.InchargName;
                            cta.courseId = evensts[i].ToString().Split('-')[0];
                            cta.SectionId = evensts[i].ToString().Split('-')[1];
                            cta.DepartmentId = 0;
                            cta.Status = true;
                            cta.SchoolID = Convert.ToInt32(events.SchoolID);
                            db.tblEventDetails.Add(cta);
                            db.SaveChanges();
                            //students

                            int SchoolIDD = Convert.ToInt32(events.SchoolID);
                            int courseIdd = Convert.ToInt32(evensts[i].ToString().Split('-')[0]);
                            int SectionIdd = Convert.ToInt32(evensts[i].ToString().Split('-')[1]);

                            if (events.SendSms == true)
                            {
                                string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                if (Checksmsservice != null)
                                {
                                    string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                    string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                    var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == SchoolIDD).ID.ToString();
                                    string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + SchoolIDD + "'");
                                    string studentsmsno = string.Join(",", db.TBLStudents.Where(x => x.SchoolID == SchoolIDD && x.IsDeleted == null && x.Status == 3 && x.ClassID == courseIdd && x.SectionID == SectionIdd && x.AcademicYear == GetActiveAcademicyear).Select(x => x.SMSmobileNo.ToString()));

                                    string authKey = GetauthKey;
                                    //Multiple mobiles numbers separated by comma
                                    string mobileNumber = studentsmsno;
                                    //Sender ID,While using route4 sender id should be 6 characters long.
                                    string senderId = GetsenderId;
                                    //Your message to send, Add URL encoding here.
                                    StringBuilder st = new StringBuilder();
                                    st.AppendLine("Hi Sir/mam,");
                                    st.AppendLine("Edited Event");
                                    st.AppendLine("Event Name : " + events.EventName);
                                    st.AppendLine("Event Start Date Time :" + Convert.ToDateTime(events.StartDatechange));
                                    st.AppendLine("Event End Date Time :" + Convert.ToDateTime(events.EndDatechange));
                                    st.AppendLine("Event description  :" + events.Description);

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
                            //


                        }
                        msg = "Events Updated Successfully";

                    }
                        
                    }
                    else if (events.EventFor == "3")
                    {
                        string[] evensts = events.Department.Split(',');
                    if (evensts[0] == "0")
                    {
                        msg = "please select Department";
                    }
                    else
                    {
                        for (int i = 0; i < evensts.Length; i++)
                        {
                            tblEventDetail cta = new SchoolErp.tblEventDetail();
                            cta.EventName = events.EventName;
                            cta.EventType = Convert.ToInt32(events.EventType);
                            cta.Description = events.Description;
                            cta.StartdateTime = Convert.ToDateTime(events.StartDatechange);
                            cta.EnddateTime = Convert.ToDateTime(events.EndDatechange);
                            cta.InchargeName = events.InchargName;
                            cta.EventFor = events.EventFor;
                            cta.courseId = "";
                            cta.SectionId = "";
                            cta.Status = true;
                            cta.DepartmentId = Convert.ToInt64(evensts[i]);
                            db.tblEventDetails.Add(cta);
                            cta.SchoolID = Convert.ToInt32(events.SchoolID);
                            db.SaveChanges();
                            //employee department sms
                            int SchoolIDD = Convert.ToInt32(events.SchoolID);
                            long DepartmentId = Convert.ToInt64(evensts[i]);


                            if (events.SendSms == true)
                            {
                                string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                if (Checksmsservice != null)
                                {
                                    string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                                    string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");

                                    string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + SchoolIDD + "'");


                                    string EmpMobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == SchoolIDD && x.DeptID == DepartmentId && x.Status == true && x.IsDeleted == null).Select(x => x.Mobile.ToString()));

                                    string authKey = GetauthKey;
                                    //Multiple mobiles numbers separated by comma
                                    string mobileNumber = EmpMobileNo;
                                    //Sender ID,While using route4 sender id should be 6 characters long.
                                    string senderId = GetsenderId;
                                    //Your message to send, Add URL encoding here.
                                    StringBuilder st = new StringBuilder();
                                    st.AppendLine("Hi Sir/mam,");
                                    st.AppendLine("Edited");
                                    st.AppendLine("Event Name : " + events.EventName);
                                    st.AppendLine("Event Start Date Time :" + Convert.ToDateTime(events.StartDatechange));
                                    st.AppendLine("Event End Date Time :" + Convert.ToDateTime(events.EndDatechange));
                                    st.AppendLine("Event description  :" + events.Description);
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
                        msg = "Events Updated Successfully";
                    }
                   
                    }
                    else if  (events.EventFor == "1")
                    {
                        tblEventDetail cta = new SchoolErp.tblEventDetail();
                        cta.EventName = events.EventName;
                        cta.EventType = Convert.ToInt32(events.EventType);
                        cta.Description = events.Description;
                        cta.StartdateTime = Convert.ToDateTime(events.StartDatechange);
                        cta.EnddateTime = Convert.ToDateTime(events.EndDatechange);
                        cta.InchargeName = events.InchargName;
                        cta.EventFor = events.EventFor;
                        cta.courseId = "";
                        cta.SectionId = "";
                        cta.DepartmentId = 0;
                        cta.Status = true;
                        cta.SchoolID = Convert.ToInt32(events.SchoolID);
                        db.tblEventDetails.Add(cta);
                        db.SaveChanges();

                    //common sms whole School emp+stu
                    int SchoolIDD = Convert.ToInt32(events.SchoolID);

                    if (events.SendSms == true)
                    {
                        string Checksmsservice = obj.ExecuteScaler("select id  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                        if (Checksmsservice != null)
                        {
                            string GetauthKey = obj.ExecuteScaler("select authKey  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                            string GetsenderId = obj.ExecuteScaler("select senderId  from tblSchoolSms where SchoolID='" + SchoolIDD + "' and  active=1");
                            var GetActiveAcademicyear = db.tblAcademicYears.FirstOrDefault(x => x.Status == true && x.CurrActive == true && x.SchoolID == SchoolIDD).ID.ToString();
                            string Schoolname = obj.ExecuteScaler("select school from tblSchoolDetails where ID='" + SchoolIDD + "'");
                            string studentsmsno = string.Join(",", db.TBLStudents.Where(x => x.SchoolID == SchoolIDD && x.IsDeleted == null && x.Status == 3 && x.AcademicYear == GetActiveAcademicyear).Select(x => x.SMSmobileNo.ToString()));
                            string EmpMobileNo = string.Join(",", db.tblEmployees.Where(x => x.SchoolID == SchoolIDD && x.Status == true && x.IsDeleted == null).Select(x => x.Mobile.ToString()));
                            string authKey = GetauthKey;
                            //Multiple mobiles numbers separated by comma
                            string mobileNumber = studentsmsno + "," + EmpMobileNo;
                            //Sender ID,While using route4 sender id should be 6 characters long.
                            string senderId = GetsenderId;
                            //Your message to send, Add URL encoding here.
                            StringBuilder st = new StringBuilder();
                            st.AppendLine("Hi Sir/mam,");
                            st.AppendLine("Edited Event");
                            st.AppendLine("Event Name : " + events.EventName);
                            st.AppendLine("Event Start Date Time :" + Convert.ToDateTime(events.StartDatechange));
                            st.AppendLine("Event End Date Time :" + Convert.ToDateTime(events.EndDatechange));
                            st.AppendLine("Event description  :" + events.Description);
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
                    msg = "Events Updated Successfully";

                }
                    else
                {
                    msg = "please select Event For";
                }
                   
                }

                return msg;
            }
        


        [System.Web.Http.Route("api/EventsAPI/getAllEvents")]
        [System.Web.Http.HttpPost]
        public List<EventsDetails> getAllEvents(List<string> aa)
        {
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<EventsDetails> list = new List<EventsDetails>();

            try
            {

                if (typeuser == 2)
                {
                    DataTable dt = obj.getDataTable(@"select a.ID SchoolID,a.School,c.CourseName ClassId,s.SectionName Section,ed.EventName,cast(ed.StartdateTime as varchar(50)) StartDate
                            ,cast(ed.EnddateTime as varchar(50)) EndDate,  case  when ed.EventFor=1 then 'Common to All' when ed.EventFor=2 then 'Selected Batch' when ed.EventFor=3
                            then 'Selected Department' end EventFor,et.EventName EventType,ed.EventId eventId 
                            ,d.DepartmentName  Department,ed.EventType eventTypeId,CONVERT(varchar(15),CAST(ed.StartdateTime AS TIME),100) starttime,ed.EventFor EventForId,
                            CONVERT(varchar(15),CAST(ed.EnddateTime AS TIME),100) endtime,ed.courseId+'-'+ed.SectionId classSection,ed.InchargeName InchargName,
                            ed.Description,convert(varchar(10), ed.StartdateTime,120) startdated,convert(varchar(10), ed.EnddateTime,120) enddated from tblEventDetails ed
                            left outer join tblCourses c on c.Id=ed.courseId
                            left outer join tblSections s on s.Id=ed.SectionId
                            left outer join tblDepartmnet d on d.DepartmentId=ed.DepartmentId
                            left outer join tblEventType et  on et.EventId=ed.EventType 
							left outer join tblSchoolDetails a on a.ID = ed.SchoolID
                            where ed.EventFor in (1,2) and ed.IsDeleted is null order by ed.StartdateTime desc");
                    //var result = db.Database.SqlQuery<EventsDetails>(sliderquery).ToList();

                    // var result = db.Database.SqlQuery(@"").ToList();
                    foreach (DataRow dr in dt.Rows)
                    {
                        EventsDetails items = new EventsDetails();
                        items.EventName = dr["EventName"].ToString();
                        items.EventFor = dr["EventFor"].ToString();
                        items.EventType = dr["EventType"].ToString();
                        items.startdated = dr["startdated"].ToString();
                        items.enddated = dr["enddated"].ToString();
                        items.School = dr["School"].ToString();
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                        if (string.IsNullOrEmpty(dr["Section"].ToString()))
                        {
                            items.Section = "ALL";
                        }
                        else
                        {
                            items.Section = dr["Section"].ToString();
                        }
                        if (string.IsNullOrEmpty(dr["ClassId"].ToString()))
                        {
                            items.ClassId = "ALL";
                        }
                        else
                        {
                            items.ClassId = dr["ClassId"].ToString();
                        }
                        items.StartDate = dr["StartDate"].ToString();
                        items.EndDate = dr["EndDate"].ToString();
                        if (string.IsNullOrEmpty(dr["Department"].ToString()))
                        {
                            items.Department = "0";
                        }
                        else
                        {
                            items.Department = dr["Department"].ToString();
                        }
                        items.eventId = Convert.ToInt32(dr["eventId"].ToString());
                        items.eventTypeId = Convert.ToInt32(dr["eventTypeId"].ToString());
                        items.Description = dr["Description"].ToString();
                        items.InchargName = dr["InchargName"].ToString();
                        //items.StartDate = a.StartDate;
                        items.starttime = dr["starttime"].ToString();
                        items.endtime = dr["endtime"].ToString();
                        //  items.EndDate= a.EndDate;

                        items.EventForId = dr["EventForId"].ToString();
                        items.classSection = dr["classSection"].ToString();






                        //items.Department = a.Department.ToString();
                        //items.EventType = a.EventId.ToString();
                        //items.InchargName = a.EventId.ToString();
                        //items.StartDate = a.EventId.ToString();


                        //  items.Status = a.Status.ToString();
                        list.Add(items);
                    }
                }
                else
                {
                    DataTable dt = obj.getDataTable(@"select a.ID SchoolID,a.School,c.CourseName ClassId,s.SectionName Section,ed.EventName,cast(ed.StartdateTime as varchar(50)) StartDate
                            ,cast(ed.EnddateTime as varchar(50)) EndDate,  case  when ed.EventFor=1 then 'Common to All' when ed.EventFor=2 then 'Selected Batch' when ed.EventFor=3
                            then 'Selected Department' end EventFor,et.EventName EventType,ed.EventId eventId 
                            ,d.DepartmentName  Department,ed.EventType eventTypeId,CONVERT(varchar(15),CAST(ed.StartdateTime AS TIME),100) starttime,ed.EventFor EventForId,
                            CONVERT(varchar(15),CAST(ed.EnddateTime AS TIME),100) endtime,ed.courseId+'-'+ed.SectionId classSection,ed.InchargeName InchargName,
                            ed.Description,convert(varchar(10), ed.StartdateTime,120) startdated,convert(varchar(10), ed.EnddateTime,120) enddated from tblEventDetails ed
                            left outer join tblCourses c on c.Id=ed.courseId
                            left outer join tblSections s on s.Id=ed.SectionId
                            left outer join tblDepartmnet d on d.DepartmentId=ed.DepartmentId
                            left outer join tblEventType et  on et.EventId=ed.EventType 
							left outer join tblSchoolDetails a on a.ID = ed.SchoolID
							left outer join tblEmployee em on ed.SchoolID=em.SchoolID
                            where em.UserID ='" + loginuser + "' and em.IsDeleted is null and ed.EventFor in (1,2) and ed.IsDeleted is null order by ed.StartdateTime desc");
                    //var result = db.Database.SqlQuery<EventsDetails>(sliderquery).ToList();

                    // var result = db.Database.SqlQuery(@"").ToList();
                    foreach (DataRow dr in dt.Rows)
                    {
                        EventsDetails items = new EventsDetails();
                        items.EventName = dr["EventName"].ToString();
                        items.EventFor = dr["EventFor"].ToString();
                        items.EventType = dr["EventType"].ToString();
                        items.startdated = dr["startdated"].ToString();
                        items.enddated = dr["enddated"].ToString();
                        items.School = dr["School"].ToString();
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                        if (string.IsNullOrEmpty(dr["Section"].ToString()))
                        {
                            items.Section = "ALL";
                        }
                        else
                        {
                            items.Section = dr["Section"].ToString();
                        }
                        if (string.IsNullOrEmpty(dr["ClassId"].ToString()))
                        {
                            items.ClassId = "ALL";
                        }
                        else
                        {
                            items.ClassId = dr["ClassId"].ToString();
                        }
                        items.StartDate = dr["StartDate"].ToString();
                        items.EndDate = dr["EndDate"].ToString();
                        if (string.IsNullOrEmpty(dr["Department"].ToString()))
                        {
                            items.Department = "0";
                        }
                        else
                        {
                            items.Department = dr["Department"].ToString();
                        }
                        items.eventId = Convert.ToInt32(dr["eventId"].ToString());
                        items.eventTypeId = Convert.ToInt32(dr["eventTypeId"].ToString());
                        items.Description = dr["Description"].ToString();
                        items.InchargName = dr["InchargName"].ToString();
                        //items.StartDate = a.StartDate;
                        items.starttime = dr["starttime"].ToString();
                        items.endtime = dr["endtime"].ToString();
                        //  items.EndDate= a.EndDate;

                        items.EventForId = dr["EventForId"].ToString();
                        items.classSection = dr["classSection"].ToString();






                        //items.Department = a.Department.ToString();
                        //items.EventType = a.EventId.ToString();
                        //items.InchargName = a.EventId.ToString();
                        //items.StartDate = a.EventId.ToString();


                        //  items.Status = a.Status.ToString();
                        list.Add(items);
                    }

                    //foreach (var a in result)
                    //{
                    //    EventsDetails items = new EventsDetails();
                    //    items.EventName = a.EventName;
                    //    items.EventFor = a.EventFor;
                    //    items.EventType = a.EventType;
                    //    items.startdated = a.startdated;
                    //    items.enddated = a.enddated;
                    //    items.School = a.School;
                    //    items.SchoolID = a.SchoolID;
                    //    if (string.IsNullOrEmpty(a.Section))
                    //    {
                    //        items.Section = "ALL";
                    //    }
                    //    else
                    //    {
                    //        items.Section = a.Section.ToString();
                    //    }
                    //    if (string.IsNullOrEmpty(a.ClassId))
                    //    {
                    //        items.ClassId = "ALL";
                    //    }
                    //    else
                    //    {
                    //        items.ClassId = a.ClassId;
                    //    }
                    //    items.StartDate = a.StartDate;
                    //    items.EndDate = a.EndDate;
                    //    if (string.IsNullOrEmpty(a.Department))
                    //    {
                    //        items.Department = "0";
                    //    }
                    //    else
                    //    {
                    //        items.Department = a.Department;
                    //    }
                    //    items.eventId = a.eventId;
                    //    items.eventTypeId = a.eventTypeId;
                    //    items.Description = a.Description;
                    //    items.InchargName = a.InchargName;
                    //    //items.StartDate = a.StartDate;
                    //    items.starttime = a.starttime;
                    //    items.endtime = a.endtime;
                    //    //  items.EndDate= a.EndDate;

                    //    items.EventForId = a.EventForId;
                    //    items.classSection = a.classSection;






                    //    //items.Department = a.Department.ToString();
                    //    //items.EventType = a.EventId.ToString();
                    //    //items.InchargName = a.EventId.ToString();
                    //    //items.StartDate = a.EventId.ToString();


                    //    //  items.Status = a.Status.ToString();
                    //    list.Add(items);

                    //}
                }

            }
            catch (Exception e)
            { throw e; }
            return list;
        }




        [System.Web.Http.Route("api/EventsAPI/getAllEventsForDepartment")]
        [System.Web.Http.HttpPost]
        public List<EventsDetails> getAllEventsForDepartment(List<string> aa)
        {

            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            List<EventsDetails> list = new List<EventsDetails>();

            try
            {

                if (typeuser == 2)
                {
                    DataTable dt = obj.getDataTable(@"select a.ID SchoolID, a.School, c.CourseName ClassId,s.SectionName Section,ed.EventName,cast(ed.StartdateTime as varchar(50)) StartDate
                                    ,cast(ed.EnddateTime as varchar(50)) EndDate,  case  when ed.EventFor=1 then 'Common to All' when ed.EventFor=2 then 'Selected Batch' when ed.EventFor=3
                                    then 'Selected Department' end EventFor,et.EventName EventType,ed.EventId eventId ,d.DepartmentName  Department,
                                    ed.InchargeName InchargName,ed.Description,ed.EventType eventTypeId,CONVERT(varchar(15),CAST(ed.StartdateTime AS TIME),100) starttime,ed.EventFor EventForId,
                                     CONVERT(varchar(15),CAST(ed.EnddateTime AS TIME),100) endtime,ed.DepartmentId depId,convert(varchar(10), ed.StartdateTime,120) startdated,convert(varchar(10), ed.EnddateTime,120) enddated from tblEventDetails ed
                                    left outer join tblCourses c on c.Id=ed.courseId
                                    left outer join tblSections s on s.Id=ed.SectionId
                                    left outer join tblDepartmnet d on d.DepartmentId=ed.DepartmentId
                                    left outer join tblEventType et  on et.EventId=ed.EventType 
									left outer join tblSchoolDetails a on a.ID = ed.SchoolID
                                    where ed.EventFor in (1,3) and ed.IsDeleted is null order by ed.StartdateTime desc");
                    //  var result = db.Database.SqlQuery<EventsDetails>(sliderquery).ToList();

                    // var result = db.Database.SqlQuery(@"").ToList();
                    foreach (DataRow dr in dt.Rows)
                    {
                        EventsDetails items = new EventsDetails();
                        items.EventName = dr["EventName"].ToString();
                        items.EventFor = dr["EventFor"].ToString();
                        items.EventType = dr["EventType"].ToString();
                        items.StartDate = dr["StartDate"].ToString();
                        items.EndDate = dr["EndDate"].ToString();
                        items.startdated = dr["startdated"].ToString();
                        items.enddated = dr["enddated"].ToString();
                        items.Description = dr["Description"].ToString();
                        items.InchargName = dr["InchargName"].ToString();
                        items.EventForId = dr["EventForId"].ToString();
                        items.School = dr["School"].ToString();
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
                        if (string.IsNullOrEmpty(dr["Department"].ToString()))
                        {
                            items.Department = "ALL";
                        }
                        else
                        {
                            items.Department = dr["Department"].ToString();
                        }
                        items.starttime = dr["starttime"].ToString();
                        items.endtime = dr["endtime"].ToString();
                        items.eventId = Convert.ToInt32(dr["eventId"].ToString());
                        items.depId = Convert.ToInt32(dr["depId"].ToString());
                        items.eventTypeId = Convert.ToInt32(dr["eventTypeId"].ToString());
                        //items.Department = a.Department.ToString();
                        //items.EventType = a.EventId.ToString();
                        //items.InchargName = a.EventId.ToString();
                        //items.StartDate = a.EventId.ToString();


                        //  items.Status = a.Status.ToString();
                        list.Add(items);
                    }
                }
                else
                {
                    //string sliderquery =

                    DataTable dt = obj.getDataTable(@"select a.ID SchoolID, a.School, c.CourseName ClassId,s.SectionName Section,ed.EventName,cast(ed.StartdateTime as varchar(50)) StartDate
                                    ,cast(ed.EnddateTime as varchar(50)) EndDate,  case  when ed.EventFor=1 then 'Common to All' when ed.EventFor=2 then 'Selected Batch' when ed.EventFor=3
                                    then 'Selected Department' end EventFor,et.EventName EventType,ed.EventId eventId ,d.DepartmentName  Department,
                                    ed.InchargeName InchargName,ed.Description,ed.EventType eventTypeId,CONVERT(varchar(15),CAST(ed.StartdateTime AS TIME),100) starttime,ed.EventFor EventForId,
                                     CONVERT(varchar(15),CAST(ed.EnddateTime AS TIME),100) endtime,ed.DepartmentId depId,convert(varchar(10), ed.StartdateTime,120) startdated,convert(varchar(10), ed.EnddateTime,120) enddated from tblEventDetails ed
                                    left outer join tblCourses c on c.Id=ed.courseId
                                    left outer join tblSections s on s.Id=ed.SectionId
                                    left outer join tblDepartmnet d on d.DepartmentId=ed.DepartmentId
                                    left outer join tblEventType et  on et.EventId=ed.EventType 
									left outer join tblSchoolDetails a on a.ID = ed.SchoolID
									left outer join tblEmployee em on ed.SchoolID=em.SchoolID
                                    where em.UserID='" + loginuser + "' and em.IsDeleted is null and ed.EventFor in (1,3) and ed.IsDeleted is null order by ed.StartdateTime desc");
                    foreach (DataRow dr in dt.Rows)
                    {
                        EventsDetails items = new EventsDetails();
                        items.EventName = dr["EventName"].ToString();
                        items.EventFor = dr["EventFor"].ToString();
                        items.EventType = dr["EventType"].ToString();
                        items.StartDate = dr["StartDate"].ToString();
                        items.EndDate = dr["EndDate"].ToString();
                        items.startdated = dr["startdated"].ToString();
                        items.enddated = dr["enddated"].ToString();
                        items.Description = dr["Description"].ToString();
                        items.InchargName = dr["InchargName"].ToString();
                        items.EventForId = dr["EventForId"].ToString();
                        items.School = dr["School"].ToString();
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
                        if (string.IsNullOrEmpty(dr["Department"].ToString()))
                        {
                            items.Department = "ALL";
                        }
                        else
                        {
                            items.Department = dr["Department"].ToString();
                        }
                        items.starttime = dr["starttime"].ToString();
                        items.endtime = dr["endtime"].ToString();
                        items.eventId = Convert.ToInt32(dr["eventId"].ToString());
                        items.depId = Convert.ToInt32(dr["depId"].ToString());
                        items.eventTypeId = Convert.ToInt32(dr["eventTypeId"].ToString());
                        //items.Department = a.Department.ToString();
                        //items.EventType = a.EventId.ToString();
                        //items.InchargName = a.EventId.ToString();
                        //items.StartDate = a.EventId.ToString();


                        //  items.Status = a.Status.ToString();
                        list.Add(items);
                    }
                    // var result = db.Database.SqlQuery<EventsDetails>(sliderquery).ToList();

                    // var result = db.Database.SqlQuery(@"").ToList();
                    //foreach (var a in result)
                    //{
                    //    EventsDetails items = new EventsDetails();
                    //    items.EventName = a.EventName;
                    //    items.EventFor = a.EventFor;
                    //    items.EventType = a.EventType;
                    //    items.StartDate = a.StartDate;
                    //    items.EndDate = a.EndDate;
                    //    items.startdated = a.startdated;
                    //    items.enddated = a.enddated;
                    //    items.Description = a.Description;
                    //    items.InchargName = a.InchargName;
                    //    items.EventForId = a.EventForId;
                    //    items.School = a.School;
                    //    items.SchoolID = a.SchoolID;
                    //    if (string.IsNullOrEmpty(a.Department))
                    //    {
                    //        items.Department = "ALL";
                    //    }
                    //    else
                    //    {
                    //        items.Department = a.Department;
                    //    }
                    //    items.starttime = a.starttime;
                    //    items.endtime = a.endtime;
                    //    items.eventId = a.eventId;
                    //    items.depId = a.depId;
                    //    items.eventTypeId = a.eventTypeId;
                    //    //items.Department = a.Department.ToString();
                    //    //items.EventType = a.EventId.ToString();
                    //    //items.InchargName = a.EventId.ToString();
                    //    //items.StartDate = a.EventId.ToString();


                    //    //  items.Status = a.Status.ToString();
                    //    list.Add(items);
                    //}
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }






        [System.Web.Http.Route("api/EventsAPI/deleteEventsById")]
        [System.Web.Http.HttpPost]
        public string deleteEventsById(Int32 id)
        {
            int idd = Convert.ToInt32(id);
            var aa = db.tblEventDetails.SingleOrDefault(s => s.EventId == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            //var employer = new tblEventDetail { EventId = id };
            //db.Entry(employer).State = EntityState.Deleted;
            db.SaveChanges();
            // var employer = new tblEventType { EventId = id };
            //db.Entry(employer).State = EntityState.Deleted;
            //db.SaveChanges();

            return "Record successfully deleted"; ;

        }



        [System.Web.Http.Route("api/EventsAPI/saveTaskDetails")]
        [System.Web.Http.HttpPost]
        public string saveTaskDetails(TaskDetails events)
        {
            if (string.IsNullOrEmpty(events.Id))
            {
                if (events.UserType == "1")
                {
                    string[] evensts = events.Student;
                    for (int i = 0; i < evensts.Length; i++)
                    {
                        tblTaskDetail tsk = new tblTaskDetail();
                        tsk.TaskName = events.TaskName;
                        tsk.Description = events.Description;
                        tsk.TaskPriority = events.Priority;
                        tsk.TaskDate = Convert.ToDateTime(events.TaskDate);
                        tsk.UserType = Convert.ToInt32(events.UserType);
                        tsk.classId = Convert.ToInt64(events.Class);
                        tsk.SectionId = Convert.ToInt64(events.Section);
                        tsk.Student = Convert.ToInt64(evensts[i]);
                        tsk.Status = events.Status;
                        tsk.SchoolID = events.SchoolID;
                        db.tblTaskDetails.Add(tsk);
                        db.SaveChanges();
                    }


                }
                else
                {
                    string[] evensts = events.Employee;
                    for (int i = 0; i < evensts.Length; i++)
                    {
                        tblTaskDetail tsk = new tblTaskDetail();
                        tsk.TaskName = events.TaskName;
                        tsk.Description = events.Description;
                        tsk.TaskPriority = events.Priority;
                        tsk.TaskDate = Convert.ToDateTime(events.TaskDate);
                        tsk.UserType = Convert.ToInt32(events.UserType);
                        tsk.Department = Convert.ToInt64(events.Department);
                        tsk.Employee = Convert.ToInt64(evensts[i]);
                        tsk.Status = events.Status;
                        tsk.SchoolID = events.SchoolID;
                        db.tblTaskDetails.Add(tsk);
                        db.SaveChanges();
                    }
                }
                return "Task Assigned Successfully";
            }
            else
            {

                if (events.UserType == "1")
                {
                    string[] evensts = events.Student;
                    for (int i = 0; i < evensts.Length; i++)
                    {
                        int id = Convert.ToInt16(events.Id);
                        //  int SchoolID = Convert.ToInt32(events.SchoolID);
                        var tsk = db.tblTaskDetails.SingleOrDefault(s => s.Id == id);

                        //  tblTaskDetail tsk = new tblTaskDetail();
                        tsk.TaskName = events.TaskName;
                        tsk.Description = events.Description;
                        tsk.TaskPriority = events.Priority;
                        tsk.TaskDate = Convert.ToDateTime(events.TaskDate);
                        tsk.UserType = Convert.ToInt32(events.UserType);
                        tsk.classId = Convert.ToInt64(events.Class);
                        tsk.SectionId = Convert.ToInt64(events.Section);
                        tsk.Student = Convert.ToInt64(evensts[i]);
                        tsk.Status = events.Status;
                        tsk.SchoolID = events.SchoolID;
                        // db.tblTaskDetails.Add(tsk);
                        db.SaveChanges();
                    }


                }
                else
                {
                    string[] evensts = events.Employee;
                    for (int i = 0; i < evensts.Length; i++)
                    {
                        int id = Convert.ToInt16(events.Id);
                        var tsk = db.tblTaskDetails.SingleOrDefault(s => s.Id == id);
                        // tblTaskDetail tsk = new tblTaskDetail();
                        tsk.TaskName = events.TaskName;
                        tsk.Description = events.Description;
                        tsk.TaskPriority = events.Priority;
                        tsk.TaskDate = Convert.ToDateTime(events.TaskDate);
                        tsk.UserType = Convert.ToInt32(events.UserType);
                        tsk.Department = Convert.ToInt64(events.Department);
                        tsk.Employee = Convert.ToInt64(evensts[i]);
                        tsk.Status = events.Status;
                        tsk.SchoolID = events.SchoolID;
                        //  db.tblTaskDetails.Add(tsk);
                        db.SaveChanges();
                    }
                }



                return "Task Assigned updated Successfully";
            }


        }

        [System.Web.Http.Route("api/EventsAPI/getAllTaskDetailsForStudent")]
        [System.Web.Http.HttpPost]
        public List<TaskDetails> getAllTaskDetailsForStudent(List<string> aa)
        {
            List<TaskDetails> list = new List<TaskDetails>();
            sqlHelper obj = new sqlHelper();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);

            try
            {

                if (typeuser == 2)
                {
                    DataTable dt = obj.getDataTable(@"select ROW_NUMBER() over(order by TaskDate desc) sno,a.School, ss.FirstName+' '+ss.MiddleName+' '+ss.LastName FullName,cc.CourseName,s.SectionName,CONVERT(varchar(100),td.taskDate, 107) TaskDatets,td.* from tblTaskDetails td
                                        left outer join TBLStudent ss on ss.ID = td.Student
                                         left outer join tblCourses cc on cc.Id = td.classId
                                         left outer join tblSections s on s.Id = td.SectionId and td.classId = cc.Id
										 left join tblSchoolDetails a on a.ID=td.SchoolID
                                         where UserType = 1 and td.IsDeleted is null  order by TaskDate desc");

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
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
                        items.School = dr["School"].ToString();
                        list.Add(items);
                    }

                }
                else
                {
                    DataTable dt = obj.getDataTable(@"select ROW_NUMBER() over(order by TaskDate desc) sno,a.School, ss.FirstName+' '+ss.MiddleName+' '+ss.LastName FullName,cc.CourseName,s.SectionName,CONVERT(varchar(100),td.taskDate, 107) TaskDatets,td.* from tblTaskDetails td
                                        left outer join TBLStudent ss on ss.ID = td.Student
                                         left outer join tblCourses cc on cc.Id = td.classId
                                         left outer join tblSections s on s.Id = td.SectionId and td.classId = cc.Id
										 left join tblSchoolDetails a on a.ID=td.SchoolID
										 left join tblEmployee em on td.SchoolID=em.SchoolID
                                         where em.UserID='" + loginuser + "' and em.IsDeleted is null and  td.UserType = 1 and td.IsDeleted is null  order by TaskDate desc");

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
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
                        items.School = dr["School"].ToString();
                        list.Add(items);
                    }
                }

            }
            catch (Exception e)
            { throw e; }
            return list;
        }



        [System.Web.Http.Route("api/EventsAPI/getAllEventsTypesEmployess")]
        [System.Web.Http.HttpPost]
        public List<TaskDetails> getAllEventsTypesEmployess(List<string> aa)
        {
            List<TaskDetails> list = new List<TaskDetails>();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            sqlHelper obj = new sqlHelper();
            try
            {

                if (typeuser == 2)
                {
                    DataTable dt = obj.getDataTable(@"select ROW_NUMBER() over(order by TaskDate desc) sno,a.School, ee.FirstName+' '+ee.MiddleName+' '+ee.LastName FullName,dd.DepartmentName,CONVERT(varchar(100),td.taskDate, 107) TaskDatets,td.* from tblTaskDetails td
                                                left outer join tblemployee ee on ee.Id = td.Employee
                                                left outer join tblDepartmnet dd on dd.DepartmentId = td.Department
												left join tblSchoolDetails a on a.ID=td.SchoolID
                                                where td.UserType = 2 and td.IsDeleted is null order by TaskDate desc");

                    foreach (DataRow dr in dt.Rows)
                    {
                        TaskDetails items = new TaskDetails();
                        items.sno = dr["sno"].ToString();
                        items.TaskName = dr["TaskName"].ToString();
                        items.Department = dr["DepartmentName"].ToString();

                        //  items.Class = dr["CourseName"].ToString();
                        items.EmployeeName = dr["FullName"].ToString();

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
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
                        items.School = dr["School"].ToString();
                        list.Add(items);
                    }
                }
                else
                {
                    DataTable dt = obj.getDataTable(@"select ROW_NUMBER() over(order by TaskDate desc) sno,a.School, ee.FirstName+' '+ee.MiddleName+' '+ee.LastName FullName,dd.DepartmentName,CONVERT(varchar(100),td.taskDate, 107) TaskDatets,td.* from tblTaskDetails td
                                                left outer join tblemployee ee on ee.Id = td.Employee
                                                left outer join tblDepartmnet dd on dd.DepartmentId = td.Department
												left join tblSchoolDetails a on a.ID=td.SchoolID
												left join tblEmployee em on td.SchoolID=em.SchoolID
                                                where em.UserID='" + loginuser + "' and em.IsDeleted is null and  td.UserType = 2 and td.IsDeleted is null order by TaskDate desc");

                    foreach (DataRow dr in dt.Rows)
                    {
                        TaskDetails items = new TaskDetails();
                        items.sno = dr["sno"].ToString();
                        items.TaskName = dr["TaskName"].ToString();
                        items.Department = dr["DepartmentName"].ToString();

                        //  items.Class = dr["CourseName"].ToString();
                        items.EmployeeName = dr["FullName"].ToString();

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
                        items.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
                        items.School = dr["School"].ToString();
                        list.Add(items);
                    }
                }


            }
            catch (Exception e)
            { throw e; }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/deleteStudentTaskById")]
        [System.Web.Http.HttpPost]
        public string deleteStudentTaskById(Int32 id)
        {

            //var task = new tblTaskDetail { Id = id };
            //db.Entry(task).State = EntityState.Deleted;
            int idd = Convert.ToInt32(id);
            var aa = db.tblTaskDetails.SingleOrDefault(s => s.Id == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            db.SaveChanges();
            return "Student Task Deleted Successfully";
        }


        [System.Web.Http.Route("api/EventsAPI/deleteEmployeeTaskbyId")]
        [System.Web.Http.HttpPost]
        public string deleteEmployeeTaskbyId(Int32 id)
        {
            int idd = Convert.ToInt32(id);
            var aa = db.tblTaskDetails.SingleOrDefault(s => s.Id == idd);
            aa.IsDeleted = 1;
            aa.Deleted_on = DateTime.Now;
            //var empId = new tblTaskDetail { Id = id };
            //db.Entry(empId).State = EntityState.Deleted;
            db.SaveChanges();
            return "Employee Task Deleted Successfully";

        }



        [System.Web.Http.Route("api/EventsAPI/editTaskDetailsById")]
        [System.Web.Http.HttpPost]
        public TaskDetails editTaskDetailsById(List<string> id)
        {

            sqlHelper obj = new sqlHelper();
            DataRow dr = obj.getDataRow("select a.School,b.Id,b.TaskName,b.TaskPriority,b.Description,b.TaskDate,b.UserType,b.Department,b.Employee, b.classId, b.SectionId, b.Student, b.Status, b.dateCreated, b.SchoolID, a.School from tblTaskDetails b left outer join tblSchoolDetails a on a.ID = b.SchoolID where b.Id = '" + id[0] + "' ");
            TaskDetails usr = new TaskDetails();
            usr.Id = dr["Id"].ToString();
            usr.TaskName = dr["TaskName"].ToString();
            usr.Description = dr["Description"].ToString();
            usr.Priority = dr["TaskPriority"].ToString();
            usr.TaskDate = dr["TaskDate"].ToString();
            usr.UserType = dr["UserType"].ToString();
            usr.Class = dr["classId"].ToString();
            usr.Section = dr["SectionId"].ToString();
            usr.StudentName = dr["Student"].ToString();
            usr.Status = dr["Status"].ToString();
            usr.Department = dr["Department"].ToString();
            usr.EmployeeName = dr["Employee"].ToString();
            usr.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
            usr.School = dr["School"].ToString();



            return usr;

        }

        [System.Web.Http.Route("api/EventsAPI/getAllNoticeBoardsbySchoolappp")]
        [System.Web.Http.HttpPost]
        public NoticeboardDetailsforapp getAllNoticeBoardsbySchoolappp(NoticeboardDetails1 aa)
        {



            NoticeboardDetailsforapp obj = new NoticeboardDetailsforapp();
            List<NoticeboardDetails1> list = new List<NoticeboardDetails1>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();


            //sqlHelper obj = new sqlHelper();
            try
            {

                if (aa.SchoolID.Equals(null))
                {
                    obj.message = "Please Enter SchoolID";
                    obj.status = true;
                    obj.data = list;
                }
                else
                {
                    int SchoolID = Convert.ToInt32(aa.SchoolID);
                    int Count = 0;


                    var result = (from a in db.tblNoticeBoards
                                  join s in db.tblSchoolDetails on a.SchoolID equals s.ID

                                  where a.IsDeleted == null && s.ID == SchoolID

                                  select new
                                  {
                                      model = a,
                                      SchoolName = s.School
                                  }

                                        ).ToList();
                    foreach (var a in result)
                    {
                        Count++;
                        NoticeboardDetails1 items = new NoticeboardDetails1();
                        items.Title = a.model.Title;
                        items.Desc = a.model.description;
                        items.NoticeDate = Convert.ToDateTime(a.model.NoticeDate).ToString("dd MMMM yyyy");
                        items.DateCreated = Convert.ToDateTime(a.model.dateCreated).ToString("dd MMMM yyyy");
                        items.DateTimeCreated = Convert.ToDateTime(a.model.dateCreated).ToString("yyyy-MM-dd T HH:mm:ss");
                        //items.NoticeDate =Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("MM/dd/yy"));
                        //items.sNoticeDate = items.NoticeDate.ToString("MM/dd/yy");
                        items.NoticeFile = a.model.NoticeFile;
                        //if (items.NoticeFile != "")
                        //{
                        //    items.blanValue = "";
                        //}
                        //else
                        //{
                        //    items.blanValue = "none";
                        //}
                        items.userType = a.model.userType;
                        if (items.userType == "S")
                        {
                            items.userType = "Student";
                        }
                        else if (items.userType == "E")
                        {
                            items.userType = "Employee";
                        }
                        else
                        {
                            items.userType = "General";
                        }
                        if (a.model.status == true)
                        {
                            items.Status = "Active";
                            items.color = "#00a65a";
                        }
                        else
                        {
                            items.Status = "DeActive";
                            items.color = "#dd4b39";
                        }
                       


                        items.Id = a.model.Id.ToString();
                       
                        items.SchoolID = Convert.ToInt32(a.model.SchoolID);
                        list.Add(items);

                    }
                    if (Count != 0)
                    {
                        obj.message = "Sucess";
                        obj.status = true;
                        obj.data = list;
                    }
                    else
                    {
                        obj.message = "No Data Found";
                        obj.status = false;
                        obj.data = list;
                    }
                }


                //var result = db.tblNoticeBoards.ToList();
            }
            catch
            {
                obj.message = "Something Went Wrong";
                obj.status = true;
                obj.data = list;
            }

            return obj;
        }




        //[System.Web.Http.Route("api/EventsAPI/getAllNoticeBoardsbySchoolapp")]
        //[System.Web.Http.HttpPost]
        //public NoticeboardDetailsforapp getAllNoticeBoardsbySchoolapp(List<string> aa)
        //{

        //    string loginuser = aa[0];
        //    int typeuser = Convert.ToInt32(aa[1]);
        //    NoticeboardDetailsforapp obj = new NoticeboardDetailsforapp();
        //    List<NoticeboardDetails> list = new List<NoticeboardDetails>();
        //    SCHOOLERPEntities db = new SCHOOLERPEntities();


        //    //sqlHelper obj = new sqlHelper();
        //    try
        //    {

        //        if (typeuser == 2)
        //        {
        //            var result = (from a in db.tblNoticeBoards
        //                          join s in db.tblSchoolDetails on a.SchoolID equals s.ID
        //                          where a.IsDeleted == null
        //                          select new
        //                          {
        //                              model = a,
        //                              SchoolName = s.School
        //                          }

        //        ).ToList();
        //            foreach (var a in result)
        //            {
        //                NoticeboardDetails items = new NoticeboardDetails();
        //                items.Title = a.model.Title;
        //                items.Desc = a.model.description;
        //                items.NoticeDate = Convert.ToDateTime(a.model.NoticeDate);
        //                items.sNoticeDate = items.NoticeDate.ToString("dd/MM/yyyy");

        //                //items.NoticeDate =Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("MM/dd/yy"));
        //                //items.sNoticeDate = items.NoticeDate.ToString("MM/dd/yy");
        //                items.NoticeFile = a.model.NoticeFile;
        //                if (items.NoticeFile != "")
        //                {
        //                    items.blanValue = "";
        //                }
        //                else
        //                {
        //                    items.blanValue = "none";
        //                }
        //                items.userType = a.model.userType;
        //                if (items.userType == "S")
        //                {
        //                    items.userType = "Student";
        //                }
        //                else if (items.userType == "E")
        //                {
        //                    items.userType = "Employee";
        //                }
        //                else
        //                {
        //                    items.userType = "General";
        //                }
        //                if (a.model.status == true)
        //                {
        //                    items.Status = "Active";
        //                    items.color = "#00a65a";
        //                }
        //                else
        //                {
        //                    items.Status = "DeActive";
        //                    items.color = "#dd4b39";
        //                }
        //                if (string.IsNullOrEmpty(items.NoticeFile))
        //                {
        //                    items.blanValue = "hideCol";
        //                }


        //                items.Id = a.model.Id.ToString();
        //                items.School = a.SchoolName;
        //                items.SchoolID = Convert.ToInt32(a.model.SchoolID);
        //                list.Add(items);
        //            }
        //            obj.message = "Sucess";
        //            obj.status = true;
        //            obj.data = list;


        //        }
        //        else
        //        {
        //            var result = (from a in db.tblNoticeBoards
        //                          join s in db.tblSchoolDetails on a.SchoolID equals s.ID
        //                          join em in db.tblEmployees on a.SchoolID equals em.SchoolID
        //                          where a.IsDeleted == null && em.UserID == loginuser

        //                          select new
        //                          {
        //                              model = a,
        //                              SchoolName = s.School
        //                          }

        //                            ).ToList();
        //            foreach (var a in result)
        //            {
        //                NoticeboardDetails items = new NoticeboardDetails();
        //                items.Title = a.model.Title;
        //                items.Desc = a.model.description;
        //                items.NoticeDate = Convert.ToDateTime(a.model.NoticeDate);
        //                items.sNoticeDate = items.NoticeDate.ToString("dd/MM/yyyy");

        //                //items.NoticeDate =Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("MM/dd/yy"));
        //                //items.sNoticeDate = items.NoticeDate.ToString("MM/dd/yy");
        //                items.NoticeFile = a.model.NoticeFile;
        //                if (items.NoticeFile != "")
        //                {
        //                    items.blanValue = "";
        //                }
        //                else
        //                {
        //                    items.blanValue = "none";
        //                }
        //                items.userType = a.model.userType;
        //                if (items.userType == "S")
        //                {
        //                    items.userType = "Student";
        //                }
        //                else if (items.userType == "E")
        //                {
        //                    items.userType = "Employee";
        //                }
        //                else
        //                {
        //                    items.userType = "General";
        //                }
        //                if (a.model.status == true)
        //                {
        //                    items.Status = "Active";
        //                    items.color = "#00a65a";
        //                }
        //                else
        //                {
        //                    items.Status = "DeActive";
        //                    items.color = "#dd4b39";
        //                }
        //                if (string.IsNullOrEmpty(items.NoticeFile))
        //                {
        //                    items.blanValue = "hideCol";
        //                }


        //                items.Id = a.model.Id.ToString();
        //                items.School = a.SchoolName;
        //                items.SchoolID = Convert.ToInt32(a.model.SchoolID);
        //                list.Add(items);

        //            }
        //            obj.message = "Sucess";
        //            obj.status = true;
        //            obj.data = list;

        //        }
        //        //var result = db.tblNoticeBoards.ToList();
        //    }
        //    catch
        //    {
        //        obj.message = "Something Went Wrong";
        //        obj.status = false;
        //        obj.data = list;
        //    }

        //    return obj;
        //}



        [System.Web.Http.Route("api/EventsAPI/getAllNoticeBoardsbySchool")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getAllNoticeBoardsbySchool(List<string> aa)
        {

            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();


            //sqlHelper obj = new sqlHelper();
            try
            {

                if (typeuser == 2)
                {
                    var result = (from a in db.tblNoticeBoards
                                  join s in db.tblSchoolDetails on a.SchoolID equals s.ID
                                  where a.IsDeleted == null
                                  select new
                                  {
                                      model = a,
                                      SchoolName = s.School
                                  }

                ).ToList();
                    foreach (var a in result)
                    {
                        NoticeboardDetails items = new NoticeboardDetails();
                        items.Title = a.model.Title;
                        items.Desc = a.model.description;
                        items.NoticeDate = Convert.ToDateTime(a.model.NoticeDate);
                        items.sNoticeDate = items.NoticeDate.ToString("dd/MM/yyyy");

                        //items.NoticeDate =Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("MM/dd/yy"));
                        //items.sNoticeDate = items.NoticeDate.ToString("MM/dd/yy");
                        items.NoticeFile = a.model.NoticeFile;
                        if (items.NoticeFile != "")
                        {
                            items.blanValue = "";
                        }
                        else
                        {
                            items.blanValue = "none";
                        }
                        items.userType = a.model.userType;
                        if (items.userType == "S")
                        {
                            items.userType = "Student";
                        }
                        else if (items.userType == "E")
                        {
                            items.userType = "Employee";
                        }
                        else
                        {
                            items.userType = "General";
                        }
                        if (a.model.status == true)
                        {
                            items.Status = "Active";
                            items.color = "#00a65a";
                        }
                        else
                        {
                            items.Status = "DeActive";
                            items.color = "#dd4b39";
                        }
                        if (string.IsNullOrEmpty(items.NoticeFile))
                        {
                            items.blanValue = "hideCol";
                        }


                        items.Id = a.model.Id.ToString();
                        items.School = a.SchoolName;
                        items.SchoolID = Convert.ToInt32(a.model.SchoolID);
                        list.Add(items);
                    }


                }
                else
                {
                    var result = (from a in db.tblNoticeBoards
                                  join s in db.tblSchoolDetails on a.SchoolID equals s.ID
                                  join em in db.tblEmployees on a.SchoolID equals em.SchoolID
                                  where a.IsDeleted == null && em.UserID == loginuser && em.IsDeleted ==null

                                  select new
                                  {
                                      model = a,
                                      SchoolName = s.School
                                  }

                                    ).ToList();
                    foreach (var a in result)
                    {
                        NoticeboardDetails items = new NoticeboardDetails();
                        items.Title = a.model.Title;
                        items.Desc = a.model.description;
                        items.NoticeDate = Convert.ToDateTime(a.model.NoticeDate);
                        items.sNoticeDate = items.NoticeDate.ToString("dd/MM/yyyy");

                        //items.NoticeDate =Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("MM/dd/yy"));
                        //items.sNoticeDate = items.NoticeDate.ToString("MM/dd/yy");
                        items.NoticeFile = a.model.NoticeFile;
                        if (items.NoticeFile != "")
                        {
                            items.blanValue = "";
                        }
                        else
                        {
                            items.blanValue = "none";
                        }
                        items.userType = a.model.userType;
                        if (items.userType == "S")
                        {
                            items.userType = "Student";
                        }
                        else if (items.userType == "E")
                        {
                            items.userType = "Employee";
                        }
                        else
                        {
                            items.userType = "General";
                        }
                        if (a.model.status == true)
                        {
                            items.Status = "Active";
                            items.color = "#00a65a";
                        }
                        else
                        {
                            items.Status = "DeActive";
                            items.color = "#dd4b39";
                        }
                        if (string.IsNullOrEmpty(items.NoticeFile))
                        {
                            items.blanValue = "hideCol";
                        }


                        items.Id = a.model.Id.ToString();
                        items.School = a.SchoolName;
                        items.SchoolID = Convert.ToInt32(a.model.SchoolID);
                        list.Add(items);
                    }

                }
                //var result = db.tblNoticeBoards.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }



        [System.Web.Http.Route("api/EventsAPI/getAllNoticeBoards")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getAllNoticeBoards()
        {
            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            //sqlHelper obj = new sqlHelper();
            try
            {
                //var result = db.tblNoticeBoards.ToList();
                var result = (from a in db.tblNoticeBoards
                              join s in db.tblSchoolDetails on a.SchoolID equals s.ID
                              where a.IsDeleted == null
                              select new
                              {
                                  model = a,
                                  SchoolName = s.School
                              }

                 ).ToList();
                foreach (var a in result)
                {
                    NoticeboardDetails items = new NoticeboardDetails();
                    items.Title = a.model.Title;
                    items.Desc = a.model.description;
                    items.NoticeDate = Convert.ToDateTime(a.model.NoticeDate);
                    items.sNoticeDate = items.NoticeDate.ToString("dd/MM/yyyy");

                    //items.NoticeDate =Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("MM/dd/yy"));
                    //items.sNoticeDate = items.NoticeDate.ToString("MM/dd/yy");
                    items.NoticeFile = a.model.NoticeFile;
                    if (items.NoticeFile != "")
                    {
                        items.blanValue = "";
                    }
                    else
                    {
                        items.blanValue = "none";
                    }
                    items.userType = a.model.userType;
                    if (items.userType == "S")
                    {
                        items.userType = "Student";
                    }
                    else if (items.userType == "E")
                    {
                        items.userType = "Employee";
                    }
                    else
                    {
                        items.userType = "General";
                    }
                    if (a.model.status == true)
                    {
                        items.Status = "Active";
                        items.color = "#00a65a";
                    }
                    else
                    {
                        items.Status = "DeActive";
                        items.color = "#dd4b39";
                    }
                    if (string.IsNullOrEmpty(items.NoticeFile))
                    {
                        items.blanValue = "hideCol";
                    }


                    items.Id = a.model.Id.ToString();
                    items.School = a.SchoolName;
                    items.SchoolID = Convert.ToInt32(a.model.SchoolID);
                    list.Add(items);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/editNoticeBoardDetailsById")]
        [System.Web.Http.HttpPost]
        public NoticeboardDetails editNoticeBoardDetailsById(List<string> id)
        {
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            int idd = Convert.ToInt32(id[0]);
            //var result = (from a in db.tblNoticeBoards
            //              join s in db.tblSchoolDetails on a.SchoolID equals s.ID
            //              select new
            //              {
            //                  model = a,
            //                  SchoolName = s.School
            //              }

            //    ).ToList();

            var result = db.tblNoticeBoards.Where(s => s.Id == idd).SingleOrDefault();

            NoticeboardDetails items = new NoticeboardDetails();
            items.Title = result.Title;
            items.Desc = result.description;
            items.NoticeDate = Convert.ToDateTime(Convert.ToDateTime(result.NoticeDate));
            items.sNoticeDate = Convert.ToDateTime(result.NoticeDate).ToString("MM/dd/yyyy");
            //items.NoticeDate = Convert.ToDateTime(Convert.ToDateTime(result.NoticeDate).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            //items.sNoticeDate = Convert.ToDateTime(result.NoticeDate).ToString("MM/dd/yyyy");
            items.NoticeFile = result.NoticeFile;
            items.userType = result.userType;
            items.Status = result.status.ToString();
            items.Id = result.Id.ToString();
            items.SchoolID = result.SchoolID;

            return items;
        }




        [System.Web.Http.Route("api/EventsAPI/deleteNoticeBoardDetails")]
        [System.Web.Http.HttpPost]
        public string deleteNoticeBoardDetails(List<int> id)
        {

            //var task = new tblNoticeBoard { Id = id[0] };
            //db.Entry(task).State = EntityState.Deleted;
            int idd = Convert.ToInt32(id[0]);
            var result = db.tblNoticeBoards.SingleOrDefault(a => a.Id == idd);
            result.IsDeleted = 1;
            result.Deleted_on = DateTime.Now;
            db.SaveChanges();
            return "Noticed Deleted Successfully";
        }



        //        [System.Web.Http.Route("api/EventsAPI/getAllNoticeBoardDetails")]
        //        [System.Web.Http.HttpPost]
        //        public List<NoticeboardDetails> getAllNoticeBoardDetails(NoticeboardDetails not)
        //        {
        //SCHOOLERPEntities db = new SCHOOLERPEntities();
        //                List<NoticeboardDetails> list = new List<NoticeboardDetails>();
        //           try
        //            {

        //                DateTime sDate = Convert.ToDateTime(not.StartDate);
        //            DateTime eDate = Convert.ToDateTime(not.EndDate);

        //               .
        //.

        //               var result = db.tblNoticeBoards.Where(x=>x.NoticeDate >= sDate && x.NoticeDate<= eDate).ToList();

        //                foreach (var a in result)
        //                {
        //                    NoticeboardDetails items = new NoticeboardDetails();
        //                    items.Title = a.Title;
        //                    items.Desc = a.description;
        //                    items.NoticeDate = Convert.ToDateTime(a.NoticeDate).ToString("dddd ,MMMM,dd,yyyy");
        //                    items.NoticeFile = a.NoticeFile;
        //                    items.userType = a.userType;
        //                    items.Extra = a.userType;
        //                    if (items.userType == "S")
        //                    {
        //                        items.userType = "Student";
        //                    }
        //                    else if (items.userType == "E")
        //                    {
        //                        items.userType = "Employee";
        //                    }
        //                    else
        //                    {
        //                        items.userType = "General";
        //                    }


        //                    items.Id = a.Id.ToString();
        //                    list.Add(items);
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                throw e;
        //            }
        //            return list;
        //        }






        [System.Web.Http.Route("api/EventsAPI/getAllNoticeBoardDetails")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getAllNoticeBoardDetails(NoticeboardDetails notice)
        {
            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            sqlHelper obj = new sqlHelper();

            DateTime sDate = notice.StDt;
            DateTime eDate = notice.eDt;
            int? schid = Convert.ToInt32(obj.ExecuteScaler("select schoolid from tblemployee where isdeleted is null and userid='" + notice.LoginUser + "'"));
            if (schid == null)
            {
                schid = 0;
            }
            var result = db.tblNoticeBoards.Where(x => x.NoticeDate >= sDate && x.NoticeDate <= eDate && x.IsDeleted == null && x.SchoolID == schid).ToList();

            foreach (var a in result)
            {
                NoticeboardDetails items = new NoticeboardDetails();
                items.Title = a.Title;
                items.Desc = a.description;
                items.NoticeDate = Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("dddd, dd, MMMM, yyyy", CultureInfo.InvariantCulture));
                items.datetime = items.NoticeDate.ToString().Split(' ')[0];
                items.NoticeFile = a.NoticeFile;
                items.userType = a.userType;
                items.Extra = a.userType;
                if (items.userType == "S")
                {
                    items.userType = "Student";
                }
                else if (items.userType == "E")
                {
                    items.userType = "Employee";
                }
                else
                {
                    items.userType = "General";
                }
                if (string.IsNullOrEmpty(items.NoticeFile))
                {
                    items.blanValue = "hideCol";
                }

                items.Id = a.Id.ToString();
                list.Add(items);

            }


            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/getEmployeeNoticeBoardDetails")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getEmployeeNoticeBoardDetails(NoticeboardDetails notice)
        {
            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            sqlHelper obj = new sqlHelper();

            DateTime sDate = notice.StDt;
            DateTime eDate = notice.eDt;
            

            var result = db.tblNoticeBoards.Where(x => x.NoticeDate >= sDate && x.NoticeDate <= eDate && (x.userType == "E" || x.userType == "G") && x.IsDeleted==null && x.SchoolID==notice.SchoolID).ToList();

            foreach (var a in result)
            {
                NoticeboardDetails items = new NoticeboardDetails();
                items.Title = a.Title;
                items.Desc = a.description;
                items.NoticeDate = Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("dddd ,MMMM,dd,yyyy", CultureInfo.InvariantCulture));
                items.NoticeFile = a.NoticeFile;
                items.userType = a.userType;
                items.Extra = a.userType;
                if (items.userType == "S")
                {
                    items.userType = "Student";
                }
                else if (items.userType == "E")
                {
                    items.userType = "Employee";
                }
                else
                {
                    items.userType = "General";
                }


                items.Id = a.Id.ToString();
                list.Add(items);

            }


            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/getAllNoticeBoardsForEmployeeBySchool")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getAllNoticeBoardsForEmployeeBySchool(List<string> aa)
        {

            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            string loginuser = aa[0];
            int typeuser = Convert.ToInt32(aa[1]);
            //sqlHelper obj = new sqlHelper();
            try
            {
                if (typeuser == 2)
                {
                    var result = db.tblNoticeBoards.Where(x =>x.IsDeleted==null &&( x.userType == "E" || x.userType == "G") ).ToList();

                    foreach (var a in result)
                    {
                        NoticeboardDetails items = new NoticeboardDetails();
                        items.Title = a.Title;
                        items.Desc = a.description;
                        items.NoticeDate = Convert.ToDateTime(a.NoticeDate.ToString());
                        items.noticeda = ((DateTime)a.NoticeDate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        items.NoticeFile = a.NoticeFile;
                        items.userType = a.userType;
                        if (items.userType == "S")
                        {
                            items.userType = "Student";
                        }
                        else if (items.userType == "E")
                        {
                            items.userType = "Employee";
                        }
                        else
                        {
                            items.userType = "General";
                        }
                        if (a.status == true)
                        {
                            items.Status = "Active";
                        }
                        else
                        {
                            items.Status = "DeActive";
                        }
                        if (string.IsNullOrEmpty(items.NoticeFile))
                        {
                            items.blanValue = "hideCol";
                        }

                        items.Id = a.Id.ToString();
                        list.Add(items);
                    }
                }
                else
                {
                    var School = db.tblEmployees.Where(a => a.UserID == loginuser && a.IsDeleted==null).FirstOrDefault();
                    int SchoolID = Convert.ToInt32(School.SchoolID);

                    var result = db.tblNoticeBoards.Where(x =>x.SchoolID== SchoolID && ( x.userType == "E" || x.userType == "G") && x.IsDeleted ==null).ToList();

                    foreach (var a in result)
                    {
                        NoticeboardDetails items = new NoticeboardDetails();
                        items.Title = a.Title;
                        items.Desc = a.description;
                        items.NoticeDate = Convert.ToDateTime(a.NoticeDate.ToString());
                        items.noticeda = ((DateTime)a.NoticeDate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        items.NoticeFile = a.NoticeFile;
                        items.userType = a.userType;
                        if (items.userType == "S")
                        {
                            items.userType = "Student";
                        }
                        else if (items.userType == "E")
                        {
                            items.userType = "Employee";
                        }
                        else
                        {
                            items.userType = "General";
                        }
                        if (a.status == true)
                        {
                            items.Status = "Active";
                            items.color = "#00a65a";
                        }
                        else
                        {
                            items.Status = "DeActive";
                            items.color = "#dd4b39";
                        }
                        if (string.IsNullOrEmpty(items.NoticeFile))
                        {
                            items.blanValue = "hideCol";
                        }

                        items.Id = a.Id.ToString();
                        list.Add(items);
                    }

                }

            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        [System.Web.Http.Route("api/EventsAPI/getNoticeBoardsForStudentBySchool1")]
        [System.Web.Http.HttpPost]
        public NoticeboardDetails getNoticeBoardsForStudentBySchool1(List<string> aa)
        {
            NoticeboardDetails obj = new NoticeboardDetails();
            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            //sqlHelper obj = new sqlHelper();
            int SchoolID = Convert.ToInt32(aa[0]);


            try
            {


                var result = db.tblNoticeBoards.Where(x => x.SchoolID == SchoolID && x.IsDeleted == null && (x.userType == "S" || x.userType == "G")).ToList();

                foreach (var a in result)
                {
                    NoticeboardDetails items = new NoticeboardDetails();
                    items.Title = a.Title;
                    items.Desc = a.description;
                    items.NoticeDate = Convert.ToDateTime( a.NoticeDate.ToString());
                    items.sNoticeDate= items.NoticeDate.ToString("MM/dd/yyyy");
                    items.NoticeFile = a.NoticeFile;
                    items.userType = a.userType;
                    if (items.userType == "S")
                    {
                        items.userType = "Student";
                    }
                    else if (items.userType == "E")
                    {
                        items.userType = "Employee";
                    }
                    else
                    {
                        items.userType = "General";
                    }
                    if (a.status == true)
                    {
                        items.Status = "Active";
                    }
                    else
                    {
                        items.Status = "DeActive";
                    }


                    items.Id = a.Id.ToString();
                    list.Add(items);
                }


            }

            catch (Exception e)
            { throw e; }
            obj.status = "200";
            obj.message = "success";
            obj.data = list;
            return obj;
        }


        [System.Web.Http.Route("api/EventsAPI/getAllNoticeBoardsForEmployee")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getAllNoticeBoardsForEmployee()
        {

            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            //sqlHelper obj = new sqlHelper();
            try
            {
                var result = db.tblNoticeBoards.Where(x => x.userType == "E" || x.userType == "G" && x.IsDeleted == null).ToList();

                foreach (var a in result)
                {
                    NoticeboardDetails items = new NoticeboardDetails();
                    items.Title = a.Title;
                    items.Desc = a.description;
                    items.NoticeDate = Convert.ToDateTime(a.NoticeDate.ToString());
                    items.NoticeFile = a.NoticeFile;
                    items.userType = a.userType;
                    if (items.userType == "S")
                    {
                        items.userType = "Student";
                    }
                    else if (items.userType == "E")
                    {
                        items.userType = "Employee";
                    }
                    else
                    {
                        items.userType = "General";
                    }
                    if (a.status == true)
                    {
                        items.Status = "Active";
                    }
                    else
                    {
                        items.Status = "DeActive";
                    }
                    if (string.IsNullOrEmpty(items.NoticeFile))
                    {
                        items.blanValue = "hideCol";
                    }

                    items.Id = a.Id.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }


        [System.Web.Http.Route("api/EventsAPI/getNoticeBoardsForStudentBySchoolAPP")]
        [System.Web.Http.HttpPost]
        public NoticeboardDetailsforapp getNoticeBoardsForStudentBySchoolAPP(NoticeboardDetails1 aa)
        {
            NoticeboardDetailsforapp obj = new NoticeboardDetailsforapp();
            List<NoticeboardDetails1> list = new List<NoticeboardDetails1>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            //sqlHelper obj = new sqlHelper();



            try
            {
                if (aa.SchoolID.Equals(null) || "".Equals(aa.SchoolID) || aa.SchoolID == 0)
                {
                    obj.status = false;
                    obj.message = "please Enter SchoolID";
                    obj.data = list;
                }
                else
                {
                    int Count = 0;
                    int SchoolID = Convert.ToInt32(aa.SchoolID);
                    var result = db.tblNoticeBoards.Where(x => x.SchoolID == SchoolID && x.IsDeleted == null && x.IsDeleted == null && (x.userType == "S" || x.userType == "G")).ToList();

                    foreach (var a in result)
                    {
                        Count++;
                        NoticeboardDetails1 items = new NoticeboardDetails1();
                        items.Title = a.Title;
                        items.Desc = a.description;
                        items.NoticeDate = Convert.ToDateTime(a.NoticeDate).ToString("dd MMMM yyyy");
                        items.NoticeFile = a.NoticeFile;
                        items.userType = a.userType; 
                        items.DateTimeCreated = Convert.ToDateTime( a.dateCreated).ToString("yyyy-MM-dd T HH:mm:ss ");
                        items.DateCreated = Convert.ToDateTime(a.dateCreated).ToString("dd MMMM yyyy");
                        items.SchoolID = a.SchoolID;
                        if (items.userType == "S")
                        {
                            items.userType = "Student";
                        }
                        else if (items.userType == "E")
                        {
                            items.userType = "Employee";
                        }
                        else
                        {
                            items.userType = "General";
                        }
                        if (a.status == true)
                        {
                            items.Status = "Active";
                            items.color = "#00a65a";
                        }
                        else
                        {
                            items.Status = "DeActive";
                            items.color = "#dd4b39";
                        }
                        
                      
                        items.Id = a.Id.ToString();
                        list.Add(items);
                    }
                    if (Count != 0)
                    {
                        obj.status = true;
                        obj.message = "Sucess";
                        obj.data = list;
                    }
                    else
                    {
                        obj.status = false;
                        obj.message = "No Data Found";
                        obj.data = list;
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


        [System.Web.Http.Route("api/EventsAPI/getNoticeBoardsForStudentBySchool")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getNoticeBoardsForStudentBySchool(List<string> aa)
        {

            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            //sqlHelper obj = new sqlHelper();
            int SchoolID = Convert.ToInt32(aa[0]);


            try
            {


                var result = db.tblNoticeBoards.Where(x => x.SchoolID == SchoolID && x.IsDeleted == null && (x.userType == "S" || x.userType == "G")).ToList();

                foreach (var a in result)
                {
                    NoticeboardDetails items = new NoticeboardDetails();
                    items.Title = a.Title;
                    items.Desc = a.description;
                    items.NoticeDate = Convert.ToDateTime(a.NoticeDate.ToString());
                    items.sNoticeDate = items.NoticeDate.ToString("MM/dd/yyyy");
                    items.datetime1 = a.NoticeDate.ToString();
                    items.NoticeFile = a.NoticeFile;
                    items.userType = a.userType;
                    if (items.userType == "S")
                    {
                        items.userType = "Student";
                    }
                    else if (items.userType == "E")
                    {
                        items.userType = "Employee";
                    }
                    else
                    {
                        items.userType = "General";
                    }
                    if (a.status == true)
                    {
                        items.Status = "Active";
                    }
                    else
                    {
                        items.Status = "DeActive";
                    }


                    items.Id = a.Id.ToString();
                    list.Add(items);
                }


            }
            catch (Exception e)
            { throw e; }
            return list;
        }



        [System.Web.Http.Route("api/EventsAPI/getNoticeBoardsForStudent")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getNoticeBoardsForStudent()
        {
            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            SCHOOLERPEntities db = new SCHOOLERPEntities();
            //sqlHelper obj = new sqlHelper();
            try
            {
                var result = db.tblNoticeBoards.Where(x => x.userType == "S" || x.userType == "G" && x.IsDeleted == null).ToList();

                foreach (var a in result)
                {
                    NoticeboardDetails items = new NoticeboardDetails();
                    items.Title = a.Title;
                    items.Desc = a.description;
                    items.NoticeDate = Convert.ToDateTime( Convert.ToDateTime(a.NoticeDate).ToString("MM/dd/yyyy"));
                    items.NoticeFile = a.NoticeFile;
                    items.userType = a.userType;
                    if (items.userType == "S")
                    {
                        items.userType = "Student";
                    }
                    else if (items.userType == "E")
                    {
                        items.userType = "Employee";
                    }
                    else
                    {
                        items.userType = "General";
                    }
                    if (a.status == true)
                    {
                        items.Status = "Active";
                    }
                    else
                    {
                        items.Status = "DeActive";
                    }


                    items.Id = a.Id.ToString();
                    list.Add(items);
                }
            }
            catch (Exception e)
            { throw e; }
            return list;
        }

        [System.Web.Http.Route("api/EventsAPI/getStudentNoticeBoardDetails")]
        [System.Web.Http.HttpPost]
        public List<NoticeboardDetails> getStudentNoticeBoardDetails(NoticeboardDetails notice)
        {
            List<NoticeboardDetails> list = new List<NoticeboardDetails>();
            sqlHelper obj = new sqlHelper();

            DateTime sDate = Convert.ToDateTime(notice.StartDate);
            DateTime eDate = Convert.ToDateTime(notice.EndDate);


            var result = db.tblNoticeBoards.Where(x => x.NoticeDate >= sDate && x.NoticeDate <= eDate && (x.userType == "S" || x.userType == "G") && x.IsDeleted==null && x.SchoolID==notice.SchoolID).ToList();

            foreach (var a in result)
            {
                NoticeboardDetails items = new NoticeboardDetails();
                items.Title = a.Title;
                items.Desc = a.description;
                items.NoticeDate = Convert.ToDateTime(Convert.ToDateTime(a.NoticeDate).ToString("dddd, MMMM, dd, yyyy", CultureInfo.InvariantCulture)); //Convert.ToDateTime(a.NoticeDate).ToString("dddd ,MMMM,dd,yyyy");
                items.NoticeFile = a.NoticeFile;
                items.userType = a.userType;
                items.Extra = a.userType;
                if (items.userType == "S")
                {
                    items.userType = "Student";
                }
                else if (items.userType == "E")
                {
                    items.userType = "Employee";
                }
                else
                {
                    items.userType = "General";
                }


                items.Id = a.Id.ToString();
                list.Add(items);

            }


            return list;
        }


    }
}
